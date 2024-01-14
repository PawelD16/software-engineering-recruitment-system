using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.Users;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;

namespace projektowaniaOprogramowania.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            DefaultValuesCreator creator = new();

            modelBuilder.Entity<RekrutacjaModel>().HasData(creator.Rekrutacje);

            modelBuilder.Entity<KandydatModel>().HasData(creator.Kandydaci);

            modelBuilder.Entity<PrzedmiotModel>().HasData(creator.Przedmioty);

            modelBuilder.Entity<KategoriaDorobkuModel>().HasData(creator.KategorieDorobku);

            modelBuilder.Entity<PrzelicznikPrzedmiotuModel>().HasData(creator.PrzelicznikiPrzedmiotow);

            modelBuilder.Entity<PrzelicznikKierunkowyModel>().HasData(creator.PrzelicznikiKierunkowe);

            modelBuilder.Entity<PrzelicznikOsiagniecModel>().HasData(creator.PrzelicznikiOsiagniec);

            modelBuilder.Entity<MiastoModel>().HasData(creator.Miasta);

            modelBuilder.Entity<KategoriaOsiagnieciaModel>().HasData(creator.KategorieOsiagniec);

            modelBuilder.Entity<WydzialModel>().HasData(creator.Wydzialy);

            modelBuilder.Entity<KierunekModel>().HasData(creator.Kierunki);

            modelBuilder.Entity<PodanieKandydataModel>().HasData(creator.PodaniaKandydata);
        }
    }
}
