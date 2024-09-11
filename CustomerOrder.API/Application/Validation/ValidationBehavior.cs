using CustomerOrder.API.Domain.Models;
using FluentValidation;
using MediatR;

namespace CustomerOrder.API.Application.Validation;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators ?? throw new ArgumentNullException(nameof(validators));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context)));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage
            )).ToArray();

        if (0 != errors.Length)
        {
            throw new Domain.Exceptions.ValidationException(errors);
        }

        var response = await next();

        return response;
    }
}
