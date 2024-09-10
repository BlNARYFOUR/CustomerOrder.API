using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.QueryHandlers;

public class CustomerGetByIdQueryHandler(ICustomerRepository repository) : IRequestHandler<CustomerGetByIdQuery, Customer>
{
    private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Customer> Handle(CustomerGetByIdQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(query.Id);
    }
}
