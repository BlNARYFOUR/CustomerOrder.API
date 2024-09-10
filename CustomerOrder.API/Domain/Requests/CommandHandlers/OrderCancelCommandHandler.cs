using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.CommandHandlers;

public class OrderCancelCommandHandler(IOrderRepository repository) : IRequestHandler<OrderCancelCommand>
{
    private readonly IOrderRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    /// exception <exception cref="NotFoundException" />
    public async Task Handle(OrderCancelCommand command, CancellationToken cancellationToken)
    {
       await _repository.CancelAsync(command.Id);
    }
}
