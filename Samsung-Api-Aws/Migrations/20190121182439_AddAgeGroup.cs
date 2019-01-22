using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace samsung.api.Migrations
{
    public partial class AddAgeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgeGroupId",
                table: "GeneralUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgeGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeGroups", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AgeGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "10 - 15" },
                    { 2, "15 - 20" },
                    { 3, "25 - 30" },
                    { 4, "30+" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUsers_AgeGroupId",
                table: "GeneralUsers",
                column: "AgeGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralUsers_AgeGroups_AgeGroupId",
                table: "GeneralUsers",
                column: "AgeGroupId",
                principalTable: "AgeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralUsers_AgeGroups_AgeGroupId",
                table: "GeneralUsers");

            migrationBuilder.DropTable(
                name: "AgeGroups");

            migrationBuilder.DropIndex(
                name: "IX_GeneralUsers_AgeGroupId",
                table: "GeneralUsers");

            migrationBuilder.DropColumn(
                name: "AgeGroupId",
                table: "GeneralUsers");
        }
    }
}
