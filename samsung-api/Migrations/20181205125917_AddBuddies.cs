using Microsoft.EntityFrameworkCore.Migrations;

namespace samsung.api.Migrations
{
    public partial class AddBuddies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buddies",
                columns: table => new
                {
                    RequestingGeneralUserId = table.Column<int>(nullable: false),
                    ReceivingGeneralUserId = table.Column<int>(nullable: false),
                    RequestState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buddies", x => new { x.ReceivingGeneralUserId, x.RequestingGeneralUserId });
                    table.ForeignKey(
                        name: "FK_Buddies_GeneralUsers_ReceivingGeneralUserId",
                        column: x => x.ReceivingGeneralUserId,
                        principalTable: "GeneralUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buddies_GeneralUsers_RequestingGeneralUserId",
                        column: x => x.RequestingGeneralUserId,
                        principalTable: "GeneralUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buddies_RequestingGeneralUserId",
                table: "Buddies",
                column: "RequestingGeneralUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buddies");
        }
    }
}