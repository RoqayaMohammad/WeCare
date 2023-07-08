using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentIssues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Branches_BranchId",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Employees_EmployeeId",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Patients_PatientId",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_ServicesDoctors_Serv_doctor_id",
                table: "Appoinments");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_BranchId",
                table: "Appoinments");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_EmployeeId",
                table: "Appoinments");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_PatientId",
                table: "Appoinments");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Appoinments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Appoinments");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appoinments");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Branches_Branch_id",
                table: "Appoinments",
                column: "Branch_id",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Employees_emp_id",
                table: "Appoinments",
                column: "emp_id",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Patients_Patient_id",
                table: "Appoinments",
                column: "Patient_id",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_ServicesDoctors_Serv_doctor_id",
                table: "Appoinments",
                column: "Serv_doctor_id",
                principalTable: "ServicesDoctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Branches_Branch_id",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Employees_emp_id",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Patients_Patient_id",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_ServicesDoctors_Serv_doctor_id",
                table: "Appoinments");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_Branch_id",
                table: "Appoinments");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_emp_id",
                table: "Appoinments");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_Patient_id",
                table: "Appoinments");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Appoinments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Appoinments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Appoinments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Branches_BranchId",
                table: "Appoinments",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Employees_EmployeeId",
                table: "Appoinments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Patients_PatientId",
                table: "Appoinments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_ServicesDoctors_Serv_doctor_id",
                table: "Appoinments",
                column: "Serv_doctor_id",
                principalTable: "ServicesDoctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
