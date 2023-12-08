using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Model
{
    public class HelpRequest
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("Accounts")]
        [Required]
        [MaxLength(20)]
        public string? AccountId {  get; set; }

        [ForeignKey("Customers")]
        [Required]
        [MaxLength(20)]
        public string? CustomerPersonalId { get; set; }


        [ForeignKey("Employees")]
        [Required]
        public int? EmployeeId { get; set; }

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
