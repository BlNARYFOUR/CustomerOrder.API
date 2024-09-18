namespace CustomerOrder.API.Domain.Exceptions;

public class NotFoundException : Exception
{
    public static NotFoundException ForClass(string className)
    {
        return new NotFoundException($"Resource not found. For class {className}");
    }

    public NotFoundException() : base() {}

    public NotFoundException(string? message) : base(message) {}

    public NotFoundException(string? message, Exception? innerException) : base(message, innerException) {}
}
