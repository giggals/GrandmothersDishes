using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandmothersDishes.Data.Migrations
{
    public partial class AddedDescrition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Drinks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Dishes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Dishes");
        }
    }
}
