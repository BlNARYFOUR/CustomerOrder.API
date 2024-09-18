using CustomerOrder.API.Domain.Models;

namespace CustomerOrder.API.Domain.Exceptions;

public class ValidationException : Exception
{
    public ValidationError[] Errors { get; } = [];

    public ValidationException(params ValidationError[] errors) : base() => Errors = errors;

    public ValidationException() : base() {}

    public ValidationException(string? message) : base(message) {}

    public ValidationException(string? message, Exception? innerException) : base(message, innerException) {}
}
