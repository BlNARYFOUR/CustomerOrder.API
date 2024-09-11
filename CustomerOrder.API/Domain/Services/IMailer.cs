namespace CustomerOrder.API.Domain.Services;

public interface IMailer
{
    public void Send(string mailTo, string subject, string message);
}
