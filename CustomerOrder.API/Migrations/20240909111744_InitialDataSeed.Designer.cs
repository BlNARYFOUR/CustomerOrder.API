using CustomerOrder.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerOrder.API.Migrations;

[DbContext(typeof(CustomerOrderContext))]
[Migration("20240909111744_InitialDataSeed")]
partial class InitialDataSeed
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.8")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("CustomerOrder.API.Domain.Entities.Customer", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<int>("NumberOfOrders")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.ToTable("Customers");

                b.HasData(
                    new
                    {
                        Id = 1,
                        Email = "john.doe@test.test",
                        FirstName = "John",
                        LastName = "Doe",
                        NumberOfOrders = 2
                    },
                    new
                    {
                        Id = 2,
                        Email = "jane.doe@test.test",
                        FirstName = "Jane",
                        LastName = "Doe",
                        NumberOfOrders = 1
                    });
            });

        modelBuilder.Entity("CustomerOrder.API.Domain.Entities.Order", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<DateTime>("CreationDate")
                    .HasColumnType("datetime2");

                b.Property<int>("CustomerId")
                    .HasColumnType("int");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<double>("Price")
                    .HasColumnType("float");

                b.HasKey("Id");

                b.HasIndex("CustomerId");

                b.ToTable("Orders");

                b.HasData(
                    new
                    {
                        Id = 1,
                        CreationDate = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        CustomerId = 1,
                        Description = "Order 1",
                        Price = 1.99
                    },
                    new
                    {
                        Id = 2,
                        CreationDate = new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        CustomerId = 1,
                        Description = "Order 2",
                        Price = 2.9900000000000002
                    },
                    new
                    {
                        Id = 3,
                        CreationDate = new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        CustomerId = 2,
                        Description = "Order 3",
                        Price = 3.9900000000000002
                    });
            });

        modelBuilder.Entity("CustomerOrder.API.Domain.Entities.Order", b =>
            {
                b.HasOne("CustomerOrder.API.Domain.Entities.Customer", "Customer")
                    .WithMany("Orders")
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Customer");
            });

        modelBuilder.Entity("CustomerOrder.API.Domain.Entities.Customer", b =>
            {
                b.Navigation("Orders");
            });
#pragma warning restore 612, 618
    }
}
