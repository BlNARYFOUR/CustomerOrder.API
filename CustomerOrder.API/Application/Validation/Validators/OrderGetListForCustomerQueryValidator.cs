using CustomerOrder.API.Domain.Requests.Queries;
using FluentValidation;

namespace CustomerOrder.API.Application.Validation.Validators;

public class OrderGetListForCustomerQueryValidator : AbstractValidator<OrderGetListForCustomerQuery>
{
    public OrderGetListForCustomerQueryValidator()
    {
        RuleFor(q => q.From).Must(ValidateDateString).WithMessage("'From' is not a valid UTC date-time.");
        RuleFor(q => q.To).Must(ValidateDateString).WithMessage("'To' is not a valid UTC date-time.");
    }

    private bool ValidateDateString(string? dateString)
    {
        return null == dateString || (
            dateString.EndsWith('Z')
            && DateTime.TryParse(dateString, out var _)
        );
    }
}
