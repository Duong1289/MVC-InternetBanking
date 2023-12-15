using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    public class TestLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}