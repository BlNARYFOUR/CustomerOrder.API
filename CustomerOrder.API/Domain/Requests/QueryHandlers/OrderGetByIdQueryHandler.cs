using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.QueryHandlers;

public class OrderGetByIdQueryHandler(IOrderRepository repository, IPublisher eventBus) : IRequestHandler<OrderGetByIdQuery, Order>
{
    private readonly IOrderRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Order> Handle(OrderGetByIdQuery query, CancellationToken cancellationToken)
    {
        // todo: cleanup test code
        // await eventBus.Publish(new OrderCreatedEvent(1, 2), cancellationToken);

        return await _repository.GetByIdAsync(query.Id);
    }
}
