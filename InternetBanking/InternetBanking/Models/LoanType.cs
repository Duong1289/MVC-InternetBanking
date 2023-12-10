using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Models
{
    public class LoanType
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? Period { get; set; }

        public ICollection<Loan>? Loans { get; set; }
    }
}
