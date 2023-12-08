using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Model
{
    public class Employee
    {
        [Key]
        public string? Id { get; set; }
        
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
        public string? LastName { get; set;}

        [Required]
        [MaxLength(15)]
        public string? Phone { get; set; }

        //block/ublock
        [Required]
        public bool? Status { get; set; }

        [Required]
        [MaxLength(5)]
        public string? RoleId { get; set; }

        
        [MaxLength(5)]
        [ForeignKey(nameof(Id))]
        public string? ManageBy { get; set;}

        [Required]
        [ForeignKey("Branches")]
        public int? BranchId { get; set; }
        public ICollection<HelpRequest>? HelpRequests { get; set; }
        public ICollection<Service>? Services { get; set; }
        public ICollection<Loan>? Loans { get; set; }
        public ICollection<Image>? Images { get; set; }

    }
}
