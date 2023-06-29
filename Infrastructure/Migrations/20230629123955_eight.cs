using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "jobTitle",
                table: "Employees");


            migrationBuilder.AddColumn<int>(
                name: "jobTitleId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "startTime",
                table: "DoctorShifts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "endTime",
                table: "DoctorShifts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_jobTitleId",
                table: "Employees",
                column: "jobTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_JobTitles_jobTitleId",
                table: "Employees",
                column: "jobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropIndex(
                name: "IX_Employees_jobTitleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "jobTitleId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "jobID",
                table: "Employees",
                newName: "DeptId");

            migrationBuilder.AddColumn<string>(
                name: "jobTitle",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "startTime",
                table: "DoctorShifts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "endTime",
                table: "DoctorShifts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartementId",
                table: "Employees",
                column: "DepartementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departements_DepartementId",
                table: "Employees",
                column: "DepartementId",
                principalTable: "Departements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
