using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Model
{
    public class AccountType
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? AccTypeName { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}
