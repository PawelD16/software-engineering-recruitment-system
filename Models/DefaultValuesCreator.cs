using projektowaniaOprogramowania.ViewModels.CollegeStructures;
using projektowaniaOprogramowania.ViewModels.Users;
using projektowaniaOprogramowania.ViewModels;
using System.Collections.Generic;
using System;

namespace projektowaniaOprogramowania.Models
{

    public class DefaultValuesCreator
    {
        public List<RekrutacjaModel> Rekrutacje { get; }
        public List<KandydatModel> Kandydaci { get; }
        public List<PrzedmiotModel> Przedmioty { get; }
        public List<KategoriaDorobkuModel> KategorieDorobku { get; }
        public List<PrzelicznikKierunkowyModel> PrzelicznikiKierunkowe { get; }
        public List<PrzelicznikOsiagniecModel> PrzelicznikiOsiagniec { get; }
        public List<PrzelicznikPrzedmiotuModel> PrzelicznikiPrzedmiotow { get; }
        public List<KategoriaOsiagnieciaModel> KategorieOsiagniec { get; }
        public List<WydzialModel> Wydzialy { get; }
        public List<KierunekModel> Kierunki { get; }
        public List<MiastoModel> Miasta { get; }
        public List<PodanieKandydataModel> PodaniaKandydata { get; }

