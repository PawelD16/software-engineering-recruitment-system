using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projektowaniaOprogramowania.Migrations
{
    public partial class AddOpisKierunku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "kierunki",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Opis",
                value: "Kształcimy informatyków, którzy – oprócz wiedzy podstawowej – specjalizują się w zakresie:\nużytkowania, projektowania i programowania cyfrowych systemów automatyki, sieci i telematyki przemysłowej, systemów optymalizacji i sterowania z wykorzystaniem sterowników mikroprocesorowych, sieci neuronowych, w tym sieci głębokich\nmetod przetwarzania i rozpoznawania obrazów. ");

            migrationBuilder.UpdateData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Opis",
                value: "Kształcimy informatyków, którzy – oprócz wiedzy podstawowej – specjalizują się w zakresie:\nużytkowania, projektowania i programowania cyfrowych systemów automatyki, sieci i telematyki przemysłowej, systemów optymalizacji i sterowania z wykorzystaniem sterowników mikroprocesorowych, sieci neuronowych, w tym sieci głębokich\nmetod przetwarzania i rozpoznawania obrazów. ");

            migrationBuilder.UpdateData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Opis",
                value: "Kształcimy informatyków, którzy – oprócz wiedzy podstawowej – specjalizują się w zakresie:\nużytkowania, projektowania i programowania cyfrowych systemów automatyki, sieci i telematyki przemysłowej, systemów optymalizacji i sterowania z wykorzystaniem sterowników mikroprocesorowych, sieci neuronowych, w tym sieci głębokich\nmetod przetwarzania i rozpoznawania obrazów. ");

            migrationBuilder.UpdateData(
                table: "kierunki",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Opis",
                value: "Studia umożliwiają doskonalenie umiejętności i zdobywanie wiedzy z szeroko pojmowanej informatyki i jej różnorakiego zastosowania m.in. w rozwiązywaniu problemów biznesowych, technicznych czy w obszarze gier komputerowych. Informatyka stosowana jest uzupełniana wiedzą z fizyki i matematyki, podstaw przedsiębiorczości oraz społecznych i zawodowych problemów informatyki. Dużą rolę przywiązuje się też do umiejętności miękkich, jak umiejętność prezentacji oraz umiejętność pracy w zespole.");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Opis",
                table: "kierunki");

            migrationBuilder.UpdateData(
                table: "osoby",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataZarejestrowania",
                value: new DateTime(2024, 1, 10, 23, 41, 27, 459, DateTimeKind.Local).AddTicks(346));

            migrationBuilder.UpdateData(
                table: "podania_kandydatow",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataZlozeniaPodania",
                value: new DateTime(2024, 1, 10, 23, 41, 27, 463, DateTimeKind.Local).AddTicks(1445));
        }
    }
}
