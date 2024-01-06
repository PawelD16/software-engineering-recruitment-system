using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.ViewModels;

namespace projektowaniaOprogramowania.Controllers
{
    public class PredykcjaController : Controller
    {
        private readonly MyDbContext _context;

        public PredykcjaController(MyDbContext context)
        {
            _context = context;
        }

        // GET: MaturaViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Matury.ToListAsync());
        }

        // GET: MaturaViewModels/Details/
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maturaViewModel = await _context.Matury
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maturaViewModel == null)
            {
                return NotFound();
            }

            return View(maturaViewModel);
        }

        // GET: MaturaViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaturaViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataPrzystapieniaDoMatury")] MaturaViewModel maturaViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maturaViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maturaViewModel);
        }

        // GET: MaturaViewModels/Edit/5
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

        // POST: MaturaViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: MaturaViewModels/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maturaViewModel = await _context.Matury
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maturaViewModel == null)
            {
                return NotFound();
            }

            return View(maturaViewModel);
        }

        // POST: MaturaViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var maturaViewModel = await _context.Matury.FindAsync(id);
            _context.Matury.Remove(maturaViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaturaViewModelExists(long id)
        {
            return _context.Matury.Any(e => e.Id == id);
        }
    }
}
