using CustomerOrder.API.Domain.Requests.Commands;
using FluentValidation;

namespace CustomerOrder.API.Application.Validation.Validators.Commands;

public class OrderCancelCommandValidator : AbstractValidator<OrderCancelCommand>
{
    public OrderCancelCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
