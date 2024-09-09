﻿using Microsoft.EntityFrameworkCore;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Infrastructure.Data
{
    public class CustomerOrderContext(DbContextOptions<CustomerOrderContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer("John", "Doe", "john.doe@test.test") { Id = 1, NumberOfOrders = 2 },
                new Customer("Jane", "Doe", "jane.doe@test.test") { Id = 2, NumberOfOrders = 1 }
            );
            modelBuilder.Entity<Order>().HasData(
                new Order("Order 1", 1.99) { Id = 1, CustomerId = 1, CreationDate = new DateTime(2024, 1, 2) },
                new Order("Order 2", 2.99) { Id = 2, CustomerId = 1, CreationDate = new DateTime(2024, 2, 4) },
                new Order("Order 3", 3.99) { Id = 3, CustomerId = 2, CreationDate = new DateTime(2024, 3, 6) }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
