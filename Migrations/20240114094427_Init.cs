using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace projektowaniaOprogramowania.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kategorie_dorobku",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NazwaKategoriiDorobku = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategorie_dorobku", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kategorie_osiagniecia",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NazwaKategorii = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategorie_osiagniecia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "miasta",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NazwaMiasta = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_miasta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "osoby",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Haslo = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Pesel = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    NumerPaszportu = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Imie = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Nazwisko = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DataZarejestrowania = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CzyEmailPotwierdzony = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_osoby", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "przedmioty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NazwaPrzedmiotu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_przedmioty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "przeliczniki_kierunkowe",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaksymalnaWartoscPrzelicznika = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_przeliczniki_kierunkowe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rekrutacje",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataOtwarciaRekrutacji = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataZamknieciaRekrutacji = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataZamknieciaPrzyjec = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StopienStudiow = table.Column<int>(type: "integer", nullable: false),
                    StatusRekrutacji = table.Column<int>(type: "integer", nullable: false),
                    SemestrRekrutacji = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rekrutacje", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wydzialy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumerWydzialu = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    NazwaWydzialu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MiastoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wydzialy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wydzialy_miasta_MiastoId",
                        column: x => x.MiastoId,
                        principalTable: "miasta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kandydaci",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumerKandydata = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kandydaci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kandydaci_osoby_Id",
                        column: x => x.Id,
                        principalTable: "osoby",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pracownicy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumerPracownika = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    DataZatrudnienia = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TypPracownika = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pracownicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pracownicy_osoby_Id",
                        column: x => x.Id,
                        principalTable: "osoby",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "przeliczniki_dorobku",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrzyznawanePunkty = table.Column<int>(type: "integer", nullable: false),
                    FkIdKategoriaDorobku = table.Column<long>(type: "bigint", nullable: false),
                    FkIdPrzelicznikKierunkowy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_przeliczniki_dorobku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_przeliczniki_dorobku_kategorie_dorobku_FkIdKategoriaDorobku",
                        column: x => x.FkIdKategoriaDorobku,
                        principalTable: "kategorie_dorobku",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_przeliczniki_dorobku_przeliczniki_kierunkowe_FkIdPrzeliczni~",
                        column: x => x.FkIdPrzelicznikKierunkowy,
                        principalTable: "przeliczniki_kierunkowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "przeliczniki_osiagniec",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrzyznawanePunkty = table.Column<int>(type: "integer", nullable: false),
                    FkIdPrzelicznikKierunkowy = table.Column<long>(type: "bigint", nullable: false),
                    FkIdKategoriaOsiagniecia = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_przeliczniki_osiagniec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_przeliczniki_osiagniec_kategorie_osiagniecia_FkIdKategoriaO~",
                        column: x => x.FkIdKategoriaOsiagniecia,
                        principalTable: "kategorie_osiagniecia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_przeliczniki_osiagniec_przeliczniki_kierunkowe_FkIdPrzelicz~",
                        column: x => x.FkIdPrzelicznikKierunkowy,
                        principalTable: "przeliczniki_kierunkowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "przeliczniki_przedmiotu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Wspolczynnik = table.Column<float>(type: "real", nullable: false),
                    FkIdPrzedmiot = table.Column<long>(type: "bigint", nullable: false),
                    FkIdPrzelicznikKierunkowy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_przeliczniki_przedmiotu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_przeliczniki_przedmiotu_przedmioty_FkIdPrzedmiot",
                        column: x => x.FkIdPrzedmiot,
                        principalTable: "przedmioty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_przeliczniki_przedmiotu_przeliczniki_kierunkowe_FkIdPrzelic~",
                        column: x => x.FkIdPrzelicznikKierunkowy,
                        principalTable: "przeliczniki_kierunkowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kierunki",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NazwaKierunku = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SkrotKierunku = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ProfilKierunku = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Czesne = table.Column<int>(type: "integer", nullable: false),
                    CzesneDlaCudzoziemcow = table.Column<int>(type: "integer", nullable: false),
                    CzasTrwaniaWSemestrach = table.Column<int>(type: "integer", nullable: false),
                    Dyscyplina = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Opis = table.Column<string>(type: "text", nullable: false),
                    Jezyk = table.Column<int>(type: "integer", maxLength: 20, nullable: false),
                    StopienStudiow = table.Column<int>(type: "integer", maxLength: 20, nullable: false),
                    TrybStudiowania = table.Column<int>(type: "integer", nullable: false),
                    FkIdWydzial = table.Column<long>(type: "bigint", nullable: false),
                    FkIdPrzelicznik = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kierunki", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kierunki_przeliczniki_kierunkowe_FkIdPrzelicznik",
                        column: x => x.FkIdPrzelicznik,
                        principalTable: "przeliczniki_kierunkowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kierunki_wydzialy_FkIdWydzial",
                        column: x => x.FkIdWydzial,
                        principalTable: "wydzialy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "podania_kandydatow",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataZlozeniaPodania = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CzyAktywny = table.Column<bool>(type: "boolean", nullable: false),
                    StatusPodania = table.Column<int>(type: "integer", nullable: false),
                    FkIdRekrutacja = table.Column<long>(type: "bigint", nullable: false),
                    FkIdKandydat = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_podania_kandydatow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_podania_kandydatow_kandydaci_FkIdKandydat",
                        column: x => x.FkIdKandydat,
                        principalTable: "kandydaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_podania_kandydatow_rekrutacje_FkIdRekrutacja",
                        column: x => x.FkIdRekrutacja,
                        principalTable: "rekrutacje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pracownicy_dzialu_rekrutacji",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkIdWydzial = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pracownicy_dzialu_rekrutacji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pracownicy_dzialu_rekrutacji_pracownicy_Id",
                        column: x => x.Id,
                        principalTable: "pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pracownicy_dzialu_rekrutacji_wydzialy_FkIdWydzial",
                        column: x => x.FkIdWydzial,
                        principalTable: "wydzialy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dodatkowe_osiagniecia",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataZdobycia = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Opis = table.Column<string>(type: "character varying(1000000)", maxLength: 1000000, nullable: false),
                    FkIdPodanieKandydata = table.Column<long>(type: "bigint", nullable: false),
                    FkIdPrzelicznikOsiagniec = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dodatkowe_osiagniecia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dodatkowe_osiagniecia_podania_kandydatow_FkIdPodanieKandyda~",
                        column: x => x.FkIdPodanieKandydata,
                        principalTable: "podania_kandydatow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dodatkowe_osiagniecia_przeliczniki_osiagniec_FkIdPrzeliczni~",
                        column: x => x.FkIdPrzelicznikOsiagniec,
                        principalTable: "przeliczniki_osiagniec",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kierunki_na_podaniach",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Priorytet = table.Column<int>(type: "integer", nullable: false),
                    FkIdPodanieKandydata = table.Column<long>(type: "bigint", nullable: false),
                    FkIdKierunek = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kierunki_na_podaniach", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kierunki_na_podaniach_kierunki_FkIdKierunek",
                        column: x => x.FkIdKierunek,
                        principalTable: "kierunki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kierunki_na_podaniach_podania_kandydatow_FkIdPodanieKandyda~",
                        column: x => x.FkIdPodanieKandydata,
                        principalTable: "podania_kandydatow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "podania_na_I_stopien",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_podania_na_I_stopien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_podania_na_I_stopien_podania_kandydatow_Id",
                        column: x => x.Id,
                        principalTable: "podania_kandydatow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "podania_na_II_stopien",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SredniaZTokuStudiow = table.Column<float>(type: "real", nullable: false),
                    OcenaZPracyDyplomowej = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_podania_na_II_stopien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_podania_na_II_stopien_podania_kandydatow_Id",
                        column: x => x.Id,
                        principalTable: "podania_kandydatow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pracownicy_dzialu_rekrutacji_na_podania_kandydata",
                columns: table => new
                {
                    PkFkIdPodanieKandydata = table.Column<long>(type: "bigint", nullable: false),
                    PkFkIdPracownikDzialuRekrutacji = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pracownicy_dzialu_rekrutacji_na_podania_kandydata", x => new { x.PkFkIdPodanieKandydata, x.PkFkIdPracownikDzialuRekrutacji });
                    table.ForeignKey(
                        name: "FK_pracownicy_dzialu_rekrutacji_na_podania_kandydata_podania_k~",
                        column: x => x.PkFkIdPodanieKandydata,
                        principalTable: "podania_kandydatow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pracownicy_dzialu_rekrutacji_na_podania_kandydata_pracownic~",
                        column: x => x.PkFkIdPracownikDzialuRekrutacji,
                        principalTable: "pracownicy_dzialu_rekrutacji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matury",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataPrzystapieniaDoMatury = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FkIdPodanieNaIStopien = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matury", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matury_podania_na_I_stopien_FkIdPodanieNaIStopien",
                        column: x => x.FkIdPodanieNaIStopien,
                        principalTable: "podania_na_I_stopien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dorobei_naukowe",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataUzyskaniaDorobku = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FkIdPodanieNaIIStopien = table.Column<long>(type: "bigint", nullable: false),
                    FkIdKategoriaDorobku = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dorobei_naukowe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dorobei_naukowe_kategorie_dorobku_FkIdKategoriaDorobku",
                        column: x => x.FkIdKategoriaDorobku,
                        principalTable: "kategorie_dorobku",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dorobei_naukowe_podania_na_II_stopien_FkIdPodanieNaIIStopien",
                        column: x => x.FkIdPodanieNaIIStopien,
                        principalTable: "podania_na_II_stopien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "oceny",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WartoscProcentowa = table.Column<int>(type: "integer", nullable: false),
                    MaturaId = table.Column<long>(type: "bigint", nullable: false),
                    FkIdPrzedmiot = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oceny", x => x.Id);
                    table.ForeignKey(
                        name: "FK_oceny_matury_MaturaId",
                        column: x => x.MaturaId,
                        principalTable: "matury",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oceny_przedmioty_FkIdPrzedmiot",
                        column: x => x.FkIdPrzedmiot,
                        principalTable: "przedmioty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "kategorie_dorobku",
                columns: new[] { "Id", "NazwaKategoriiDorobku" },
                values: new object[,]
                {
                    { 1L, "Publikacja w czasopiśmie" },
                    { 2L, "Publikacja książki" },
                    { 3L, "Publikacja patentu" },
                    { 4L, "Rysunek architektoniczny" }
                });

            migrationBuilder.InsertData(
                table: "kategorie_osiagniecia",
                columns: new[] { "Id", "NazwaKategorii" },
                values: new object[,]
                {
                    { 1L, "Laureat konkursu - przedmiot ścisły" },
                    { 2L, "Laureat konkursu - przedmiot humanistyczny" },
                    { 3L, "Paszport Polsatu" },
                    { 4L, "Nagroda Nobla" }
                });

            migrationBuilder.InsertData(
                table: "miasta",
                columns: new[] { "Id", "NazwaMiasta" },
                values: new object[] { 1L, "Wrocław" });

            migrationBuilder.InsertData(
                table: "osoby",
                columns: new[] { "Id", "CzyEmailPotwierdzony", "DataZarejestrowania", "Email", "Haslo", "Imie", "Login", "Nazwisko", "NumerPaszportu", "Pesel" },
                values: new object[] { 1L, true, new DateTime(2024, 1, 14, 10, 44, 27, 306, DateTimeKind.Local).AddTicks(1741), "testowykandydat@gmail.com", "zahaszowaneHaselko", "Jan", "testowyKandydat", "Testowy", null, "59070575419" });

            migrationBuilder.InsertData(
                table: "przedmioty",
                columns: new[] { "Id", "NazwaPrzedmiotu" },
                values: new object[,]
                {
                    { 2L, "Fizyka podstawowa" },
                    { 16L, "Język obcy rozszerzony" },
                    { 15L, "Język polski rozszerzony" },
                    { 14L, "Biologia rozszerzona" },
                    { 13L, "Geografia rozszerzona" },
                    { 12L, "Informatyka rozszerzona" },
                    { 1L, "Matematyka podstawowa" },
                    { 10L, "Fizyka rozszerzona" },
                    { 11L, "Chemia rozszerzona" },
                    { 8L, "Język obcy podstawowy" },
                    { 7L, "Język polski podstawowy" },
                    { 5L, "Geografia podstawowa" },
                    { 4L, "Informatyka podstawowa" },
                    { 3L, "Chemia podstawowa" },
                    { 9L, "Matematyka rozszerzona" },
                    { 6L, "Biologia podstawowa" }
                });

            migrationBuilder.InsertData(
                table: "przeliczniki_kierunkowe",
                columns: new[] { "Id", "MaksymalnaWartoscPrzelicznika" },
                values: new object[,]
                {
                    { 1L, 1 },
                    { 2L, 1 }
                });

            migrationBuilder.InsertData(
                table: "rekrutacje",
                columns: new[] { "Id", "DataOtwarciaRekrutacji", "DataZamknieciaPrzyjec", "DataZamknieciaRekrutacji", "SemestrRekrutacji", "StatusRekrutacji", "StopienStudiow" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 14, 10, 44, 27, 304, DateTimeKind.Local).AddTicks(6731), new DateTime(2024, 6, 14, 10, 44, 27, 306, DateTimeKind.Local).AddTicks(254), 1, 0, 0 });

            migrationBuilder.InsertData(
                table: "kandydaci",
                columns: new[] { "Id", "NumerKandydata" },
                values: new object[] { 1L, "5907057541" });

            migrationBuilder.InsertData(
                table: "przeliczniki_osiagniec",
                columns: new[] { "Id", "FkIdKategoriaOsiagniecia", "FkIdPrzelicznikKierunkowy", "PrzyznawanePunkty" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, 0 },
                    { 3L, 3L, 1L, 0 },
                    { 2L, 2L, 2L, 0 },
                    { 4L, 4L, 2L, 0 }
                });

            migrationBuilder.InsertData(
                table: "przeliczniki_przedmiotu",
                columns: new[] { "Id", "FkIdPrzedmiot", "FkIdPrzelicznikKierunkowy", "Wspolczynnik" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, 0.2f },
                    { 2L, 2L, 1L, 0.5f },
                    { 3L, 3L, 1L, 0.7f }
                });

            migrationBuilder.InsertData(
                table: "wydzialy",
                columns: new[] { "Id", "MiastoId", "NazwaWydzialu", "NumerWydzialu" },
                values: new object[] { 1L, 1L, "Wydział Informatyki i Telekomunikacji", "W04n" });

            migrationBuilder.InsertData(
                table: "kierunki",
                columns: new[] { "Id", "CzasTrwaniaWSemestrach", "Czesne", "CzesneDlaCudzoziemcow", "Dyscyplina", "FkIdPrzelicznik", "FkIdWydzial", "Jezyk", "NazwaKierunku", "Opis", "ProfilKierunku", "SkrotKierunku", "StopienStudiow", "TrybStudiowania" },
                values: new object[,]
                {
                    { 1L, 7, 0, 1250, "informatyka techniczna i telekomunikacja", 1L, 1L, 0, "Informatyka Stosowana", "Kształcimy informatyków, którzy – oprócz wiedzy podstawowej – specjalizują się w zakresie:\nużytkowania, projektowania i programowania cyfrowych systemów automatyki, sieci i telematyki przemysłowej, systemów optymalizacji i sterowania z wykorzystaniem sterowników mikroprocesorowych, sieci neuronowych, w tym sieci głębokich\nmetod przetwarzania i rozpoznawania obrazów. ", "ogólnoakademicki", "IST", 0, 0 },
                    { 2L, 7, 50, 1300, "informatyka techniczna i telekomunikacja", 2L, 1L, 1, "Informatyka Techniczna w j. angielskim", "Kształcimy informatyków, którzy – oprócz wiedzy podstawowej – specjalizują się w zakresie:\nużytkowania, projektowania i programowania cyfrowych systemów automatyki, sieci i telematyki przemysłowej, systemów optymalizacji i sterowania z wykorzystaniem sterowników mikroprocesorowych, sieci neuronowych, w tym sieci głębokich\nmetod przetwarzania i rozpoznawania obrazów. ", "ogólnoakademicki", "ITA", 1, 0 },
                    { 3L, 7, 0, 1250, "informatyka techniczna i telekomunikacja", 1L, 1L, 0, "Informatyczne Systemy Automatyki", "Kształcimy informatyków, którzy – oprócz wiedzy podstawowej – specjalizują się w zakresie:\nużytkowania, projektowania i programowania cyfrowych systemów automatyki, sieci i telematyki przemysłowej, systemów optymalizacji i sterowania z wykorzystaniem sterowników mikroprocesorowych, sieci neuronowych, w tym sieci głębokich\nmetod przetwarzania i rozpoznawania obrazów. ", "ogólnoakademicki", "ISA", 0, 2 },
                    { 4L, 7, 100, 1350, "informatyka techniczna i telekomunikacja", 2L, 1L, 1, "Informatyka Stosowana w j.angielskim", "Studia umożliwiają doskonalenie umiejętności i zdobywanie wiedzy z szeroko pojmowanej informatyki i jej różnorakiego zastosowania m.in. w rozwiązywaniu problemów biznesowych, technicznych czy w obszarze gier komputerowych. Informatyka stosowana jest uzupełniana wiedzą z fizyki i matematyki, podstaw przedsiębiorczości oraz społecznych i zawodowych problemów informatyki. Dużą rolę przywiązuje się też do umiejętności miękkich, jak umiejętność prezentacji oraz umiejętność pracy w zespole.", "", "ISTA", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "podania_kandydatow",
                columns: new[] { "Id", "CzyAktywny", "DataZlozeniaPodania", "FkIdKandydat", "FkIdRekrutacja", "StatusPodania" },
                values: new object[] { 1L, true, new DateTime(2024, 1, 14, 10, 44, 27, 306, DateTimeKind.Local).AddTicks(6951), 1L, 1L, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_dodatkowe_osiagniecia_FkIdPodanieKandydata",
                table: "dodatkowe_osiagniecia",
                column: "FkIdPodanieKandydata");

            migrationBuilder.CreateIndex(
                name: "IX_dodatkowe_osiagniecia_FkIdPrzelicznikOsiagniec",
                table: "dodatkowe_osiagniecia",
                column: "FkIdPrzelicznikOsiagniec");

            migrationBuilder.CreateIndex(
                name: "IX_dorobei_naukowe_FkIdKategoriaDorobku",
                table: "dorobei_naukowe",
                column: "FkIdKategoriaDorobku");

            migrationBuilder.CreateIndex(
                name: "IX_dorobei_naukowe_FkIdPodanieNaIIStopien",
                table: "dorobei_naukowe",
                column: "FkIdPodanieNaIIStopien");

            migrationBuilder.CreateIndex(
                name: "IX_kandydaci_NumerKandydata",
                table: "kandydaci",
                column: "NumerKandydata",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_kierunki_FkIdPrzelicznik",
                table: "kierunki",
                column: "FkIdPrzelicznik");

            migrationBuilder.CreateIndex(
                name: "IX_kierunki_FkIdWydzial",
                table: "kierunki",
                column: "FkIdWydzial");

            migrationBuilder.CreateIndex(
                name: "IX_kierunki_na_podaniach_FkIdKierunek",
                table: "kierunki_na_podaniach",
                column: "FkIdKierunek");

            migrationBuilder.CreateIndex(
                name: "IX_kierunki_na_podaniach_FkIdPodanieKandydata",
                table: "kierunki_na_podaniach",
                column: "FkIdPodanieKandydata");

            migrationBuilder.CreateIndex(
                name: "IX_matury_FkIdPodanieNaIStopien",
                table: "matury",
                column: "FkIdPodanieNaIStopien");

            migrationBuilder.CreateIndex(
                name: "IX_oceny_FkIdPrzedmiot",
                table: "oceny",
                column: "FkIdPrzedmiot");

            migrationBuilder.CreateIndex(
                name: "IX_oceny_MaturaId",
                table: "oceny",
                column: "MaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_podania_kandydatow_FkIdKandydat",
                table: "podania_kandydatow",
                column: "FkIdKandydat");

            migrationBuilder.CreateIndex(
                name: "IX_podania_kandydatow_FkIdRekrutacja",
                table: "podania_kandydatow",
                column: "FkIdRekrutacja");

            migrationBuilder.CreateIndex(
                name: "IX_pracownicy_NumerPracownika",
                table: "pracownicy",
                column: "NumerPracownika",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pracownicy_dzialu_rekrutacji_FkIdWydzial",
                table: "pracownicy_dzialu_rekrutacji",
                column: "FkIdWydzial");

            migrationBuilder.CreateIndex(
                name: "IX_pracownicy_dzialu_rekrutacji_na_podania_kandydata_PkFkIdPra~",
                table: "pracownicy_dzialu_rekrutacji_na_podania_kandydata",
                column: "PkFkIdPracownikDzialuRekrutacji");

            migrationBuilder.CreateIndex(
                name: "IX_przeliczniki_dorobku_FkIdKategoriaDorobku",
                table: "przeliczniki_dorobku",
                column: "FkIdKategoriaDorobku");

            migrationBuilder.CreateIndex(
                name: "IX_przeliczniki_dorobku_FkIdPrzelicznikKierunkowy",
                table: "przeliczniki_dorobku",
                column: "FkIdPrzelicznikKierunkowy");

            migrationBuilder.CreateIndex(
                name: "IX_przeliczniki_osiagniec_FkIdKategoriaOsiagniecia",
                table: "przeliczniki_osiagniec",
                column: "FkIdKategoriaOsiagniecia");

            migrationBuilder.CreateIndex(
                name: "IX_przeliczniki_osiagniec_FkIdPrzelicznikKierunkowy",
                table: "przeliczniki_osiagniec",
                column: "FkIdPrzelicznikKierunkowy");

            migrationBuilder.CreateIndex(
                name: "IX_przeliczniki_przedmiotu_FkIdPrzedmiot",
                table: "przeliczniki_przedmiotu",
                column: "FkIdPrzedmiot");

            migrationBuilder.CreateIndex(
                name: "IX_przeliczniki_przedmiotu_FkIdPrzelicznikKierunkowy",
                table: "przeliczniki_przedmiotu",
                column: "FkIdPrzelicznikKierunkowy");

            migrationBuilder.CreateIndex(
                name: "IX_wydzialy_MiastoId",
                table: "wydzialy",
                column: "MiastoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dodatkowe_osiagniecia");

            migrationBuilder.DropTable(
                name: "dorobei_naukowe");

            migrationBuilder.DropTable(
                name: "kierunki_na_podaniach");

            migrationBuilder.DropTable(
                name: "oceny");

            migrationBuilder.DropTable(
                name: "pracownicy_dzialu_rekrutacji_na_podania_kandydata");

            migrationBuilder.DropTable(
                name: "przeliczniki_dorobku");

            migrationBuilder.DropTable(
                name: "przeliczniki_przedmiotu");

            migrationBuilder.DropTable(
                name: "przeliczniki_osiagniec");

            migrationBuilder.DropTable(
                name: "podania_na_II_stopien");

            migrationBuilder.DropTable(
                name: "kierunki");

            migrationBuilder.DropTable(
                name: "matury");

            migrationBuilder.DropTable(
                name: "pracownicy_dzialu_rekrutacji");

            migrationBuilder.DropTable(
                name: "kategorie_dorobku");

            migrationBuilder.DropTable(
                name: "przedmioty");

            migrationBuilder.DropTable(
                name: "kategorie_osiagniecia");

            migrationBuilder.DropTable(
                name: "przeliczniki_kierunkowe");

            migrationBuilder.DropTable(
                name: "podania_na_I_stopien");

            migrationBuilder.DropTable(
                name: "pracownicy");

            migrationBuilder.DropTable(
                name: "wydzialy");

            migrationBuilder.DropTable(
                name: "podania_kandydatow");

            migrationBuilder.DropTable(
                name: "miasta");

            migrationBuilder.DropTable(
                name: "kandydaci");

            migrationBuilder.DropTable(
                name: "rekrutacje");

            migrationBuilder.DropTable(
                name: "osoby");
        }
    }
}
