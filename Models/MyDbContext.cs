using System;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models.Other;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowania.Models
{
    public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public virtual DbSet<OsobaModel> Osoby { get; set; }
		public virtual DbSet<KandydatModel> Kandydaci { get; set; }
		public virtual DbSet<PracownikModel> Pracownicy { get; set; }
		public virtual DbSet<RekrutacjaModel> Rekrutacje { get; set; }
		public virtual DbSet<KategoriaOsiagnieciaModel> KategorieOsiagniec { get; set; }
		public virtual DbSet<DodatkoweOsiagniecieModel> DodatkoweOsiagniecia { get; set; }
		public virtual DbSet<DorobekNaukowyModel> DorobkiNaukowe { get; set; }
		public virtual DbSet<KategoriaDorobkuModel> KategorieDorobku { get; set; }
		public virtual DbSet<MiastoModel> Miasta { get; set; }
		public virtual DbSet<WydzialModel> Wydzialy { get; set; }
		public virtual DbSet<KierunekModel> Kierunki { get; set; }
		public virtual DbSet<PodanieNaStudiaIIStopniaModel> PodaniaNaIIStopien { get; set; }
		public virtual DbSet<MaturaModel> Matury { get; set; }
		public virtual DbSet<OcenaModel> Oceny { get; set; }
		public virtual DbSet<PrzedmiotModel> Przedmioty { get; set; }
		public virtual DbSet<KierunekNaPodaniuModel> KierunkiNaPodaniu { get; set; }
		public virtual DbSet<PrzelicznikKierunkowyModel> PrzelicznikiKierunkowe { get; set; }
		public virtual DbSet<PrzelicznikOsiagniecModel> PrzelicznikiOsiagniec { get; set; }
		public virtual DbSet<PrzelicznikDorobkuModel> PrzelicznikiDorobku { get; set; }
		public virtual DbSet<PrzelicznikPrzedmiotuModel> PrzelicznikiPrzedmiotow { get; set; }
		public virtual DbSet<PodanieNaStudiaIStopniaModel> PodaniaNaIStopien { get; set; }
		public virtual DbSet<PracownikDzialuRekrutacjiModel> PracownicyDzialuRekrutacji { get; set; }
		public virtual DbSet<PracownikDzialuRekrutacjiNaPodanieKandydataModel> PracownicyDzialuRekrutacjiNaPodaniaKandydatow { get; set; }
		public virtual DbSet<PodanieKandydataModel> PodaniaKandydatow { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			// Table per Type / Joined table
			modelBuilder.Entity<OsobaModel>().ToTable("osoby");
			modelBuilder.Entity<KandydatModel>().ToTable("kandydaci");
			modelBuilder.Entity<PracownikModel>().ToTable("pracownicy");
			modelBuilder.Entity<PracownikDzialuRekrutacjiModel>().ToTable("pracownicy_dzialu_rekrutacji");

			modelBuilder.Entity<PodanieKandydataModel>().ToTable("podania_kandydatow");
			modelBuilder.Entity<PodanieNaStudiaIIStopniaModel>().ToTable("podania_na_II_stopien");
			modelBuilder.Entity<PodanieNaStudiaIStopniaModel>().ToTable("podania_na_I_stopien");

			// Setting unique (but not key) values
			modelBuilder.Entity<KandydatModel>()
						.HasIndex(u => u.NumerKandydata)
						.IsUnique();

			modelBuilder.Entity<PracownikModel>()
						.HasIndex(u => u.NumerPracownika)
						.IsUnique();

			modelBuilder.Entity<PracownikDzialuRekrutacjiNaPodanieKandydataModel>()
				.HasKey(m => new { m.PkFkIdPodanieKandydata, m.PkFkIdPracownikDzialuRekrutacji });

			// Extension method for seeding the databse with data. It is defined in ModelBuilderExtension
			modelBuilder.Seed();

		}
	}

}
