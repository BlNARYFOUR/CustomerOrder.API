using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Tests.Domain.Entities;

public class EmailTest
{
    private readonly Email _entity;

    public EmailTest()
    {
        _entity = new Email("from", "to", "subject", "message");
    }

    [Fact]
    public void ItHasAnIdTest()
    {
        Assert.Equal(0, _entity.Id);
    }

    [Fact]
    public void ItHasATokenTest()
    {
        Assert.Empty(_entity.Token);
    }

    [Fact]
    public void ItHasAFromTest()
    {
        Assert.Equal("from", _entity.From);
    }

    [Fact]
    public void ItHasAToTest()
    {
        Assert.Equal("to", _entity.To);
    }

    [Fact]
    public void ItHasASubjectTest()
    {
        Assert.Equal("subject", _entity.Subject);
    }

    [Fact]
    public void ItHasAMessageTest()
    {
        Assert.Equal("message", _entity.Message);
    }

    [Fact]
    public void ItCanSetAnIdTest()
    {
        _entity.Id = 2;
        Assert.Equal(2, _entity.Id);
    }

    [Fact]
    public void ItCanSetATokenTest()
    {
        _entity.Token = "token_2";
        Assert.Equal("token_2", _entity.Token);
    }

    [Fact]
    public void ItCanSetAFromTest()
    {
        _entity.From = "from_2";
        Assert.Equal("from_2", _entity.From);
    }

    [Fact]
    public void ItCanSetAToTest()
    {
        _entity.To = "to_2";
        Assert.Equal("to_2", _entity.To);
    }

    [Fact]
    public void ItCanSetASubjectTest()
    {
        _entity.Subject = "subject_2";
        Assert.Equal("subject_2", _entity.Subject);
    }

    [Fact]
    public void ItCanSetAMessageTest()
    {
        _entity.Message = "message_2";
        Assert.Equal("message_2", _entity.Message);
    }
}
