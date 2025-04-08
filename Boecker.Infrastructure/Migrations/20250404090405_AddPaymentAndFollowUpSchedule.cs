using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boecker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentAndFollowUpSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IncludesFollowUp = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_Contract_Clients_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    TechnicianId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.TechnicianId);
                });

            migrationBuilder.CreateTable(
                name: "FollowUpSchedules",
                columns: table => new
                {
                    FollowUpScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUpSchedules", x => x.FollowUpScheduleId);
                    table.ForeignKey(
                        name: "FK_FollowUpSchedules_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUpSchedules_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId");
                });

            migrationBuilder.CreateTable(
                name: "ServiceSchedules",
                columns: table => new
                {
                    ServiceScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    TechnicianId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    DateScheduled = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsFollowUp = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSchedules", x => x.ServiceScheduleId);
                    table.ForeignKey(
                        name: "FK_ServiceSchedules_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceSchedules_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceSchedules_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "TechnicianId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ContractId",
                table: "Invoices",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CustomerId",
                table: "Contract",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpSchedules_ContractId",
                table: "FollowUpSchedules",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpSchedules_InvoiceId",
                table: "FollowUpSchedules",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSchedules_ContractId",
                table: "ServiceSchedules",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSchedules_ServiceId",
                table: "ServiceSchedules",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSchedules_TechnicianId",
                table: "ServiceSchedules",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Contract_ContractId",
                table: "Invoices",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Contract_ContractId",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "FollowUpSchedules");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ServiceSchedules");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Technicians");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ContractId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Invoices");
        }
    }
}
