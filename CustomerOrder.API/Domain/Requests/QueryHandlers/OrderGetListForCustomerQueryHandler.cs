using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.QueryHandlers;

public class OrderGetListForCustomerQueryHandler(ICustomerRepository customerRepository, IOrderRepository repository) : IRequestHandler<OrderGetListForCustomerQuery, IEnumerable<Order>>
{
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    private readonly IOrderRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    /// exception <exception cref="NotFoundException" />
    public async Task<IEnumerable<Order>> Handle(OrderGetListForCustomerQuery query, CancellationToken cancellationToken)
    {
        await _customerRepository.GetByIdAsync(query.CustomerId);

        if (null != query.From || null != query.To)
        {
            return await _repository.SearchOnCreationDateForCustomersAsync(
                null == query.From ? DateTime.MinValue : DateTime.Parse(query.From),
                null == query.To ? DateTime.UtcNow : DateTime.Parse(query.To),
                [query.CustomerId]
            );
        }

        return await _repository.GetListForCustomerAsync(query.CustomerId);
    }
}
