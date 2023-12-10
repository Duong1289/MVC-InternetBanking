// using InternetBanking.Mail;
// using InternetBanking.Models;
// using InternetBanking.Service;
// using Microsoft.AspNetCore.Mvc;
//
// namespace InternetBanking.Controllers
// {
//     public class TransactionController : Controller
//     {
//         TransactionService transactionService;
//         SendMailService sendMailService;
//
//         public TransactionController(TransactionService transactionService, SendMailService sendMailService)
//         {
//             this.transactionService = transactionService;
//             this.sendMailService = sendMailService;
//         }
//
//         // public IActionResult Index()
//         // {
//         //     if (TempData["TransactionSuccess"] != null)
//         //     {
//         //         ViewBag.TransactionStatus = TempData["TransactionSuccess"];
//         //     }
//         //     else
//         //     {
//         //         ViewBag.TransactionStatus = TempData["TransactionError"];
//         //     }
//         //
//         //     return View();
//         // }
//
//         public async Task<IActionResult> ProcessTransaction(Transaction transac)
//         {
//             try
//             {
//                 bool receiverExist = await transactionService.CheckReceiver(transac.ReceiverAccountNumber);
//                 if (!receiverExist)
//                 {
//                     throw new InvalidOperationException("Invalid receiver account number!!");
//                 }
//                 bool validFunds = await transactionService.CheckBalance(transac.Amount,transac.SenderAccountNumber);
//                 if (!validFunds)
//                 {
//                     throw new InvalidOperationException("Insufficient funds to transfer!!");
//                 }
//                 var sender = await transactionService.GetAccount(transac.SenderAccountNumber);
//                 var receiver = await transactionService.GetAccount(transac.ReceiverAccountNumber);
//
//
//                 if (receiverExist && validFunds)
//                 {
//                     bool processTransaction = await transactionService.SaveTransactionDeltail(transac);
//                     if (processTransaction)
//                     {
//                         bool updateBalance = await transactionService.UpdateBalance(receiver, sender, transac.Amount);
//                         if (!updateBalance)
//                         {
//                             string transactionId = await transactionService.SetStatusFalse(transac.Id);
//                             throw new InvalidOperationException("Update balance failed! Please try again! Your transaction ID: "+ transactionId);
//                         }
//                     }
//                     else
//                     {
//                         throw new InvalidOperationException("Transaction failed! Please try again later");
//                     }
//                 }
//
//                 return RedirectToAction("Index");
//             }
//             catch (InvalidOperationException ex)
//             {
//                 TempData["TransactionError"] = $"Transaction failed: {ex.Message}";
//             }
//             catch (Exception ex)
//             {
//                 TempData["TransactionError"] = $"An unexpected error occurred: {ex.Message}";
//             }
//
//             return RedirectToAction("Index");
//         }
//
//
//     }
// }
