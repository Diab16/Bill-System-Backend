using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bill_system_API.Migrations
{
    /// <inheritdoc />
    public partial class add_Name_in_invoiceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InvoiceItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "InvoiceItems");
        }
    }
}
