using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.QueryHandlers;

public class CustomerSearchListQueryHandler(ICustomerRepository repository) : IRequestHandler<CustomerSearchListQuery, IEnumerable<Customer>>
{
    private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<IEnumerable<Customer>> Handle(CustomerSearchListQuery query, CancellationToken cancellationToken)
    {
        if (null != query.EmailSearch && "" != query.EmailSearch)
        {
            return await _repository.SearchOnEmailAsync(query.EmailSearch);
        }


        return await _repository.GetAllAsync();
    }
}
