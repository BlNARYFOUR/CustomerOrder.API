using CustomerOrder.API.Domain.Events;
using MediatR;

namespace CustomerOrder.API.Tests.Domain.Events;

public class OrderCreatedEventTest
{
    private readonly OrderCreatedEvent _event;

    public OrderCreatedEventTest()
    {
        _event = new OrderCreatedEvent(1234, 2345);
    }

    [Fact]
    public void ItIsAnEventTest()
    {
        Assert.IsAssignableFrom<INotification>(_event);
    }

    [Fact]
    public void ItHasAnIdTest()
    {
        Assert.Equal(1234, _event.Id);
    }

    [Fact]
    public void ItHasACustomerIdTest()
    {
        Assert.Equal(2345, _event.CustomerId);
    }
}