        public DefaultValuesCreator()
        {
            Rekrutacje = new()
            {
            new() {
                Id = 1,
                DataOtwarciaRekrutacji = DateTime.MinValue,
                DataZamknieciaPrzyjec = DateTime.Now.AddMonths(5),
                DataZamknieciaRekrutacji = DateTime.Now.AddMonths(5),
                SemestrRekrutacji = SemestrRekrutacji.Zimowy,
                StatusRekrutacji = StatusRekrutacji.Otwarta,
                StopienStudiow = StopienStudiow.Pierwszy
            }
                };

            Kandydaci = new()
            {new()
            {
                Id = 1,
                Login = "testowyKandydat",
                Email = "testowykandydat@gmail.com",
                Haslo = "zahaszowaneHaselko",
                Pesel = "59070575419",
                Imie = "Jan",
                Nazwisko = "Testowy",
                DataZarejestrowania = DateTime.Now,
                CzyEmailPotwierdzony = true,
                NumerKandydata = "5907057541"
            }
            };

            Przedmioty = new()
            {
                new() { Id = 1, NazwaPrzedmiotu = "Matematyka podstawowa" },
                new() { Id = 2, NazwaPrzedmiotu = "Fizyka podstawowa" },
                new() { Id = 3, NazwaPrzedmiotu = "Chemia podstawowa" },
                new() { Id = 4, NazwaPrzedmiotu = "Informatyka podstawowa" },
                new() { Id = 5, NazwaPrzedmiotu = "Geografia podstawowa" },
                new() { Id = 6, NazwaPrzedmiotu = "Biologia podstawowa" },
                new() { Id = 7, NazwaPrzedmiotu = "Język polski podstawowy" },
                new() { Id = 8, NazwaPrzedmiotu = "Język obcy podstawowy" },

                new() { Id = 9, NazwaPrzedmiotu = "Matematyka rozszerzona" },
                new() { Id = 10, NazwaPrzedmiotu = "Fizyka rozszerzona" },
                new() { Id = 11, NazwaPrzedmiotu = "Chemia rozszerzona" },
                new() { Id = 12, NazwaPrzedmiotu = "Informatyka rozszerzona" },
                new() { Id = 13, NazwaPrzedmiotu = "Geografia rozszerzona" },
                new() { Id = 14, NazwaPrzedmiotu = "Biologia rozszerzona" },
                new() { Id = 15, NazwaPrzedmiotu = "Język polski rozszerzony" },
                new() { Id = 16, NazwaPrzedmiotu = "Język obcy rozszerzony" }
            };

            KategorieDorobku = new()
            {
                new() { Id = 1, NazwaKategoriiDorobku = "Publikacja w czasopiśmie" },
                new() { Id = 2, NazwaKategoriiDorobku = "Publikacja książki" },
                new() { Id = 3, NazwaKategoriiDorobku = "Publikacja patentu" },
                new() { Id = 4, NazwaKategoriiDorobku = "Rysunek architektoniczny" }
            };

            PrzelicznikiPrzedmiotow = new ()
            {
                new()
                {
                    Id = 1,
                    Wspolczynnik = 0.2f,
                    FkIdPrzedmiot = 1,
                    FkIdPrzelicznikKierunkowy = 1
                },
                new()
                {
                    Id = 2,
                    Wspolczynnik = 0.5f,
                    FkIdPrzedmiot = 2,
                    FkIdPrzelicznikKierunkowy = 1,
                },
                new()
                {
                    Id = 3,
                    Wspolczynnik = 0.7f,
                    FkIdPrzedmiot = 3,
                    FkIdPrzelicznikKierunkowy = 1,
                }
            };

            PrzelicznikiKierunkowe = new()
            {
                new()
                {
                    MaksymalnaWartoscPrzelicznika = 1,
                    Id = 1,
                    PrzelicznikDorobku = null,
                },
                new()
                {
                    MaksymalnaWartoscPrzelicznika = 1,
                    Id = 2,
                }
            };

            PrzelicznikiOsiagniec = new()
            {

                new()
                {
                    Id = 1,
                    FkIdKategoriaOsiagniecia = 1,
                    FkIdPrzelicznikKierunkowy = PrzelicznikiKierunkowe[0].Id,

                },
                new()
                {
                    Id = 2,
                    FkIdKategoriaOsiagniecia = 2,
                    FkIdPrzelicznikKierunkowy = PrzelicznikiKierunkowe[1].Id,

                },
                new()
                {
                    Id = 3,
                    FkIdKategoriaOsiagniecia = 3,
                    FkIdPrzelicznikKierunkowy = PrzelicznikiKierunkowe[0].Id,

                },
                new()
                {
                    Id = 4,
                    FkIdKategoriaOsiagniecia = 4,
                    FkIdPrzelicznikKierunkowy = PrzelicznikiKierunkowe[1].Id,
                }
            };

            Miasta = new()
            {new() { Id = 1, NazwaMiasta = "Wrocław" } };

            KategorieOsiagniec = new()
            {
                new() { Id = 1, NazwaKategorii = "Laureat konkursu - przedmiot ścisły" },
                new() { Id = 2, NazwaKategorii = "Laureat konkursu - przedmiot humanistyczny" },
                new() { Id = 3, NazwaKategorii = "Paszport Polsatu" },
                new() { Id = 4, NazwaKategorii = "Nagroda Nobla" }
            };

            Wydzialy = new()
                { new() { Id = 1, MiastoId = Miasta[0].Id, NazwaWydzialu = "Wydział Informatyki i Telekomunikacji", NumerWydzialu = "W04n" } };

            
            Kierunki = new()
            {
                new()
                {
                    Id = 1,
                    CzasTrwaniaWSemestrach = 7,
                    Czesne = 0,
                    CzesneDlaCudzoziemcow = 1250,
                    Dyscyplina = "informatyka techniczna i telekomunikacja",
                    FkIdWydzial = Wydzialy[0].Id,
                    Jezyk = Jezyk.Polski,
                    NazwaKierunku = "Informatyka Stosowana",
                    ProfilKierunku = "ogólnoakademicki",
                    SkrotKierunku = "IST",
                    StopienStudiow = StopienStudiow.Pierwszy,
                    TrybStudiowania = TrybStudiowania.Stacjonarne,
                    FkIdPrzelicznik = 1

                },
                new()
                {
                    Id = 2,
                    CzasTrwaniaWSemestrach = 7,
                    Czesne = 50,
                    CzesneDlaCudzoziemcow = 1300,
                    Dyscyplina = "informatyka techniczna i telekomunikacja",
                    FkIdWydzial = Wydzialy[0].Id,
                    Jezyk = Jezyk.Angielski,
                    NazwaKierunku = "Informatyka Techniczna w j. angielskim",
                    ProfilKierunku = "ogólnoakademicki",
                    SkrotKierunku = "ITA",
                    StopienStudiow = StopienStudiow.Drugi,
                    TrybStudiowania = TrybStudiowania.Stacjonarne,
                    FkIdPrzelicznik = 2


                },
                new()
                {
                    Id = 3,
                    CzasTrwaniaWSemestrach = 7,
                    Czesne = 0,
                    CzesneDlaCudzoziemcow = 1250,
                    Dyscyplina = "informatyka techniczna i telekomunikacja",
                    FkIdWydzial = Wydzialy[0].Id,
                    Jezyk = Jezyk.Polski,
                    NazwaKierunku = "Informatyczne Systemy Automatyki",
                    ProfilKierunku = "ogólnoakademicki",
                    SkrotKierunku = "ISA",
                    StopienStudiow = StopienStudiow.Pierwszy,
                    TrybStudiowania = TrybStudiowania.Mieszane,
                    FkIdPrzelicznik = 1


                },
                new()
                {
                    Id = 4,
                    CzasTrwaniaWSemestrach = 7,
                    Czesne = 100,
                    CzesneDlaCudzoziemcow = 1350,
                    Dyscyplina = "informatyka techniczna i telekomunikacja",
                    FkIdWydzial = Wydzialy[0].Id,
                    Jezyk = Jezyk.Angielski,
                    NazwaKierunku = "Informatyka Stosowana w j.angielskim",
                    ProfilKierunku = "",
                    SkrotKierunku = "ISTA",
                    StopienStudiow = StopienStudiow.Pierwszy,
                    TrybStudiowania = TrybStudiowania.Zdalne,
                    FkIdPrzelicznik = 2

                }
            };

            PodaniaKandydata = new()
                {new()
            {
                Id = 1,
                CzyAktywny = true,
                DataZlozeniaPodania = DateTime.Now,
                FkIdKandydat = Kandydaci[0].Id,
                FkIdRekrutacja = Rekrutacje[0].Id
            } };
        }



    }
}
