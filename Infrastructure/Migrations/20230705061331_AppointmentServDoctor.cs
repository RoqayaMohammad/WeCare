using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentServDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_ServicesDoctors_Serv_doctor_id",
                table: "Appoinments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_ServicesDoctors_Serv_doctor_id",
                table: "Appoinments",
                column: "Serv_doctor_id",
                principalTable: "ServicesDoctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_ServicesDoctors_Serv_doctor_id",
                table: "Appoinments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_ServicesDoctors_Serv_doctor_id",
                table: "Appoinments",
                column: "Serv_doctor_id",
                principalTable: "ServicesDoctors",
                principalColumn: "Id");
        }
    }
}
