using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITrassBank.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTransactionsAddATM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_Sender_AccountId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_Sender_AccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Sender_AccountId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "OtherInvolvedId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ATMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Operative = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATMS", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OtherInvolvedId",
                table: "Transactions",
                column: "OtherInvolvedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_OtherInvolvedId",
                table: "Transactions",
                column: "OtherInvolvedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_OtherInvolvedId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "ATMS");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_OtherInvolvedId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OtherInvolvedId",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "Sender_AccountId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(30,2)", precision: 30, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Sender_AccountId",
                table: "Transactions",
                column: "Sender_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_Sender_AccountId",
                table: "Transactions",
                column: "Sender_AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
