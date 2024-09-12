using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bill_system_API.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountsAndPaidUpToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PaidUp",
                table: "Invoices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PercentageDiscount",
                table: "Invoices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueDiscount",
                table: "Invoices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidUp",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PercentageDiscount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ValueDiscount",
                table: "Invoices");
        }
    }
}
