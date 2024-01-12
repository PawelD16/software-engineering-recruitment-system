using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projektowaniaOprogramowania.Migrations
{
    public partial class AddStatusPodaniaToPodanieKandydata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "wydzialy",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "miasta",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.AddColumn<string>(
                name: "StatusPodania",
                table: "podania_kandydatow",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "miasta",
                columns: new[] { "Id", "NazwaMiasta" },
                values: new object[] { 1L, "Wrocław" });

            migrationBuilder.UpdateData(
                table: "osoby",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataZarejestrowania",
                value: new DateTime(2024, 1, 10, 9, 45, 31, 639, DateTimeKind.Local).AddTicks(2774));

            migrationBuilder.UpdateData(
                table: "podania_kandydatow",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataZlozeniaPodania", "StatusPodania" },
                values: new object[] { new DateTime(2024, 1, 10, 9, 45, 31, 641, DateTimeKind.Local).AddTicks(7557), "Przyjete do rozpatrzenia" });

            migrationBuilder.InsertData(
                table: "wydzialy",
                columns: new[] { "Id", "MiastoId", "NazwaWydzialu", "NumerWydzialu" },
                values: new object[] { 1L, 1L, "Wydział Informatyki i Telekomunikacji", "W04n" });

            migrationBuilder.InsertData(
                table: "kierunki",
                columns: new[] { "Id", "CzasTrwaniaWSemestrach", "Czesne", "CzesneDlaCudzoziemcow", "Dyscyplina", "FkIdWydzial", "Jezyk", "NazwaKierunku", "ProfilKierunku", "SkrotKierunku", "StopienStudiow", "TrybStudiowania" },
                values: new object[,]
                {
                    { 1L, 7, 0, 1250, "informatyka techniczna i telekomunikacja", 1L, 0, "Informatyka Stosowana", "ogólnoakademicki", "IST", 0, 0 },
                    { 2L, 7, 50, 1300, "informatyka techniczna i telekomunikacja", 1L, 1, "Informatyka Techniczna w j. angielskim", "ogólnoakademicki", "ITA", 1, 0 },
                    { 3L, 7, 0, 1250, "informatyka techniczna i telekomunikacja", 1L, 0, "Informatyczne Systemy Automatyki", "ogólnoakademicki", "ISA", 0, 2 },
                    { 4L, 7, 100, 1350, "informatyka techniczna i telekomunikacja", 1L, 1, "Informatyka Stosowana w j.angielskim", "", "ISTA", 0, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "wydzialy",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "miasta",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DropColumn(
                name: "StatusPodania",
                table: "podania_kandydatow");

            migrationBuilder.InsertData(
                table: "miasta",
                columns: new[] { "Id", "NazwaMiasta" },
                values: new object[] { 1L, "Wrocław" });

            migrationBuilder.UpdateData(
                table: "osoby",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataZarejestrowania",
                value: new DateTime(2024, 1, 6, 18, 13, 32, 488, DateTimeKind.Local).AddTicks(5269));

            migrationBuilder.UpdateData(
                table: "podania_kandydatow",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataZlozeniaPodania",
                value: new DateTime(2024, 1, 6, 18, 13, 32, 490, DateTimeKind.Local).AddTicks(6018));

            migrationBuilder.InsertData(
                table: "wydzialy",
                columns: new[] { "Id", "MiastoId", "NazwaWydzialu", "NumerWydzialu" },
                values: new object[] { 1L, 1L, "Wydział Informatyki i Telekomunikacji", "W04n" });

            migrationBuilder.InsertData(
                table: "kierunki",
                columns: new[] { "Id", "CzasTrwaniaWSemestrach", "Czesne", "CzesneDlaCudzoziemcow", "Dyscyplina", "FkIdWydzial", "Jezyk", "NazwaKierunku", "ProfilKierunku", "SkrotKierunku", "StopienStudiow", "TrybStudiowania" },
                values: new object[,]
                {
                    { 1L, 7, 0, 1250, "informatyka techniczna i telekomunikacja", 1L, 0, "Informatyka Stosowana", "ogólnoakademicki", "IST", 0, 0 },
                    { 2L, 7, 50, 1300, "informatyka techniczna i telekomunikacja", 1L, 1, "Informatyka Techniczna w j. angielskim", "ogólnoakademicki", "ITA", 1, 0 },
                    { 3L, 7, 0, 1250, "informatyka techniczna i telekomunikacja", 1L, 0, "Informatyczne Systemy Automatyki", "ogólnoakademicki", "ISA", 0, 2 },
                    { 4L, 7, 100, 1350, "informatyka techniczna i telekomunikacja", 1L, 1, "Informatyka Stosowana w j.angielskim", "", "ISTA", 0, 1 }
                });
        }
    }
}
