using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Domain.Repositories;

public interface IOrderRepository
{
    /// <exception cref="Exceptions.NotFoundException" />
    public Task<IEnumerable<Order>> GetAllForCustomerAsync(int customerId);
    /// <exception cref="Exceptions.NotFoundException" />
    public Task<Order> GetByIdAsync(int id);
    public Task<Order> CreateAsync(Order order);
}
