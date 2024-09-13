using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Models;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Commands;
using CustomerOrder.API.Infrastructure.Repositories;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.CommandHandlers;

public class CustomerUpdateCommandHandler(ICustomerRepository repository) : IRequestHandler<CustomerUpdateCommand>
{
    private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    /// exception <exception cref="NotFoundException" />
    public async Task Handle(CustomerUpdateCommand command, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(command.Id);

        if (null != await _repository.FindByEmailAsync(command.Email))
        {
            throw new ValidationException(new ValidationError("Email", "'Email' is already used."));
        }

        customer.FirstName = command.FirstName;
        customer.LastName = command.LastName;
        customer.Email = command.Email;

        await _repository.UpdateAsync(customer);
    }
}
