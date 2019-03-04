using Microsoft.EntityFrameworkCore.Migrations;

namespace SamsungApiAws.Migrations
{
    public partial class MoveTechLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TechLevel",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "TechLevel",
                table: "GeneralUsers",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechLevel",
                table: "GeneralUsers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechLevel",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 1);
        }
    }
}
