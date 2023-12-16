using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Models
{
    public class Withdraw
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public double Amount { get; set; }
        
        [Required]
        public string? WithdrawAccountNumber { get; set; }

        [ForeignKey("Customers")]
        public string? CustomerId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IssueDate { get; set; } = DateTime.Now;

        public Account Account { get; set; }
        public Customer Customer { get; set; }
        
    }
}
