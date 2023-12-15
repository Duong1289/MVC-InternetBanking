using InternetBanking.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Service
{
    public class ServiceService
    {
        InternetBankingContext ctx;

        public ServiceService(InternetBankingContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<bool> ValidateAccount(string accountId)
        {
            // Check if an account with the given ID exists in the database
            var accountExists = await ctx.Accounts!.AnyAsync(a => a.AccountNumber == accountId);

            // Return true if the account exists, otherwise return false
            return accountExists;
        }

        public async Task<string> getCustomerId(string accountId)
        {
            var customer = await ctx.Customers!
            .SingleOrDefaultAsync(c => c.Accounts!.Any(a => a.AccountNumber == accountId));
            return customer.Id;
        }

        public async Task<bool> Withdraw(string accountId, double amount)
        {
            var account = await ctx.Accounts!.SingleOrDefaultAsync(a => a.AccountNumber == accountId);
            account.Balance -= amount;
            return await ctx.SaveChangesAsync()>0;
        }

        public async Task<bool> Deposit(string accountId, double amount)
        {
            var account = await ctx.Accounts!.SingleOrDefaultAsync(a => a.AccountNumber == accountId);
            account.Balance += amount;
            return await ctx.SaveChangesAsync() > 0;
        }


    }
}
