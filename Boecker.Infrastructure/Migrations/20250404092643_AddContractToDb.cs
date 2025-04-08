using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boecker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContractToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Clients_CustomerId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpSchedules_Contract_ContractId",
                table: "FollowUpSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Contract_ContractId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSchedules_Contract_ContractId",
                table: "ServiceSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                table: "Contract");

            migrationBuilder.RenameTable(
                name: "Contract",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_CustomerId",
                table: "Contracts",
                newName: "IX_Contracts_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Clients_CustomerId",
                table: "Contracts",
                column: "CustomerId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpSchedules_Contracts_ContractId",
                table: "FollowUpSchedules",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Contracts_ContractId",
                table: "Invoices",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSchedules_Contracts_ContractId",
                table: "ServiceSchedules",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Clients_CustomerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUpSchedules_Contracts_ContractId",
                table: "FollowUpSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Contracts_ContractId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSchedules_Contracts_ContractId",
                table: "ServiceSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contract");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CustomerId",
                table: "Contract",
                newName: "IX_Contract_CustomerId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Contract",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Clients_CustomerId",
                table: "Contract",
                column: "CustomerId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUpSchedules_Contract_ContractId",
                table: "FollowUpSchedules",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Contract_ContractId",
                table: "Invoices",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSchedules_Contract_ContractId",
                table: "ServiceSchedules",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
