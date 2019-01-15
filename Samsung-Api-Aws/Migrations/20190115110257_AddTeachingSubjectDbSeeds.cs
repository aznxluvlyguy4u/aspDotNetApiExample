using Microsoft.EntityFrameworkCore.Migrations;

namespace samsung.api.Migrations
{
    public partial class AddTeachingSubjectDbSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TeachingSubjects",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Subject 1" });

            migrationBuilder.InsertData(
                table: "TeachingSubjects",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Subject 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
