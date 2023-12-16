﻿using System;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowania.Models
{
    public class MyDbContext : DbContext
    { 
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<OsobaViewModel> Osoby { get; set; }
        public DbSet<KandydatViewModel> Kandydaci { get; set; }
        public DbSet<PracownikViewModel> Pracownicy { get; set; }
        public DbSet<RekrutacjaViewModel> Rekrutacje { get; set; }
        public DbSet<StatusRekrutacjiViewModel> StatusyRekrutacji { get; set; }
        public DbSet<SemestrRekrutacjiViewModel> SemestryRekrutacji { get; set; }
        public DbSet<KategoriaOsiagnieciaViewModel> KategorieOsiagniec { get; set; }
        public DbSet<DodatkoweOsiagniecieViewModel> DodatkoweOsiagniecia { get; set; }
        public DbSet<DorobekNaukowyViewModel> DorobkiNaukowe { get; set; }
        public DbSet<KategoriaDorobkuViewModel> KategorieDorobku { get; set; }
        public DbSet<MiastoViewModel> Miasta { get; set; }
        public DbSet<WydzialViewModel> Wydzialy { get; set; }
        public DbSet<KierunekViewModel> Kierunki { get; set; }
        public DbSet<StopienStudiowViewModel> StopnieStudiow { get; set; }
        public DbSet<PodanieNaIIStopienViewModel> PodaniaNaIIStopien { get; set; }
        public DbSet<MaturaViewModel> Matury { get; set; }
        public DbSet<OcenaViewModel> Oceny { get; set; }
        public DbSet<PrzedmiotViewModel> Przedmioty { get; set; }
        public DbSet<KierunekNaPodaniuViewModel> KierunkiNaPodaniu { get; set; }
        public DbSet<PrzelicznikKierunkowyViewModel> PrzelicznikiKierunkowe { get; set; }
        public DbSet<PrzelicznikOsiagniecViewModel> PrzelicznikiOsiagniec { get; set; }
        public DbSet<PrzelicznikDorobkuViewModel> PrzelicznikiDorobku { get; set; }
        public DbSet<PrzelicznikPrzedmiotuViewModel> PrzelicznikiPrzedmiotow { get; set; }
        public DbSet<PodanieNaIStopienViewModel> PodaniaNaIStopien { get; set; }
        public DbSet<JezykViewModel> Jezyki { get; set; }
        public DbSet<TrybStudiowaniaViewModel> TrybyStudiowania { get; set; }
        public DbSet<PracownikDzialuRekrutacjiViewModel> PracownicyDzialuRekrutacji { get; set; }
        public DbSet<PracownikDzialuRekrutacjiNaPodanieKandydataViewModel> PracownicyDzialuRekrutacjiNaPodaniaKandydatow { get; set; }
        public DbSet<PodanieKandydataViewModel> PodaniaKandydatow { get; set; }

    }
}
