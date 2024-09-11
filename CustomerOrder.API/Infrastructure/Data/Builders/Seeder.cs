using CustomerOrder.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrder.API.Infrastructure.Data.Builders
{
    public class Seeder
    {
        public static void Build(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasData(
                new Customer("John", "Doe", "john.doe@test.test", 2) { Id = 1 },
                new Customer("Jane", "Doe", "jane.doe@test.test", 1) { Id = 2 }
            );

            builder.Entity<Order>().HasData(
                new Order(1, "Order 1", 1.99) { Id = 1, CreationDate = new DateTime(2024, 1, 2) },
                new Order(1, "Order 2", 2.99) { Id = 2, CreationDate = new DateTime(2024, 2, 4) },
                new Order(2, "Order 3", 3.99) { Id = 3, CreationDate = new DateTime(2024, 3, 6) }
            );

            builder.Entity<Email>().HasData(
                new Email("noreply@test.test", "john.doe@test.test", "Test Email", "Hi User\n\nThis is a test email!", "test-email-token") { Id = 1 }
            );
        }
    }
}
