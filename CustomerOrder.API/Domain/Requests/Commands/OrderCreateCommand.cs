using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.Commands;

public record OrderCreateCommand(
    int CustomerId,
    string Description,
    double Price
) : IRequest<int>;
