using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Model
{
    public class LoanType
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? Period { get; set; }

        [Required]
        [MaxLength(200)]
        public string? LoanName { get; set; }

        public ICollection<Loan>? Loans { get; set; }
    }
}
