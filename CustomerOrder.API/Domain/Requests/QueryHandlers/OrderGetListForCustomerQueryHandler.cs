using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.QueryHandlers;

public class OrderGetListForCustomerQueryHandler(IOrderRepository repository) : IRequestHandler<OrderGetListForCustomerQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<IEnumerable<Order>> Handle(OrderGetListForCustomerQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetAllForCustomerAsync(query.CustomerId);
    }
}
