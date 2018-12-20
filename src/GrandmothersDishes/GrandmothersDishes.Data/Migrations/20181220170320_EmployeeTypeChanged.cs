using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandmothersDishes.Data.Migrations
{
    public partial class EmployeeTypeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmloyeeType",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeType",
                table: "Employees",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeType",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmloyeeType",
                table: "Employees",
                nullable: false,
                defaultValue: 0);
        }
    }
}
