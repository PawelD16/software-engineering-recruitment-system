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
    public class RekrutacjaController : Controller
    {
        private readonly MyDbContext _context;

        public RekrutacjaController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Rekrutacja
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rekrutacje.ToListAsync());
        }

        // GET: Rekrutacja/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rekrutacjaViewModel = await _context.Rekrutacje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rekrutacjaViewModel == null)
            {
                return NotFound();
            }

            return View(rekrutacjaViewModel);
        }

        // GET: Rekrutacja/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rekrutacja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataOtwarciaRekrutacji,DataZamknieciaRekrutacji,DataZamknieciaPrzyjec,StopienStudiow,StatusRekrutacji,SemestrRekrutacji")] RekrutacjaViewModel rekrutacjaViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rekrutacjaViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rekrutacjaViewModel);
        }

        // GET: Rekrutacja/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rekrutacjaViewModel = await _context.Rekrutacje.FindAsync(id);
            if (rekrutacjaViewModel == null)
            {
                return NotFound();
            }
            return View(rekrutacjaViewModel);
        }

        // POST: Rekrutacja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DataOtwarciaRekrutacji,DataZamknieciaRekrutacji,DataZamknieciaPrzyjec,StopienStudiow,StatusRekrutacji,SemestrRekrutacji")] RekrutacjaViewModel rekrutacjaViewModel)
        {
            if (id != rekrutacjaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rekrutacjaViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RekrutacjaViewModelExists(rekrutacjaViewModel.Id))
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
            return View(rekrutacjaViewModel);
        }

        // GET: Rekrutacja/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rekrutacjaViewModel = await _context.Rekrutacje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rekrutacjaViewModel == null)
            {
                return NotFound();
            }

            return View(rekrutacjaViewModel);
        }

        // POST: Rekrutacja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var rekrutacjaViewModel = await _context.Rekrutacje.FindAsync(id);
            _context.Rekrutacje.Remove(rekrutacjaViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RekrutacjaViewModelExists(long id)
        {
            return _context.Rekrutacje.Any(e => e.Id == id);
        }
    }
}
