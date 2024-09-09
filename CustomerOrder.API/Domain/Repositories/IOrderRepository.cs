using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Domain.Repositories;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetAllAsync();
    /// <exception cref="Exceptions.NotFoundException" />
    public Task<Order> GetByIdAsync(int id);
    public Task<Order> CreateAsync(Order order);
}
