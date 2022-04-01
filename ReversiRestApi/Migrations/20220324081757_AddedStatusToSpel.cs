using Microsoft.EntityFrameworkCore.Migrations;

namespace ReversiRestApi.Migrations
{
    public partial class AddedStatusToSpel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Spellen",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Spellen");
        }
    }
}
