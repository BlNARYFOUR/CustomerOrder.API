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

        if ( null == customer)
        {
            throw NotFoundException.ForClass(nameof(Customer));
        }

        return await _context.Customers.Where(c => id == c.Id).FirstAsync();
    }
}
