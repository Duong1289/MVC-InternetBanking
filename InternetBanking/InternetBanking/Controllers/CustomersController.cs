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
using InternetBanking.Service.MailService;

namespace InternetBanking.Controllers
{
    [Authorize(Roles = "Admin, Employee,Customer")]
    public class CustomersController : Controller
    {
        private readonly InternetBankingContext _context;
        private readonly SendBankMailService sendMailService;
        UserManager<InternetBankingUser> _userManager;

        public CustomersController(InternetBankingContext context, SendBankMailService sendMailService, UserManager<InternetBankingUser> userManager)
        {
            _context = context;
            this.sendMailService = sendMailService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DetailsbyCustomer()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var customer = await _context.Customers!
                .Include(c => c.InternetBankingUser)
                .Include(c => c.Accounts)
                .Include(c => c.HelpRequests)
                
                .FirstOrDefaultAsync(m => m.Id == currentUser.Id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Index()
        {
            var internetBankingContext = _context.Customers!.Include(c => c.InternetBankingUser);
            return View(await internetBankingContext.ToListAsync());
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.InternetBankingUser)
                .Include(c => c.Accounts)
                .Include(c=>c.HelpRequests)
                .Include(c => c.Withdraws)
                .Include(c=>c.Deposits)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // GET: Customers/Create
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.InternetBankingUsers, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Create([Bind("Id,PersonalId,Email,Phone,FirstName,LastName,Address,OpenDate,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.InternetBankingUsers, "Id", "Id", customer.Id);
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin, Employee,Customer")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.InternetBankingUsers, "Id", "Id", customer.Id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Employee,Customer")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,PersonalId,Email,Phone,FirstName,LastName,Address,OpenDate,Status")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["Id"] = new SelectList(_context.InternetBankingUsers, "Id", "Id", customer.Id);
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.InternetBankingUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'InternetBankingContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
          return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
