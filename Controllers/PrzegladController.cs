using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projektowaniaOprogramowania.Models;

using projektowaniaOprogramowania.Services;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowania.Controllers
{
    public class PrzegladController : Controller
	{
		private readonly MyDbContext _context;
		private readonly PunktyRekrutacyjneService _punktyRekrutacyjneService;
		private readonly ISessionWrapper _sessionWrapper;

		public PrzegladController(MyDbContext context, PunktyRekrutacyjneService punktyRekrutacyjneService, ISessionWrapper sessionWrapper)
		{
			_context = context;
			_punktyRekrutacyjneService = punktyRekrutacyjneService;
			_sessionWrapper = sessionWrapper;
		}

		// funkcja podobna do Predykcja/Index, ale nie wyciągałem osobnej funkcji, żeby zapewnić prostote analizy kodu
		public IActionResult WszystkieKierunki(string kierunekNameFilter, long? wydzialIdFilter)
		{
			List<KierunekModel> kierunki = _context.Kierunki.ToList();

			if (wydzialIdFilter != null)
				kierunki = kierunki.Where(k => k.FkIdWydzial == wydzialIdFilter).ToList();

			if (kierunekNameFilter != null)
				kierunki = kierunki.Where(k => k.NazwaKierunku.ToLower().Contains(kierunekNameFilter.ToLower())).ToList();

			ViewBag.Wydzialy = _context.Wydzialy.ToList();
			ViewBag.KierunekNameFilter = kierunekNameFilter;
			ViewBag.WydzialIdFilter = wydzialIdFilter;

			return View(kierunki);
		}

		public IActionResult DetaleKierunku(long kierunekId)
		{
			KierunekModel kierunek = _context.Kierunki.SingleOrDefault(k => k.Id == kierunekId);
			if (kierunek == null)
				return RedirectToAction("Error", "Home");

			KandydatModel kandydat = _context.Kandydaci.SingleOrDefault(k => k.Id == _sessionWrapper.GetUserId());

			if (kandydat != null)
				_punktyRekrutacyjneService.WyliczPrzelicznikKandydataDlaKierunku(kandydat, kierunek);	

			return View(kierunek);
		}
	}
}
