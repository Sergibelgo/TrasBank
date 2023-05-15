using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITrassBank.Migrations
{
    /// <inheritdoc />
    public partial class ChangeScoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FIN",
                table: "Scoring",
                newName: "Spens");

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Scoring",
                type: "decimal(30,2)",
                precision: 30,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Scoring");

            migrationBuilder.RenameColumn(
                name: "Spens",
                table: "Scoring",
                newName: "FIN");
        }
    }
}
