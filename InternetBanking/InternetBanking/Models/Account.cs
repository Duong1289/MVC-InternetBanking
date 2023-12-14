using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace InternetBanking.Models
{
    public class Account
    {
        //auto gen number
        [Key]
        [MaxLength(20)]
        public required string AccountNumber { get; set; }
        
        
        [DefaultValue(0.0)]
        public double Balance { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime OpenDate { get; set; } = DateTime.Now;

        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime ExpireDate { get; set;}

        //Block/unlock
        
        [DefaultValue(true)]
        public required bool Status { get; set;}        
        
        [ForeignKey("Customers")]
        public required string CustomerPersonalId { get; set; }
 
        public ICollection<Service>? Services  { get; set; }

        public ICollection<Transaction>? Transactions  { get; set; }
        
        public ICollection<HelpRequest>? HelpRequests { get; set; }
        public ICollection<Loan>? Loans { get; set; }

    }
}
