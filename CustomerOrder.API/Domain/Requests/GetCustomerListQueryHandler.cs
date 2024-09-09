using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using MediatR;

namespace CustomerOrder.API.Domain.Requests;

public class GetCustomerListQueryHandler(ICustomerRepository repository) : IRequestHandler<GetCustomerListQuery, IEnumerable<Customer>>
{
    private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<IEnumerable<Customer>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
