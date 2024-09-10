using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.CommandHandlers;

public class CustomerCreateCommandHandler(ICustomerRepository repository) : IRequestHandler<CustomerCreateCommand, int>
{
    private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<int> Handle(CustomerCreateCommand command, CancellationToken cancellationToken)
    {
        return (await _repository.CreateAsync(command.Entity)).Id;
    }
}
