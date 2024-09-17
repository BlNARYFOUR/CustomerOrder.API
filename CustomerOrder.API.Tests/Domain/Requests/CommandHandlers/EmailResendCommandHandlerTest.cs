using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.CommandHandlers;
using CustomerOrder.API.Domain.Requests.Commands;
using CustomerOrder.API.Domain.Services;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.Requests.CommandHandlers;

public class EmailResendCommandHandlerTest
{
    private readonly Mock<IEmailRepository> _repositoryMock;
    private readonly Mock<IMailer> _mailerMock;

    private readonly EmailResendCommandHandler _commandHandler;

    public EmailResendCommandHandlerTest()
    {
        _repositoryMock = new Mock<IEmailRepository>();
        _mailerMock = new Mock<IMailer>();

        _commandHandler = new EmailResendCommandHandler(
            _repositoryMock.Object,
            _mailerMock.Object
        );
    }

    [Fact]
    public void ItIsACommandtHandlerTest()
    {
        Assert.IsAssignableFrom<IRequestHandler<EmailResendCommand>>(_commandHandler);
    }

    [Fact]
    public async Task ItCanHandleTheCommandTest()
    {
        var expectedCommand = new EmailResendCommand("test_token");
        var expectedEmail = new Email("test_from", "test_to", "test_subject", "test_message") { Id = 1234, Token = expectedCommand.Token };
        var expectedUpdatedToken = "test_token_updated";
        var expectedEmailUpdated = new Email("test_from", "test_to", "test_subject", "test_message") { Id = 1234, Token = expectedUpdatedToken };

        _repositoryMock.Setup(r => r.GetByTokenAsync(expectedCommand.Token))
            .Returns(Task.FromResult(expectedEmail));
        _mailerMock.Setup(m => m.Send(It.Is<Email>(
            e => expectedEmail.Id == e.Id
                && expectedEmail.Token == e.Token
                && expectedEmail.From == e.From
                && expectedEmail.To == e.To
                && expectedEmail.Subject == e.Subject
                && expectedEmail.Message == e.Message
        ))).Returns(expectedUpdatedToken);
        _repositoryMock.Setup(r => r.UpdateAsync(It.Is<Email>(
            e => expectedEmail.Id == e.Id
                && expectedUpdatedToken == e.Token
                && expectedEmail.From == e.From
                && expectedEmail.To == e.To
                && expectedEmail.Subject == e.Subject
                && expectedEmail.Message == e.Message
        ))).Returns(Task.FromResult(expectedEmailUpdated));

        await _commandHandler.Handle(expectedCommand, CancellationToken.None);

        _repositoryMock.Verify(r => r.GetByTokenAsync(It.IsAny<string>()), Times.Once);
        _mailerMock.Verify(m => m.Send(It.IsAny<Email>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Email>()), Times.Once);
    }

    [Fact]
    public async Task ItPropegatesNotFoundExceptionTest()
    {
        var expectedCommand = new EmailResendCommand("test_token"); 
        var expectedException = NotFoundException.ForClass("TestClass");

        _repositoryMock.Setup(r => r.GetByTokenAsync(expectedCommand.Token))
            .Throws(expectedException);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () => {
            await _commandHandler.Handle(expectedCommand, CancellationToken.None);
        });

        _repositoryMock.Verify(r => r.GetByTokenAsync(It.IsAny<string>()), Times.Once);
        _mailerMock.Verify(m => m.Send(It.IsAny<Email>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Email>()), Times.Never);

        Assert.Equal(expectedException.Message, exception.Message);
    }
}
