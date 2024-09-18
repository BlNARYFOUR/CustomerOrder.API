using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Models;

namespace CustomerOrder.API.Tests.Domain.Exceptions;

public class ValidationExceptionTest
{
    private readonly ValidationException _exception;

    public ValidationExceptionTest()
    {
        _exception = new ValidationException();
    }

    [Fact]
    public void ItIsAnExceptionTest()
    {
        Assert.IsAssignableFrom<Exception>(_exception);
    }

    [Fact]
    public void ItHasErrorsTest()
    {
        Assert.Equal((ValidationError[])[], _exception.Errors);
    }

    [Fact]
    public void ItHasAMessageTest()
    {
        Assert.Equal($"Exception of type '{typeof(ValidationException)}' was thrown.", _exception.Message);
    }

    [Fact]
    public void ItCanBeConstructedWithAMessageTest()
    {
        var exception = new ValidationException("test_message");
        Assert.Equal("test_message", exception.Message);
    }

    [Fact]
    public void ItCanBeConstructedWithAMessageAndAnInnerExceptionTest()
    {
        var innerException = new Exception("test_inner");
        var exception = new ValidationException("test_message", innerException);

        Assert.Equal("test_message", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Fact]
    public void ItCanBeConstructedWithOneErrorTest()
    {
        var expectedError1 = new ValidationError("prop_1", "message_1");

        var exception = new ValidationException(expectedError1);

        Assert.Equal([expectedError1], exception.Errors);
    }

    [Fact]
    public void ItCanBeConstructedWithFiveErrorsTest()
    {
        var expectedError1 = new ValidationError("prop_1", "message_1");
        var expectedError2 = new ValidationError("prop_2", "message_2");
        var expectedError3 = new ValidationError("prop_3", "message_3");
        var expectedError4 = new ValidationError("prop_4", "message_4");
        var expectedError5 = new ValidationError("prop_5", "message_5");

        var exception = new ValidationException(
            expectedError1,
            expectedError2,
            expectedError3,
            expectedError4,
            expectedError5
        );

        Assert.Equal([
            expectedError1,
            expectedError2,
            expectedError3,
            expectedError4,
            expectedError5
        ], exception.Errors);
    }

    [Theory]
    [MemberData(nameof(ParamData))]
    public void ItCanBeConstructedWithErrorArrayTest(ValidationError[] paramData)
    {
        var exception = new ValidationException(paramData);
        Assert.Equal(paramData, exception.Errors);
    }

    public static IEnumerable<object[]> ParamData()
    {
        var paramList = new List<ValidationError>();

        for (int i = 1; i <= 10; i++)
        {
            paramList.Add(new ValidationError($"prop_{i}", $"message_{i}"));

            yield return new object[] { paramList.ToArray() };
        }
    }
}
