using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;

namespace CustomerOrder.API.Tests.Domain.Requests.Commands;

public class EmailResendCommandTest
{
    private readonly EmailResendCommand _command;

    public EmailResendCommandTest()
    {
        _command = new EmailResendCommand("test_token");
    }

    [Fact]
    public void ItIsACommandTest()
    {
        Assert.IsAssignableFrom<IRequest>(_command);
    }

    [Fact]
    public void ItHasATokenTest()
    {
        Assert.Equal("test_token", _command.Token);
    }
}
