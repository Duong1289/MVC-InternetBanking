using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace InternetBanking.Model
{
    public class Account
    {
        //auto gen number
        [Key]
        [MaxLength(20)]
        public string? AccountNumber { get; set; }
        
        [Required]
        [DefaultValue(0.0)]
        public double? Balance { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OpenDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpireDate { get; set;}

        //Block/unlock
        [Required]
        [DefaultValue(true)]
        public bool? Status { get; set;}

        [ForeignKey("AccountTypes")]
        public int? AccountTypeId { get; set; }
        
        [ForeignKey("Customers")]
        [Required]
        [MaxLength(20)]
        public string? CustomerPersonalId { get; set; }
 
        public ICollection<Service>? Services  { get; set; }

        public ICollection<Transaction>? Transactions  { get; set; }
        
        public ICollection<HelpRequest>? HelpRequests { get; set; }
        public ICollection<Loan>? Loans { get; set; }

    }
}
