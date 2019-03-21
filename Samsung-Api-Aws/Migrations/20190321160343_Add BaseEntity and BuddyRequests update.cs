using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamsungApiAws.Migrations
{
    public partial class AddBaseEntityandBuddyRequestsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TeachingSubjects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TeachingSubjects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TeachingLevels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TeachingLevels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TeachingAgeGroups",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TeachingAgeGroups",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Links",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Links",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Links",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Links",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "LinkInterest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "LinkInterest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Interests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Interests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GeneralUserTeachingSubject",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "GeneralUserTeachingSubject",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GeneralUserTeachingLevel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "GeneralUserTeachingLevel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GeneralUserTeachingAgeGroup",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "GeneralUserTeachingAgeGroup",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GeneralUserSeenLink",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "GeneralUserSeenLink",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GeneralUserSeenGeneralUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "GeneralUserSeenGeneralUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GeneralUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "GeneralUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GeneralUserInterest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "GeneralUserInterest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FavoriteLinks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FavoriteLinks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Cities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BuddyRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BuddyRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 165, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 167, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 167, DateTimeKind.Local), new DateTime(2019, 3, 21, 17, 3, 43, 167, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TeachingSubjects");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TeachingSubjects");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TeachingLevels");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TeachingLevels");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TeachingAgeGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TeachingAgeGroups");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "LinkInterest");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "LinkInterest");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Interests");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Interests");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GeneralUserTeachingSubject");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "GeneralUserTeachingSubject");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GeneralUserTeachingLevel");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "GeneralUserTeachingLevel");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GeneralUserTeachingAgeGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "GeneralUserTeachingAgeGroup");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GeneralUserSeenLink");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "GeneralUserSeenLink");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GeneralUserSeenGeneralUser");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "GeneralUserSeenGeneralUser");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GeneralUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "GeneralUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GeneralUserInterest");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "GeneralUserInterest");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FavoriteLinks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FavoriteLinks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BuddyRequests");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BuddyRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Links",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Links",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
