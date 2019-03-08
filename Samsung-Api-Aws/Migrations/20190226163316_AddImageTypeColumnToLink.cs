using Microsoft.EntityFrameworkCore.Migrations;

namespace SamsungApiAws.Migrations
{
    public partial class AddImageTypeColumnToLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageType",
                table: "Links",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageWebUrl",
                table: "Links",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "ImageWebUrl",
                table: "Links");
        }
    }
}