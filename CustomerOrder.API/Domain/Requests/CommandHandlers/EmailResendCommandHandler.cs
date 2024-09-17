using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Commands;
using CustomerOrder.API.Domain.Services;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.CommandHandlers;

public class EmailResendCommandHandler(
    IEmailRepository repository,
    IMailer mailer
) : IRequestHandler<EmailResendCommand> {
    private readonly IEmailRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IMailer _mailer = mailer ?? throw new ArgumentNullException(nameof(mailer));

    /// exception <exception cref="NotFoundException" />
    public async Task Handle(EmailResendCommand command, CancellationToken cancellationToken)
    {
        var email = await _repository.GetByTokenAsync(command.Token);
        email.Token = _mailer.Send(email);
        await _repository.UpdateAsync(email);
    }
}
