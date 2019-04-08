using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamsungApiAws.Migrations
{
    public partial class ChangeAllSeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Algoritmes", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Animaties maken", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Annotatietools", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Artificial intelligence", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 29, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Naakfoto’s", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 30, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Online pesten", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 31, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Podcasting", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 32, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Podcasts", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 33, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Posters maken", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 34, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Presentaties", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 35, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Robots", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 36, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Samen in documenten werken", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 37, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Sextortion", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 39, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Sexting", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 28, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Mediawijsheid", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 40, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Storytelling", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 41, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Student response systeem", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 43, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Video’s gebruiken", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 44, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Virtual Reality", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 45, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Vloggen", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 46, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Webinars", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 47, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Weblectures", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 48, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Wraakporno", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 38, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Serious Games", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 27, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Mediagebruik", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 42, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Veiligheid", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 25, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Livestreamen", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 5, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Apps", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 6, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Augmented Reality", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 7, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Bloggen", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 8, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Connected Toys", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 9, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Content Cureren", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 10, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Cybercrime", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 26, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Mediaopvoeding", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 12, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Discussies", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 13, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Elektronisch portfolio", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 14, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "eSports", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 11, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Cyberpesten", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 16, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Gamen", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 24, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Lesmateriaal", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 15, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Filterbubbels", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 22, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Kunstmatige intelligentie", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 21, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Internet of Things", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 23, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Lerende vragen laten maken", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 19, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Grooming", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 18, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Gamificationtools", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 17, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Gamestreaming", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) },
                    { 20, new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local), "Infographics maken", new DateTime(2019, 4, 8, 12, 54, 26, 855, DateTimeKind.Local) }
                });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "4 - 6", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "6 - 8", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "6 - 9", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "8 - 10", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "TeachingAgeGroups",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 13, new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "20+", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) },
                    { 12, new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "18+", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) },
                    { 11, new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "15 - 20", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) },
                    { 10, new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "16 - 18", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) },
                    { 7, new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "12 - 14", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) },
                    { 8, new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "12 - 15", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) },
                    { 6, new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "10 - 12", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) },
                    { 5, new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "9 - 12", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) },
                    { 9, new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local), "14 - 16", new DateTime(2019, 4, 8, 12, 54, 26, 857, DateTimeKind.Local) }
                });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "Speciaal onderwijs", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "Basisonderwijs", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "VMBO", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "Havo", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "Vwo/Gymnasium", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "MBO", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "WO", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "TeachingLevels",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 13, new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "Volwassenenonderwijs", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) },
                    { 12, new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "Lerarenopleiding", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) },
                    { 11, new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "KSO", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) },
                    { 10, new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "BSO", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) },
                    { 8, new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "ASO", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) },
                    { 9, new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local), "TSO", new DateTime(2019, 4, 8, 12, 54, 26, 854, DateTimeKind.Local) }
                });

            migrationBuilder.UpdateData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 851, DateTimeKind.Local), "Beroepsgerichte vakken", new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local), "Bewegingsonderwijs en sport", new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "TeachingSubjects",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 9, new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local), "Natuur & techniek", new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local) },
                    { 3, new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local), "Klassieke talen", new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local) },
                    { 4, new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local), "Nederlands", new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local) },
                    { 5, new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local), "Rekenen & wiskunde", new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local) },
                    { 6, new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local), "Moderne vreemde talen", new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local) },
                    { 7, new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local), "Kunst & cultuur", new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local) },
                    { 8, new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local), "Mens & maatschappij", new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local) },
                    { 10, new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local), "Anders", new DateTime(2019, 4, 8, 12, 54, 26, 853, DateTimeKind.Local) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), "Interest 1", new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), "Interest 2", new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), "Interest 3", new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local), "Interest 4", new DateTime(2019, 3, 21, 17, 3, 43, 170, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), "10 - 15", new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), "15 - 20", new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), "25 - 30", new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingAgeGroups",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local), "30+", new DateTime(2019, 3, 21, 17, 3, 43, 171, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), "VMBO", new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), "MAVO", new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), "HAVO", new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), "VWO", new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), "HBO", new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), "WO", new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingLevels",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local), "Anders", new DateTime(2019, 3, 21, 17, 3, 43, 168, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 165, DateTimeKind.Local), "Subject 1", new DateTime(2019, 3, 21, 17, 3, 43, 167, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TeachingSubjects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 3, 21, 17, 3, 43, 167, DateTimeKind.Local), "Subject 2", new DateTime(2019, 3, 21, 17, 3, 43, 167, DateTimeKind.Local) });
        }
    }
}
