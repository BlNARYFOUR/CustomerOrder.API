using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.QueryHandlers;

public class OrderGetCancelledListForCustomerQueryHandler(
    ICustomerRepository customerRepository,
    IOrderRepository repository
) : IRequestHandler<OrderGetCancelledListForCustomerQuery, IEnumerable<Order>> {
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    private readonly IOrderRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    /// exception <exception cref="NotFoundException" />
    public async Task<IEnumerable<Order>> Handle(OrderGetCancelledListForCustomerQuery query, CancellationToken cancellationToken)
    {
        await _customerRepository.GetByIdAsync(query.CustomerId);

        return await _repository.GetAllCancelledForCustomerAsync(query.CustomerId);
    }
}
