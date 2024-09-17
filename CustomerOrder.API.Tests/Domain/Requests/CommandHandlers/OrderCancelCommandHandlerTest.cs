using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.CommandHandlers;
using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.Requests.CommandHandlers;

public class OrderCancelCommandHandlerTest
{
    private readonly Mock<IOrderRepository> _repositoryMock;

    private readonly OrderCancelCommandHandler _commandHandler;

    public OrderCancelCommandHandlerTest()
    {
        _repositoryMock = new Mock<IOrderRepository>();

        _commandHandler = new OrderCancelCommandHandler(
            _repositoryMock.Object
        );
    }

    [Fact]
    public void ItIsACommandHandlerTest()
    {
        Assert.IsAssignableFrom<IRequestHandler<OrderCancelCommand>>(_commandHandler);
    }

    [Fact]
    public async Task ItCanHandleTheCommandTest()
    {
        var expectedCommand = new OrderCancelCommand(1234);
        var order = new Order(4321, "test_description", 1.23, new DateTime(1999, 1, 1)) { Id = expectedCommand.Id };
        
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns(Task.FromResult(order));
        _repositoryMock.Setup(r => r.CancelAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        await _commandHandler.Handle(expectedCommand, CancellationToken.None);

        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByIdAsync(expectedCommand.Id), Times.Once);
        _repositoryMock.Verify(r => r.CancelAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.CancelAsync(expectedCommand.Id), Times.Once);
    }

    [Fact]
    public async Task ItPropegatesNotFoundExceptionTest()
    {
        var expectedCommand = new OrderCancelCommand(1234);
        var expectedException = NotFoundException.ForClass("TestClass");

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Throws(expectedException);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () => {
            await _commandHandler.Handle(expectedCommand, CancellationToken.None);
        });

        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByIdAsync(expectedCommand.Id), Times.Once);
        _repositoryMock.Verify(r => r.CancelAsync(It.IsAny<int>()), Times.Never);

        Assert.Equal(expectedException.Message, exception.Message);
    }
}
