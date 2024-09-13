using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Tests.Domain.Entities;

public class EmailTest
{
    private readonly Email _email;

    public EmailTest()
    {
        _email = new Email("from", "to", "subject", "message");
    }

    [Fact]
    public void ItHasAnIdTest()
    {
        Assert.Equal(0, _email.Id);
    }

    [Fact]
    public void ItHasATokenTest()
    {
        Assert.Empty(_email.Token);
    }

    [Fact]
    public void ItHasAFromTest()
    {
        Assert.Same("from", _email.From);
    }

    [Fact]
    public void ItHasAToTest()
    {
        Assert.Same("to", _email.To);
    }

    [Fact]
    public void ItHasASubjectTest()
    {
        Assert.Same("subject", _email.Subject);
    }

    [Fact]
    public void ItHasAMessageTest()
    {
        Assert.Same("message", _email.Message);
    }

    [Fact]
    public void ItCanSetAnIdTest()
    {
        _email.Id = 2;
        Assert.Equal(2, _email.Id);
    }

    [Fact]
    public void ItCanSetATokenTest()
    {
        _email.Token = "token_2";
        Assert.Same("token_2", _email.Token);
    }

    [Fact]
    public void ItCanSetAFromTest()
    {
        _email.From = "from_2";
        Assert.Same("from_2", _email.From);
    }

    [Fact]
    public void ItCanSetAToTest()
    {
        _email.To = "to_2";
        Assert.Same("to_2", _email.To);
    }

    [Fact]
    public void ItCanSetASubjectTest()
    {
        _email.Subject = "subject_2";
        Assert.Same("subject_2", _email.Subject);
    }

    [Fact]
    public void ItCanSetAMessageTest()
    {
        _email.Message = "message_2";
        Assert.Same("message_2", _email.Message);
    }
}