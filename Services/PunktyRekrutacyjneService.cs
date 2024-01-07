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

		public (int, float) PrzewidujPrzelicznikDlaKierunkuISzanseDostaniaSie(KierunekViewModel kierunek)
		{
			// mock

			int points = getRandomRecruitationPoints();

			return (points, (points * kierunek.CalculatedRecruitationPoints * 100F) / (MAX_RECRUITATION_POINTS * MAX_RECRUITATION_POINTS * 1.0F));
		}

		public KierunekViewModel WyliczPrzelicznikKandydataDlaKierunku(KandydatViewModel kandydat, KierunekViewModel kierunek)
		{
			var (_, podaniaKandydata) = GetActiveRekrutacjaAndPodaniaKandydata(kandydat);

			if (podaniaKandydata == null)
				return null;

			foreach (var podanieKandydata in podaniaKandydata)
			{
				var (dodatkoweOsiagniecia, dorobkiNaukowe, matura) = GetDodatkoweOsiagnieciaAndDorobekNaukowyAndMatura(podanieKandydata);

				kierunek.CalculatedRecruitationPoints = PrzeliczPunktyDlaKierunku(
						kierunek, matura, dorobkiNaukowe, dodatkoweOsiagniecia, podanieKandydata
					);

				kierunek.CalculatedProbabilityOfSucessfulRecruitation = PrzewidujPrzelicznikDlaKierunkuISzanseDostaniaSie(kierunek).Item2;
				
			}

			return kierunek;
		}

		public List<KierunekViewModel> WyliczPrzelicznikKandydataDlaKazdegoKierunku(KandydatViewModel kandydat)
		{

			var (_, podaniaKandydata) = GetActiveRekrutacjaAndPodaniaKandydata(kandydat);

			if (podaniaKandydata == null)
				return null;

			var kierunki = _context.Kierunki.ToList();

			foreach (var podanieKandydata in podaniaKandydata)
			{
				var (dodatkoweOsiagniecia, dorobkiNaukowe, matura) = GetDodatkoweOsiagnieciaAndDorobekNaukowyAndMatura(podanieKandydata);	

				foreach (KierunekViewModel kierunek in kierunki)
				{
					kierunek.CalculatedRecruitationPoints = PrzeliczPunktyDlaKierunku(
							kierunek, matura, dorobkiNaukowe, dodatkoweOsiagniecia, podanieKandydata
						);

					kierunek.CalculatedProbabilityOfSucessfulRecruitation = PrzewidujPrzelicznikDlaKierunkuISzanseDostaniaSie(kierunek).Item2;
				}
			}

			return kierunki;
		}

		private (RekrutacjaViewModel, List<PodanieKandydataViewModel>) GetActiveRekrutacjaAndPodaniaKandydata(KandydatViewModel kandydat)
		{
			if (kandydat == null)
				return (null, new List<PodanieKandydataViewModel>());
			// 1 aktywna rekrutacja
			RekrutacjaViewModel rekrutacja = _context.Rekrutacje
				.FirstOrDefault(rekrutacja => rekrutacja.StatusRekrutacji == StatusRekrutacji.Otwarta);

			// podanie kandydata do 2 sztuk, po jednym na każdy z stopniów
			List<PodanieKandydataViewModel> podaniaKandydata = rekrutacja == null ? null : _context.PodaniaKandydatow
				.Where(pk => pk.FkIdKandydat == kandydat.Id && pk.FkIdRekrutacja == rekrutacja.Id)
				.ToList();

			return (rekrutacja, podaniaKandydata);
		}

		private (List<DodatkoweOsiagniecieViewModel>, List<DorobekNaukowyViewModel>, MaturaViewModel) GetDodatkoweOsiagnieciaAndDorobekNaukowyAndMatura(PodanieKandydataViewModel podanieKandydata)
		{
			List<DodatkoweOsiagniecieViewModel> dodatkoweOsiagniecia = _context.DodatkoweOsiagniecia
				.Where(o => o.FkIdPodanieKandydata == podanieKandydata.Id)
				.ToList();

			List<DorobekNaukowyViewModel> dorobkiNaukowe = _context.DorobkiNaukowe
				.Where(o => o.FkIdPodanieNaIIStopien == podanieKandydata.Id)
				.ToList();

			MaturaViewModel matura = _context.Matury
				.SingleOrDefault(m => m.FkIdPodanieNaIStopien == podanieKandydata.Id);

			return (dodatkoweOsiagniecia, dorobkiNaukowe, matura);
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

			if (kierunek.StopienStudiow == StopienStudiow.Pierwszy && _context.PodaniaNaIStopien.FirstOrDefault(p => p.Id == podanie.Id) != null)
				return PoliczPunktyDlaKierunkuIStopnia(kierunek, matura, dodatkoweOsiagniecia);

			if (kierunek.StopienStudiow == StopienStudiow.Drugi && _context.PodaniaNaIIStopien.FirstOrDefault(p => p.Id == podanie.Id) != null)
				return PoliczPunktyDlaKierunkuIIStopnia(dorobkiNaukowe);

			return kierunek.CalculatedRecruitationPoints;
		}

		private int PoliczPunktyDlaKierunkuIIStopnia(List<DorobekNaukowyViewModel> dorobkiNaukowe)
		{
			// mock
			return getRandomRecruitationPoints();
		}

		private int PoliczPunktyDlaKierunkuIStopnia(KierunekViewModel kierunek, MaturaViewModel matura, List<DodatkoweOsiagniecieViewModel> dodatkoweOsiagniecia)
		{
			if (kierunek == null || matura == null)
				return 0;
			// mock
			return getRandomRecruitationPoints();
		}

		private int getRandomRecruitationPoints()
		{
			return _random.Next((MAX_RECRUITATION_POINTS - MIN_RECRUITATION_POINTS) / 2, MAX_RECRUITATION_POINTS);
		}
	}
}
