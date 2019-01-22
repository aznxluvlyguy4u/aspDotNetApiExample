using Microsoft.EntityFrameworkCore.Migrations;

namespace samsung.api.Migrations
{
    public partial class TestingMIgration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CitiesId",
                table: "GeneralUsers",
                nullable: false,
                defaultValue: 1);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralUsers_Cities_CitiesId",
                table: "GeneralUsers");

            migrationBuilder.DropIndex(
                name: "IX_GeneralUsers_CitiesId",
                table: "GeneralUsers");

            migrationBuilder.DropColumn(
                name: "CitiesId",
                table: "GeneralUsers");
        }
    }
}
