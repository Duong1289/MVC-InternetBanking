using InternetBanking.Areas.Identity.Data;
using InternetBanking.Models;
using InternetBanking.Service.MailService;
using InternetBanking.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace InternetBanking.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        TransactionService transactionService;
        InternetBankingContext ctx;
        SendBankMailService sendMailService;
        UserManager<InternetBankingUser> _userManager;

        public TransactionController(TransactionService transactionService, SendBankMailService sendMailService, InternetBankingContext ctx, UserManager<InternetBankingUser> _userManager)
        {
            this.transactionService = transactionService;
            this.sendMailService = sendMailService;
            this.ctx = ctx;
            this._userManager = _userManager;
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Index()
        {

            if (TempData["ResultSuccess"] != null)
            {
                ViewBag.TransactionStatus = TempData["ResultSuccess"];
                ViewBag.Color = "success";
            }
            else if(TempData["ResultFail"] != null)
            {
                ViewBag.TransactionStatus = TempData["ResultFail"];
                ViewBag.Color = "danger";
            }
            else
            {
                ViewBag.TransactionStatus = null;
            }
            var currentUser = await _userManager.GetUserAsync(User);
            var accounts = await ctx.Accounts
                .Where(a => a.CustomerId == currentUser.Id)
                .ToListAsync();
            ViewBag.Accounts = accounts;

            // Retrieve transactions associated with the accounts
            var today = DateTime.Today;
            var transactions = await ctx.Transactions
                .Where(t => (accounts.Select(a => a.AccountNumber).Contains(t.SenderAccountNumber) || accounts.Select(a => a.AccountNumber).Contains(t.ReceiverAccountNumber)) && t.TransactionDate.Date == today)
                .OrderByDescending(t => t.TransactionDate)
                .Take(5)
                .ToListAsync();
            ViewBag.History = transactions;
            ViewBag.CurrentUserId = currentUser.Id;
            return View();
        }

        public async Task<IActionResult> ProcessTransaction(Transaction transac)
        {
            try
            {
                // Retrieve the current user
                var currentUser = await _userManager.GetUserAsync(User);
                var customer = await ctx.Customers.SingleOrDefaultAsync(c => c.Id == currentUser.Id);

                // Check if the receiver account exists
                bool receiverExist = await transactionService.CheckReceiver(transac.ReceiverAccountNumber);
                if (!receiverExist)
                {
                    throw new InvalidOperationException("Invalid receiver account number!!");
                }

                // Check if there are sufficient funds in the sender's account
                bool validFunds = await transactionService.CheckBalance(transac.Amount, transac.SenderAccountNumber);
                if (!validFunds)
                {
                    throw new InvalidOperationException("Insufficient funds to transfer!!");
                }

                // Retrieve sender and receiver account details
                var sender = await transactionService.GetAccount(transac.SenderAccountNumber);
                var receiver = await transactionService.GetAccount(transac.ReceiverAccountNumber);

                // Check if the sender and receiver accounts are the same
                if (transac.SenderAccountNumber == transac.ReceiverAccountNumber)
                {
                    throw new InvalidOperationException("Invalid receiver account number! Cannot make a transaction for your own account");
                }

                // Process the transaction
                if (receiverExist && validFunds)
                {
                    // Save transaction details
                    bool processTransaction = await transactionService.SaveTransactionDeltail(transac);

                    if (processTransaction)
                    {
                        // Update account balances
                        bool updateBalance = await transactionService.UpdateBalance(receiver, sender, transac.Amount);

                        if (updateBalance)
                        {
                            // Send email notification
                            await sendMailService.SendEmailTransaction(transac, currentUser.Email, (customer.FirstName + " " + customer.LastName));

                            // Set success message and redirect to Index
                            TempData["ResultSuccess"] = "Transaction successfully!";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // Handle balance update failure
                            string transactionId = await transactionService.SetStatusFalse(transac.Id);
                            throw new InvalidOperationException("Update balance failed! Please try again! Your transaction ID: " + transactionId);
                        }
                    }
                    else
                    {
                        // Handle transaction save failure
                        throw new InvalidOperationException("Transaction failed! Please try again later");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                // Handle specific exceptions
                TempData["ResultFail"] = $"Transaction failed: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                TempData["ResultFail"] = $"An unexpected error occurred: {ex.Message}";
            }

            // Redirect to Index in case of any exception
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Customer")]
        [Route("Transaction/view-history")]
        public async Task<IActionResult> TransactionHistory()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Retrieve accounts belonging to the current user
            var accounts = await ctx.Accounts
                .Where(a => a.CustomerId == currentUser.Id)
                .ToListAsync();

            ViewBag.Accounts = accounts;

          
            ViewBag.Transaction = await ctx.Transactions!
                .Where(t => accounts.Select(a => a.AccountNumber).Contains(t.SenderAccountNumber) || accounts.Select(a => a.AccountNumber).Contains(t.ReceiverAccountNumber))
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            return View();
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Detail(string id)
        {
            var transaction = await ctx.Transactions!.SingleOrDefaultAsync(t=>t.Id == id);
            return View(transaction);

        }

        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> ViewAllHistory()
        {
            var transaction = await ctx.Transactions!.ToListAsync();
            return View(transaction);
        }

    }
}
