using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandmothersDishes.Data.Migrations
{
    public partial class CardsCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UsersDiscountCard",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    DiscountCardId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDiscountCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersDiscountCard_DiscountCards_DiscountCardId",
                        column: x => x.DiscountCardId,
                        principalTable: "DiscountCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersDiscountCard_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersDiscountCard_DiscountCardId",
                table: "UsersDiscountCard",
                column: "DiscountCardId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDiscountCard_UserId",
                table: "UsersDiscountCard",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersDiscountCard");

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
    }
}
