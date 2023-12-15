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
    [Authorize(Roles ="Customer")]
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

        public async Task<IActionResult> ProcessTransaction(Transaction transac)
        {
            
            var currentUser = await _userManager.GetUserAsync(User);
            var customer = await ctx.Customers.SingleOrDefaultAsync(c => c.Id == currentUser.Id);
            try
            {
                bool receiverExist = await transactionService.CheckReceiver(transac.ReceiverAccountNumber);
                if (!receiverExist)
                {
                    throw new InvalidOperationException("Invalid receiver account number!!");
                }
                bool validFunds = await transactionService.CheckBalance(transac.Amount, transac.SenderAccountNumber);
                if (!validFunds)
                {
                    throw new InvalidOperationException("Insufficient funds to transfer!!");
                }
                var sender = await transactionService.GetAccount(transac.SenderAccountNumber);
                var receiver = await transactionService.GetAccount(transac.ReceiverAccountNumber);


                if (receiverExist && validFunds)
                {
                    bool processTransaction = await transactionService.SaveTransactionDeltail(transac);
                    if (processTransaction)
                    {
                        bool updateBalance = await transactionService.UpdateBalance(receiver, sender, transac.Amount);
                        if (updateBalance)
                        {
                            await sendMailService.SendEmailTransaction(transac, currentUser.Email, (customer.FirstName+" "+customer.LastName));
                        }
                       else
                        {
                            string transactionId = await transactionService.SetStatusFalse(transac.Id);
                            throw new InvalidOperationException("Update balance failed! Please try again! Your transaction ID: " + transactionId);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Transaction failed! Please try again later");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                TempData["TransactionError"] = $"Transaction failed: {ex.Message}";
            }
            catch (Exception ex)
            {
                TempData["TransactionError"] = $"An unexpected error occurred: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [Route("Transaction/view-history")]
        public async Task<IActionResult> TransactionHistory()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Retrieve accounts belonging to the current user
            var accounts = await ctx.Accounts
                .Where(a => a.CustomerId == currentUser.Id)
                .ToListAsync();

            // Retrieve transactions associated with the accounts
            var transactions = await ctx.Transactions
            .Where(t => accounts.Select(a => a.AccountNumber).Contains(t.SenderAccountNumber) || accounts.Select(a => a.AccountNumber).Contains(t.ReceiverAccountNumber))
            .ToListAsync();


            // Pass the transactions to the view
            

            return View(transactions);
        }



    }
}
