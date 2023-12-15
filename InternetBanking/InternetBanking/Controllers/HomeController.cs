using InternetBanking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InternetBankingContext _context;
        

        public HomeController(ILogger<HomeController> logger, InternetBankingContext context)
        {
            _logger = logger;
            _context = context;
        }
        

        public async Task<IActionResult> Index()
        {
            var fAQ = await _context.FAQ.ToListAsync();
            return View(fAQ);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}