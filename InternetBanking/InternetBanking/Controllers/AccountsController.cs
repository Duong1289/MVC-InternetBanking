using System;
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
        UserManager<InternetBankingUser> _userManager;

        public AccountsController(InternetBankingContext context, UserManager<InternetBankingUser> _userManager)
        {
            _context = context;
            this._userManager = _userManager;
        }

        // GET: Accounts
        //[Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Index()
        {
            var internetBankingContext = _context.Accounts!.Include(a => a.AccountType).Include(a => a.Customer);
            return View(await internetBankingContext.ToListAsync());
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

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Types = await _context.AccountTypes!.ToListAsync();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(Account account)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            string generatedAccountNumber;
            bool isUnique = false;
            do
            {
                generatedAccountNumber = GenerateUniqueAccountNumber();
                isUnique = !_context.Accounts!.Any(a => a.AccountNumber == generatedAccountNumber);
            } while (!isUnique);
            account.AccountNumber = generatedAccountNumber;
            account.CustomerId = currentUser.Id;
            account.Balance = 0;
            account.OpenDate = DateTime.Now;
            account.ExpireDate = DateTime.Now.AddYears(5);
            account.Status = true;

            _context.Add(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            
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

        public async Task<IActionResult> EmployeeIndex()
        {
            var internetBankingContext = _context.Accounts!.Include(a => a.AccountType).Include(a => a.Customer);
            return View(await internetBankingContext.ToListAsync());

        }

        public async Task<IActionResult> EmployeeDetail(string id)
        {
            var account  = await _context.Accounts!.Include(a=>a.AccountType).Include(a=>a.Customer).SingleOrDefaultAsync(a=>a.AccountNumber == id);
            return View(account);
        }





    }
}
