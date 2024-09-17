using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Domain.Repositories;

public interface IEmailRepository
{
    /// <exception cref="Exceptions.NotFoundException" />
    public Task<Email> GetByTokenAsync(string token);
    public Task<Email> CreateAsync(Email email);
    public Task<Email> UpdateAsync(Email email);
}
