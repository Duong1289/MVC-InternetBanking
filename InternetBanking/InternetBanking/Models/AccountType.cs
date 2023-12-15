using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Models
{
    public class AccountType
    {
        [Key]
        public int Id { get; set; }

        public required string TypeName { get; set; }

        public ICollection<Account>? Accounts { get; set; }

    }
}
