using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using MediatR;

namespace CustomerOrder.API.Domain.Requests;

public class GetOrderListQueryHandler(IOrderRepository repository) : IRequestHandler<GetOrderListQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<IEnumerable<Order>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
