using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Domain.Services;

public interface IMailer
{
    public string Send(Email email);
}
