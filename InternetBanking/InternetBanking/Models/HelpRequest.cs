using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Models
{
    [Authorize("Admin, User")]
    public class HelpRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Accounts")]
        [MaxLength(20)]
        public required string AccountId {  get; set; }

        [ForeignKey("Customers")]
        [MaxLength(20)]
        public required string CustomerId { get; set; }


        [ForeignKey("Employees")]
        public required string EmployeeId { get; set; }

        [ForeignKey("HelpRequestTypes")]
        [Required]
        public int RequestTypeId { get; set; }
        
        [Required]
        [MaxLength(1000)]
        public required string Content {  get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime CreatedDate { get; set; } = DateTime.Now;
        
        [Required]
        [MaxLength(1000)]
        public required string Answer { get; set; }

        [DefaultValue(false)]
        public  bool Status { get; set; }

        public required string HelpRequestImageId { get; set; }

        public ICollection<Image>? Images { get; set; }
    }
}
