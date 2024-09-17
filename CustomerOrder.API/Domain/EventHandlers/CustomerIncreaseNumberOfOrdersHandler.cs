using CustomerOrder.API.Domain.Events;
using CustomerOrder.API.Domain.Repositories;
using MediatR;

namespace CustomerOrder.API.Domain.EventHandlers;

public class CustomerIncreaseNumberOfOrdersHandler(ICustomerRepository repository) : INotificationHandler<OrderCreatedEvent>
{
    private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    /// <exception cref="Exceptions.NotFoundException" />
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _repository.IncreaseNumberOfOrdersAsync(notification.CustomerId);
    }
}
