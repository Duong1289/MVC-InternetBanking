using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Models;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Controllers
{
    // [Authorize]
    public class FaqCategoryController : Controller
    {
        private readonly InternetBankingContext _context;

        public FaqCategoryController(InternetBankingContext context)
        {
            _context = context;
        }

        // GET: FaqCategory
        public async Task<IActionResult> Index()
        {
              return _context.FAQCategories != null ? 
                          View(await _context.FAQCategories.ToListAsync()) :
                          Problem("Entity set 'InternetBankingContext.FAQCategories'  is null.");
        }

        // GET: FaqCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FAQCategories == null)
            {
                return NotFound();
            }

            var fAQCategory = await _context.FAQCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQCategory == null)
            {
                return NotFound();
            }

            return View(fAQCategory);
        }

        // GET: FaqCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FaqCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName")] FAQCategory fAQCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fAQCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fAQCategory);
        }

        // GET: FaqCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FAQCategories == null)
            {
                return NotFound();
            }

            var fAQCategory = await _context.FAQCategories.FindAsync(id);
            if (fAQCategory == null)
            {
                return NotFound();
            }
            return View(fAQCategory);
        }

        // POST: FaqCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,CategoryName")] FAQCategory fAQCategory)
        {
            if (id != fAQCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fAQCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FAQCategoryExists(fAQCategory.Id))
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
            return View(fAQCategory);
        }

        // GET: FaqCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FAQCategories == null)
            {
                return NotFound();
            }

            var fAQCategory = await _context.FAQCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQCategory == null)
            {
                return NotFound();
            }

            return View(fAQCategory);
        }

        // POST: FaqCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.FAQCategories == null)
            {
                return Problem("Entity set 'InternetBankingContext.FAQCategories'  is null.");
            }
            var fAQCategory = await _context.FAQCategories.FindAsync(id);
            if (fAQCategory != null)
            {
                _context.FAQCategories.Remove(fAQCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FAQCategoryExists(int? id)
        {
          return (_context.FAQCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
