using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bill_system_API.Migrations
{
    /// <inheritdoc />
    public partial class createRelationBetweenTypeAndCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Types",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Types_CompanyId",
                table: "Types",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Companies_CompanyId",
                table: "Types",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Companies_CompanyId",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_CompanyId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Types");
        }
    }
}
