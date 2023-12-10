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
    public class HelpRequestTypeController : Controller
    {
        private readonly InternetBankingContext _context;

        public HelpRequestTypeController(InternetBankingContext context)
        {
            _context = context;
        }

        // GET: HelpRequestType
        public async Task<IActionResult> Index()
        {
              return _context.HelpRequestsTypes != null ? 
                          View(await _context.HelpRequestsTypes.ToListAsync()) :
                          Problem("Entity set 'InternetBankingContext.HelpRequestsTypes'  is null.");
        }

        // GET: HelpRequestType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HelpRequestsTypes == null)
            {
                return NotFound();
            }

            var helpRequestType = await _context.HelpRequestsTypes
                .FirstOrDefaultAsync(m => m.RequestTypeId == id);
            if (helpRequestType == null)
            {
                return NotFound();
            }

            return View(helpRequestType);
        }

        // GET: HelpRequestType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HelpRequestType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestTypeId,ServiceName,Description")] HelpRequestType helpRequestType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(helpRequestType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(helpRequestType);
        }

        // GET: HelpRequestType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HelpRequestsTypes == null)
            {
                return NotFound();
            }

            var helpRequestType = await _context.HelpRequestsTypes.FindAsync(id);
            if (helpRequestType == null)
            {
                return NotFound();
            }
            return View(helpRequestType);
        }

        // POST: HelpRequestType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("RequestTypeId,ServiceName,Description")] HelpRequestType helpRequestType)
        {
            if (id != helpRequestType.RequestTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helpRequestType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelpRequestTypeExists(helpRequestType.RequestTypeId))
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
            return View(helpRequestType);
        }

        // GET: HelpRequestType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HelpRequestsTypes == null)
            {
                return NotFound();
            }

            var helpRequestType = await _context.HelpRequestsTypes
                .FirstOrDefaultAsync(m => m.RequestTypeId == id);
            if (helpRequestType == null)
            {
                return NotFound();
            }

            return View(helpRequestType);
        }

        // POST: HelpRequestType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.HelpRequestsTypes == null)
            {
                return Problem("Entity set 'InternetBankingContext.HelpRequestsTypes'  is null.");
            }
            var helpRequestType = await _context.HelpRequestsTypes.FindAsync(id);
            if (helpRequestType != null)
            {
                _context.HelpRequestsTypes.Remove(helpRequestType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HelpRequestTypeExists(int? id)
        {
          return (_context.HelpRequestsTypes?.Any(e => e.RequestTypeId == id)).GetValueOrDefault();
        }
    }
}
