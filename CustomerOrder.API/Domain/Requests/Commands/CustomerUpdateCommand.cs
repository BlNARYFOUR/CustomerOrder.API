using MediatR;

namespace CustomerOrder.API.Domain.Requests.Commands;

public record CustomerUpdateCommand(
    int Id,
    string FirstName,
    string LastName,
    string Email
) : IRequest;
