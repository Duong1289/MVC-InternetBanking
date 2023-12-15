using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Models
{
    // [Authorize("Admin, User")]
    public class HelpRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Accounts")]
        [MaxLength(20)]
        public required string AccountId {  get; set; }

        [ForeignKey("Customers")]
        public  string? CustomerId { get; set; }


        [ForeignKey("Employees")]
        public string? EmployeeId { get; set; }

        [ForeignKey("HelpRequestTypes")]
        
        public required int RequestTypeId { get; set; }
        
       
        [MaxLength(1000)]
        public string? Content {  get; set; }
        
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public required DateTime CreatedDate { get; set; } = DateTime.Now;
        
        [MaxLength(1000)]
        public string? Answer { get; set; }

        public string? HelpRequestImageId { get; set; }

        [DefaultValue(false)]
        public bool Status { get; set; }

        public ICollection<Image>? Images { get; set; }
    }
}
