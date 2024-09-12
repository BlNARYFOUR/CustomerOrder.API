using CustomerOrder.API.Domain.Requests.Commands;
using FluentValidation;

namespace CustomerOrder.API.Application.Validation.Validators;

public class EmailResendCommandValidator : AbstractValidator<EmailResendCommand>
{
    public EmailResendCommandValidator()
    {
        RuleFor(c => c.Token).Must(ValidateToken).WithMessage("'Token' is not a valid GUID.");
    }

    private bool ValidateToken(string token)
    {
        return Guid.TryParse(token, out _);
    }
}
