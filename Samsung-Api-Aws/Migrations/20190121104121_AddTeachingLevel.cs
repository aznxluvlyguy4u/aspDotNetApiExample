using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace samsung.api.Migrations
{
    public partial class AddTeachingLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeachingLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralUserTeachingLevel",
                columns: table => new
                {
                    GeneralUserId = table.Column<int>(nullable: false),
                    TeachingLevelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralUserTeachingLevel", x => new { x.GeneralUserId, x.TeachingLevelId });
                    table.ForeignKey(
                        name: "FK_GeneralUserTeachingLevel_GeneralUsers_GeneralUserId",
                        column: x => x.GeneralUserId,
                        principalTable: "GeneralUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralUserTeachingLevel_TeachingLevels_TeachingLevelId",
                        column: x => x.TeachingLevelId,
                        principalTable: "TeachingLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TeachingLevels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "VMBO" },
                    { 2, "MAVO" },
                    { 3, "HAVO" },
                    { 4, "VWO" },
                    { 5, "HBO" },
                    { 6, "WO" },
                    { 7, "Anders" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUserTeachingLevel_TeachingLevelId",
                table: "GeneralUserTeachingLevel",
                column: "TeachingLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralUserTeachingLevel");

            migrationBuilder.DropTable(
                name: "TeachingLevels");
        }
    }
}
