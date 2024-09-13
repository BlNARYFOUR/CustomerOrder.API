using CustomerOrder.API.Domain.Requests.Queries;
using FluentValidation;

namespace CustomerOrder.API.Application.Validation.Validators.Queries;

public class OrderSearchListQueryValidator : AbstractValidator<OrderSearchListQuery>
{
    public OrderSearchListQueryValidator()
    {
        RuleFor(q => q.CustomerIds).ForEach(c => c.NotEmpty().WithMessage("Values in 'CustomerIds' must not be empty."));
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
