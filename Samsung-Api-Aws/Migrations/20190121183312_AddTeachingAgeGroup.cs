using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace samsung.api.Migrations
{
    public partial class AddTeachingAgeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeachingAgeGroupId",
                table: "GeneralUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeachingAgeGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingAgeGroups", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TeachingAgeGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "10 - 15" },
                    { 2, "15 - 20" },
                    { 3, "25 - 30" },
                    { 4, "30+" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUsers_TeachingAgeGroupId",
                table: "GeneralUsers",
                column: "TeachingAgeGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralUsers_TeachingAgeGroups_TeachingAgeGroupId",
                table: "GeneralUsers",
                column: "TeachingAgeGroupId",
                principalTable: "TeachingAgeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralUsers_TeachingAgeGroups_TeachingAgeGroupId",
                table: "GeneralUsers");

            migrationBuilder.DropTable(
                name: "TeachingAgeGroups");

            migrationBuilder.DropIndex(
                name: "IX_GeneralUsers_TeachingAgeGroupId",
                table: "GeneralUsers");

            migrationBuilder.DropColumn(
                name: "TeachingAgeGroupId",
                table: "GeneralUsers");
        }
    }
}
