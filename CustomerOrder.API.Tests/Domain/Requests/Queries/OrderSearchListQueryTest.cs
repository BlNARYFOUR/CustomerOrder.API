using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Tests.Domain.Requests.Queries;

public class OrderSearchListQueryTest
{
    private readonly OrderSearchListQuery _command;

    public OrderSearchListQueryTest()
    {
        _command = new OrderSearchListQuery(
            [1, 2],
            "test_from",
            "test_to"
        );
    }

    [Fact]
    public void ItIsAQueryTest()
    {
        Assert.IsAssignableFrom<IRequest<IEnumerable<Order>>>(_command);
    }

    [Fact]
    public void ItHasCustomerIdsTest()
    {
        Assert.Equal([1, 2], _command.CustomerIds);
    }

    [Fact]
    public void ItHasAFromTest()
    {
        Assert.Equal("test_from", _command.From);
    }

    [Fact]
    public void ItHasAToTest()
    {
        Assert.Equal("test_to", _command.To);
    }
}
