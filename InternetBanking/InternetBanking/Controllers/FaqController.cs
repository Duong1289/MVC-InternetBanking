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
    [Authorize]
    public class FaqController : Controller
    {
        private readonly InternetBankingContext _context;

        public FaqController(InternetBankingContext context)
        {
            _context = context;
        }

        // GET: Faq
        public async Task<IActionResult> Index()
        {
              return _context.FAQ != null ? 
                          View(await _context.FAQ.ToListAsync()) :
                          Problem("Entity set 'InternetBankingContext.FAQ'  is null.");
        }

        // GET: Faq/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FAQ == null)
            {
                return NotFound();
            }

            var fAQ = await _context.FAQ
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // GET: Faq/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.FAQCategory = await _context.FAQCategories!.ToListAsync();
            return View();
        }

        // POST: Faq/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer,FAQCategoryId")] FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fAQ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fAQ);
        }

        // GET: Faq/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FAQ == null)
            {
                return NotFound();
            }

            var fAQ = await _context.FAQ.FindAsync(id);
            if (fAQ == null)
            {
                return NotFound();
            }
            return View(fAQ);
        }

        // POST: Faq/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Question,Answer,FAQCategoryId")] FAQ fAQ)
        {
            if (id != fAQ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fAQ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FAQExists(fAQ.Id))
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
            return View(fAQ);
        }

        // GET: Faq/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FAQ == null)
            {
                return NotFound();
            }

            var fAQ = await _context.FAQ
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // POST: Faq/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.FAQ == null)
            {
                return Problem("Entity set 'InternetBankingContext.FAQ'  is null.");
            }
            var fAQ = await _context.FAQ.FindAsync(id);
            if (fAQ != null)
            {
                _context.FAQ.Remove(fAQ);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FAQExists(int? id)
        {
          return (_context.FAQ?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
