using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.QueryHandlers;

public class CustomerGetListQueryHandler(ICustomerRepository repository) : IRequestHandler<CustomerGetListQuery, IEnumerable<Customer>>
{
    private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<IEnumerable<Customer>> Handle(CustomerGetListQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
