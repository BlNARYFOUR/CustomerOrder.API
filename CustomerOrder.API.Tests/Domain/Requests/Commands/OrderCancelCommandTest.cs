using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;

namespace CustomerOrder.API.Tests.Domain.Requests.Commands;

public class OrderCancelCommandTest
{
    private readonly OrderCancelCommand _command;

    public OrderCancelCommandTest()
    {
        _command = new OrderCancelCommand(1234);
    }

    [Fact]
    public void ItIsACommandTest()
    {
        Assert.IsAssignableFrom<IRequest>(_command);
    }

    [Fact]
    public void ItHasAnIdTest()
    {
        Assert.Equal(1234, _command.Id);
    }
}
