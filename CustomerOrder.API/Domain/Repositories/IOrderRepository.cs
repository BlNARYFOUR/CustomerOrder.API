using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Domain.Repositories;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetAllForCustomerAsync(int customerId);
    public Task<IEnumerable<Order>> GetAllCancelledForCustomerAsync(int customerId);
    /// <exception cref="Exceptions.NotFoundException" />
    public Task<Order> CreateAsync(Order order);
    /// <exception cref="Exceptions.NotFoundException" />
    public Task CancelAsync(int id);
}
