using CustomerOrder.API.Domain.EventHandlers;
using CustomerOrder.API.Domain.Events;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.EventHandlers;

public class CustomerIncreaseNumberOfOrdersHandlerTest
{
    private readonly Mock<ICustomerRepository> _repositoryMock;

    private readonly CustomerIncreaseNumberOfOrdersHandler _eventHandler;

    public CustomerIncreaseNumberOfOrdersHandlerTest()
    {
        _repositoryMock = new Mock<ICustomerRepository>();

        _eventHandler = new CustomerIncreaseNumberOfOrdersHandler(
            _repositoryMock.Object
        );
    }

    [Fact]
    public void ItIsAnEventHandlerTest()
    {
        Assert.IsAssignableFrom<INotificationHandler<OrderCreatedEvent>>(_eventHandler);
    }

    [Fact]
    public async Task ItCanHandleAnOrderCreatedEventTest()
    {
        var expectedEvent = new OrderCreatedEvent(1234, 4321);

        _repositoryMock.Setup(r => r.IncreaseNumberOfOrdersAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        await _eventHandler.Handle(expectedEvent, CancellationToken.None);

        _repositoryMock.Verify(r => r.IncreaseNumberOfOrdersAsync(expectedEvent.CustomerId), Times.Once);
    }

    [Fact]
    public async Task ItPropegatesNotFoundExceptionTest()
    {
        var expectedEvent = new OrderCreatedEvent(1234, 4321);

        _repositoryMock.Setup(r => r.IncreaseNumberOfOrdersAsync(expectedEvent.CustomerId))
            .Throws(new NotFoundException("Test message"));

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () => {
            await _eventHandler.Handle(expectedEvent, CancellationToken.None);
        });

        _repositoryMock.Verify(r => r.IncreaseNumberOfOrdersAsync(It.IsAny<int>()), Times.Once);

        Assert.Same("Test message", exception.Message);
    }
}
