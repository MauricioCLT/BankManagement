using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedInstallmentPaymentInstallmentRalation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentPayments_Installments_InstallmentId",
                table: "InstallmentPayments");

            migrationBuilder.DropIndex(
                name: "IX_InstallmentPayments_InstallmentId",
                table: "InstallmentPayments");

            migrationBuilder.DropColumn(
                name: "InstallmentId",
                table: "InstallmentPayments");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "InstallmentPaymentId",
                table: "Installments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Installments_InstallmentPaymentId",
                table: "Installments",
                column: "InstallmentPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_InstallmentPayments_InstallmentPaymentId",
                table: "Installments",
                column: "InstallmentPaymentId",
                principalTable: "InstallmentPayments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_InstallmentPayments_InstallmentPaymentId",
                table: "Installments");

            migrationBuilder.DropIndex(
                name: "IX_Installments_InstallmentPaymentId",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "InstallmentPaymentId",
                table: "Installments");

            migrationBuilder.AddColumn<int>(
                name: "InstallmentId",
                table: "InstallmentPayments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentPayments_InstallmentId",
                table: "InstallmentPayments",
                column: "InstallmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentPayments_Installments_InstallmentId",
                table: "InstallmentPayments",
                column: "InstallmentId",
                principalTable: "Installments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
