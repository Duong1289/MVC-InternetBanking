using InternetBanking.Models;
using InternetBanking.Service.MailService;
using InternetBanking.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using InternetBanking.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Controllers
{
    public class DepositController : Controller
    {
        InternetBankingContext ctx;
        ServiceService service;
        SendBankMailService mailService;
        UserManager<InternetBankingUser> _userManager;

        public DepositController(InternetBankingContext ctx, ServiceService service, SendBankMailService mailService, UserManager<InternetBankingUser> _userManager)
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
            else if (TempData["ResultFail"] != null)
            {
                ViewBag.Result = TempData["ResultFail"];
                ViewBag.Color = "danger";
            }
            return View();
        }

        public async Task<IActionResult> ProcessWithdraw(Deposit deposit)
        {
            try
            {
                bool accExist = await service.ValidateAccount(deposit.DepositAccountNumber);

                if (!accExist)
                {
                    throw new InvalidOperationException("Invalid account number!");
                }

                deposit.CustomerId = await service.getCustomerId(deposit.DepositAccountNumber);
                deposit.IssueDate = DateTime.Now;
                var currentUser = await _userManager.GetUserAsync(User);

                if (await ctx.SaveChangesAsync() > 0)
                {
                    bool res = await service.Deposit(deposit.DepositAccountNumber, deposit.Amount);

                    if (res)
                    {
                        TempData["ResultSucess"] = "Deposit Successful!";
                    }
                    else
                    {
                        TempData["ResultFail"] = "Error processing Deposit. Please try again later!";
                    }
                }
                else
                {
                    TempData["ResultFail"] = "Error processing Deposit. Please try again later!";
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
            var deposits = await ctx.Deposits!.ToListAsync();
            return View(deposits);
        }
    }
}
