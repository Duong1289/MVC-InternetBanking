using InternetBanking.Models;
using InternetBanking.Service.MailService;
using InternetBanking.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using InternetBanking.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin,Employee")]
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
            else
            {
                ViewBag.Result = null;
            }
            return View();
        }


        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> ProcessDeposit(Deposit deposit)
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
                var customer = await ctx.Customers!.Where(c => c.Accounts
             .Any(a => a.AccountNumber == deposit.DepositAccountNumber)).SingleOrDefaultAsync();

                ctx.Deposits!.Add(deposit);
                if (await ctx.SaveChangesAsync() > 0)
                {
                    bool res = await service.Deposit(deposit.DepositAccountNumber, deposit.Amount);

                    if (res)
                    {
                        await mailService.SendEmailDeposit(customer, deposit);
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

        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> History()
        {
            var deposits = await ctx.Deposits!.ToListAsync();
            return View(deposits);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UserHistory()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Retrieve accounts belonging to the current user
            var accounts = await ctx.Accounts!
                .Where(a => a.CustomerId == currentUser.Id)
                .ToListAsync();

            // Retrieve withdrawal transactions associated with the user's accounts
            var withdrawals = await ctx.Deposits!
                .Where(w => accounts.Select(a => a.AccountNumber).Contains(w.DepositAccountNumber))
                .ToListAsync();

            // Additional logic for displaying or processing withdrawal transactions

            return View(withdrawals);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Details(int id)
        {
            var withdraw = await ctx.Deposits!.SingleOrDefaultAsync(d => d.Id == id);
            return View(withdraw);
        }

        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AdminDetails(int id)
        {
            var withdraw = await ctx.Deposits!.SingleOrDefaultAsync(d => d.Id == id);
            return View(withdraw);
        }
    }
}
