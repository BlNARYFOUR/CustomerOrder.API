using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.QueryHandlers;

public class OrderSearchListQueryHandler(ICustomerRepository customerRepository, IOrderRepository repository) : IRequestHandler<OrderSearchListQuery, IEnumerable<Order>>
{
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    private readonly IOrderRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    /// exception <exception cref="NotFoundException" />
    public async Task<IEnumerable<Order>> Handle(OrderSearchListQuery query, CancellationToken cancellationToken)
    {
        foreach (var id in query.CustomerIds)
        {
            await _customerRepository.GetByIdAsync(id);
        }

        if (1 != query.CustomerIds.Count || null != query.From || null != query.To)
        {
            return await _repository.SearchOnCreationDateForCustomersAsync(
                null == query.From ? DateTime.MinValue : DateTime.Parse(query.From),
                null == query.To ? DateTime.UtcNow : DateTime.Parse(query.To),
                query.CustomerIds
            );
        }

        return await _repository.GetListForCustomerAsync(query.CustomerIds[0]);
    }
}
