using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ninth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_days_weekendID",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorShifts_days_dayId",
                table: "DoctorShifts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_days",
                table: "days");

            migrationBuilder.RenameTable(
                name: "days",
                newName: "Days");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Days",
                table: "Days",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BrachEmps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_id = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Emp_id = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrachEmps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrachEmps_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrachEmps_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchDepts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_id = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Dept_id = table.Column<int>(type: "int", nullable: false),
                    DepartementId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchDepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchDepts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchDepts_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Debt_id = table.Column<int>(type: "int", nullable: false),
                    DepartementId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_emp_id = table.Column<int>(type: "int", nullable: false),
                    BrachEmpId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpShifts_BrachEmps_BrachEmpId",
                        column: x => x.BrachEmpId,
                        principalTable: "BrachEmps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesDoctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serv_id = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Branch_doctor_id = table.Column<int>(type: "int", nullable: false),
                    BranchDoctorId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesDoctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesDoctors_BranchDoctors_BranchDoctorId",
                        column: x => x.BranchDoctorId,
                        principalTable: "BranchDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesDoctors_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appoinments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patient_id = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Serv_doctor_id = table.Column<int>(type: "int", nullable: false),
                    ServiceDoctorId = table.Column<int>(type: "int", nullable: false),
                    Branch_id = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    emp_id = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoinments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appoinments_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appoinments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appoinments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appoinments_ServicesDoctors_ServiceDoctorId",
                        column: x => x.ServiceDoctorId,
                        principalTable: "ServicesDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_BranchId",
                table: "Appoinments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_EmployeeId",
                table: "Appoinments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_PatientId",
                table: "Appoinments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_ServiceDoctorId",
                table: "Appoinments",
                column: "ServiceDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_BrachEmps_BranchId",
                table: "BrachEmps",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BrachEmps_EmployeeId",
                table: "BrachEmps",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchDepts_BranchId",
                table: "BranchDepts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchDepts_DepartementId",
                table: "BranchDepts",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpShifts_BrachEmpId",
                table: "EmpShifts",
                column: "BrachEmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DepartementId",
                table: "Services",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesDoctors_BranchDoctorId",
                table: "ServicesDoctors",
                column: "BranchDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesDoctors_ServiceId",
                table: "ServicesDoctors",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Days_weekendID",
                table: "Branches",
                column: "weekendID",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorShifts_Days_dayId",
                table: "DoctorShifts",
                column: "dayId",
                principalTable: "Days",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Days_weekendID",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorShifts_Days_dayId",
                table: "DoctorShifts");

            migrationBuilder.DropTable(
                name: "Appoinments");

            migrationBuilder.DropTable(
                name: "BranchDepts");

            migrationBuilder.DropTable(
                name: "EmpShifts");

            migrationBuilder.DropTable(
                name: "ServicesDoctors");

            migrationBuilder.DropTable(
                name: "BrachEmps");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Days",
                table: "Days");

            migrationBuilder.RenameTable(
                name: "Days",
                newName: "days");

            migrationBuilder.AddPrimaryKey(
                name: "PK_days",
                table: "days",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_days_weekendID",
                table: "Branches",
                column: "weekendID",
                principalTable: "days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorShifts_days_dayId",
                table: "DoctorShifts",
                column: "dayId",
                principalTable: "days",
                principalColumn: "Id");
        }
    }
}
