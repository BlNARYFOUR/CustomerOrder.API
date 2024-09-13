using CustomerOrder.API.Domain.Requests.Commands;
using FluentValidation;

namespace CustomerOrder.API.Application.Validation.Validators.Commands;

public class CustomerUpdateCommandValidator : AbstractValidator<CustomerUpdateCommand>
{
    public CustomerUpdateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
    }
}
