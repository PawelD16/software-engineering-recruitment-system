using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.Services;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowania.Controllers
{
    public class KierunekNaPodaniuController : Controller
    {
        private readonly MyDbContext _context;
        private readonly PunktyRekrutacyjneService _punktyRekrutacyjneService;

        public KierunekNaPodaniuController(MyDbContext context, PunktyRekrutacyjneService punktyRekrutacyjneService)
        {
            _context = context;
            _punktyRekrutacyjneService = punktyRekrutacyjneService;
        }

        // GET: Kierunek
        public async Task<IActionResult> Index()
        {
            if (AktualnyKandydat() == null)
            {
                return View("BrakDanychDoPodania");
            }
            
            
            var podanie =  getCurrentPodanie();
            
            if (getCurrentPodanie().StatusPodania == StatusPodania.Zlozone)
            {
                // var podanie = getCurrentPodanie();
                podanie.StatusPodania = StatusPodania.Niezlozone;
                 _context.PodaniaKandydatow.Update(podanie);
                 await _context.SaveChangesAsync();
                return View("PodanieJuzZlozone");
            }
            
            var myDbContext = _context.Kierunki
                .Include(k => k.Przelicznik)
                .Include(k => k.Wydzial)
                .Include(k => k.Przelicznik.PrzelicznikPrzemiotu);
            
            var przedmioty = _context.PrzelicznikiPrzedmiotow
                .Include(e => e.Przedmiot);

           
            
            ViewData["przedmioty"] = await przedmioty.ToListAsync();
            ViewData["kierunki"] =  getSelectedKierunki();
            ViewData["podanie"] = podanie;
            ViewData["punkty"] = PunktyNaKierunki();
            ViewData["rekrutacja"] = _context.Rekrutacje.Where(e=>e.Id==podanie.FkIdRekrutacja).First();
            ViewData["kandydat"] = AktualnyKandydat();
            
            
            return View(await myDbContext.ToListAsync());
        }

        private (KierunekModel, int) PunktyNaKierunek(KierunekModel kierunekViewModel)
        {
            var random = new Random();
            return (kierunekViewModel, random.Next(100, 500));
        }

        private List<(KierunekModel, int)> PunktyNaKierunki()
        {
            var random = new Random();
            return _context.Kierunki.Select(PunktyNaKierunek).ToList();
        }

        private KandydatModel AktualnyKandydat()
        {
            var session = HttpContext.Session.GetLong("UserId");
            Console.WriteLine(session);
            if (session == null)
            {
                return null;
            }
            
            KandydatModel kandydat = _context.Kandydaci
                .SingleOrDefault(k => k.Id == HttpContext.Session.GetLong("UserId"));
            Console.WriteLine(kandydat.Nazwisko);
            return kandydat;
        }

        public async Task<IActionResult> WyslijPodanie()
        {
            var kierunki = _context.KierunkiNaPodaniu
                .Where(el => el.FkIdPodanieKandydata == getCurrentPodanie().Id)
                .ToList();

            ///For testing purpouses only
            var podanie = getCurrentPodanie();
            podanie.StatusPodania = StatusPodania.Zlozone;
            _context.PodaniaKandydatow.Update(podanie);
            
            foreach (var kierunek in kierunki)
            {
                _context.KierunkiNaPodaniu.Remove(kierunek);
            }
            await _context.SaveChangesAsync();

            
            Thread.Sleep(5000);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> WyczyscKierunki()
        {
           var kierunki = _context.KierunkiNaPodaniu.Where(el => el.FkIdPodanieKandydata == getCurrentPodanie().Id).ToList();
           foreach (var kierunek in kierunki)
           {
               _context.KierunkiNaPodaniu.Remove(kierunek);
               await _context.SaveChangesAsync();
           }

           return RedirectToAction(nameof(Index));

        }
        
        public async Task<IActionResult> AddKierunekToPodanie(int id)
        {
            var isAlreadyPresent = _context.KierunkiNaPodaniu.ToList()
                .Find(e => e.FkIdKierunek == id);
            if (isAlreadyPresent != null)
            {
                return RedirectToAction(nameof(Index));
            }
            _context.KierunkiNaPodaniu.Add(new KierunekNaPodaniuModel
            {
                FkIdPodanieKandydata = this.getCurrentPodanie().Id,
                FkIdKierunek = id,
                Priorytet = _context.KierunkiNaPodaniu.ToList().Count()
                
            });
            await _context.SaveChangesAsync();
            Console.WriteLine("Dodano Kierunek do podania");
            
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> RemoveKierunekFromPodanie(int id)
        {
            var entity = _context.KierunkiNaPodaniu.Where(e => e.FkIdKierunek == id).ToList();
            if (!entity.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            _context.KierunkiNaPodaniu.Remove(entity[0]);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private PodanieKandydataModel getCurrentPodanie()
        {
            var kandydat = AktualnyKandydat();
            return _context.PodaniaKandydatow.Where((podanie) => podanie.FkIdKandydat == kandydat.Id)
                .Where((podanie) => (podanie.CzyAktywny)).First();
        }

        private List<KierunekModel> getSelectedKierunki()
        {
            var kierunki = _context.Kierunki.ToList();
            var kierunkiPodania =  _context.KierunkiNaPodaniu
                .Where(e=>e.PodanieKandydata.Id == getCurrentPodanie().Id)
                .Select(e=>e.FkIdKierunek)
                .ToList();
            
            return kierunki.Join(kierunkiPodania,
                kierunki => kierunki.Id, 
                kierunkiPodania => kierunkiPodania,
                (kierunki, kierunkiNaPodaniu) => kierunki).ToList();

        }

        // GET: Kierunek/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kierunekViewModel = await _context.Kierunki
                .Include(k => k.Przelicznik)
                .Include(k => k.Wydzial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kierunekViewModel == null)
            {
                return NotFound();
            }

            return View(kierunekViewModel);
        }

        // GET: Kierunek/Create
        public IActionResult Create()
        {
            ViewData["FkIdPrzelicznik"] = new SelectList(_context.PrzelicznikiKierunkowe, "Id", "Id");
            ViewData["FkIdWydzial"] = new SelectList(_context.Wydzialy, "Id", "NazwaWydzialu");
            return View();
        }

        // POST: Kierunek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazwaKierunku,SkrotKierunku,ProfilKierunku,Czesne,CzesneDlaCudzoziemcow,CzasTrwaniaWSemestrach,Dyscyplina,Jezyk,StopienStudiow,TrybStudiowania,FkIdWydzial,FkIdPrzelicznik")] KierunekModel kierunekViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kierunekViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkIdPrzelicznik"] = new SelectList(_context.PrzelicznikiKierunkowe, "Id", "Id", kierunekViewModel.FkIdPrzelicznik);
            ViewData["FkIdWydzial"] = new SelectList(_context.Wydzialy, "Id", "NazwaWydzialu", kierunekViewModel.FkIdWydzial);
            return View(kierunekViewModel);
        }

        // GET: Kierunek/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kierunekViewModel = await _context.Kierunki.FindAsync(id);
            if (kierunekViewModel == null)
            {
                return NotFound();
            }
            ViewData["FkIdPrzelicznik"] = new SelectList(_context.PrzelicznikiKierunkowe, "Id", "Id", kierunekViewModel.FkIdPrzelicznik);
            ViewData["FkIdWydzial"] = new SelectList(_context.Wydzialy, "Id", "NazwaWydzialu", kierunekViewModel.FkIdWydzial);
            return View(kierunekViewModel);
        }

        // POST: Kierunek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,NazwaKierunku,SkrotKierunku,ProfilKierunku,Czesne,CzesneDlaCudzoziemcow,CzasTrwaniaWSemestrach,Dyscyplina,Jezyk,StopienStudiow,TrybStudiowania,FkIdWydzial,FkIdPrzelicznik")] KierunekModel kierunekViewModel)
        {
            if (id != kierunekViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kierunekViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KierunekViewModelExists(kierunekViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkIdPrzelicznik"] = new SelectList(_context.PrzelicznikiKierunkowe, "Id", "Id", kierunekViewModel.FkIdPrzelicznik);
            ViewData["FkIdWydzial"] = new SelectList(_context.Wydzialy, "Id", "NazwaWydzialu", kierunekViewModel.FkIdWydzial);
            return View(kierunekViewModel);
        }

        // GET: Kierunek/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kierunekViewModel = await _context.Kierunki
                .Include(k => k.Przelicznik)
                .Include(k => k.Wydzial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kierunekViewModel == null)
            {
                return NotFound();
            }

            return View(kierunekViewModel);
        }

        // POST: Kierunek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var kierunekViewModel = await _context.Kierunki.FindAsync(id);
            _context.Kierunki.Remove(kierunekViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KierunekViewModelExists(long id)
        {
            return _context.Kierunki.Any(e => e.Id == id);
        }
    }
}
