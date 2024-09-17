using CustomerOrder.API.Domain.Requests.Queries;
using FluentValidation;

namespace CustomerOrder.API.Application.Validation.Validators.Queries;

public class CustomerGetByIdQueryValidator : AbstractValidator<CustomerGetByIdQuery>
{
    public CustomerGetByIdQueryValidator()
    {
        RuleFor(q => q.Id).NotEmpty();
    }
}
