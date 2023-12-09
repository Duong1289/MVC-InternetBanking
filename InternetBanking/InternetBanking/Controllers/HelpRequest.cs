using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Models;

namespace InternetBanking.Controllers
{
    public class HelpRequest : Controller
    {
        private readonly InternetBankingContext _context;

        public HelpRequest(InternetBankingContext context)
        {
            _context = context;
        }

        // GET: HelpRequest
        public async Task<IActionResult> Index()
        {
              return _context.HelpRequests != null ? 
                          View(await _context.HelpRequests.ToListAsync()) :
                          Problem("Entity set 'InternetBankingContext.HelpRequests'  is null.");
        }

        // GET: HelpRequest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HelpRequests == null)
            {
                return NotFound();
            }

            var helpRequest = await _context.HelpRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helpRequest == null)
            {
                return NotFound();
            }

            return View(helpRequest);
        }

        // GET: HelpRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HelpRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountId,CustomerPersonalId,EmployeeId,RequestTypeId,Content,CreatedDate,Status")] HelpRequest helpRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(helpRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(helpRequest);
        }

        // GET: HelpRequest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HelpRequests == null)
            {
                return NotFound();
            }

            var helpRequest = await _context.HelpRequests.FindAsync(id);
            if (helpRequest == null)
            {
                return NotFound();
            }
            return View(helpRequest);
        }

        // POST: HelpRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,AccountId,CustomerPersonalId,EmployeeId,RequestTypeId,Content,CreatedDate,Status")] HelpRequest helpRequest)
        {
           if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helpRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelpRequestExists(helpRequest.Id))
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
            return View(helpRequest);
        }

        // GET: HelpRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HelpRequests == null)
            {
                return NotFound();
            }

            var helpRequest = await _context.HelpRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helpRequest == null)
            {
                return NotFound();
            }

            return View(helpRequest);
        }

        // POST: HelpRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.HelpRequests == null)
            {
                return Problem("Entity set 'InternetBankingContext.HelpRequests'  is null.");
            }
            var helpRequest = await _context.HelpRequests.FindAsync(id);
            if (helpRequest != null)
            {
                _context.HelpRequests.Remove(helpRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HelpRequestExists(int? id)
        {
          return (_context.HelpRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
