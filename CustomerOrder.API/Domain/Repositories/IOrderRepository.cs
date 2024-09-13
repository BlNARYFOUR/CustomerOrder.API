using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Domain.Repositories;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetListForCustomerAsync(int customerId);
    public Task<IEnumerable<Order>> GetAllCancelledForCustomerAsync(int customerId);
    public Task<IEnumerable<Order>> SearchOnCreationDateForCustomersAsync(DateTime from, DateTime to, List<int> customerIds);
    /// <exception cref="Exceptions.NotFoundException" />
    public Task<Order> GetByIdAsync(int id);
    public Task<Order> CreateAsync(Order order);
    /// <exception cref="Exceptions.NotFoundException" />
    public Task CancelAsync(int id);
}
