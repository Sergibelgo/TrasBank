using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITrassBank.Migrations
{
    /// <inheritdoc />
    public partial class AddLoanName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoanName",
                table: "Proyecto_Loans",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanName",
                table: "Proyecto_Loans");
        }
    }
}
