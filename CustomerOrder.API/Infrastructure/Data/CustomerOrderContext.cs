using Microsoft.EntityFrameworkCore;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Infrastructure.Data;

public class CustomerOrderContext(DbContextOptions<CustomerOrderContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(
            new Customer("John", "Doe", "john.doe@test.test", 2) { Id = 1 },
            new Customer("Jane", "Doe", "jane.doe@test.test", 1) { Id = 2 }
        );
        modelBuilder.Entity<Order>().HasData(
            new Order(1, "Order 1", 1.99) { Id = 1, CreationDate = new DateTime(2024, 1, 2) },
            new Order(1, "Order 2", 2.99) { Id = 2, CreationDate = new DateTime(2024, 2, 4) },
            new Order(2, "Order 3", 3.99) { Id = 3, CreationDate = new DateTime(2024, 3, 6) }
        );


        base.OnModelCreating(modelBuilder);
    }
}
