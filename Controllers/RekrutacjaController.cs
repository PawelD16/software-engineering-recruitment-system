using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.Services.Recrutation;
using projektowaniaOprogramowania.ViewModels;

namespace projektowaniaOprogramowania.Controllers
{
    public class RekrutacjaController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IRecruitmentValidationService _recruitmentValidationService;

        public RekrutacjaController(MyDbContext context, IRecruitmentValidationService recruitmentValidationService)
        {
            _context = context;
            _recruitmentValidationService = recruitmentValidationService;
        }

        // GET: Rekrutacja
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rekrutacje.OrderBy((rekrutacja)=>rekrutacja.DataOtwarciaRekrutacji).ToListAsync());
        }

        public async Task<IActionResult> IndexWithError(string error)
        {
            ViewData["error-message"] = error;
            return View("Index",await _context.Rekrutacje.OrderBy((rekrutacja)=>rekrutacja.DataOtwarciaRekrutacji).ToListAsync());
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
            
            if (!ModelState.IsValid)
            {
                return View(rekrutacjaViewModel);
            }

            var (validity, errorMessage) = await _recruitmentValidationService.IsRecruitmentValid(rekrutacjaViewModel);
            if (!validity)
            {
                ViewData["error-message"] = errorMessage;
                return await IndexWithError(errorMessage);
            }
            try
            {
                // rekrutacjaViewModel.Id += 1;
                Console.WriteLine(rekrutacjaViewModel);
                _context.Update(rekrutacjaViewModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException err)
            {
                Console.WriteLine(err);
                if (!RekrutacjaViewModelExists(rekrutacjaViewModel.Id))
                {
                    return NotFound();
                }
                    
                throw;
                    
            }
                
                
            return RedirectToAction("Index");
           
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
