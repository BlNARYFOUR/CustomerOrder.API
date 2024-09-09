namespace CustomerOrder.API.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public static NotFoundException ForClass(string className)
        {
            return new($"Resource not found. For class ${className}");
        }

        public NotFoundException(string message) : base(message) {}
        
        public NotFoundException(string message, Exception inner) : base(message, inner) {}
    }
}
