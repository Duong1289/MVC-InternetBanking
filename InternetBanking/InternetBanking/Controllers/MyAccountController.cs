using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    public class MyAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
