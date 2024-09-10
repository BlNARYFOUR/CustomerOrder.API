namespace CustomerOrder.API.Application.Dtos;

public class StatusGet(int statusCode)
{
    public int StatusCode { get; } = statusCode;
}
