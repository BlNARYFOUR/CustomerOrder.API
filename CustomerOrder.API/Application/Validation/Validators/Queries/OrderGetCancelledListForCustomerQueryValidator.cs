using CustomerOrder.API.Domain.Requests.Queries;
using FluentValidation;

namespace CustomerOrder.API.Application.Validation.Validators.Queries;

public class OrderGetCancelledListForCustomerQueryValidator : AbstractValidator<OrderGetCancelledListForCustomerQuery>
{
    public OrderGetCancelledListForCustomerQueryValidator()
    {
        RuleFor(q => q.CustomerId).NotEmpty();
    }
}
