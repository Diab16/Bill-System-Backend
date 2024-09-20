using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bill_system_API.Migrations
{
    /// <inheritdoc />
    public partial class edit_invoice_and_invoiceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "InvoiceItems");

            migrationBuilder.RenameColumn(
                name: "ValueDiscount",
                table: "Invoices",
                newName: "BillTotal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillTotal",
                table: "Invoices",
                newName: "ValueDiscount");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "Invoices",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "Invoices",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "InvoiceItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
