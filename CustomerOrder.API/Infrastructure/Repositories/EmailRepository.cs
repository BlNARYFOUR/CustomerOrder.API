using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrder.API.Infrastructure.Repositories;

public class EmailRepository(CustomerOrderContext context) : IEmailRepository
{
    private readonly CustomerOrderContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<Email> CreateAsync(Email email)
    {
        var createdEmail = _context.Emails.Add(email).Entity;
        await _context.SaveChangesAsync();

        return createdEmail;
    }

    public async Task<Email> GetByTokenAsync(string token)
    {
        var email = await _context.Emails.Where(e => token == e.Token).FirstOrDefaultAsync();

        if (null == email)
        {
            throw NotFoundException.ForClass(nameof(Email));
        }

        return email;
    }
}
