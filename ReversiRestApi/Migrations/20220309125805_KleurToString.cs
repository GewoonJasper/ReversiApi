using Microsoft.EntityFrameworkCore.Migrations;

namespace ReversiRestApi.Migrations
{
    public partial class KleurToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bord",
                table: "Spellen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Winnaar",
                table: "Spellen",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bord",
                table: "Spellen");

            migrationBuilder.DropColumn(
                name: "Winnaar",
                table: "Spellen");
        }
    }
}
