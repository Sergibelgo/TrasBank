using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITrassBank.Migrations
{
    /// <inheritdoc />
    public partial class ChangePayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_OtherInvolvedId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Payments");

            migrationBuilder.AlterColumn<string>(
                name: "OtherInvolvedId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Concept",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ATMS",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ATMS_AppUserId",
                table: "ATMS",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ATMS_AspNetUsers_AppUserId",
                table: "ATMS",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_OtherInvolvedId",
                table: "Transactions",
                column: "OtherInvolvedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATMS_AspNetUsers_AppUserId",
                table: "ATMS");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_OtherInvolvedId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_ATMS_AppUserId",
                table: "ATMS");

            migrationBuilder.DropColumn(
                name: "Concept",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ATMS");

            migrationBuilder.AlterColumn<string>(
                name: "OtherInvolvedId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_OtherInvolvedId",
                table: "Transactions",
                column: "OtherInvolvedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
