using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.Commands;

public record OrderCreateCommand(Order Entity) : IRequest<int>;
