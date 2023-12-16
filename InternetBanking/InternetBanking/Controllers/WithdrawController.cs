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
                if (!await service.CheckBalance(withdraw.WithdrawAccountNumber, withdraw.Amount))
                {
                    throw new InvalidOperationException("Insufficient funds to withdraw!!");
                }

                withdraw.CustomerId = await service.getCustomerId(withdraw.WithdrawAccountNumber);
                withdraw.IssueDate = DateTime.Now;
                var currentUser = await _userManager.GetUserAsync(User);
                var customer = await ctx.Customers.SingleOrDefaultAsync(c => c.Id == currentUser.Id);

                ctx.Withdraws!.Add(withdraw);
                if (await ctx.SaveChangesAsync() > 0)
                {
                    bool res = await service.Withdraw(withdraw.WithdrawAccountNumber, withdraw.Amount);

                    if (res)
                    {
                        await mailService.SendEmailWithedraw(customer, withdraw);
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

        public async Task<IActionResult> UserHistory()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Retrieve accounts belonging to the current user
            var accounts = await ctx.Accounts!
                .Where(a => a.CustomerId == currentUser.Id)
                .ToListAsync();

            // Retrieve withdrawal transactions associated with the user's accounts
            var withdrawals = await ctx.Withdraws!
                .Where(w => accounts.Select(a => a.AccountNumber).Contains(w.WithdrawAccountNumber))
                .ToListAsync();

            // Additional logic for displaying or processing withdrawal transactions

            return View(withdrawals);
        }

        public async Task<IActionResult> Details(int id)
        {
            var withdraw = await ctx.Withdraws!.SingleOrDefaultAsync(w=>w.Id== id);
            return View(withdraw);  
        }




    }
}
