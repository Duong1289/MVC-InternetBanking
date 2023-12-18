using InternetBanking.Areas.Identity.Data;
using InternetBanking.Models;
using InternetBanking.Service.MailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly InternetBankingContext _context;
        private readonly SendBankMailService sendMailService;
        UserManager<InternetBankingUser> _userManager;

        public MyAccountController(InternetBankingContext context, SendBankMailService sendMailService, UserManager<InternetBankingUser> userManager)
        {
            _context = context;
            this.sendMailService = sendMailService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            return View();
        }
    }
}
