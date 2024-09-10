using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.Commands;

public record CustomerCreateCommand(Customer Entity) : IRequest<int>;
