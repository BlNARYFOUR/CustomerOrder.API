using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrder.API.Infrastructure.Repositories;

public class CustomerRepository(CustomerOrderContext context) : ICustomerRepository
{
    private readonly CustomerOrderContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        var customer = await _context.Customers.Where(c => id == c.Id).FirstOrDefaultAsync();

        if (null == customer)
        {
            throw NotFoundException.ForClass(nameof(Customer));
        }

        return customer;
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        var updatedCustomer = _context.Customers.Add(customer).Entity;
        await _context.SaveChangesAsync();

        return updatedCustomer;
    }

    public async Task IncreaseNumberOfOrdersAsync(int id)
    {
        await _context.Customers.Where(c => id == c.Id).ExecuteUpdateAsync(
            setters => setters.SetProperty(c => c.NumberOfOrders, c => c.NumberOfOrders + 1)
        );
    }
}
