using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
    }
}
