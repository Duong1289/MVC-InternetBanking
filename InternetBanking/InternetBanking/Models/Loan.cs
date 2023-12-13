using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [ForeignKey("Accounts")]
        public string? AccountId { get; set; }
        
        [ForeignKey("Customers")]
        public string? CustomerId { get; set; }

        [ForeignKey("Employees")]
        public string? EmployeeId { get; set; }
        [Required]
        [ForeignKey("LoanTypes")]
        public int? LoanTypeId { get; set; }

        [Required]
        public double? Amount { get; set; }

        [Required]
        public double? Interest {  get; set; }

        [Required]
        public DateTime? IssueDate {  get; set; }

        [Required]
        public DateTime? ExpireDate { get; set; }

        [Required]
        public bool? Status { get; set; }
    }
}
