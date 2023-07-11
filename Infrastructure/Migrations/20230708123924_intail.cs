using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Debt_id = table.Column<int>(type: "int", nullable: false),
                    DepartementId = table.Column<int>(type: "int", nullable: true),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BranchDepts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_id = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    Dept_id = table.Column<int>(type: "int", nullable: false),
                    DepartementId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchDepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchDepts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BranchDepts_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departements",
                        principalColumn: "Id");
                });


            migrationBuilder.CreateTable(
                name: "BrachEmps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_id = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    Emp_id = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrachEmps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrachEmps_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BrachEmps_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServicesDoctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serv_id = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    Branch_doctor_id = table.Column<int>(type: "int", nullable: false),
                    BranchDoctorId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesDoctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesDoctors_BranchDoctors_BranchDoctorId",
                        column: x => x.BranchDoctorId,
                        principalTable: "BranchDoctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServicesDoctors_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_emp_id = table.Column<int>(type: "int", nullable: false),
                    BrachEmpId = table.Column<int>(type: "int", nullable: true),
                    dayId = table.Column<int>(type: "int", nullable: true),
                    day_ID = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmpShifts_Days_dayId",
                        column: x => x.dayId,
                        principalTable: "Days",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appoinments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patient_id = table.Column<int>(type: "int", nullable: false),
                    ServiceDoctorId = table.Column<int>(type: "int", nullable: false),
                    Branch_id = table.Column<int>(type: "int", nullable: false),
                    emp_id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoinments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appoinments_Branches_Branch_id",
                        column: x => x.Branch_id,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appoinments_Employees_emp_id",
                        column: x => x.emp_id,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appoinments_Patients_Patient_id",
                        column: x => x.Patient_id,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appoinments_ServicesDoctors_ServiceDoctorId",
                        column: x => x.ServiceDoctorId,
                        principalTable: "ServicesDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_Branch_id",
                table: "Appoinments",
                column: "Branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_emp_id",
                table: "Appoinments",
                column: "emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_Patient_id",
                table: "Appoinments",
                column: "Patient_id");

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
                name: "IX_EmpShifts_dayId",
                table: "EmpShifts",
                column: "dayId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

        }
    }
}
