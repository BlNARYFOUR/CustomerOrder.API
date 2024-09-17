using MediatR;

namespace CustomerOrder.API.Domain.Requests.Commands;

public record CustomerCreateCommand(
    string FirstName,
    string LastName,
    string Email
) : IRequest<int>;
