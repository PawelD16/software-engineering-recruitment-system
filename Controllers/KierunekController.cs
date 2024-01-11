using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;

namespace projektowaniaOprogramowania.Controllers
{
    public class KierunekController : Controller
    {
        private readonly MyDbContext _context;

        public KierunekController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Kierunek
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Kierunki.Include(k => k.Przelicznik).Include(k => k.Wydzial);
            return View(await myDbContext.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,NazwaKierunku,SkrotKierunku,ProfilKierunku,Czesne,CzesneDlaCudzoziemcow,CzasTrwaniaWSemestrach,Dyscyplina,Jezyk,StopienStudiow,TrybStudiowania,FkIdWydzial,FkIdPrzelicznik")] KierunekViewModel kierunekViewModel)
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
        public async Task<IActionResult> Edit(long id, [Bind("Id,NazwaKierunku,SkrotKierunku,ProfilKierunku,Czesne,CzesneDlaCudzoziemcow,CzasTrwaniaWSemestrach,Dyscyplina,Jezyk,StopienStudiow,TrybStudiowania,FkIdWydzial,FkIdPrzelicznik")] KierunekViewModel kierunekViewModel)
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
