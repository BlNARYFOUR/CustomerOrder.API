namespace CustomerOrder.API.Domain.Models;

public record ValidationError(string PropertyName, string ErrorMessage);
