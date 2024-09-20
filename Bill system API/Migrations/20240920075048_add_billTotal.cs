using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bill_system_API.Migrations
{
    /// <inheritdoc />
    public partial class add_billTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BillTotal",
                table: "Invoices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillTotal",
                table: "Invoices");
        }
    }
}
