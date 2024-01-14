using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.Users;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace projektowaniaOprogramowania.Services
{
    public class PodanieService
    {
        private readonly MyDbContext _context;

        public PodanieService(MyDbContext context)
        {
            _context = context;
        }
        public PodanieKandydataModel GetPodanieKandydata(KandydatModel kandydat)
        {
            var (_, podaniaKandydata) = GetActiveRekrutacjaAndPodaniaKandydata(kandydat);

            if (podaniaKandydata == null)
            {
                return null;
            }
            var podanie = podaniaKandydata.FirstOrDefault(p => p.FkIdKandydat == kandydat.Id);
            //if (podanie == null)
            //    throw new System.Exception();
            return podanie;
        }

        private (RekrutacjaModel, List<PodanieKandydataModel>) GetActiveRekrutacjaAndPodaniaKandydata(KandydatModel kandydat)
        {
            if (kandydat == null)
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
    }
}
