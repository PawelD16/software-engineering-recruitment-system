﻿using System;
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
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.PodaniaKandydatow.Include(p => p.Kandydat);
            return View(await myDbContext.ToListAsync());
        }

        // GET: PracownikEdycja/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
			KandydatModel candidate = _context.Kandydaci.SingleOrDefault(candidate => candidate.Id == id);

			PodanieKandydataModel podanie = _podanieService.GetPodanieKandydata(candidate);

			if (candidate == null)
				return RedirectToAction("Error", "Home");

			RekrutacjaModel rekrutacja = _context.Rekrutacje
				.FirstOrDefault(rekrutacja => rekrutacja.StatusRekrutacji == StatusRekrutacji.Otwarta);

			if (rekrutacja == null)
				return RedirectToAction("Error", "Home");

			PodanieNaStudiaIStopniaModel podanieNaIStopien = _context.PodaniaNaIStopien
					.SingleOrDefault(pk => pk.FkIdKandydat == id && pk.FkIdRekrutacja == rekrutacja.Id)
						?? new() { DataZlozeniaPodania = DateTime.Now };

			PodanieNaStudiaIIStopniaModel podanieNaIIStopien = _context.PodaniaNaIIStopien
					.SingleOrDefault(pk => pk.FkIdKandydat == id && pk.FkIdRekrutacja == rekrutacja.Id)
						?? new() { DataZlozeniaPodania = DateTime.Now };

			MaturaModel matura = podanieNaIStopien.Id == 0 ? new() : _context.Matury
				.SingleOrDefault(m => m.FkIdPodanieNaIStopien == podanieNaIStopien.Id);

			var oceny = matura.Id == 0 ?
				new List<OcenaModel>() :
				_context.Oceny.Where(o => o.MaturaId == matura.Id).ToList();

			var model = new WszystkieInformacjePodaniaViewModel
			{
                Oceny = _context.Przedmioty
                        .ToList()
                        .Select(p => oceny == null ? null : oceny.FirstOrDefault(o => o.Przedmiot.Id == p.Id) ?? new OcenaModel { Przedmiot = p, FkIdPrzedmiot = p.Id })
                        .OrderBy(o => o.Przedmiot.NazwaPrzedmiotu)
                        .ToList(),
                KategorieDorobku = _context.KategorieDorobku.ToList(),
                PrzelicznikiOsiagniec = _context.PrzelicznikiOsiagniec.ToList(),
                DodatkoweOsiagniecia = podanieNaIStopien == null && podanieNaIIStopien == null ?
                    new List<DodatkoweOsiagniecieModel>() :
                    _context.DodatkoweOsiagniecia
                        .Where(d => d.FkIdPodanieKandydata == podanieNaIStopien.Id || d.FkIdPodanieKandydata == podanieNaIIStopien.Id)
                        .ToList(),
                DorobkiNaukowe = podanieNaIIStopien == null ?
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
        public async Task<IActionResult> Edit(long id, MaturaAndDorobekNaukowyAndDodatkoweOsiagnieciePackage podanieKandydataViewModel)
        {
			KandydatModel kandydat = _context.Kandydaci.SingleOrDefault(candidate => candidate.Id == id);

			if (kandydat == null)
				return RedirectToAction("Error", "Home");

			RekrutacjaModel rekrutacja = _context.Rekrutacje
				.FirstOrDefault(rekrutacja => rekrutacja.StatusRekrutacji == StatusRekrutacji.Otwarta);

			if (rekrutacja == null)
				return RedirectToAction("Error", "Home");

			if (!ModelState.IsValid)
			{
				TempData["Message"] = $"Wpisane dane są błędne! {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage))}";
				return RedirectToAction("Index");
			}


            var podanieNaIStopien = _context.PodaniaNaIStopien.SingleOrDefault(p => podanieKandydataViewModel.PodanieNaIStopien.Id == p.Id);
			if (podanieNaIStopien != null)
			{
				podanieNaIStopien.CzyAktywny = true;
				podanieNaIStopien.DataZlozeniaPodania = podanieKandydataViewModel.PodanieNaIStopien.DataZlozeniaPodania;
				podanieNaIStopien.FkIdKandydat = kandydat.Id;
				podanieNaIStopien.FkIdRekrutacja = rekrutacja.Id;
			}

            var podanieNaIIStopien = _context.PodaniaNaIIStopien.SingleOrDefault(p => podanieKandydataViewModel.PodanieNaIIStopien.Id == p.Id);
			if (podanieNaIIStopien != null)
			{
				podanieNaIIStopien.CzyAktywny = true;
				podanieNaIIStopien.DataZlozeniaPodania = podanieKandydataViewModel.PodanieNaIIStopien.DataZlozeniaPodania;
				podanieNaIIStopien.FkIdKandydat = kandydat.Id;
				podanieNaIIStopien.FkIdRekrutacja = rekrutacja.Id;
				podanieNaIIStopien.SredniaZTokuStudiow = podanieKandydataViewModel.PodanieNaIIStopien.SredniaZTokuStudiow;
				podanieNaIIStopien.OcenaZPracyDyplomowej = podanieKandydataViewModel.PodanieNaIIStopien.OcenaZPracyDyplomowej;
			}

            _context.SaveChanges();

            podanieKandydataViewModel.Matura.FkIdPodanieNaIStopien = podanieNaIStopien.Id;

			var matura = (podanieKandydataViewModel.Matura.Id == 0 ? _context.Add(podanieKandydataViewModel.Matura) : _context.Update(podanieKandydataViewModel.Matura)).Entity;
			_context.SaveChanges();

			foreach (var ocena in podanieKandydataViewModel.Oceny)
			{
				ocena.MaturaId = matura.Id;
				if (ocena.Id != 0)
				{
					var existingEntity = _context.Oceny.SingleOrDefault(e => e.Id == ocena.Id);
					_context.Oceny.Update(existingEntity);
				}

				_context.Update(ocena);

			}
			_context.AddRange(podanieKandydataViewModel.Oceny);

			if (podanieKandydataViewModel.DorobkiNaukowe != null)
			{
				foreach (var dorobekNaukowy in podanieKandydataViewModel.DorobkiNaukowe)
				{
					dorobekNaukowy.FkIdPodanieNaIIStopien = podanieNaIIStopien.Id;
					if (dorobekNaukowy.Id != 0)
					{
						var existingEntity = _context.DorobkiNaukowe.SingleOrDefault(e => e.Id == dorobekNaukowy.Id);
						_context.DorobkiNaukowe.Update(existingEntity);
					}

					_context.Update(dorobekNaukowy);
				}
			}

			_context.RemoveRange(_context.DodatkoweOsiagniecia.Where(d => d.FkIdPodanieKandydata == podanieNaIStopien.Id || d.FkIdPodanieKandydata == podanieNaIIStopien.Id));
			if (podanieKandydataViewModel.DodatkoweOsiagniecia != null)
			{
				foreach (var dodatkoweOsiagniecie in podanieKandydataViewModel.DodatkoweOsiagniecia)
				{
					dodatkoweOsiagniecie.FkIdPodanieKandydata = podanieNaIStopien.Id;
					_context.Update(dodatkoweOsiagniecie);
				}

				// deep copy!
				foreach (var dodatkoweOsiagniecie in podanieKandydataViewModel.DodatkoweOsiagniecia.Select(d => new DodatkoweOsiagniecieModel()
				{
					DataZdobycia = d.DataZdobycia,
					Opis = d.Opis,
					FkIdPrzelicznikOsiagniec = d.FkIdPrzelicznikOsiagniec
				}))
				{
					dodatkoweOsiagniecie.FkIdPodanieKandydata = podanieNaIIStopien.Id;
					_context.Update(dodatkoweOsiagniecie);
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
