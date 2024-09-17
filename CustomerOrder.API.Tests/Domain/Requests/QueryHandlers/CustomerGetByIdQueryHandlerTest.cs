using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using CustomerOrder.API.Domain.Requests.QueryHandlers;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.Requests.QueryHandlers;

public class CustomerGetByIdQueryHandlerTest
{
    private readonly Mock<ICustomerRepository> _repositoryMock;

    private readonly CustomerGetByIdQueryHandler _queryHandler;

    public CustomerGetByIdQueryHandlerTest()
    {
        _repositoryMock = new Mock<ICustomerRepository>();

        _queryHandler = new CustomerGetByIdQueryHandler(
            _repositoryMock.Object
        );
    }

    [Fact]
    public void ItIsAQueryHandlerTest()
    {
        Assert.IsAssignableFrom<IRequestHandler<CustomerGetByIdQuery, Customer>>(_queryHandler);
    }

    [Fact]
    public async Task ItCanHandleTheQueryTest()
    {
        var expectedQuery = new CustomerGetByIdQuery(1234);
        var expectedCustomer = new Customer("test_first_name", "test_last_name", "test_email") { Id = expectedQuery.Id };

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns(Task.FromResult(expectedCustomer));

        Customer result = await _queryHandler.Handle(expectedQuery, CancellationToken.None);

        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByIdAsync(expectedQuery.Id), Times.Once);

        Assert.Equal(expectedCustomer, result);
    }

    [Fact]
    public async Task ItPropegatesNotFoundExceptionTest()
    {
        var expectedQuery = new CustomerGetByIdQuery(1234);
        var expectedException = NotFoundException.ForClass("TestClass");

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Throws(expectedException);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () => {
            await _queryHandler.Handle(expectedQuery, CancellationToken.None);
        });

        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByIdAsync(expectedQuery.Id), Times.Once);

        Assert.Equal(expectedException.Message, exception.Message);
    }
}
