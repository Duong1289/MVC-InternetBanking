using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Model
{
    public class InternetBankingContext: IdentityDbContext
    {
        public InternetBankingContext(DbContextOptions<InternetBankingContext> options):base(options) 
        {
        }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<AccountType>? AccountTypes { get; set; }
        public DbSet<Bank>? Banks { get; set; }
        public DbSet<Branch>? Branches { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<FAQ>? FAQ { get; set; }
        public DbSet<FAQCategory>? FAQCategories { get; set; }
        public DbSet<HelpRequest>? HelpRequests { get; set; }
        public DbSet<HelpRequestType>? HelpRequestsTypes { get; set; }
        public DbSet<Service>? Services { get; set; }
        public DbSet<ServiceType>? ServicesTypes { get; set; }
        public DbSet<Transaction>? Transactions { get; set; }

        public DbSet<Image>? Images { get; set; }
        public DbSet<Loan>? Loans { get; set; }
        public DbSet<LoanType>? LoanTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //cắt bỏ chữ AspNet trong tên của table
            var entities = builder.Model.GetEntityTypes();
            foreach (var entityType in entities)
            {
                string name = entityType.Name;
                if (name!.StartsWith("AspNet"))
                {
                    entityType.SetTableName(name.Substring(6));
                }
            }
        }
    }
    
}
