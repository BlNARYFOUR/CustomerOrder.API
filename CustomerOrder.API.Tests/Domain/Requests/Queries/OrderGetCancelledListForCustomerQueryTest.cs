using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Tests.Domain.Requests.Queries;

public class OrderGetCancelledListForCustomerQueryTest
{
    private readonly OrderGetCancelledListForCustomerQuery _command;

    public OrderGetCancelledListForCustomerQueryTest()
    {
        _command = new OrderGetCancelledListForCustomerQuery(1234);
    }

    [Fact]
    public void ItIsAQueryTest()
    {
        Assert.IsAssignableFrom<IRequest<IEnumerable<Order>>>(_command);
    }

    [Fact]
    public void ItHasACustomerIdTest()
    {
        Assert.Equal(1234, _command.CustomerId);
    }
}
