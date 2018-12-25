using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandmothersDishes.Data.Migrations
{
    public partial class CardDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DiscountCards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DiscountCards");
        }
    }
}
