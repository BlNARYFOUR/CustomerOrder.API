using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.CommandHandlers;
using CustomerOrder.API.Domain.Requests.Commands;
using CustomerOrder.API.Domain.Services;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.Requests.CommandHandlers;

public class OrderCreateCommandHandlerTest
{
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IPublisher> _eventBusMock;

    private readonly OrderCreateCommandHandler _commandHandler;

    public OrderCreateCommandHandlerTest()
    {
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _eventBusMock = new Mock<IPublisher>();

        _commandHandler = new OrderCreateCommandHandler(
            _dateTimeProviderMock.Object,
            _customerRepositoryMock.Object,
            _orderRepositoryMock.Object,
            _eventBusMock.Object
        );
    }

    [Fact]
    public void ItIsACommandHandlerTest()
    {
        Assert.IsAssignableFrom<IRequestHandler<OrderCreateCommand, int>>(_commandHandler);
    }

    [Fact]
    public async Task ItCanHandleTheCommandTest()
    {
        var expectedDate = new DateTime(1999, 1, 1);
        var expectedCommand = new OrderCreateCommand(4321, "test_description", 1.23);
        var expectedOrder = new Order(expectedCommand.CustomerId, expectedCommand.Description, expectedCommand.Price, expectedDate);
        var expectedCustomer = new Customer("test_first_name", "test_last_name", "test_email") { Id = expectedCommand.CustomerId };
        var orderWithId = new Order(expectedCommand.CustomerId, expectedCommand.Description, expectedCommand.Price, expectedDate) { Id = 1234 };

        _dateTimeProviderMock.Setup(s => s.Now()).Returns(expectedDate);
        _customerRepositoryMock.Setup(r => r.GetByIdAsync(expectedCommand.CustomerId))
            .Returns(Task.FromResult(expectedCustomer));
        _orderRepositoryMock.Setup(r => r.CreateAsync(It.Is<Order>(
            o => expectedOrder.Id == o.Id
                && expectedOrder.Description == o.Description
                && expectedOrder.Price == o.Price
                && expectedOrder.CreationDate == o.CreationDate
                && expectedOrder.CustomerId == o.CustomerId
                && expectedOrder.Customer == o.Customer
                && expectedOrder.Status == o.Status
        ))).Returns(Task.FromResult(orderWithId));

        await _commandHandler.Handle(expectedCommand, CancellationToken.None);

        _customerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _orderRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Order>()), Times.Once);
    }
}
