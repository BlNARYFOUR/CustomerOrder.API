using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using MediatR;

namespace CustomerOrder.API.Domain.Requests;

public class GetCustomerByIdQueryHandler(ICustomerRepository repository) : IRequestHandler<GetCustomerByIdQuery, Customer>
{
    private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
