using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Domain.Repositories;

public interface ICustomerRepository
{
    public Task<IEnumerable<Customer>> GetAllAsync();
    /// <exception cref="Exceptions.NotFoundException" />
    public Task<Customer> GetByIdAsync(int id);
    public Task<Customer> CreateAsync(Customer customer);
    public Task IncreaseNumberOfOrdersAsync(int id);
}
