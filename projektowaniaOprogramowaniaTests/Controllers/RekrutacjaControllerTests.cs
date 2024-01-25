using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using projektowaniaOprogramowania.Controllers;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.Services;
using projektowaniaOprogramowania.Services.Recrutation;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowaniaTests.Controllers
{
    [TestClass()]
    public class RekrutacjaControllerTests
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
                    StatusRekrutacji = StatusRekrutacji.Otwarta
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
        public async Task EditNonExistentRekrutacja()
        {
            DefaultValuesCreator creator = new();

            using MyDbContext context = CreateContext();

            var rekrutacjaController = new RekrutacjaController(context, new RecruitmentValidationService(context));

            var editResult = await rekrutacjaController.Edit(3, new RekrutacjaModel
            {
                Id = 3,
                DataOtwarciaRekrutacji = DateTime.Now,
                StatusRekrutacji = StatusRekrutacji.Zamknieta
            });
            
            Assert.IsNotNull(editResult);

        }

        [TestMethod]
        public async Task IncorrectRekrutacjeInfoWhenEditing()
        {
            using MyDbContext context = CreateContext();

            var rekrutacjaController = new RekrutacjaController(context, new RecruitmentValidationService(context));
            var editResult = await rekrutacjaController.Edit(1, new RekrutacjaModel
            {
                Id = 3,
                DataOtwarciaRekrutacji = DateTime.Now,
                StatusRekrutacji = StatusRekrutacji.Zamknieta
            });
            
            
            Assert.IsNotNull(editResult);
            Assert.AreEqual(404, ((editResult) as NotFoundResult).StatusCode);
        }

        [TestMethod]
        public async Task RecruitationValidation()
        {
            using MyDbContext context = CreateContext();

            var validationService = new RecruitmentValidationService(context);
            var rekrutacja = new RekrutacjaModel()
            {
                Id = 3,
                DataOtwarciaRekrutacji = DateTime.Now,
                StatusRekrutacji = StatusRekrutacji.Zamknieta
            };

            var (result, error) = await validationService.IsRecruitmentValid(rekrutacja);
            
            Assert.AreEqual(result, false);



        }
    }
}