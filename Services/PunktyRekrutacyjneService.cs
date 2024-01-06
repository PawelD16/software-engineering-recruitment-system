using System;
using System.Collections.Generic;
using System.Linq;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowania.Services
{
	public class PunktyRekrutacyjneService
	{
		private readonly MyDbContext _context;
		private readonly Random _random = new();

		private readonly int MAX_RECRUITATION_POINTS = 530;
		private readonly int MIN_RECRUITATION_POINTS = 0;

		public PunktyRekrutacyjneService(MyDbContext context)
		{
			_context = context;
		}

		public (int, int) PrzewidujPrzelicznikDlaKierunkuISzanseDostaniaSie(KierunekViewModel kierunek)
		{
			// mock

			int points = GetBetweenMinAndMaxRecPoints();

			return (points, points / MAX_RECRUITATION_POINTS);
		}

		public List<KierunekViewModel> WyliczPrzelicznikKandydataDlaKazdegoKierunku(KandydatViewModel candidate)
		{
			// 1 aktywna rekrutacja
			RekrutacjaViewModel rekrutacja = _context.Rekrutacje
				.First(rekrutacja => rekrutacja.StatusRekrutacji == StatusRekrutacji.Otwarta);

			// 1 podanie per rekrutacja
			PodanieKandydataViewModel podanieKandydata = _context.PodaniaKandydatow
				.First(pk => pk.FkIdKandydat == candidate.Id && pk.FkIdRekrutacja == rekrutacja.Id);

			if (podanieKandydata == null)
				return null;

			List<DodatkoweOsiagniecieViewModel> dodatkoweOsiagniecia = _context.DodatkoweOsiagniecia
				.Where(o => o.FkIdPodanieKandydata == podanieKandydata.Id)
				.ToList();

			List<DorobekNaukowyViewModel> dorobkiNaukowe = _context.DorobkiNaukowe
				.Where(o => o.FkIdPodanieNaIIStopien == podanieKandydata.Id)
				.ToList();

			MaturaViewModel matura = _context.Matury
				.First(m => m.FkIdPodanieNaIStopien == podanieKandydata.Id);

			var kierunki = _context.Kierunki.ToList();

			foreach (KierunekViewModel kierunek in kierunki)
			{
				kierunek.CalculatedRecruitationPoints = PrzeliczPunktyDlaKierunku(
						kierunek, matura, dorobkiNaukowe, dodatkoweOsiagniecia, podanieKandydata
					);

				kierunek.CalculatedProbabilityOfSucessfulRecruitation = PrzewidujPrzelicznikDlaKierunkuISzanseDostaniaSie(kierunek).Item2;
			}

			return kierunki;
		}

		private int PrzeliczPunktyDlaKierunku(
			KierunekViewModel kierunek, 
			MaturaViewModel matura, 
			List<DorobekNaukowyViewModel> dorobkiNaukowe, 
			List<DodatkoweOsiagniecieViewModel> dodatkoweOsiagniecia,
			PodanieKandydataViewModel podanie)
		{
			if (kierunek == null)
				return 0;

			if (kierunek.StopienStudiow == StopienStudiow.Pierwszy && _context.PodaniaNaIStopien.First(p => p.Id == podanie.Id) != null)
				return PoliczPunktyDlaKierunkuIStopnia(kierunek, matura, dodatkoweOsiagniecia);

			if (kierunek.StopienStudiow == StopienStudiow.Drugi && _context.PodaniaNaIIStopien.First(p => p.Id == podanie.Id) != null)
				return PoliczPunktyDlaKierunkuIIStopnia(dorobkiNaukowe);

			return 0;
		}

		private int PoliczPunktyDlaKierunkuIIStopnia(List<DorobekNaukowyViewModel> dorobkiNaukowe)
		{
			// mock
			return GetBetweenMinAndMaxRecPoints();
		}

		private int PoliczPunktyDlaKierunkuIStopnia(KierunekViewModel kierunek, MaturaViewModel matura, List<DodatkoweOsiagniecieViewModel> dodatkoweOsiagniecia)
		{
			// mock
			return GetBetweenMinAndMaxRecPoints();
		}

		private int GetBetweenMinAndMaxRecPoints()
		{
			return _random.Next(MIN_RECRUITATION_POINTS, MAX_RECRUITATION_POINTS);
		}
	}
}
