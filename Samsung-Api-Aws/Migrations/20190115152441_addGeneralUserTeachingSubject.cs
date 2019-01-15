using Microsoft.EntityFrameworkCore.Migrations;

namespace samsung.api.Migrations
{
    public partial class addGeneralUserTeachingSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralUserTeachingSubject",
                columns: table => new
                {
                    GeneralUserId = table.Column<int>(nullable: false),
                    TeachingSubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralUserTeachingSubject", x => new { x.GeneralUserId, x.TeachingSubjectId });
                    table.ForeignKey(
                        name: "FK_GeneralUserTeachingSubject_GeneralUsers_GeneralUserId",
                        column: x => x.GeneralUserId,
                        principalTable: "GeneralUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralUserTeachingSubject_TeachingSubjects_TeachingSubjectId",
                        column: x => x.TeachingSubjectId,
                        principalTable: "TeachingSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUserTeachingSubject_TeachingSubjectId",
                table: "GeneralUserTeachingSubject",
                column: "TeachingSubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralUserTeachingSubject");
        }
    }
}
