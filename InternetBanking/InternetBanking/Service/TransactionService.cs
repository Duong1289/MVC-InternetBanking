using InternetBanking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Service
{
    public class TransactionService
    {
        InternetBankingContext ctx;

        public TransactionService(InternetBankingContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<bool> CheckReceiver(string receiverId)
        {
            bool receiverExist = await ctx.Accounts!.AnyAsync(account => account.AccountNumber == receiverId);

            return receiverExist;
        }



        public async Task<bool> CheckBalance(double Amount, string accountNumber)
        {
            var account = await ctx.Accounts!.SingleOrDefaultAsync(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                bool ValidBalance = Amount < account.Balance;

                return ValidBalance;
            }
            return false;
        }

        public async Task<Account?> GetAccount(string accountNumber)
        {
            return await ctx.Accounts!.FirstOrDefaultAsync(account => account.AccountNumber == accountNumber);
        }

        public async Task<bool> SaveTransactionDeltail(Transaction transac)
        {
            transac.Id = GenerateTransactionCode();
            transac.TransactionDate = DateTime.Now;
            transac.Status = true;
            ctx.Transactions!.Add(transac);
            return await ctx.SaveChangesAsync() >0;

        }

        public async Task<bool> UpdateBalance(Account receiver, Account sender, double amount)
        {
            receiver.Balance += amount;
            sender.Balance -= amount;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<string> SetStatusFalse(string transactionId)
        {
            var transaction = await ctx.Transactions!.SingleOrDefaultAsync(t => t.Id == transactionId);
            transaction.Status = false;
            return transactionId;
        }




        public string GenerateTransactionCode()
        {
            string part1 = DateTime.Now.ToString("yy");
            string part2 = DateTime.Now.ToString("MMdd");
            string part3 = DateTime.Now.ToString("HHmmfff");
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" + part1 + part2 + part3;
            Random random = new Random();

            // Generating a random string by selecting characters from the set
            string randomString = new string(Enumerable.Repeat(chars, 20)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }
    }
}
