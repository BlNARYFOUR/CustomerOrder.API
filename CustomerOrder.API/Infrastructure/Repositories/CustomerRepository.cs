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

    public async Task<IEnumerable<Customer>> SearchOnEmailAsync(string searchString)
    {
        return await _context.Customers.Where(c => c.Email.Contains(searchString)).ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (null == customer)
        {
            throw NotFoundException.ForClass(nameof(Customer));
        }

        return customer;
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        var createdCustomer = _context.Customers.Add(customer).Entity;
        await _context.SaveChangesAsync();

        return createdCustomer;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        var createdCustomer = _context.Customers.Update(customer).Entity;
        await _context.SaveChangesAsync();

        return createdCustomer;
    }

    public async Task IncreaseNumberOfOrdersAsync(int id)
    {
        var updatedRows = await _context.Customers.Where(c => id == c.Id).ExecuteUpdateAsync(
            setters => setters.SetProperty(c => c.NumberOfOrders, c => c.NumberOfOrders + 1)
        );

        if (0 == updatedRows)
        {
            throw NotFoundException.ForClass(nameof(Customer));
        }
    }

    public async Task<Customer?> FindByEmailAsync(string email)
    {
        return await _context.Customers.Where(c => email == c.Email).FirstOrDefaultAsync();
    }
}
