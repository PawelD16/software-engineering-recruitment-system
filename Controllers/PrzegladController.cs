using System.Collections.Generic;
using System.Linq;
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

		public PrzegladController(MyDbContext context, PunktyRekrutacyjneService punktyRekrutacyjneService)
		{
			_context = context;
			_punktyRekrutacyjneService = punktyRekrutacyjneService;
		}

		// funkcja podobna do Predykcja/Index, ale nie wyciągałem osobnej funkcji, żeby zapewnić prostote analizy kodu
		public IActionResult Index(string kierunekNameFilter, long? wydzialIdFilter)
		{
			List<KierunekViewModel> kierunki = _context.Kierunki.ToList();

			if (wydzialIdFilter != null)
				kierunki = kierunki.Where(k => k.FkIdWydzial == wydzialIdFilter).ToList();

			if (kierunekNameFilter != null)
				kierunki = kierunki.Where(k => k.NazwaKierunku.ToLower().Contains(kierunekNameFilter.ToLower())).ToList();

			ViewBag.Wydzialy = _context.Wydzialy.ToList();
			ViewBag.KierunekNameFilter = kierunekNameFilter;
			ViewBag.WydzialIdFilter = wydzialIdFilter;

			return View(kierunki);
		}

		public IActionResult Details(long kierunekId)
		{
			KierunekViewModel kierunek = _context.Kierunki.SingleOrDefault(k => k.Id == kierunekId);
			if (kierunek == null)
				return RedirectToAction("Error", "Home");

			KandydatViewModel kandydat = _context.Kandydaci.SingleOrDefault(k => k.Id == HttpContext.Session.GetLong("UserId"));

			if (kandydat != null)
				_punktyRekrutacyjneService.WyliczPrzelicznikKandydataDlaKierunku(kandydat, kierunek);	

			return View(kierunek);
		}
	}
}
