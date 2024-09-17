using CustomerOrder.API.Domain.Requests.Commands;
using FluentValidation;

namespace CustomerOrder.API.Application.Validation.Validators.Commands;

public class CustomerCreateCommandValidator : AbstractValidator<CustomerCreateCommand>
{
    public CustomerCreateCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
    }
}
