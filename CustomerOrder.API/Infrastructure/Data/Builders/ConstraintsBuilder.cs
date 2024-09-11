using CustomerOrder.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrder.API.Infrastructure.Data.Builders
{
    public class ConstraintsBuilder
    {
        public static void Build(ModelBuilder builder)
        {
            //builder.Entity<Customer>(entity => {
            //    entity.HasIndex(e => e.Email).IsUnique();
            //});
            builder.Entity<Email>(entity => {
                entity.HasIndex(e => e.Token).IsUnique();
            });
        }
    }
}
