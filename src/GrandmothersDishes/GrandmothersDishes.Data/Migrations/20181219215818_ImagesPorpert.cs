using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandmothersDishes.Data.Migrations
{
    public partial class ImagesPorpert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Employees");
        }
    }
}
