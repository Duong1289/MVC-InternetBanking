using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using InternetBanking.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace InternetBanking.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly InternetBankingContext _context;
        private readonly UserManager<InternetBankingUser> _userManager;
        private readonly SignInManager<InternetBankingUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public EmployeesController(InternetBankingContext context, UserManager<InternetBankingUser> userManager, SignInManager<InternetBankingUser> signInManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        // GET: Employees1
        public async Task<IActionResult> Index()
        {
            var internetBankingContext = _context.Employees.Include(e => e.InternetBankingUser);
            return View(await internetBankingContext.ToListAsync());
        }

        // GET: Employees1/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.InternetBankingUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees1/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.InternetBankingUsers, "Id", "Id");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonalId,Email,FirstName,LastName,Address,Phone,CreateDate,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Generate a random 4-digit number for the username
                var randomNumbers = new Random().Next(1000, 9999).ToString();

                // Generate username and password
                var username = $"nexEmp{randomNumbers}";
                var password = "nexemp";

                var user = new InternetBankingUser { UserName = username, Email = employee.Email };

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var emp = new Employee
                    {
                        Id = user.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Phone = employee.Phone,
                        PersonalId = employee.PersonalId,
                        Address = employee.Address,
                        Status = false,
                        CreateDate = DateTime.Now,
                        Email = employee.Email,
                        // Set other properties if needed
                    };
                    // Assign the "Employee" role to the user
                    await _userManager.AddToRoleAsync(user, "Employee");
                    _context.Employees.Add(emp);
                    await _context.SaveChangesAsync();

                    // Send confirmation email
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code },
                        protocol: HttpContext.Request.Scheme);

                    await _emailSender.SendEmailAsync(employee.Email, "Confirm your email",
                        $"Please confirm your account by clicking this link: {callbackUrl}");

                    // Your existing code for the confirmation message
                    // ...

                    return RedirectToAction("Index", "Home"); // Redirect to the desired page after successful registration
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If ModelState is not valid, redisplay the registration form with errors
            return View(employee);
        }

    public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.InternetBankingUsers, "Id", "Id", employee.Id);
            return View(employee);
        }

        // POST: Employees1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,PersonalId,Email,FirstName,LastName,Address,Phone,CreateDate,Status")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["Id"] = new SelectList(_context.InternetBankingUsers, "Id", "Id", employee.Id);
            return View(employee);
        }

        // GET: Employees1/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.InternetBankingUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'InternetBankingContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
          return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
