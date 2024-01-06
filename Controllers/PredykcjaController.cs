using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.Services;
using projektowaniaOprogramowania.ViewModels;
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

        public IActionResult Index()
        {
            KandydatViewModel candidate = _context.Kandydaci.First(candidate => candidate.Id == HttpContext.Session.GetLong("UserId"));
            if (candidate == null)
            {
				return RedirectToAction("Error", "Home");
			}

            var kierunki = _punktyRekrutacyjneService.WyliczPrzelicznikKandydataDlaKazdegoKierunku(candidate);

            if (kierunki == null)
				// formularz!


			kierunki = kierunki.Where(k => k.CalculatedProbabilityOfSucessfulRecruitation >= 50).ToList();

			return View(kierunki);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maturaViewModel = await _context.Matury.FindAsync(id);
            if (maturaViewModel == null)
            {
                return NotFound();
            }
            return View(maturaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DataPrzystapieniaDoMatury")] MaturaViewModel maturaViewModel)
        {
            if (id != maturaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maturaViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaturaViewModelExists(maturaViewModel.Id))
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
            return View(maturaViewModel);
        }

        private bool MaturaViewModelExists(long id)
        {
            return _context.Matury.Any(e => e.Id == id);
        }
    }
}
