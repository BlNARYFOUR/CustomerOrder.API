namespace CustomerOrder.API.Domain.Exceptions;

public class InvalidIdException : Exception
{
    public static InvalidIdException ForClass(int id, string className)
    {
        return new InvalidIdException($"Resource with id '${id}' does not exist. For class ${className}");
    }

    public InvalidIdException(string message) : base(message) {}
    
    public InvalidIdException(string message, Exception inner) : base(message, inner) {}
}
