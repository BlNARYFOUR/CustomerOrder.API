using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using CustomerOrder.API.Domain.Requests.QueryHandlers;
using CustomerOrder.API.Domain.Services;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.Requests.QueryHandlers;

public class OrderSearchListQueryHandlerTest
{
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;

    private readonly OrderSearchListQueryHandler _queryHandler;

    public OrderSearchListQueryHandlerTest()
    {
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _orderRepositoryMock = new Mock<IOrderRepository>();

        _queryHandler = new OrderSearchListQueryHandler(
            _dateTimeProviderMock.Object,
            _customerRepositoryMock.Object,
            _orderRepositoryMock.Object
        );
    }

    [Fact]
    public void ItIsAQueryHandlerTest()
    {
        Assert.IsAssignableFrom<IRequestHandler<OrderSearchListQuery, IEnumerable<Order>>>(_queryHandler);
    }

    [Theory]
    [MemberData(nameof(SearchData))]
    public async Task ItCanHandleTheQueryTest(List<int> customerIds, string? from, string? to)
    {
        var expectedQuery = new OrderSearchListQuery(customerIds, from, to);
        IEnumerable<Order> expectedOrders = [new Order(1234, "test_description", 1.23, new DateTime(1999, 1, 1)) { Id = 4321 }];
        var customer = new Customer("test_first_name", "test_last_name", "test_email_search") { Id = 1234 };
        var minDateTime = DateTime.MinValue;
        var nowDateTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime expectedFrom = minDateTime;
        DateTime expectedTo = nowDateTime;
        
        if(!DateTime.TryParse(from, out expectedFrom))
        {
            expectedFrom = minDateTime;
        }
        
        if(!DateTime.TryParse(to, out expectedTo))
        {
            expectedTo = nowDateTime;
        }

        _dateTimeProviderMock.Setup(s => s.MinValue())
            .Returns(minDateTime);
        _dateTimeProviderMock.Setup(s => s.Now())
            .Returns(nowDateTime);
        _customerRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns(Task.FromResult(customer));
        _orderRepositoryMock.Setup(r => r.SearchOnCreationDateForCustomersAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<List<int>>()))
            .Returns(Task.FromResult(expectedOrders));

        IEnumerable<Order> result = await _queryHandler.Handle(expectedQuery, CancellationToken.None);

