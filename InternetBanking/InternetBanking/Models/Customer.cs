using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using InternetBanking.Areas.Identity.Data;

namespace InternetBanking.Models
{
    public class Customer
    {

        [Key, ForeignKey("InternetBankingUser")]
        public string? Id { get; set; }

        [MaxLength(20)]
        public string? PersonalId { get; set; }

        [MaxLength(200)]
        public required string Email { get; set; }

        [MaxLength(20)]
        public string? Phone {  get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }
       
        [MaxLength(50)]
        public string? LastName { get; set; }
       
        [MaxLength(250)]
        public string? Address { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime? OpenDate { get; set; }

        //nguoi dung da duoc xac thuc
        [Required]
        public bool Status { get; set; }

        public ICollection<Account>? Accounts { get; set; }
        public ICollection<HelpRequest>? HelpRequests { get; set; }
        public ICollection<Service>? Services { get; set; }
        public ICollection<Image>? Images { get; set; }
        
        public virtual InternetBankingUser? InternetBankingUser { get; set; }
        // Mối quan hệ 1-1 với IdentityUser
    }
}
