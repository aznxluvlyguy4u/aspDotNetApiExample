using Microsoft.EntityFrameworkCore.Migrations;

namespace SamsungApiAws.Migrations
{
    public partial class AddLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Links");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Links",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Links",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Links",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GeneralUserId",
                table: "Links",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GeneralUserLink",
                columns: table => new
                {
                    GeneralUserId = table.Column<int>(nullable: false),
                    LinkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralUserLink", x => new { x.GeneralUserId, x.LinkId });
                    table.ForeignKey(
                        name: "FK_GeneralUserLink_GeneralUsers_GeneralUserId",
                        column: x => x.GeneralUserId,
                        principalTable: "GeneralUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralUserLink_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_GeneralUserId",
                table: "Links",
                column: "GeneralUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUserLink_LinkId",
                table: "GeneralUserLink",
                column: "LinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_GeneralUsers_GeneralUserId",
                table: "Links",
                column: "GeneralUserId",
                principalTable: "GeneralUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_GeneralUsers_GeneralUserId",
                table: "Links");

            migrationBuilder.DropTable(
                name: "GeneralUserLink");

            migrationBuilder.DropIndex(
                name: "IX_Links_GeneralUserId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "GeneralUserId",
                table: "Links");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Links",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Links",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Links",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Links",
                nullable: true);
        }
    }
}
