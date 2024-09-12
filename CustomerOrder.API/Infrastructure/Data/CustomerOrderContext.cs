using Microsoft.EntityFrameworkCore;
using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Infrastructure.Data.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerOrder.API.Infrastructure.Data;

public class CustomerOrderContext(DbContextOptions<CustomerOrderContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Email> Emails { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? v.Value.ToUniversalTime() : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.IsKeyless)
            {
                continue;
            }

            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
                else if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(nullableDateTimeConverter);
                }
            }
        }

        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        ConstraintsBuilder.Build(modelBuilder);
        Seeder.Build(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
