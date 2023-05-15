using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITrassBank.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIntToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Resources",
                type: "decimal(30,2)",
                precision: 30,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Loans",
                type: "decimal(30,2)",
                precision: 30,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Resources",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,2)",
                oldPrecision: 30,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "InterestRate",
                table: "Loans",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,2)",
                oldPrecision: 30,
                oldScale: 2);
        }
    }
}
