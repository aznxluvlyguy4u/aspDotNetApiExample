using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace samsung.api.Migrations
{
    public partial class AddAdditionalPropertiesToProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Profiles",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "ImagesId",
                table: "Profiles",
                newName: "TechLevel");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Profiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FacebookId",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LinkedInId",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Profiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Profiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Profiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Profiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ImageId",
                table: "Profiles",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Images_ImageId",
                table: "Profiles",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Images_ImageId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_ImageId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "FacebookId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "LinkedInId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Profiles",
                newName: "Firstname");

            migrationBuilder.RenameColumn(
                name: "TechLevel",
                table: "Profiles",
                newName: "ImagesId");
        }
    }
}
