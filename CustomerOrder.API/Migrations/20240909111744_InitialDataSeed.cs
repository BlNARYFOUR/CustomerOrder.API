using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerOrder.API.Migrations;

/// <inheritdoc />
public partial class InitialDataSeed : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Customers",
            columns: ["Id", "Email", "FirstName", "LastName", "NumberOfOrders"],
            values: new object[,]
            {
                { 1, "john.doe@test.test", "John", "Doe", 2 },
                { 2, "jane.doe@test.test", "Jane", "Doe", 1 }
            });

        migrationBuilder.InsertData(
            table: "Orders",
            columns: ["Id", "CreationDate", "CustomerId", "Description", "Price"],
            values: new object[,]
            {
                { 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Order 1", 1.99 },
                { 2, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Order 2", 2.9900000000000002 },
                { 3, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Order 3", 3.9900000000000002 }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Orders",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Orders",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Orders",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Customers",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Customers",
            keyColumn: "Id",
            keyValue: 2);
    }
}
