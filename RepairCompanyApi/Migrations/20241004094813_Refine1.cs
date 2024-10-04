using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairCompanyApi.Migrations
{
    /// <inheritdoc />
    public partial class Refine1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "PropertyOwners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "PropertyOwners");
        }
    }
}
