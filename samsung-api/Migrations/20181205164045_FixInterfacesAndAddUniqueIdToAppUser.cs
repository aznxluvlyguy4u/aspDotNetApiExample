using Microsoft.EntityFrameworkCore.Migrations;

namespace samsung.api.Migrations
{
    public partial class FixInterfacesAndAddUniqueIdToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}