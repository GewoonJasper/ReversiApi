using Microsoft.EntityFrameworkCore.Migrations;

namespace ReversiRestApi.Migrations
{
    public partial class RemovedNotMappedFromBeurtAndFichesStrings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AantalFichesWitPerBeurt",
                table: "Spellen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AantalFichesZwartPerBeurt",
                table: "Spellen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Beurt",
                table: "Spellen",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AantalFichesWitPerBeurt",
                table: "Spellen");

            migrationBuilder.DropColumn(
                name: "AantalFichesZwartPerBeurt",
                table: "Spellen");

            migrationBuilder.DropColumn(
                name: "Beurt",
                table: "Spellen");
        }
    }
}
