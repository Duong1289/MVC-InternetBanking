using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Models;
using InternetBanking.Service.MailService;
using Microsoft.AspNetCore.Identity;
using InternetBanking.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Controllers
{

   
    public class HelpRequestController : Controller
    {
        private readonly InternetBankingContext _context;
        private readonly SendBankMailService _sendMailServiceTransHelp;
        UserManager<InternetBankingUser> _userManager;

        public HelpRequestController(InternetBankingContext context, SendBankMailService _sendMailServiceTransHelp, UserManager<InternetBankingUser> _userManager)
        {
            _context = context;
            this._userManager = _userManager;
            this._sendMailServiceTransHelp = _sendMailServiceTransHelp;
        }


        // GET: HelpRequest/Create
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var accounts = await _context.Accounts!.Where(a => a.CustomerId == currentUser.Id).ToListAsync();
            var helpRequestTypes = await _context.HelpRequestsTypes!.ToListAsync();
            ViewBag.CustomerId = currentUser.Id;
            ViewBag.UserAccounts = accounts;
            ViewBag.HelpRequestsTypes = helpRequestTypes;
            return View();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Create(HelpRequest helpRequest)
        {
            try { 
                helpRequest.CreatedDate = DateTime.Now;
                helpRequest.Status = false;
                helpRequest.Answer = "";
                helpRequest.HelpRequestImageId = "";
                _context.Add(helpRequest);
                await _context.SaveChangesAsync();
                TempData["ResultSucces"] = "Create request Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["ResultFail"] = "Failed to create help request.";
                return RedirectToAction(nameof(Index));
            }
        }


        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UserView()
        {
            if (TempData["ResultSuccess"] != null)
            {
                ViewBag.TransactionStatus = TempData["ResultSuccess"];
                ViewBag.Color = "success";
            }
            else if (TempData["ResultFail"] != null)
            {
                ViewBag.TransactionStatus = TempData["ResultFail"];
                ViewBag.Color = "danger";
            }
            else
            {
                ViewBag.TransactionStatus = null;
            }
            var currentUser = await _userManager.GetUserAsync(User);
            var requests = await _context.HelpRequests!
            .Where(r => r.CustomerId == currentUser.Id && r.Status==false)
            .ToListAsync();
            return View(requests);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Update(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var accounts = await _context.Accounts!.Where(a => a.CustomerId == currentUser.Id).ToListAsync();
            var helpRequestTypes = await _context.HelpRequestsTypes!.ToListAsync();
            ViewBag.CustomerId = currentUser.Id;
            ViewBag.UserAccounts = accounts;
            ViewBag.HelpRequestsTypes = helpRequestTypes;
            var request = await _context.HelpRequests!.SingleOrDefaultAsync(r=>r.Id==id);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HelpRequest request)
        {
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: HelpRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HelpRequests == null)
            {
                return NotFound();
            }

            return RedirectToAction("UserView");
        }

        public async Task<IActionResult> Detail(string id)
        {
            var request = await _context.HelpRequests!.SingleOrDefaultAsync(r => r.Id == id);
            var type = await _context.HelpRequestsTypes!.SingleOrDefaultAsync(r => r.RequestTypeId == request.RequestTypeId);
            ViewBag.Type = type.TypeName;
            return View(request);
        }

        public async Task<IActionResult> DetailTrue(string id)
        {
            var request = await _context.HelpRequests!.SingleOrDefaultAsync(r => r.Id == id);
            var type = await _context.HelpRequestsTypes!.SingleOrDefaultAsync(r => r.RequestTypeId == request.RequestTypeId);
            var customer = await _context.Customers!.SingleOrDefaultAsync(r => r.Id == request.CustomerId);
            var employee = await _context.Employees!.SingleOrDefaultAsync(r => r.Id == request.EmployeeId);
            ViewBag.Type = type.TypeName;
            ViewBag.Name = customer.FirstName + " " + customer.LastName;
            ViewBag.EmpName = employee.FirstName + " " + employee.LastName;
            return View(request);
        }

 

        private bool HelpRequestExists(string id)
        {
          return (_context.HelpRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> Answer(string id)
        {
            var request = await _context.HelpRequests!.SingleOrDefaultAsync(r => r.Id == id);
            return View(request);
        }

        public async Task<IActionResult> AnswerRequest(HelpRequest request)
        {
            var helpRequest = await _context.HelpRequests!.SingleOrDefaultAsync(r=>r.Id == request.Id);
            if (helpRequest == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            request.EmployeeId = currentUser.Id;
            request.Status = true;

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Send email after saving changes
                await _sendMailServiceTransHelp.SendEmailHelpRequest(request);

                TempData["ResultSuccess"] = "Help request answered successfully!";
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["ResultFail"] = "Failed to answer help request!";
            }

            return RedirectToAction("EmployeeView");
        }

        public async Task<IActionResult> EmployeeDetail(string id)
        {
            var request = await _context.HelpRequests!.SingleOrDefaultAsync(r=>r.Id == id);
            var type = await _context.HelpRequestsTypes!.SingleOrDefaultAsync(r => r.RequestTypeId == request.RequestTypeId );
            var customer = await _context.Customers!.SingleOrDefaultAsync(r => r.Id == request.CustomerId);
            var employee = await _context.Employees!.SingleOrDefaultAsync(r => r.Id == request.EmployeeId);
            ViewBag.Type = type.TypeName;
            ViewBag.Name = customer.FirstName+" "+customer.LastName;
            ViewBag.EmpName = employee.FirstName + " " + employee.LastName;
            return View(request);
        }



        public string GenerateTransactionCode()
        {
            string part1 = DateTime.Now.ToString("yy");
            string part2 = DateTime.Now.ToString("MMdd");
            string part3 = DateTime.Now.ToString("HHmmfff");
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" + part1 + part2 + part3;
            Random random = new Random();

            // Generating a random string by selecting characters from the set
            string randomString = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }
    }
}
