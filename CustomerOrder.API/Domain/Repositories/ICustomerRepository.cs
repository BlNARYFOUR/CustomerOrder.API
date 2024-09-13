using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Domain.Repositories;

public interface ICustomerRepository
{
    public Task<IEnumerable<Customer>> GetAllAsync();
    public Task<IEnumerable<Customer>> SearchOnEmailAsync(string searchString);
    /// <exception cref="Exceptions.NotFoundException" />
    public Task<Customer> GetByIdAsync(int id);
    public Task<Customer> CreateAsync(Customer customer);
    public Task<Customer> UpdateAsync(Customer customer);
    public Task IncreaseNumberOfOrdersAsync(int id);
    public Task<Customer?> FindByEmailAsync(string email);
}
