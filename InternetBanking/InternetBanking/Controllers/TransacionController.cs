using InternetBanking.Mail;
using InternetBanking.Service;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    public class TransacionController : Controller
    {
        TransactionService transactionService;
        SendMailService sendMailService;

        public TransacionController(TransactionService transactionService, SendMailService sendMailService)
        {
            this.transactionService = transactionService;
            this.sendMailService = sendMailService;
        }

        public IActionResult Index()
        {
            if (TempData["TransactionSuccess"] != null)
            {
                ViewBag.TransactionStatus = TempData["TransactionSuccess"];
            }
            else
            {
                ViewBag.TransactionStatus = TempData["TransactionError"];
            }

            return View();
        }

        public async Task<IActionResult> ProcessTransaction(TransactionService transac)
        {
            try
            {
                return RedirectToAction("Index");
            }
            
            
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }


    }
}
