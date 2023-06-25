using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthOfDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    DepartementId = table.Column<int>(type: "int", nullable: true),
                    DeptID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartementId",
                table: "Doctors",
                column: "DepartementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
