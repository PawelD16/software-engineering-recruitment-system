using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using projektowaniaOprogramowania.Controllers;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.Services;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;
using projektowaniaOprogramowania.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace projektowaniaOprogramowania.Controllers.Tests
{

    [TestClass()]
    public class PredykcjaControllerTests
    {
        private DbContextOptions<MyDbContext> _options;

        /* Manual database mocking turns out to be not sensible
        internal static Mock<DbSet<T>> MockDbSet<T>(ICollection<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.AsQueryable().GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(entities.Add);
            return mockSet;
        }


        internal static Mock<MyDbContext> SeedMock(DefaultValuesCreator creator)
        {
            Mock<MyDbContext> mockContext = new();

            mockContext.Setup(c => c.Rekrutacje).Returns(MockDbSet<RekrutacjaModel>(creator.Rekrutacje).Object);
            mockContext.Setup(c => c.Kandydaci).Returns(MockDbSet<KandydatModel>(creator.Kandydaci).Object);
            mockContext.Setup(c => c.Przedmioty).Returns(MockDbSet<PrzedmiotModel>(creator.Przedmioty).Object);
            mockContext.Setup(c => c.KategorieDorobku).Returns(MockDbSet<KategoriaDorobkuModel>(creator.KategorieDorobku).Object);
            mockContext.Setup(c => c.PrzelicznikiKierunkowe).Returns(MockDbSet<PrzelicznikKierunkowyModel>(creator.PrzelicznikiKierunkowe).Object);
            mockContext.Setup(c => c.PrzelicznikiOsiagniec).Returns(MockDbSet<PrzelicznikOsiagniecModel>(creator.PrzelicznikiOsiagniec).Object);
            mockContext.Setup(c => c.Miasta).Returns(MockDbSet<MiastoModel>(creator.Miasta).Object);
            mockContext.Setup(c => c.KategorieOsiagniec).Returns(MockDbSet<KategoriaOsiagnieciaModel>(creator.KategorieOsiagniec).Object);
            mockContext.Setup(c => c.Wydzialy).Returns(MockDbSet<WydzialModel>(creator.Wydzialy).Object);
            mockContext.Setup(c => c.PrzelicznikiKierunkowe).Returns(MockDbSet<PrzelicznikKierunkowyModel>(creator.PrzelicznikiKierunkowe).Object);
            mockContext.Setup(c => c.Kierunki).Returns(MockDbSet<KierunekModel>(creator.Kierunki).Object);
            mockContext.Setup(c => c.PodaniaKandydatow).Returns(MockDbSet<PodanieKandydataModel>(creator.PodaniaKandydata).Object);

            return mockContext;
        }
        */

        internal static Mock<ISessionWrapper> SeedSessionWithGivenUserId(long userId)
        {
            Mock<ISessionWrapper> mockSession = new();

            mockSession.Setup(s => s.GetUserId()).Returns(userId);

            return mockSession;
        }

        /*
        internal static Mock<PunktyRekrutacyjneService> MockPunktyRekrutacyjneService(List<KierunekModel> kierunki)
        {
            kierunki.ForEach(k =>
            {
                k.CalculatedProbabilityOfSucessfulRecruitation = 80F;
                k.CalculatedRecruitationPoints = 400;
            });

            Mock<PunktyRekrutacyjneService> mockService = new();
            mockService.Setup(s => s.WyliczPrzelicznikKandydataDlaKazdegoKierunku(It.IsAny<KandydatModel>())).Returns(kierunki);

            return mockService;
        }
        */

        private MyDbContext CreateContext()
        {
            return new MyDbContext(_options);
        }

        [TestInitialize]
        public void Setup()
        {
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
        public void WyswietlKierunki_ReturnsNoResultForUserWithoutCalculatedData()
        {
            // Arrange
            DefaultValuesCreator creator = new();

            // Set up your controller with the in-memory database context
            using MyDbContext context = CreateContext();
            var controller = new PredykcjaController(context, new PunktyRekrutacyjneService(context), SeedSessionWithGivenUserId(creator.Kandydaci[0].Id).Object);

            // Act
            var result = controller.WyswietlKierunki(null, null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(((RedirectToActionResult)result).ActionName, "WpiszPotrzebneDane");
        }

        [TestMethod]
        public void WszystkieKierunki_ReturnsKierunkiWithoutCalculatedPredictionsForUserWIthoutCalculatedData()
        {
            // Arrange
            DefaultValuesCreator creator = new();

            // Set up your controller with the in-memory database context
            using MyDbContext context = CreateContext();
            var controller = new PrzegladController(context, new PunktyRekrutacyjneService(context), SeedSessionWithGivenUserId(creator.Kandydaci[0].Id).Object);

            // Act
            var result = controller.WszystkieKierunki(null, null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = result.Model as IEnumerable<KierunekModel>;
            Assert.IsNotNull(model);
            foreach (var item in model)
            {
                Assert.AreEqual(item.CalculatedProbabilityOfSucessfulRecruitation, 0F);
                Assert.AreEqual(item.CalculatedRecruitationPoints, 0);
            }
        }
    }
}