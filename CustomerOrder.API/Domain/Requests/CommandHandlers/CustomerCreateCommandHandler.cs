using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Models;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.CommandHandlers;

public class CustomerCreateCommandHandler(ICustomerRepository repository) : IRequestHandler<CustomerCreateCommand, int>
{
    private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<int> Handle(CustomerCreateCommand command, CancellationToken cancellationToken)
    {
        var customer = await _repository.FindByEmailAsync(command.Email);

        if (null != customer)
        {
            throw new ValidationException(new ValidationError("Email", "'Email' is already used."));
        }

        return (await _repository.CreateAsync(new Customer(
            command.FirstName,
            command.LastName,
            command.Email
        ))).Id;
    }
}
