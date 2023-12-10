using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Models
{
    public class HelpRequest
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("Accounts")]
        [MaxLength(20)]
        public string? AccountId {  get; set; }

        [ForeignKey("Customers")]
        [MaxLength(20)]
        public string? CustomerPersonalId { get; set; }


        [ForeignKey("Employees")]
        public string? EmployeeId { get; set; }

        [ForeignKey("HelpRequestTypes")]
        [Required]
        public int? RequestTypeId { get; set; }
        


        [Required]
        [MaxLength(1000)]
        public string? Content {  get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [DefaultValue(false)]
        public bool? Status { get; set; }

        public ICollection<Image>? Images { get; set; }
    }
}
