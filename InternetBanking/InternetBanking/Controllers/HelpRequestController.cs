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
        private readonly SendBankMailService sendMailService;
        UserManager<InternetBankingUser> _userManager;

        public HelpRequestController(InternetBankingContext context, SendBankMailService sendMailService, UserManager<InternetBankingUser> _userManager)
        {
            _context = context;
            this._userManager = _userManager;
            this.sendMailService = sendMailService;
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

        [Authorize(Roles ="Customer")]
        // GET: HelpRequest/Create
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var accounts = await _context.Accounts!.Where(a => a.CustomerId == currentUser.Id).ToListAsync();
            var helpRequestTypes = await _context.HelpRequestsTypes!.ToListAsync();
            ViewBag.CustomerId = currentUser.Id;
            ViewBag.UserAccounts = accounts;
            ViewBag.HelpRequestsTypes = helpRequestTypes;
            return View();
        }

        // POST: HelpRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HelpRequest helpRequest)
        {
                helpRequest.CreatedDate = DateTime.Now;
                helpRequest.Status = false;
                helpRequest.Answer = "";
            helpRequest.HelpRequestImageId = "";
                _context.Add(helpRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            
        }

        public async Task<IActionResult> Update(int id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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

        private bool HelpRequestExists(int id)
        {
          return (_context.HelpRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> EmployeeView()
        {
            var helprequest = await _context.HelpRequests!.Where(r => r.Status == false).ToListAsync();
            return View(helprequest);
        }

        public async Task<IActionResult> Answer(int id)
        {
            var request = await _context.HelpRequests!.SingleOrDefaultAsync(r => r.Id == id);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> AnswerRequest(HelpRequest request)
        {
            var customer = await _context.Customers!.SingleOrDefaultAsync(c => c.Id == request.CustomerId);
            var currentUser = await _userManager.GetUserAsync(User);
            request.EmployeeId = currentUser.Id;
            request.Status = true;
            _context.Entry(request).State = EntityState.Modified;
            bool res = await _context.SaveChangesAsync() >0;
            if(res)
            {
               await sendMailService.SendEmailHelpRequest(request);
                return RedirectToAction("EmployeeView");
            }

            return RedirectToAction("EmployeeView");
        }
    }
}
