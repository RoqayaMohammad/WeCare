using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sivnth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branchDoctorId = table.Column<int>(type: "int", nullable: true),
                    branchDoctor_ID = table.Column<int>(type: "int", nullable: false),
                    dayId = table.Column<int>(type: "int", nullable: true),
                    day_ID = table.Column<int>(type: "int", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorShifts_BranchDoctors_branchDoctorId",
                        column: x => x.branchDoctorId,
                        principalTable: "BranchDoctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DoctorShifts_days_dayId",
                        column: x => x.dayId,
                        principalTable: "days",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShifts_branchDoctorId",
                table: "DoctorShifts",
                column: "branchDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShifts_dayId",
                table: "DoctorShifts",
                column: "dayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorShifts");
        }
    }
}
