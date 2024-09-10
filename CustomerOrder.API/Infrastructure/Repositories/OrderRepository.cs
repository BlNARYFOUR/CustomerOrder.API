using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrder.API.Infrastructure.Repositories;

public class OrderRepository(CustomerOrderContext context) : IOrderRepository
{
    private readonly CustomerOrderContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<Order>> GetAllForCustomerAsync(int customerId)
    {
        return await _context.Orders.Where(o => customerId == o.CustomerId).OrderByDescending(o => o.CreationDate).ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        var order = await _context.Orders.Where(o => id == o.Id).FirstOrDefaultAsync();

        if (null == order)
        {
            throw NotFoundException.ForClass(nameof(Order));
        }

        return order;
    }

    public async Task<Order> CreateAsync(Order order)
    {
        return (await _context.Orders.AddAsync(order)).Entity;
    }
}
