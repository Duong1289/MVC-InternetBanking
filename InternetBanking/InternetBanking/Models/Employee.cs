using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using InternetBanking.Areas.Identity.Data;

namespace InternetBanking.Models
{
    public class Employee
    {
        [Key]
        public required string Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string? PersonalId { get; set; }

        [MaxLength(200)]
        public required string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        public required string Address { get; set; }
        
        [MaxLength(15)]
        public required string Phone { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime? OpenDate { get; set; }

        [Required]
        public bool? Status { get; set; }
       
        
        public ICollection<HelpRequest>? HelpRequests { get; set; }
        public ICollection<Service>? Services { get; set; }
        
        public ICollection<Image>? Images { get; set; }
        public virtual required InternetBankingUser InternetBankingUser { get; set; }
        // Mối quan hệ 1-1 với IdentityUser

    }
}
