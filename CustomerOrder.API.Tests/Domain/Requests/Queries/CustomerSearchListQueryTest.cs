using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Tests.Domain.Requests.Queries;

public class CustomerSearchListQueryTest
{
    private readonly CustomerSearchListQuery _command;

    public CustomerSearchListQueryTest()
    {
        _command = new CustomerSearchListQuery("test_email_search");
    }

    [Fact]
    public void ItIsAQueryTest()
    {
        Assert.IsAssignableFrom<IRequest<IEnumerable<Customer>>>(_command);
    }

    [Fact]
    public void ItHasAnEmailSearchTest()
    {
        Assert.Equal("test_email_search", _command.EmailSearch);
    }

    [Fact]
    public void ItHasANullableEmailSearchTest()
    {
        var command = new CustomerSearchListQuery(null);
        Assert.Null(command.EmailSearch);
    }
}
