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

		public (int, float) PrzewidujPrzelicznikDlaKierunkuISzanseDostaniaSie(KierunekModel kierunek)
		{
			// mock

			int points = getRandomRecruitationPoints();

			return (points, (points * kierunek.CalculatedRecruitationPoints * 100F) / (MAX_RECRUITATION_POINTS * MAX_RECRUITATION_POINTS * 1.0F));
		}

		public KierunekModel WyliczPrzelicznikKandydataDlaKierunku(KandydatModel kandydat, KierunekModel kierunek)
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

		public virtual List<KierunekModel> WyliczPrzelicznikKandydataDlaKazdegoKierunku(KandydatModel kandydat)
		{

			var (_, podaniaKandydata) = GetActiveRekrutacjaAndPodaniaKandydata(kandydat);

			if (podaniaKandydata == null)
				return null;

			var kierunki = _context.Kierunki.ToList();

			foreach (var podanieKandydata in podaniaKandydata)
			{
				var (dodatkoweOsiagniecia, dorobkiNaukowe, matura) = GetDodatkoweOsiagnieciaAndDorobekNaukowyAndMatura(podanieKandydata);	

				foreach (KierunekModel kierunek in kierunki)
				{
					kierunek.CalculatedRecruitationPoints = PrzeliczPunktyDlaKierunku(
							kierunek, matura, dorobkiNaukowe, dodatkoweOsiagniecia, podanieKandydata
						);

					kierunek.CalculatedProbabilityOfSucessfulRecruitation = PrzewidujPrzelicznikDlaKierunkuISzanseDostaniaSie(kierunek).Item2;
				}
			}

			return kierunki;
		}

		private (RekrutacjaModel, List<PodanieKandydataModel>) GetActiveRekrutacjaAndPodaniaKandydata(KandydatModel kandydat)
		{
			if (kandydat == null) // nie dodawanie null checka w diagramie sekwencji, gdy wczesniej sprawdzalismy czy jest nullem! Ten null check tylko profilaktycznie
				return (null, new List<PodanieKandydataModel>());
			// 1 aktywna rekrutacja
			RekrutacjaModel rekrutacja = _context.Rekrutacje
				.FirstOrDefault(rekrutacja => rekrutacja.StatusRekrutacji == StatusRekrutacji.Otwarta);

			// podanie kandydata do 2 sztuk, po jednym na każdy z stopniów
			List<PodanieKandydataModel> podaniaKandydata = rekrutacja == null ? null : _context.PodaniaKandydatow
				.Where(pk => pk.FkIdKandydat == kandydat.Id && pk.FkIdRekrutacja == rekrutacja.Id)
				.ToList();

			return (rekrutacja, podaniaKandydata);
		}

		private (List<DodatkoweOsiagniecieModel>, List<DorobekNaukowyModel>, MaturaModel) GetDodatkoweOsiagnieciaAndDorobekNaukowyAndMatura(PodanieKandydataModel podanieKandydata)
		{
			List<DodatkoweOsiagniecieModel> dodatkoweOsiagniecia = _context.DodatkoweOsiagniecia
				.Where(o => o.FkIdPodanieKandydata == podanieKandydata.Id)
				.ToList();

			List<DorobekNaukowyModel> dorobkiNaukowe = _context.DorobkiNaukowe
				.Where(o => o.FkIdPodanieNaIIStopien == podanieKandydata.Id)
				.ToList();

			MaturaModel matura = _context.Matury
				.SingleOrDefault(m => m.FkIdPodanieNaIStopien == podanieKandydata.Id);

            return (dodatkoweOsiagniecia, dorobkiNaukowe, matura);
		}

		public (List<DodatkoweOsiagniecieModel>, List<DorobekNaukowyModel>, MaturaModel, PodanieKandydataModel) GetAllPodanieKandydataInfo(PodanieKandydataModel podanieKandydata)
		{
            List<DodatkoweOsiagniecieModel> dodatkoweOsiagniecia = _context.DodatkoweOsiagniecia
                .Where(o => o.FkIdPodanieKandydata == podanieKandydata.Id)
                .ToList();

            List<DorobekNaukowyModel> dorobkiNaukowe = _context.DorobkiNaukowe
                .Where(o => o.FkIdPodanieNaIIStopien == podanieKandydata.Id)
                .ToList();

            MaturaModel matura = _context.Matury
                .SingleOrDefault(m => m.FkIdPodanieNaIStopien == podanieKandydata.Id);

			return (dodatkoweOsiagniecia, dorobkiNaukowe, matura, podanieKandydata);
        }

		private int PrzeliczPunktyDlaKierunku(
			KierunekModel kierunek,
			MaturaModel matura,
			List<DorobekNaukowyModel> dorobkiNaukowe,
			List<DodatkoweOsiagniecieModel> dodatkoweOsiagniecia,
			PodanieKandydataModel podanie)
		{
			if (kierunek == null)
				return 0;

			if (kierunek.StopienStudiow == StopienStudiow.Pierwszy && _context.PodaniaNaIStopien.FirstOrDefault(p => p.Id == podanie.Id) != null)
				return PoliczPunktyDlaKierunkuIStopnia(kierunek, matura, dodatkoweOsiagniecia);

			if (kierunek.StopienStudiow == StopienStudiow.Drugi && _context.PodaniaNaIIStopien.FirstOrDefault(p => p.Id == podanie.Id) != null)
				return PoliczPunktyDlaKierunkuIIStopnia(dorobkiNaukowe);

			return kierunek.CalculatedRecruitationPoints;
		}

		private int PoliczPunktyDlaKierunkuIIStopnia(List<DorobekNaukowyModel> dorobkiNaukowe)
		{
			// mock
			return getRandomRecruitationPoints();
		}

		private int PoliczPunktyDlaKierunkuIStopnia(KierunekModel kierunek, MaturaModel matura, List<DodatkoweOsiagniecieModel> dodatkoweOsiagniecia)
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
