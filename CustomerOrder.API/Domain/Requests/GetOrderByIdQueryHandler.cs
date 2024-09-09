using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using MediatR;

namespace CustomerOrder.API.Domain.Requests;

public class GetOrderByIdQueryHandler(IOrderRepository repository, IPublisher eventBus) : IRequestHandler<GetOrderByIdQuery, Order>
{
    private readonly IOrderRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        // todo: cleanup test code
        // await eventBus.Publish(new OrderCreatedEvent(1, 2), cancellationToken);

        return await _repository.GetByIdAsync(request.Id);
    }
}
