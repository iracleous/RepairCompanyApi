using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairCompanyApi.Migrations
{
    /// <inheritdoc />
    public partial class Step2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyOwners",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildingProperties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyOwnerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingProperties_PropertyOwners_PropertyOwnerId",
                        column: x => x.PropertyOwnerId,
                        principalTable: "PropertyOwners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepairStatus = table.Column<int>(type: "int", nullable: false),
                    BuildingPropertyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_BuildingProperties_BuildingPropertyId",
                        column: x => x.BuildingPropertyId,
                        principalTable: "BuildingProperties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildingProperties_PropertyOwnerId",
                table: "BuildingProperties",
                column: "PropertyOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_BuildingPropertyId",
                table: "Repairs",
                column: "BuildingPropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "BuildingProperties");

            migrationBuilder.DropTable(
                name: "PropertyOwners");
        }
    }
}
