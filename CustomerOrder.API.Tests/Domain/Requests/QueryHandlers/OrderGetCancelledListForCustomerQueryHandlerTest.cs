using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using CustomerOrder.API.Domain.Requests.QueryHandlers;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.Requests.QueryHandlers;

public class OrderGetCancelledListForCustomerQueryHandlerTest
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;

    private readonly OrderGetCancelledListForCustomerQueryHandler _queryHandler;

    public OrderGetCancelledListForCustomerQueryHandlerTest()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _orderRepositoryMock = new Mock<IOrderRepository>();

        _queryHandler = new OrderGetCancelledListForCustomerQueryHandler(
            _customerRepositoryMock.Object,
            _orderRepositoryMock.Object
        );
    }

    [Fact]
    public void ItIsAQueryHandlerTest()
    {
        Assert.IsAssignableFrom<IRequestHandler<OrderGetCancelledListForCustomerQuery, IEnumerable<Order>>>(_queryHandler);
    }

    [Fact]
    public async Task ItCanHandleTheQueryTest()
    {
        var expectedQuery = new OrderGetCancelledListForCustomerQuery(1234);
        IEnumerable<Order> expectedOrders = [new Order(1234, "test_description", 1.23, new DateTime(1999, 1, 1)) { Id = 4321 }];
        var customer = new Customer("test_first_name", "test_last_name", "test_email_search") { Id = expectedQuery.CustomerId };

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns(Task.FromResult(customer));
        _orderRepositoryMock.Setup(r => r.GetAllCancelledForCustomerAsync(It.IsAny<int>()))
            .Returns(Task.FromResult(expectedOrders));

        IEnumerable<Order> result = await _queryHandler.Handle(expectedQuery, CancellationToken.None);

        _customerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _customerRepositoryMock.Verify(r => r.GetByIdAsync(expectedQuery.CustomerId), Times.Once);
        _orderRepositoryMock.Verify(r => r.GetAllCancelledForCustomerAsync(It.IsAny<int>()), Times.Once);
        _orderRepositoryMock.Verify(r => r.GetAllCancelledForCustomerAsync(expectedQuery.CustomerId), Times.Once);

        Assert.Equal(expectedOrders, result);
    }

    [Fact]
    public async Task ItPropegatesNotFoundExceptionTest()
    {
        var expectedQuery = new OrderGetCancelledListForCustomerQuery(1234);
        var expectedException = NotFoundException.ForClass("TestClass");

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Throws(expectedException);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () => {
            await _queryHandler.Handle(expectedQuery, CancellationToken.None);
        });

        _customerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _customerRepositoryMock.Verify(r => r.GetByIdAsync(expectedQuery.CustomerId), Times.Once);
        _orderRepositoryMock.Verify(r => r.GetAllCancelledForCustomerAsync(It.IsAny<int>()), Times.Never);

        Assert.Equal(expectedException.Message, exception.Message);
    }
}
