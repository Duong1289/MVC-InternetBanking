﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Models;
using InternetBanking.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using InternetBanking.Areas.Identity.Data;

namespace InternetBanking.Controllers
{
    [Authorize(Roles = "Admin, Employee,Customer")]
    public class AccountsController : Controller
    {
        
        private readonly InternetBankingContext _context;
        private readonly UserManager<InternetBankingUser> _userManager;

        public AccountsController(InternetBankingContext context, UserManager<InternetBankingUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Accounts
        //[Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var myAccount = await _context.Accounts.Include(a => a.AccountType).FirstOrDefaultAsync(a => a.CustomerId == currentUser.Id);

            return View(myAccount);
        }

        // GET: Accounts/Details/5
        [Authorize(Roles = "Admin, Employee,Customer")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.AccountType)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        [Authorize(Roles = "Admin, Employee,Customer")]
        public IActionResult Create()
        {
            ViewData["AccountTypeId"] = new SelectList(_context.AccountTypes, "Id", "TypeName");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer,Admin, Employee")]
        public async Task<IActionResult> Create([Bind("AccountNumber,AccountTypeId,CustomerId,Balance,OpenDate,ExpireDate,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                
                string generatedAccountNumber;
                bool isUnique = false;
                do
                {
                    generatedAccountNumber = GenerateUniqueAccountNumber();
                    isUnique = !_context.Accounts.Any(a => a.AccountNumber == generatedAccountNumber);
                } while (!isUnique);
                account.AccountNumber = generatedAccountNumber;

                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountTypeId"] = new SelectList(_context.AccountTypes, "Id", "TypeName", account.AccountTypeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", account.CustomerId);
            return View(account);
        }
        private string GenerateUniqueAccountNumber()
        {
            Random random = new Random();
            string accountNumber = "1903" + random.Next(10000000, 99999999).ToString();
            return accountNumber;
        }
        // GET: Accounts/Edit/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["AccountTypeId"] = new SelectList(_context.AccountTypes, "Id", "TypeName", account.AccountTypeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", account.CustomerId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Edit(string id, [Bind("AccountNumber,AccountTypeId,CustomerId,Balance,OpenDate,ExpireDate,Status")] Account account)
        {
            if (id != account.AccountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountNumber))
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
            ViewData["AccountTypeId"] = new SelectList(_context.AccountTypes, "Id", "TypeName", account.AccountTypeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", account.CustomerId);
            return View(account);
        }

        // GET: Accounts/Delete/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.AccountType)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'InternetBankingContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(string id)
        {
          return (_context.Accounts?.Any(e => e.AccountNumber == id)).GetValueOrDefault();
        }



    }
}
