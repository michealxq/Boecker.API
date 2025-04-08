using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boecker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceRecurrenceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastGeneratedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecurrencePeriod",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "LastGeneratedDate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "RecurrencePeriod",
                table: "Invoices");
        }
    }
}
