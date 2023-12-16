using InternetBanking.Areas.Identity.Data;
using InternetBanking.Models;
using InternetBanking.Service;
using InternetBanking.Service.MailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Controllers
{
    public class WithdrawController : Controller
    {
        InternetBankingContext ctx;
        ServiceService service;
        SendBankMailService mailService;
        UserManager<InternetBankingUser> _userManager;


        public WithdrawController(InternetBankingContext ctx, ServiceService service, SendBankMailService mailService, UserManager<InternetBankingUser> _userManager)
        {
            this.ctx = ctx;
            this.service = service;
            this.mailService = mailService;
            this._userManager = _userManager;
        }

        public IActionResult Index()
        {
           
            if (TempData["ResultSucess"] != null)
            {
                ViewBag.Result = TempData["ResultSucess"];
                ViewBag.Color = "success";
            }
            else if(TempData["ResultFail"] !=null)
            {
                ViewBag.Result = TempData["ResultFail"];
                ViewBag.Color = "danger";
            }
            else
            {
                ViewBag.Result = null;
            }
            return View();
        }

        public async Task<IActionResult> ProcessWithdraw(Withdraw withdraw)
        {
            try
            {
                bool accExist = await service.ValidateAccount(withdraw.WithdrawAccountNumber);

                if (!accExist)
                {
                    throw new InvalidOperationException("Invalid account number!");
                }

                withdraw.CustomerId = await service.getCustomerId(withdraw.WithdrawAccountNumber);
                withdraw.IssueDate = DateTime.Now;
                var currentUser = await _userManager.GetUserAsync(User);
                if(currentUser == null) { return View(); }

                ctx.Withdraws!.Add(withdraw);
                if (await ctx.SaveChangesAsync() > 0)
                {
                    bool res = await service.Withdraw(withdraw.WithdrawAccountNumber, withdraw.Amount);

                    if (res)
                    {
                        TempData["ResultSucess"] = "Withdraw Successful!";
                    }
                    else
                    {
                        TempData["ResultFail"] = "Error processing withdrawal. Please try again later!";
                    }
                }
                else
                {
                    TempData["ResultFail"] = "Error processing withdrawal. Please try again later!";
                }
            }
            catch (ValidationException ex)
            {
                // Specific exception for validation failure
                TempData["ResultFail"] = $"Validation error: {ex.Message}";
            }
            catch (InvalidOperationException ex)
            {
                // Handle the specific exception for invalid account number
                TempData["ResultFail"] = ex.Message;
            }
            catch (Exception ex)
            {
                // Catch any other unexpected exceptions
                TempData["ResultFail"] = $"An unexpected error occurred: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> History()
        {
            var withdraws = await ctx.Withdraws!.ToListAsync();
            return View(withdraws);
        }



    }
}
