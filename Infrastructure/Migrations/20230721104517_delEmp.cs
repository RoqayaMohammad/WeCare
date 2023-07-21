using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class delEmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Employees_emp_id",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_BrachEmps_Employees_EmployeeId",
                table: "BrachEmps");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_JobTitles_jobTitleId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_jobTitleId",
                table: "Employee",
                newName: "IX_Employee_jobTitleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Employee_emp_id",
                table: "Appoinments",
                column: "emp_id",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrachEmps_Employee_EmployeeId",
                table: "BrachEmps",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_JobTitles_jobTitleId",
                table: "Employee",
                column: "jobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Employee_emp_id",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_BrachEmps_Employee_EmployeeId",
                table: "BrachEmps");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_JobTitles_jobTitleId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_jobTitleId",
                table: "Employees",
                newName: "IX_Employees_jobTitleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Employees_emp_id",
                table: "Appoinments",
                column: "emp_id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrachEmps_Employees_EmployeeId",
                table: "BrachEmps",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_JobTitles_jobTitleId",
                table: "Employees",
                column: "jobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id");
        }
    }
}
