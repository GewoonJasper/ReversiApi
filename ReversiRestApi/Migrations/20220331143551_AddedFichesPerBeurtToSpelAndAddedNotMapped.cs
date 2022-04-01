using Microsoft.EntityFrameworkCore.Migrations;

namespace ReversiRestApi.Migrations
{
    public partial class AddedFichesPerBeurtToSpelAndAddedNotMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beurt",
                table: "Spellen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Beurt",
                table: "Spellen",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
