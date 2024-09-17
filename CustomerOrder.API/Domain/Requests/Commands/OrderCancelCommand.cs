using MediatR;

namespace CustomerOrder.API.Domain.Requests.Commands;

public record OrderCancelCommand(int Id) : IRequest;
