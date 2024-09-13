using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrder.API.Infrastructure.Repositories;

public class OrderRepository(CustomerOrderContext context) : IOrderRepository
{
    private readonly CustomerOrderContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<Order>> GetListForCustomerAsync(int customerId)
    {
        return await _context.Orders
            .Where(o => customerId == o.CustomerId)
            .OrderByDescending(o => o.CreationDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllCancelledForCustomerAsync(int customerId)
    {
        return await _context.Orders
            .Where(o => customerId == o.CustomerId && OrderStatus.CANCELLED == o.Status)
            .OrderByDescending(o => o.CreationDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> SearchOnCreationDateForCustomersAsync(DateTime from, DateTime to, List<int> customerIds)
    {
        return await _context.Orders.Where(
            o => (0 == customerIds.Count || customerIds.Contains(o.CustomerId))
            && from <= o.CreationDate
            && o.CreationDate <= to
        ).OrderByDescending(o => o.CreationDate).ToListAsync();
    }

    public async Task<Order> CreateAsync(Order order)
    {
        var createdOrder = _context.Orders.Add(order).Entity;
        await _context.SaveChangesAsync();

        return createdOrder;
    }

    public async Task CancelAsync(int id)
    {
        var updatedRows = await _context.Orders.Where(o => id == o.Id).ExecuteUpdateAsync(
           setters => setters.SetProperty(o => o.Status, OrderStatus.CANCELLED)
        );

        if (0 == updatedRows)
        {
            throw NotFoundException.ForClass(nameof(Order));
        }
    }
}
