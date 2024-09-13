using CustomerOrder.API.Domain.Requests.Commands;
using FluentValidation;

namespace CustomerOrder.API.Application.Validation.Validators;

public class OrderCreateCommandValidator : AbstractValidator<OrderCreateCommand>
{
    public OrderCreateCommandValidator()
    {
        RuleFor(c => c.CustomerId).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Price).NotEmpty();
    }
}
