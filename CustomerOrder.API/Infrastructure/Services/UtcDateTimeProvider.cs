using CustomerOrder.API.Domain.Services;

namespace CustomerOrder.API.Infrastructure.Services;

public class UtcDateTimeProvider : IDateTimeProvider
{
    public DateTime MinValue() => DateTime.MinValue;

    public DateTime Now() => DateTime.UtcNow;
}
