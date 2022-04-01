using Microsoft.EntityFrameworkCore.Migrations;

namespace ReversiRestApi.Migrations
{
    public partial class AddedBeurtToSpel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Beurt",
                table: "Spellen");
        }
    }
}
