using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using InternetBanking.Areas.Identity.Data;

namespace InternetBanking.Models
{
    public class Customer
    {
        //CCCD
        [Key]
        public string? PersonalId { get; set; }
        [ForeignKey("PersonalId")]
        public virtual InternetBankingUser InternetBankingUser { get; set; }
        // Mối quan hệ 1-1 với IdentityUser

        [MaxLength(20)]
        public string? Username { get; set; }

        //[Required]
        //[MaxLength(20)]
        //public string? Password { get; set; }
        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Email { get; set; }
        //[Required]
        //[MaxLength(15)]
        //public string? Phone { get; set; }
        [Required]
        [MaxLength(250)]
        public string? Address { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OpenDate { get; set; }

        //nguoi dung da duoc xac thuc
        [Required]
        [DefaultValue(false)]
        public bool? Status { get; set; }

        //locked?
        [Required]
        [DefaultValue(false)]
        public bool? Locked { get; set; }
        [ForeignKey("Branches")]
        public int? BranchId { get; set; }
        public ICollection<Account>? Accounts { get; set; }
        public ICollection<HelpRequest>? HelpRequests { get; set; }
        public ICollection<Service>? Services { get; set; }
        public ICollection<Image>? Images { get; set; }
        public ICollection<Loan>? Loans { get; set; }
    }
}
