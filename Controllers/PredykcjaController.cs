using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.Services;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowania.Controllers
{
    public class PredykcjaController : Controller
    {
        private readonly MyDbContext _context;
        private readonly PunktyRekrutacyjneService _punktyRekrutacyjneService;

        public PredykcjaController(MyDbContext context, PunktyRekrutacyjneService punktyRekrutacyjneService)
        {
            _context = context;
            _punktyRekrutacyjneService = punktyRekrutacyjneService;
        }

        public IActionResult WyswietlKierunki(string kierunekNameFilter, long? wydzialIdFilter)
        {
            KandydatViewModel kandydat = _context.Kandydaci.SingleOrDefault(candidate => candidate.Id == HttpContext.Session.GetLong("UserId"));
            if (kandydat == null)
                return RedirectToAction("Error", "Home");

            var kierunki = _punktyRekrutacyjneService.WyliczPrzelicznikKandydataDlaKazdegoKierunku(kandydat);

			// _________________________ begin::Mock choose based on probabilities __________________________
			if (_context.PodaniaKandydatow.FirstOrDefault(p => p.FkIdKandydat == kandydat.Id) != null)
                kierunki = kierunki.Where(k => k.CalculatedProbabilityOfSucessfulRecruitation >= 50F).ToList();
			// _________________________ end:Mock choose based on probabilities __________________________


			if (kierunki.Count <= 0)
                return RedirectToAction("WpiszPotrzebneDane");

            if (wydzialIdFilter != null)
                kierunki = kierunki.Where(k => k.FkIdWydzial == wydzialIdFilter).ToList();

            if (kierunekNameFilter != null)
                kierunki = kierunki.Where(k => k.NazwaKierunku.ToLower().Contains(kierunekNameFilter.ToLower())).ToList();

            ViewBag.Wydzialy = _context.Wydzialy.ToList();
			ViewBag.KierunekNameFilter = kierunekNameFilter;
            ViewBag.WydzialIdFilter = wydzialIdFilter;

			return View(kierunki);
        }
        
        // jeżeli id == 0 to jest to nowy, niezapisany do bazy danych obiekt. Domyślne podejście w EF poleca nie null-owalne klucze główne
        public ActionResult WpiszPotrzebneDane()
        {
            KandydatViewModel kandydat = _context.Kandydaci.SingleOrDefault(c => c.Id == HttpContext.Session.GetLong("UserId"));

            if (kandydat == null)
                return RedirectToAction("Error", "Home");

            RekrutacjaViewModel rekrutacja = _context.Rekrutacje
                .FirstOrDefault(r => r.StatusRekrutacji == StatusRekrutacji.Otwarta);

            // brak otwartej rekrutacji
            if (rekrutacja == null)
                return RedirectToAction("Error", "Home");

            PodanieNaIStopienViewModel podanieNaIStopien = _context.PodaniaNaIStopien
                    .SingleOrDefault(pk => pk.FkIdKandydat == kandydat.Id && pk.FkIdRekrutacja == rekrutacja.Id)
                        ?? new() { DataZlozeniaPodania = DateTime.Now };

            PodanieNaIIStopienViewModel podanieNaIIStopien = _context.PodaniaNaIIStopien
                    .SingleOrDefault(pk => pk.FkIdKandydat == kandydat.Id && pk.FkIdRekrutacja == rekrutacja.Id)
                        ?? new() { DataZlozeniaPodania = DateTime.Now };

            MaturaViewModel matura = podanieNaIStopien.Id == 0 ? new() : _context.Matury
                .SingleOrDefault(m => m.FkIdPodanieNaIStopien == podanieNaIStopien.Id);

            var oceny = matura.Id == 0 ?
                new List<OcenaViewModel>() :
                _context.Oceny.Where(o => o.MaturaId == matura.Id).ToList();

            var model = new MaturaAndDorobekNaukowyAndDodatkoweOsiagnieciePackage
            {
                Oceny = _context.Przedmioty
                        .ToList() // Fetching data into memory as the query is incompatible with sql
                        .Select(p => oceny.FirstOrDefault(o => o.Przedmiot.Id == p.Id) ?? new OcenaViewModel { Przedmiot = p, FkIdPrzedmiot = p.Id })
                        .OrderBy(o => o.Przedmiot.NazwaPrzedmiotu)
                        .ToList(),
                KategorieDorobku = _context.KategorieDorobku.ToList(),
                PrzelicznikiOsiagniec = _context.PrzelicznikiOsiagniec.ToList(),
                DodatkoweOsiagniecia = podanieNaIStopien.Id == 0 && podanieNaIIStopien.Id == 0 ?
                    new List<DodatkoweOsiagniecieViewModel>() :
                    _context.DodatkoweOsiagniecia
                        .Where(d => d.FkIdPodanieKandydata == podanieNaIStopien.Id || d.FkIdPodanieKandydata == podanieNaIIStopien.Id)
                        .ToList(),
                DorobkiNaukowe = podanieNaIIStopien.Id == 0 ?
                    new List<DorobekNaukowyViewModel>() :
                    _context.DorobkiNaukowe.Where(d => d.FkIdPodanieNaIIStopien == podanieNaIIStopien.Id)
                        .ToList(),
                PodanieNaIIStopien = podanieNaIIStopien,
                PodanieNaIStopien = podanieNaIStopien,
                Matura = matura
            };

			foreach (var item in model.PrzelicznikiOsiagniec)
            {
                item.KategoriaOsiagniecia = _context.KategorieOsiagniec.SingleOrDefault(k => k.Id == item.FkIdKategoriaOsiagniecia);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult WpiszPotrzebneDane(MaturaAndDorobekNaukowyAndDodatkoweOsiagnieciePackage model)
        {
            KandydatViewModel kandydat = _context.Kandydaci.SingleOrDefault(candidate => candidate.Id == HttpContext.Session.GetLong("UserId"));

            if (kandydat == null)
                return RedirectToAction("Error", "Home");

            RekrutacjaViewModel rekrutacja = _context.Rekrutacje
                .FirstOrDefault(rekrutacja => rekrutacja.StatusRekrutacji == StatusRekrutacji.Otwarta);

            if (rekrutacja == null)
                return RedirectToAction("Error", "Home");

            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Wpisane dane są błędne!";
                return RedirectToAction("WpiszPotrzebneDane");
            }

            // Updating or deleting and replacing old entities!
            var podanieNaIStopien = _context.PodaniaNaIStopien.SingleOrDefault(p => model.PodanieNaIStopien.Id == p.Id);
            podanieNaIStopien ??= _context.Add(new PodanieNaIStopienViewModel()
            {
                CzyAktywny = true,
                DataZlozeniaPodania = model.PodanieNaIStopien.DataZlozeniaPodania,
                FkIdKandydat = kandydat.Id,
                FkIdRekrutacja = rekrutacja.Id
            }).Entity;

            var podanieNaIIStopien = _context.PodaniaNaIIStopien.SingleOrDefault(p => model.PodanieNaIIStopien.Id == p.Id);
            podanieNaIIStopien ??= _context.Add(new PodanieNaIIStopienViewModel()
            {
                CzyAktywny = true,
                DataZlozeniaPodania = model.PodanieNaIIStopien.DataZlozeniaPodania,
                FkIdKandydat = kandydat.Id,
                FkIdRekrutacja = rekrutacja.Id,
                SredniaZTokuStudiow = model.PodanieNaIIStopien.SredniaZTokuStudiow,
                OcenaZPracyDyplomowej = model.PodanieNaIIStopien.OcenaZPracyDyplomowej
            }).Entity;

            _context.SaveChanges();

            model.Matura.FkIdPodanieNaIStopien = podanieNaIStopien.Id;

            var matura = (model.Matura.Id == 0 ? _context.Add(model.Matura) : _context.Update(model.Matura)).Entity;
            _context.SaveChanges();

            foreach (var ocena in model.Oceny)
            {
                ocena.MaturaId = matura.Id;
                if (ocena.Id != 0)
                    _context.Oceny.Remove(_context.Oceny.SingleOrDefault(e => e.Id == ocena.Id));

                _context.Add(ocena);

            }

            if (model.DorobkiNaukowe != null)
            {
                foreach (var dorobekNaukowy in model.DorobkiNaukowe)
                {
                    dorobekNaukowy.FkIdPodanieNaIIStopien = podanieNaIIStopien.Id;
                    if (dorobekNaukowy.Id != 0)
                        _context.DorobkiNaukowe.Remove(_context.DorobkiNaukowe.SingleOrDefault(e => e.Id == dorobekNaukowy.Id));

                    _context.Add(dorobekNaukowy);
                }
            }

            _context.RemoveRange(_context.DodatkoweOsiagniecia.Where(d => d.FkIdPodanieKandydata == podanieNaIStopien.Id || d.FkIdPodanieKandydata == podanieNaIIStopien.Id));
            if (model.DodatkoweOsiagniecia != null)
            {
                foreach (var dodatkoweOsiagniecie in model.DodatkoweOsiagniecia)
                {
                    dodatkoweOsiagniecie.FkIdPodanieKandydata = podanieNaIStopien.Id;
					// deep copy!
					var dodatkoweOsiagniecie2 = new DodatkoweOsiagniecieViewModel()
                    {
                        DataZdobycia = dodatkoweOsiagniecie.DataZdobycia,
                        Opis = dodatkoweOsiagniecie.Opis,
                        FkIdPrzelicznikOsiagniec = dodatkoweOsiagniecie.FkIdPrzelicznikOsiagniec,
						FkIdPodanieKandydata = podanieNaIIStopien.Id
				};

					_context.Add(dodatkoweOsiagniecie);
					_context.Add(dodatkoweOsiagniecie2);

				}
            }

            _context.SaveChanges();

            return RedirectToAction("WyswietlKierunki");
        }
    }
}