        _customerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Exactly(customerIds.Count));
        _customerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsIn(customerIds.ToArray())), Times.Exactly(customerIds.Count));
        _orderRepositoryMock.Verify(r => r.SearchOnCreationDateForCustomersAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<List<int>>()), Times.Once);
        _orderRepositoryMock.Verify(r => r.SearchOnCreationDateForCustomersAsync(expectedFrom, expectedTo, customerIds), Times.Once);

        Assert.Equal(expectedOrders, result);
    }

    [Fact]
    public async Task ItCanHandleOneCustomerIdNullFromAndNullToTest()
    {
        var expectedCustomerId = 1234;
        List<int> customerIds = [expectedCustomerId];
        var expectedQuery = new OrderSearchListQuery(customerIds, null, null);
        IEnumerable<Order> expectedOrders = [new Order(1234, "test_description", 1.23, new DateTime(1999, 1, 1)) { Id = 4321 }];
        var customer = new Customer("test_first_name", "test_last_name", "test_email_search") { Id = 1234 };

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns(Task.FromResult(customer));
        _orderRepositoryMock.Setup(r => r.GetListForCustomerAsync(It.IsAny<int>()))
            .Returns(Task.FromResult(expectedOrders));

        IEnumerable<Order> result = await _queryHandler.Handle(expectedQuery, CancellationToken.None);

        _customerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _customerRepositoryMock.Verify(r => r.GetByIdAsync(expectedCustomerId), Times.Once);
        _orderRepositoryMock.Verify(r => r.GetListForCustomerAsync(It.IsAny<int>()), Times.Once);
        _orderRepositoryMock.Verify(r => r.GetListForCustomerAsync(expectedCustomerId), Times.Once);

        Assert.Equal(expectedOrders, result);
    }

    [Theory]
    [MemberData(nameof(ExceptionData))]
    public async Task ItPropegatesNotFoundExceptionTest(List<int> customerIds, string? from, string? to)
    {
        var expectedQuery = new OrderSearchListQuery(customerIds, from, to);
        var expectedException = NotFoundException.ForClass("TestClass");
        IEnumerable<Order> expectedOrders = [new Order(1234, "test_description", 1.23, new DateTime(1999, 1, 1)) { Id = 4321 }];
        var customer = new Customer("test_first_name", "test_last_name", "test_email_search") { Id = 1234 };

        _customerRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns(Task.FromResult(customer));
        _customerRepositoryMock.Setup(r => r.GetByIdAsync(1234))
            .Throws(expectedException);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () => {
            await _queryHandler.Handle(expectedQuery, CancellationToken.None);
        });

        _customerRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Exactly(customerIds.FindIndex(c => 1234 == c) + 1));
        _customerRepositoryMock.Verify(r => r.GetByIdAsync(1234), Times.Once);
        _orderRepositoryMock.Verify(r => r.SearchOnCreationDateForCustomersAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<List<int>>()), Times.Never);
        _orderRepositoryMock.Verify(r => r.GetListForCustomerAsync(It.IsAny<int>()), Times.Never);

        Assert.Equal(expectedException.Message, exception.Message);
    }

    public static IEnumerable<object[]> SearchData()
    {
        yield return new object[] { (List<int>)[1], "1999-01-01T00:00:00Z", "2022-01-01T00:00:00Z" };
        yield return new object[] { (List<int>)[1, 2], "1999-01-02T00:00:00Z", "2022-02-01T00:00:00Z" };
        yield return new object[] { (List<int>)[1, 2, 3], "1999-02-01T00:00:00Z", "2022-05-09T00:00:00Z" };
        yield return new object[] { (List<int>)[], "1999-02-01T00:00:00Z", "2022-05-09T00:00:00Z" };
        yield return new object[] { (List<int>)[1], null!, "2022-07-12T00:00:00Z" };
        yield return new object[] { (List<int>)[1, 2], null!, "2022-09-21T00:00:00Z" };
        yield return new object[] { (List<int>)[1, 2, 3], null!, "2022-10-29T00:00:00Z" };
        yield return new object[] { (List<int>)[], null!, "2022-10-29T00:00:00Z" };
        yield return new object[] { (List<int>)[1], "1999-04-16T00:00:00Z", null! };
        yield return new object[] { (List<int>)[1, 2], "1999-10-10T00:00:00Z", null! };
        yield return new object[] { (List<int>)[1, 2, 3], "1999-12-21T00:00:00Z", null! };
        yield return new object[] { (List<int>)[], "1999-12-21T00:00:00Z", null! };
    }

    public static IEnumerable<object[]> ExceptionData()
    {
        yield return new object[] { (List<int>)[1234], "1999-01-01T00:00:00Z", "2022-01-01T00:00:00Z" };
        yield return new object[] { (List<int>)[1, 1234], "1999-01-02T00:00:00Z", "2022-02-01T00:00:00Z" };
        yield return new object[] { (List<int>)[1, 1234, 3], "1999-02-01T00:00:00Z", "2022-05-09T00:00:00Z" };
        yield return new object[] { (List<int>)[1234, 2], null!, "2022-09-21T00:00:00Z" };
        yield return new object[] { (List<int>)[1, 2, 1234], null!, "2022-10-29T00:00:00Z" };
        yield return new object[] { (List<int>)[1234], "1999-04-16T00:00:00Z", null! };
        yield return new object[] { (List<int>)[1, 1234], "1999-10-10T00:00:00Z", null! };
        yield return new object[] { (List<int>)[1234, 2, 3], "1999-12-21T00:00:00Z", null! };
    }
}
