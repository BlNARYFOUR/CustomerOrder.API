using CustomerOrder.API.Domain.Exceptions;

namespace CustomerOrder.API.Tests.Domain.Exceptions;

public class NotFoundExceptionTest
{
    private readonly NotFoundException _exception;

    public NotFoundExceptionTest()
    {
        _exception = new NotFoundException("test");
    }

    [Fact]
    public void ItIsAnExceptionTest()
    {
        Assert.IsAssignableFrom<Exception>(_exception);
    }

    [Fact]
    public void ItHasAMessageTest()
    {
        Assert.Equal("test", _exception.Message);
    }

    [Fact]
    public void ItCanBeConstructedForClassTest()
    {
        NotFoundException exception = NotFoundException.ForClass("TestClass");

        Assert.IsType<NotFoundException>(exception);
        Assert.Equal("Resource not found. For class TestClass", exception.Message);
    }
}
