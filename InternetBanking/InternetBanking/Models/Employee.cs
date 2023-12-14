using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using InternetBanking.Areas.Identity.Data;

namespace InternetBanking.Models
{
    public class Employee
    {
        [Key]
        public string? EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual InternetBankingUser InternetBankingUser { get; set; }
        // Mối quan hệ 1-1 với IdentityUser
        [Required]
        [MaxLength(20)]
        public string? PersonalId { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Username { get; set; }
        [Required]
        [MaxLength()]
        public string? Password { get; set; }
        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(15)]
        public string? Phone { get; set; }
        //block/ublock
        [Required]
        public bool? Status { get; set; }
       
        [MaxLength(5)]
        public string? RoleId { get; set; }

        [MaxLength(5)]
        [ForeignKey(nameof(EmpId))]
        public string? ManageBy { get; set; }
        
        [ForeignKey("Branches")]
        public int? BranchId { get; set; }
        public ICollection<HelpRequest>? HelpRequests { get; set; }
        public ICollection<Service>? Services { get; set; }
        public ICollection<Loan>? Loans { get; set; }
        public ICollection<Image>? Images { get; set; }

    }
}
