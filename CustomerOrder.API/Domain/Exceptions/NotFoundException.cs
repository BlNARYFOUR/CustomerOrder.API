namespace CustomerOrder.API.Domain.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
    public static NotFoundException ForClass(string className)
    {
        return new NotFoundException($"Resource not found. For class {className}");
    }
}
