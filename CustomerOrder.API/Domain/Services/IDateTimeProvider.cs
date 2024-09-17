namespace CustomerOrder.API.Domain.Services;

public interface IDateTimeProvider
{
    public DateTime MinValue();
    public DateTime Now();
}
