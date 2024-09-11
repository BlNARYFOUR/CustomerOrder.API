using CustomerOrder.API.Domain.Models;

namespace CustomerOrder.API.Domain.Exceptions;

public class ValidationException(params ValidationError[] errors) : Exception()
{
    public ValidationError[] Errors { get; } = errors;
}
