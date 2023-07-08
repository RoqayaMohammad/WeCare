using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class twent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_ServicesDoctors_Serv_doctor_id",
                table: "Appoinments");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_Serv_doctor_id",
                table: "Appoinments");

            migrationBuilder.AddColumn<int>(
                name: "ServiceDoctorId",
                table: "Appoinments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_ServiceDoctorId",
                table: "Appoinments",
                column: "ServiceDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_ServicesDoctors_ServiceDoctorId",
                table: "Appoinments",
                column: "ServiceDoctorId",
                principalTable: "ServicesDoctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_ServicesDoctors_ServiceDoctorId",
                table: "Appoinments");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_ServiceDoctorId",
                table: "Appoinments");

            migrationBuilder.DropColumn(
                name: "ServiceDoctorId",
                table: "Appoinments");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_Serv_doctor_id",
                table: "Appoinments",
                column: "Serv_doctor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_ServicesDoctors_Serv_doctor_id",
                table: "Appoinments",
                column: "Serv_doctor_id",
                principalTable: "ServicesDoctors",
                principalColumn: "Id");
        }
    }
}
