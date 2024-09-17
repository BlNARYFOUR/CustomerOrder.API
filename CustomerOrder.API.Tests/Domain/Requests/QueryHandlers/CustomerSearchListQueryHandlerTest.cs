using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using CustomerOrder.API.Domain.Requests.QueryHandlers;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.Requests.QueryHandlers;

public class CustomerSearchListQueryHandlerTest
{
    private readonly Mock<ICustomerRepository> _repositoryMock;

    private readonly CustomerSearchListQueryHandler _queryHandler;

    public CustomerSearchListQueryHandlerTest()
    {
        _repositoryMock = new Mock<ICustomerRepository>();

        _queryHandler = new CustomerSearchListQueryHandler(
            _repositoryMock.Object
        );
    }

    [Fact]
    public void ItIsAQueryHandlerTest()
    {
        Assert.IsAssignableFrom<IRequestHandler<CustomerSearchListQuery, IEnumerable<Customer>>>(_queryHandler);
    }

    [Fact]
    public async Task ItCanHandleTheQueryTest()
    {
        var expectedQuery = new CustomerSearchListQuery("test_email_search");
        IEnumerable<Customer> expectedCustomers = [new Customer("test_first_name", "test_last_name", "test_email_search") { Id = 1234 }];

        _repositoryMock.Setup(r => r.SearchOnEmailAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(expectedCustomers));

        IEnumerable<Customer> result = await _queryHandler.Handle(expectedQuery, CancellationToken.None);

        _repositoryMock.Verify(r => r.SearchOnEmailAsync(It.IsAny<string>()), Times.Once);
        _repositoryMock.Verify(r => r.SearchOnEmailAsync(It.Is<string>(
            s => expectedQuery.EmailSearch == s
        )), Times.Once);

        Assert.Equal(expectedCustomers, result);
    }

    [Fact]
    public async Task ItCanHandleAnEmptySearchStringTest()
    {
        var expectedQuery = new CustomerSearchListQuery("");
        IEnumerable<Customer> expectedCustomers = [new Customer("test_first_name", "test_last_name", "test_email_search") { Id = 1234 }];

        _repositoryMock.Setup(r => r.GetAllAsync())
            .Returns(Task.FromResult(expectedCustomers));

        IEnumerable<Customer> result = await _queryHandler.Handle(expectedQuery, CancellationToken.None);

        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        _repositoryMock.Verify(r => r.SearchOnEmailAsync(It.IsAny<string>()), Times.Never);

        Assert.Equal(expectedCustomers, result);
    }

    [Fact]
    public async Task ItCanHandleANullSearchStringTest()
    {
        var expectedQuery = new CustomerSearchListQuery(null);
        IEnumerable<Customer> expectedCustomers = [new Customer("test_first_name", "test_last_name", "test_email_search") { Id = 1234 }];

        _repositoryMock.Setup(r => r.GetAllAsync())
            .Returns(Task.FromResult(expectedCustomers));

        IEnumerable<Customer> result = await _queryHandler.Handle(expectedQuery, CancellationToken.None);

        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        _repositoryMock.Verify(r => r.SearchOnEmailAsync(It.IsAny<string>()), Times.Never);

        Assert.Equal(expectedCustomers, result);
    }
}
