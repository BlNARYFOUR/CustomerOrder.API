using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Events;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.CommandHandlers;

public class OrderCreateCommandHandler(ICustomerRepository customerRepository, IOrderRepository repository, IPublisher eventBus) : IRequestHandler<OrderCreateCommand, int>
{
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    private readonly IOrderRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IPublisher _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

    /// exception <exception cref="NotFoundException" />
    public async Task<int> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
    {
        await _customerRepository.GetByIdAsync(command.CustomerId);

        var id = (await _repository.CreateAsync(new Order(
            command.CustomerId,
            command.Description,
            command.Price
        ))).Id;

        await _eventBus.Publish(new OrderCreatedEvent(id, command.CustomerId), cancellationToken);

        return id;
    }
}
