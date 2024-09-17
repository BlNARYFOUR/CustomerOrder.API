using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;

namespace CustomerOrder.API.Tests.Domain.Requests.Commands;

public class OrderCreateCommandTest
{
    private readonly OrderCreateCommand _command;

    public OrderCreateCommandTest()
    {
        _command = new OrderCreateCommand(
            1234,
            "test_description",
            1.23
        );
    }

    [Fact]
    public void ItIsACommandTest()
    {
        Assert.IsAssignableFrom<IRequest<int>>(_command);
    }

    [Fact]
    public void ItHasACustomerIdTest()
    {
        Assert.Equal(1234, _command.CustomerId);
    }

    [Fact]
    public void ItHasADescriptionTest()
    {
        Assert.Equal("test_description", _command.Description);
    }

    [Fact]
    public void ItHasAPriceTest()
    {
        Assert.Equal(1.23, _command.Price);
    }
}
