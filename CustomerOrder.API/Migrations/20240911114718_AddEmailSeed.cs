using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerOrder.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "From", "Message", "Subject", "To", "Token" },
                values: new object[] { 1, "noreply@test.test", "Hi User\n\nThis is a test email!", "Test Email", "john.doe@test.test", "test-email-token" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Emails",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
