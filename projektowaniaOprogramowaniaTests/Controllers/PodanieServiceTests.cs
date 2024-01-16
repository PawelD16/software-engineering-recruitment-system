using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.Services;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektowaniaOprogramowaniaTests.Controllers
{
    [TestClass()]
    public class PodanieServiceTests
    {
        private DbContextOptions<MyDbContext> _options;

        private MyDbContext CreateContext()
        {
            return new MyDbContext(_options);
        }

        [TestInitialize]
        public void Setup()
        {

            var rand = new Random();
            _options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = CreateContext())
            {
                DefaultValuesCreator creator = new();

                context.AddRange(creator.Rekrutacje);

                context.AddRange(creator.Kandydaci);

                context.AddRange(creator.Przedmioty);

                context.AddRange(creator.KategorieDorobku);

                context.AddRange(creator.PrzelicznikiPrzedmiotow);

                context.AddRange(creator.PrzelicznikiKierunkowe);

                context.AddRange(creator.PrzelicznikiOsiagniec);

                context.AddRange(creator.Miasta);

                context.AddRange(creator.KategorieOsiagniec);

                context.AddRange(creator.Wydzialy);

                context.AddRange(creator.Kierunki);

                context.AddRange(creator.PodaniaKandydata);

                var closedRecruitment = new RekrutacjaModel()
                {
                    Id = 2,
                    DataOtwarciaRekrutacji = DateTime.Now,
                    StatusRekrutacji = StatusRekrutacji.Zamknieta
                };

                context.AddRange(closedRecruitment);


                for (int i = 2; i < 7; i++)
                {
                    var candidate = new KandydatModel()
                    {
                        Id = i
                    };
                    context.AddRange(candidate);
                }

                for (int i = 2; i < 7; i++)
                {
                    int recruitmentId = rand.Next(2) + 1;
                    var podanie = new PodanieNaStudiaIStopniaModel()
                    {
                        Id = i,
                        FkIdKandydat = i,
                        Rekrutacja = context.Rekrutacje.FirstOrDefault(rekrutacja => rekrutacja.Id == recruitmentId),
                    };
                    context.AddRange(podanie);
                }             

                for (int i = 2; i < 7; i++)
                {
                    int recruitmentId = rand.Next() + 1;
                    var podanie = new PodanieNaStudiaIStopniaModel()
                    {
                        FkIdKandydat = i,
                        Rekrutacja = context.Rekrutacje.FirstOrDefault(rekrutacja => rekrutacja.Id == recruitmentId),
                    };
                }

                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var context = CreateContext())
            {
                context.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        public void ReturnCorrectPodanieFromTheCandidate()
        {
            DefaultValuesCreator creator = new();

            using MyDbContext context = CreateContext();

            var podanieService = new PodanieService(context);

            var podanie = podanieService.GetPodanieKandydata(context.Kandydaci.FirstOrDefault(kandydat => kandydat.Id == 1));

            Assert.IsNotNull(podanie);
            Assert.AreEqual(podanie.StatusPodania, StatusPodania.Niezlozone);

        }
    }

}
