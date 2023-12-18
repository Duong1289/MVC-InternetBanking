using InternetBanking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using InternetBanking.Areas.Identity.Data;

namespace InternetBanking.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InternetBankingContext _context;
        private readonly UserManager<InternetBankingUser> _userManager;
        

        public HomeController(ILogger<HomeController> logger, InternetBankingContext context, UserManager<InternetBankingUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                var customer = await _context.Customers
                    .Where(c => c.Id == currentUser.Id)
                    .FirstOrDefaultAsync();

                if (customer != null)
                {
                    ViewBag.UserName = $"{customer.FirstName} {customer.LastName}";

                    var fAQ = await _context.FAQ.ToListAsync();
                    ViewBag.FAQ = fAQ;

                    return View(fAQ);
                }
            }
            return RedirectToAction("Error"); // Redirect to an error page or another action


        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}