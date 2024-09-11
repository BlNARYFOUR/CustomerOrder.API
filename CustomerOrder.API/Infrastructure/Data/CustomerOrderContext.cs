using Microsoft.EntityFrameworkCore;
using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Infrastructure.Data.Builders;

namespace CustomerOrder.API.Infrastructure.Data;

public class CustomerOrderContext(DbContextOptions<CustomerOrderContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Email> Emails { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConstraintsBuilder.Build(modelBuilder);
        Seeder.Build(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
