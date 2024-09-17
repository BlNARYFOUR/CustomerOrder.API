using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.EventHandlers;
using CustomerOrder.API.Domain.Events;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Services;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.EventHandlers;

public class SendOrderCreatedEmailHandlerTest
{
    private readonly Mock<IMailer> _mailerMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IEmailRepository> _emailRepositoryMock;

    private readonly SendOrderCreatedEmailHandler _eventHandler;

    public SendOrderCreatedEmailHandlerTest()
    {
        _mailerMock = new Mock<IMailer>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _emailRepositoryMock = new Mock<IEmailRepository>();

        _eventHandler = new SendOrderCreatedEmailHandler(
            _mailerMock.Object,
            _customerRepositoryMock.Object,
            _emailRepositoryMock.Object
        );
    }

    [Fact]
    public void ItIsAnEventHandlerTest()
    {
        Assert.IsAssignableFrom<INotificationHandler<OrderCreatedEvent>>(_eventHandler);
    }

    [Fact]
    public async Task ItCanHandleTheEventTest()
    {
        var expectedEvent = new OrderCreatedEvent(1234, 4321);
        var expectedCustomer = new Customer("Bob", "Dylan", "bob.dylan@test.test") { Id = expectedEvent.CustomerId };
        var expectedEmail = new Email("noreply@test.test", expectedCustomer.Email, "Order Confirmation", $"Hi {expectedCustomer.FirstName},\n\nYour order #{expectedEvent.Id} is on its way!\n\nKind regards,\n\nThe CustomerOrder Team");
        var expectedToken = "test-token";

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns(Task.FromResult(expectedCustomer));
        _mailerMock.Setup(m => m.Send(It.IsAny<Email>()))
            .Returns(expectedToken);
        _emailRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Email>()))
            .Returns(Task.FromResult(expectedEmail));

        await _eventHandler.Handle(expectedEvent, CancellationToken.None);

        _customerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _customerRepositoryMock.Verify(r => r.GetByIdAsync(expectedEvent.CustomerId), Times.Once);
        _mailerMock.Verify(m => m.Send(It.IsAny<Email>()), Times.Once);
        _mailerMock.Verify(m => m.Send(It.Is<Email>(
            e => expectedEmail.Id == e.Id
                && expectedEmail.From == e.From
                && expectedEmail.To == e.To
                && expectedEmail.Subject == e.Subject
                && expectedEmail.Message == e.Message
        )), Times.Once);
        _emailRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Email>()), Times.Once);
        _emailRepositoryMock.Verify(r => r.CreateAsync(It.Is<Email>(
            e => expectedEmail.Id == e.Id
                && expectedToken == e.Token
                && expectedEmail.From == e.From
                && expectedEmail.To == e.To
                && expectedEmail.Subject == e.Subject
                && expectedEmail.Message == e.Message
        )), Times.Once);
    }

    [Fact]
    public async Task ItPropegatesNotFoundExceptionTest()
    {
        var expectedEvent = new OrderCreatedEvent(1234, 4321);
        var expectedException = NotFoundException.ForClass("TestClass");

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Throws(expectedException);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () => {
            await _eventHandler.Handle(expectedEvent, CancellationToken.None);
        });

        _customerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _customerRepositoryMock.Verify(r => r.GetByIdAsync(expectedEvent.CustomerId), Times.Once);
        _mailerMock.Verify(m => m.Send(It.IsAny<Email>()), Times.Never);
        _emailRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Email>()), Times.Never);

        Assert.Equal(expectedException.Message, exception.Message);
    }
}
