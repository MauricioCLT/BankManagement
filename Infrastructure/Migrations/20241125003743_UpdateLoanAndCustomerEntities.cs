using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoanAndCustomerEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovedLoans_Customers_CustomerId",
                table: "ApprovedLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovedLoans_LoanRequests_LoanRequestId",
                table: "ApprovedLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovedLoans_TermInterestRates_TermInterestRateId",
                table: "ApprovedLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentPayments_Installments_InstallmentId",
                table: "InstallmentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Installments_ApprovedLoans_ApprovedLoanId",
                table: "Installments");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanRequests_Customers_CustomerId",
                table: "LoanRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanRequests_TermInterestRates_TermInterestRateId",
                table: "LoanRequests");

            migrationBuilder.DropIndex(
                name: "IX_InstallmentPayments_InstallmentId",
                table: "InstallmentPayments");

            migrationBuilder.DropIndex(
                name: "IX_ApprovedLoans_TermInterestRateId",
                table: "ApprovedLoans");

            migrationBuilder.RenameColumn(
                name: "TermInterestRateId",
                table: "ApprovedLoans",
                newName: "Months");

            migrationBuilder.RenameColumn(
                name: "RequestAmount",
                table: "ApprovedLoans",
                newName: "RequestedAmount");

            migrationBuilder.AddColumn<int>(
                name: "Months",
                table: "LoanRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "LoanRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "ApprovedLoans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ApprovedLoans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentPayments_InstallmentId",
                table: "InstallmentPayments",
                column: "InstallmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Cid",
                table: "Customers",
                column: "Cid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovedLoans_Customers_CustomerId",
                table: "ApprovedLoans",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovedLoans_LoanRequests_LoanRequestId",
                table: "ApprovedLoans",
                column: "LoanRequestId",
                principalTable: "LoanRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentPayments_Installments_InstallmentId",
                table: "InstallmentPayments",
                column: "InstallmentId",
                principalTable: "Installments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_ApprovedLoans_ApprovedLoanId",
                table: "Installments",
                column: "ApprovedLoanId",
                principalTable: "ApprovedLoans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanRequests_Customers_CustomerId",
                table: "LoanRequests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanRequests_TermInterestRates_TermInterestRateId",
                table: "LoanRequests",
                column: "TermInterestRateId",
                principalTable: "TermInterestRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovedLoans_Customers_CustomerId",
                table: "ApprovedLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovedLoans_LoanRequests_LoanRequestId",
                table: "ApprovedLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentPayments_Installments_InstallmentId",
                table: "InstallmentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Installments_ApprovedLoans_ApprovedLoanId",
                table: "Installments");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanRequests_Customers_CustomerId",
                table: "LoanRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanRequests_TermInterestRates_TermInterestRateId",
                table: "LoanRequests");

            migrationBuilder.DropIndex(
                name: "IX_InstallmentPayments_InstallmentId",
                table: "InstallmentPayments");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Cid",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Months",
                table: "LoanRequests");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "LoanRequests");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "ApprovedLoans");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ApprovedLoans");

            migrationBuilder.RenameColumn(
                name: "RequestedAmount",
                table: "ApprovedLoans",
                newName: "RequestAmount");

            migrationBuilder.RenameColumn(
                name: "Months",
                table: "ApprovedLoans",
                newName: "TermInterestRateId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentPayments_InstallmentId",
                table: "InstallmentPayments",
                column: "InstallmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedLoans_TermInterestRateId",
                table: "ApprovedLoans",
                column: "TermInterestRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovedLoans_Customers_CustomerId",
                table: "ApprovedLoans",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovedLoans_LoanRequests_LoanRequestId",
                table: "ApprovedLoans",
                column: "LoanRequestId",
                principalTable: "LoanRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovedLoans_TermInterestRates_TermInterestRateId",
                table: "ApprovedLoans",
                column: "TermInterestRateId",
                principalTable: "TermInterestRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentPayments_Installments_InstallmentId",
                table: "InstallmentPayments",
                column: "InstallmentId",
                principalTable: "Installments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_ApprovedLoans_ApprovedLoanId",
                table: "Installments",
                column: "ApprovedLoanId",
                principalTable: "ApprovedLoans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanRequests_Customers_CustomerId",
                table: "LoanRequests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanRequests_TermInterestRates_TermInterestRateId",
                table: "LoanRequests",
                column: "TermInterestRateId",
                principalTable: "TermInterestRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
