using Microsoft.EntityFrameworkCore.Migrations;

namespace SamsungApiAws.Migrations
{
    public partial class AddSeenLinkAndSeenGeneralUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralUserSeenGeneralUser",
                columns: table => new
                {
                    LoggedInGeneralUserId = table.Column<int>(nullable: false),
                    HasSeenGeneralUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralUserSeenGeneralUser", x => new { x.LoggedInGeneralUserId, x.HasSeenGeneralUserId });
                    table.ForeignKey(
                        name: "FK_GeneralUserSeenGeneralUser_GeneralUsers_HasSeenGeneralUserId",
                        column: x => x.HasSeenGeneralUserId,
                        principalTable: "GeneralUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneralUserSeenGeneralUser_GeneralUsers_LoggedInGeneralUserId",
                        column: x => x.LoggedInGeneralUserId,
                        principalTable: "GeneralUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneralUserSeenLink",
                columns: table => new
                {
                    GeneralUserId = table.Column<int>(nullable: false),
                    LinkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralUserSeenLink", x => new { x.GeneralUserId, x.LinkId });
                    table.ForeignKey(
                        name: "FK_GeneralUserSeenLink_GeneralUsers_GeneralUserId",
                        column: x => x.GeneralUserId,
                        principalTable: "GeneralUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralUserSeenLink_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUserSeenGeneralUser_HasSeenGeneralUserId",
                table: "GeneralUserSeenGeneralUser",
                column: "HasSeenGeneralUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUserSeenLink_LinkId",
                table: "GeneralUserSeenLink",
                column: "LinkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralUserSeenGeneralUser");

            migrationBuilder.DropTable(
                name: "GeneralUserSeenLink");
        }
    }
}