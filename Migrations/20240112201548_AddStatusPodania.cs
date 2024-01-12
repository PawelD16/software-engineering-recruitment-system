using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projektowaniaOprogramowania.Migrations
{
    public partial class AddStatusPodania : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusPodania",
                table: "podania_kandydatow",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "osoby",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataZarejestrowania",
                value: new DateTime(2024, 1, 12, 21, 15, 48, 154, DateTimeKind.Local).AddTicks(2239));

            migrationBuilder.UpdateData(
                table: "podania_kandydatow",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataZlozeniaPodania",
                value: new DateTime(2024, 1, 12, 21, 15, 48, 158, DateTimeKind.Local).AddTicks(8518));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusPodania",
                table: "podania_kandydatow");

            migrationBuilder.UpdateData(
                table: "osoby",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataZarejestrowania",
                value: new DateTime(2024, 1, 11, 20, 37, 30, 279, DateTimeKind.Local).AddTicks(9792));

            migrationBuilder.UpdateData(
                table: "podania_kandydatow",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataZlozeniaPodania",
                value: new DateTime(2024, 1, 11, 20, 37, 30, 283, DateTimeKind.Local).AddTicks(9814));
        }
    }
}
