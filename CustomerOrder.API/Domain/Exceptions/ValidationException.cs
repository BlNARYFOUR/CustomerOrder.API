using CustomerOrder.API.Domain.Models;

namespace CustomerOrder.API.Domain.Exceptions;

public class ValidationException(params ValidationError[] errors) : Exception()
{
    public ValidationError[] Errors { get; } = errors;
}

// Example (todo: implementation for all exceptions)
public class SomeException : Exception
{
    public SomeException()
    {
    }

    public SomeException(string? message) : base(message)
    {
    }

    public SomeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
