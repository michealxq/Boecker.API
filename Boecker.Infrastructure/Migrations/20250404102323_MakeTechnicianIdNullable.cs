using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boecker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeTechnicianIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSchedules_Technicians_TechnicianId",
                table: "ServiceSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "TechnicianId",
                table: "ServiceSchedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSchedules_Technicians_TechnicianId",
                table: "ServiceSchedules",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "TechnicianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSchedules_Technicians_TechnicianId",
                table: "ServiceSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "TechnicianId",
                table: "ServiceSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSchedules_Technicians_TechnicianId",
                table: "ServiceSchedules",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "TechnicianId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
