using MediatR;

namespace CustomerOrder.API.Domain.Requests.Commands;

public record EmailResendCommand(string Token) : IRequest;
