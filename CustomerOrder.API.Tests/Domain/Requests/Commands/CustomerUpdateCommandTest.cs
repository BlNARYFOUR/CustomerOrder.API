using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;

namespace CustomerOrder.API.Tests.Domain.Requests.Commands;

public class CustomerUpdateCommandTest
{
    private readonly CustomerUpdateCommand _command;

    public CustomerUpdateCommandTest()
    {
        _command = new CustomerUpdateCommand(
            1234,
            "test_first_name",
            "test_last_name",
            "test_email"
        );
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
