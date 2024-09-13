using CustomerOrder.API.Domain.Requests.Queries;
using FluentValidation;

namespace CustomerOrder.API.Application.Validation.Validators.Queries;

public class CustomerGetListQueryValidator : AbstractValidator<CustomerGetListQuery>
{
    public CustomerGetListQueryValidator()
    {
        RuleFor(q => q.EmailSearch).Must(ValidateSearchString).WithMessage("'Email' cannot be an empty string.");
    }

    private bool ValidateSearchString(string? searchString)
    {
        return "" != searchString;
    }
}
