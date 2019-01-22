using Microsoft.EntityFrameworkCore.Migrations;

namespace samsung.api.Migrations
{
    public partial class Fixforcities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralUsers_Cities_CitiesId",
                table: "GeneralUsers");

            migrationBuilder.DropIndex(
                name: "IX_GeneralUsers_CitiesId",
                table: "GeneralUsers");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "GeneralUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUsers_CityId",
                table: "GeneralUsers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralUsers_Cities_CityId",
                table: "GeneralUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralUsers_Cities_CityId",
                table: "GeneralUsers");

            migrationBuilder.DropIndex(
                name: "IX_GeneralUsers_CityId",
                table: "GeneralUsers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "GeneralUsers");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUsers_CitiesId",
                table: "GeneralUsers",
                column: "CitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralUsers_Cities_CitiesId",
                table: "GeneralUsers",
                column: "CitiesId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
