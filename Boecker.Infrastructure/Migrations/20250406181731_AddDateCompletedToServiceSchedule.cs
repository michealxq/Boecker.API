using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boecker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateCompletedToServiceSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "ServiceSchedules",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "ServiceSchedules");
        }
    }
}
