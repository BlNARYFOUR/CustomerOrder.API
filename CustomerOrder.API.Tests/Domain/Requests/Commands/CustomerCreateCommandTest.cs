using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;

namespace CustomerOrder.API.Tests.Domain.Requests.Commands;

public class CustomerCreateCommandTest
{
    private readonly CustomerCreateCommand _command;

    public CustomerCreateCommandTest()
    {
        _command = new CustomerCreateCommand(
            "test_first_name",
            "test_last_name",
            "test_email"
        );
    }

    [Fact]
    public void ItIsACommandTest()
    {
        Assert.IsAssignableFrom<IRequest<int>>(_command);
    }

    [Fact]
    public void ItHasAFirstNameTest()
    {
        Assert.Equal("test_first_name", _command.FirstName);
    }

    [Fact]
    public void ItHasALastNameTest()
    {
        Assert.Equal("test_last_name", _command.LastName);
    }

    [Fact]
    public void ItHasAnEmailTest()
    {
        Assert.Equal("test_email", _command.Email);
    }
}
