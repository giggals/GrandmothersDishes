using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandmothersDishes.Data.Migrations
{
    public partial class CardImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "DiscountCards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "DiscountCards");
        }
    }
}
