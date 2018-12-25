using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandmothersDishes.Data.Migrations
{
    public partial class DiscountCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCards_AspNetUsers_UserId",
                table: "DiscountCards");

            migrationBuilder.DropIndex(
                name: "IX_DiscountCards_UserId",
                table: "DiscountCards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DiscountCards");

            migrationBuilder.AddColumn<string>(
                name: "DiscountCardId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DiscountCardId",
                table: "AspNetUsers",
                column: "DiscountCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DiscountCards_DiscountCardId",
                table: "AspNetUsers",
                column: "DiscountCardId",
                principalTable: "DiscountCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DiscountCards_DiscountCardId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DiscountCardId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DiscountCardId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DiscountCards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCards_UserId",
                table: "DiscountCards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCards_AspNetUsers_UserId",
                table: "DiscountCards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
