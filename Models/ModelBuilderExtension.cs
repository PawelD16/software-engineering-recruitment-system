using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using projektowaniaOprogramowania.ViewModels.Users;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;

namespace projektowaniaOprogramowania.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            RekrutacjaViewModel rekrutacja = new()
            {
                Id = 1,
                DataOtwarciaRekrutacji = DateTime.MinValue,
                DataZamknieciaPrzyjec = DateTime.MaxValue,
                DataZamknieciaRekrutacji = DateTime.MaxValue,
                SemestrRekrutacji = SemestrRekrutacji.Zimowy,
                StatusRekrutacji = StatusRekrutacji.Otwarta,
                StopienStudiow = StopienStudiow.Pierwszy
            };

            modelBuilder.Entity<RekrutacjaViewModel>().HasData(rekrutacja);

            KandydatViewModel kandydat = new()
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
            };

            modelBuilder.Entity<KandydatViewModel>().HasData(kandydat);

            modelBuilder.Entity<PrzedmiotViewModel>().HasData(
                new PrzedmiotViewModel() { Id = 1, NazwaPrzedmiotu = "Matematyka podstawowa" },
                new PrzedmiotViewModel() { Id = 2, NazwaPrzedmiotu = "Fizyka podstawowa" },
                new PrzedmiotViewModel() { Id = 3, NazwaPrzedmiotu = "Chemia podstawowa" },
                new PrzedmiotViewModel() { Id = 4, NazwaPrzedmiotu = "Informatyka podstawowa" },
                new PrzedmiotViewModel() { Id = 5, NazwaPrzedmiotu = "Geografia podstawowa" },
                new PrzedmiotViewModel() { Id = 6, NazwaPrzedmiotu = "Biologia podstawowa" },
                new PrzedmiotViewModel() { Id = 7, NazwaPrzedmiotu = "Język polski podstawowy" },
                new PrzedmiotViewModel() { Id = 8, NazwaPrzedmiotu = "Język obcy podstawowy" },

                new PrzedmiotViewModel() { Id = 9, NazwaPrzedmiotu = "Matematyka rozszerzona" },
                new PrzedmiotViewModel() { Id = 10, NazwaPrzedmiotu = "Fizyka rozszerzona" },
                new PrzedmiotViewModel() { Id = 11, NazwaPrzedmiotu = "Chemia rozszerzona" },
                new PrzedmiotViewModel() { Id = 12, NazwaPrzedmiotu = "Informatyka rozszerzona" },
                new PrzedmiotViewModel() { Id = 13, NazwaPrzedmiotu = "Geografia rozszerzona" },
                new PrzedmiotViewModel() { Id = 14, NazwaPrzedmiotu = "Biologia rozszerzona" },
                new PrzedmiotViewModel() { Id = 15, NazwaPrzedmiotu = "Język polski rozszerzony" },
                new PrzedmiotViewModel() { Id = 16, NazwaPrzedmiotu = "Język obcy rozszerzony" }
            );

            modelBuilder.Entity<KategoriaDorobkuViewModel>().HasData(
                new KategoriaDorobkuViewModel() { Id = 1, NazwaKategoriiDorobku = "Publikacja w czasopiśmie" },
                new KategoriaDorobkuViewModel() { Id = 2, NazwaKategoriiDorobku = "Publikacja książki" },
                new KategoriaDorobkuViewModel() { Id = 3, NazwaKategoriiDorobku = "Publikacja patentu" },
                new KategoriaDorobkuViewModel() { Id = 4, NazwaKategoriiDorobku = "Rysunek architektoniczny" }
            );

            modelBuilder.Entity<KategoriaOsiagnieciaViewModel>().HasData(
                new KategoriaOsiagnieciaViewModel() { Id = 1, NazwaKategorii = "Laureat konkursu - przedmiot ścisły" },
                new KategoriaOsiagnieciaViewModel() { Id = 2, NazwaKategorii = "Laureat konkursu - przedmiot humanistyczny" },
                new KategoriaOsiagnieciaViewModel() { Id = 3, NazwaKategorii = "Paszport Polsatu" },
                new KategoriaOsiagnieciaViewModel() { Id = 4, NazwaKategorii = "Nagroda Nobla" }
            );

            var przelicznikKierunkowy = new PrzelicznikKierunkowyViewModel()
            {
                Id = 1,
                FkIdKierunek = 1,
                MaksymalnaWartoscPrzelicznika = 530
            };

            modelBuilder.Entity<PrzelicznikKierunkowyViewModel>().HasData(przelicznikKierunkowy);

            modelBuilder.Entity<PrzelicznikOsiagniecViewModel>().HasData(
                new PrzelicznikOsiagniecViewModel()
                {
                    Id = 1,
                    FkIdKategoriaOsiagniecia = 1,
                    FkIdPrzelicznikKierunkowy = przelicznikKierunkowy.Id,
                    
                },
                new PrzelicznikOsiagniecViewModel()
                {
                    Id = 2,
                    FkIdKategoriaOsiagniecia = 2,
					FkIdPrzelicznikKierunkowy = przelicznikKierunkowy.Id,

				},
                new PrzelicznikOsiagniecViewModel()
                {
                    Id = 3,
                    FkIdKategoriaOsiagniecia = 3,
					FkIdPrzelicznikKierunkowy = przelicznikKierunkowy.Id,

				},
                new PrzelicznikOsiagniecViewModel()
                {
                    Id = 4,
                    FkIdKategoriaOsiagniecia = 4,
					FkIdPrzelicznikKierunkowy = przelicznikKierunkowy.Id,
				}
			);


            MiastoViewModel miasto = new() { Id = 1, NazwaMiasta = "Wrocław" };

            modelBuilder.Entity<MiastoViewModel>().HasData(miasto);

            WydzialViewModel wydzial = new() { Id = 1, MiastoId = (long)miasto.Id, NazwaWydzialu = "Wydział Informatyki i Telekomunikacji", NumerWydzialu = "W04n" };

            modelBuilder.Entity<WydzialViewModel>().HasData(wydzial);

            modelBuilder.Entity<KierunekViewModel>().HasData(
                new KierunekViewModel()
                {
                    Id = 1,
                    CzasTrwaniaWSemestrach = 7,
                    Czesne = 0,
                    CzesneDlaCudzoziemcow = 1250,
                    Dyscyplina = "informatyka techniczna i telekomunikacja",
                    FkIdWydzial = (long)wydzial.Id,
                    Jezyk = Jezyk.Polski,
                    NazwaKierunku = "Informatyka Stosowana",
                    ProfilKierunku = "ogólnoakademicki",
                    SkrotKierunku = "IST",
                    StopienStudiow = StopienStudiow.Pierwszy,
                    TrybStudiowania = TrybStudiowania.Stacjonarne,
                },
                new KierunekViewModel()
                {
                    Id = 2,
                    CzasTrwaniaWSemestrach = 7,
                    Czesne = 50,
                    CzesneDlaCudzoziemcow = 1300,
                    Dyscyplina = "informatyka techniczna i telekomunikacja",
                    FkIdWydzial = (long)wydzial.Id,
                    Jezyk = Jezyk.Angielski,
                    NazwaKierunku = "Informatyka Techniczna w j. angielskim",
                    ProfilKierunku = "ogólnoakademicki",
                    SkrotKierunku = "ITA",
                    StopienStudiow = StopienStudiow.Drugi,
                    TrybStudiowania = TrybStudiowania.Stacjonarne,
                },
                new KierunekViewModel()
                {
                    Id = 3,
                    CzasTrwaniaWSemestrach = 7,
                    Czesne = 0,
                    CzesneDlaCudzoziemcow = 1250,
                    Dyscyplina = "informatyka techniczna i telekomunikacja",
                    FkIdWydzial = (long)wydzial.Id,
                    Jezyk = Jezyk.Polski,
                    NazwaKierunku = "Informatyczne Systemy Automatyki",
                    ProfilKierunku = "ogólnoakademicki",
                    SkrotKierunku = "ISA",
                    StopienStudiow = StopienStudiow.Pierwszy,
                    TrybStudiowania = TrybStudiowania.Mieszane,
                },
                new KierunekViewModel()
                {
                    Id = 4,
                    CzasTrwaniaWSemestrach = 7,
                    Czesne = 100,
                    CzesneDlaCudzoziemcow = 1350,
                    Dyscyplina = "informatyka techniczna i telekomunikacja",
                    FkIdWydzial = (long)wydzial.Id,
                    Jezyk = Jezyk.Angielski,
                    NazwaKierunku = "Informatyka Stosowana w j.angielskim",
                    ProfilKierunku = "",
                    SkrotKierunku = "ISTA",
                    StopienStudiow = StopienStudiow.Pierwszy,
                    TrybStudiowania = TrybStudiowania.Zdalne,
                }
            );

            modelBuilder.Entity<PodanieKandydataViewModel>().HasData(
                new PodanieKandydataViewModel()
                {
                    Id = 1,
                    CzyAktywny = true,
                    DataZlozeniaPodania = DateTime.Now,
                    FkIdKandydat = (long)kandydat.Id,
                    FkIdRekrutacja = (long)rekrutacja.Id
                }
                );
        }
    }
}
