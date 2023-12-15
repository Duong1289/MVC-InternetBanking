using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace InternetBanking.Models
{
    public class Account
    {
        [Key]
        [MaxLength(20)]
        public required string AccountNumber { get; set; }

        [ForeignKey("AccountTypes")]
        public required int AccountTypeId { get; set; }

        [ForeignKey("Customers")]
        public required string CustomerId { get; set; }

        [DefaultValue(0.0)]
        public double Balance { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OpenDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpireDate { get; set; }

        [DefaultValue(true)]
        public bool Status { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual AccountType AccountType { get; set; }

        public ICollection<Withdraw>? Withdraws { get; set; }
        public ICollection<Deposit>? Deposits { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<HelpRequest> HelpRequests { get; set; }
    }
}
