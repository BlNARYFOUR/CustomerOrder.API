using CustomerOrder.API.Domain.Models;

namespace CustomerOrder.API.Tests.Domain.Models;

public class ValidationErrorTest
{
    private readonly ValidationError _model;

    public ValidationErrorTest()
    {
        _model = new ValidationError("test_name", "test_message");
    }

    [Fact]
    public void ItHasAPropertyNameTest()
    {
        Assert.Equal("test_name", _model.PropertyName);
    }

    [Fact]
    public void ItHasAnErrorMessageTest()
    {
        Assert.Equal("test_message", _model.ErrorMessage);
    }
}
