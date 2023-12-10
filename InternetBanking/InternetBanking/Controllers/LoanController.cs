using InternetBanking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Controllers
{
    public class LoanController : Controller
    {
        InternetBankingContext ctx;

        public LoanController(InternetBankingContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["LoanTypes"] = await ctx.LoanTypes!.ToListAsync();
            return View();
        }

    }
}
