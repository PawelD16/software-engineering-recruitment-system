using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.Services;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowania.Controllers
{
    public class PracownikEdycjaController : Controller
    {
        private readonly MyDbContext _context;
        private readonly PunktyRekrutacyjneService _punktyRekrutacyjne;
		private readonly PodanieService _podanieService;

		public PracownikEdycjaController(MyDbContext context, PunktyRekrutacyjneService punktyRekrutacyjne, PodanieService podanieService)
        {
            _context = context;
            _punktyRekrutacyjne = punktyRekrutacyjne;
			_podanieService = podanieService;
        }

        // GET: PracownikEdycja
        public IActionResult Index(string nazwiskoFilter = null, string statusPodaniaFilter = null)
        {
            ViewData["CurrentFilter"] = nazwiskoFilter;

            var podaniaQuery = _context.PodaniaKandydatow.Include(p => p.Kandydat).ToList();

            if (!String.IsNullOrEmpty(nazwiskoFilter))
            {
                podaniaQuery = podaniaQuery.Where(p => p.Kandydat.Nazwisko.Contains(nazwiskoFilter)).ToList();
            }

            if (!String.IsNullOrEmpty(statusPodaniaFilter))
            {
                podaniaQuery = podaniaQuery.Where(p => p.StatusPodania.ToString().Contains(statusPodaniaFilter)).ToList();
            }

            var myDbContext = podaniaQuery;
            return View(myDbContext);
        }


        // GET: PracownikEdycja/Edit/5
        public IActionResult Edit(long id)
        {
            KandydatModel kandydat = _context.Kandydaci.SingleOrDefault(c => c.Id == id);

			PodanieKandydataModel podanie = _podanieService.GetPodanieKandydata(kandydat);

            if (kandydat == null)
                return RedirectToAction("Error", "Home");

            RekrutacjaModel rekrutacja = _context.Rekrutacje
                .FirstOrDefault(r => r.StatusRekrutacji == StatusRekrutacji.Otwarta);

            // brak otwartej rekrutacji
            if (rekrutacja == null)
                return RedirectToAction("Error", "Home");

            PodanieNaStudiaIStopniaModel podanieNaIStopien = _context.PodaniaNaIStopien
                    .SingleOrDefault(pk => pk.FkIdKandydat == kandydat.Id && pk.FkIdRekrutacja == rekrutacja.Id)
                        ?? new() { DataZlozeniaPodania = DateTime.Now };

            PodanieNaStudiaIIStopniaModel podanieNaIIStopien = _context.PodaniaNaIIStopien
                    .SingleOrDefault(pk => pk.FkIdKandydat == kandydat.Id && pk.FkIdRekrutacja == rekrutacja.Id)
                        ?? new() { DataZlozeniaPodania = DateTime.Now };

            MaturaModel matura = podanieNaIStopien.Id == 0 ? new() : _context.Matury.SingleOrDefault(m => m.FkIdPodanieNaIStopien == podanieNaIStopien.Id);

            var oceny = matura.Id == 0 ?
                new List<OcenaModel>() :
                _context.Oceny.Where(o => o.MaturaId == matura.Id).ToList();

            var model = new WszystkieInformacjePodaniaViewModel
            {
                Oceny = _context.Przedmioty
                        .GroupJoin(
                            _context.Oceny,
                            p => p.Id,
                            o => o.FkIdPrzedmiot,
                            (p, ocenyJoin) => new { p, ocenyJoin }
                        )
                        .SelectMany(
                            x => x.ocenyJoin.DefaultIfEmpty(),
                            (x, oj) => new OcenaZPrzedmiotem
                            {
                                przedmiot = x.p,
                                ocena = oj ?? new OcenaModel { FkIdPrzedmiot = x.p.Id }
                            }
                        )
                        .OrderBy(x => x.przedmiot.NazwaPrzedmiotu)
                        .ToList(),

            KategorieDorobku = _context.KategorieDorobku.ToList(),
                PrzelicznikiOsiagniec = _context.PrzelicznikiOsiagniec.ToList(),
                DodatkoweOsiagniecia = podanieNaIStopien.Id == 0 && podanieNaIIStopien.Id == 0 ?
                    new List<DodatkoweOsiagniecieModel>() :
                    _context.DodatkoweOsiagniecia
                        .Where(d => d.FkIdPodanieKandydata == podanieNaIStopien.Id || d.FkIdPodanieKandydata == podanieNaIIStopien.Id)
                        .ToList(),
                DorobkiNaukowe = podanieNaIIStopien.Id == 0 ?
                    new List<DorobekNaukowyModel>() :
                    _context.DorobkiNaukowe.Where(d => d.FkIdPodanieNaIIStopien == podanieNaIIStopien.Id)
                        .ToList(),
                PodanieNaIIStopien = podanieNaIIStopien,
                PodanieNaIStopien = podanieNaIStopien,
                Matura = matura,
                PodanieKandydata = podanie
            };

            foreach (var item in model.PrzelicznikiOsiagniec)
            {
                item.KategoriaOsiagniecia = _context.KategorieOsiagniec.SingleOrDefault(k => k.Id == item.FkIdKategoriaOsiagniecia);
            }
            return View(model);
        }

        // POST: PracownikEdycja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind] WszystkieInformacjePodaniaViewModel model)
        {
            KandydatModel kandydat = _context.Kandydaci.SingleOrDefault(candidate => candidate.Id == id);

            if (kandydat == null)
                return RedirectToAction("Error", "Home");

            RekrutacjaModel rekrutacja = _context.Rekrutacje
                .FirstOrDefault(rekrutacja => rekrutacja.StatusRekrutacji == StatusRekrutacji.Otwarta);

            if (rekrutacja == null)
                return RedirectToAction("Error", "Home");

            if (model.Matura.DataPrzystapieniaDoMatury == DateTime.MinValue || rekrutacja.DataZamknieciaRekrutacji > model.Matura.DataPrzystapieniaDoMatury.AddYears(5))
            {
                TempData["Message"] = $"Matura nie może być starsza niż 5 lat od daty zamknięcia rekrutacji {rekrutacja.DataZamknieciaRekrutacji}";
                return RedirectToAction("Index");
            }

            var podanieNaIStopien = _context.PodaniaNaIStopien.SingleOrDefault(p => model.PodanieNaIStopien.Id == p.Id);
            podanieNaIStopien ??= _context.Add(new PodanieNaStudiaIStopniaModel()
            {
                CzyAktywny = true,
                DataZlozeniaPodania = model.PodanieNaIStopien.DataZlozeniaPodania,
                FkIdKandydat = kandydat.Id,
                FkIdRekrutacja = rekrutacja.Id
            }).Entity;

            var podanieNaIIStopien = _context.PodaniaNaIIStopien.SingleOrDefault(p => model.PodanieNaIIStopien.Id == p.Id);
            podanieNaIIStopien ??= _context.Add(new PodanieNaStudiaIIStopniaModel()
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
                ocena.ocena.MaturaId = matura.Id;
                if (ocena.ocena.Id != 0)
                    _context.Oceny.Remove(_context.Oceny.SingleOrDefault(e => e.Id == ocena.ocena.Id));

                _context.Add(ocena.ocena);
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
                    var dodatkoweOsiagniecie2 = new DodatkoweOsiagniecieModel()
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

            return RedirectToAction("Index");

        }


		private bool PodanieKandydataViewModelExists(long id)
        {
            return _context.PodaniaKandydatow.Any(e => e.Id == id);
        }
    }
}
