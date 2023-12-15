using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Models
{
    public class Service
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ServiceName { get; set; }


        [ForeignKey("ServicesTypes")]
        [Required]
        public int? ServiceTypeId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Content { get; set;}

        [Required]
        public double Amount { get; set; }


        [ForeignKey("Employees")]
        
        public string? EmployeeId { get; set; }

        [ForeignKey("Accounts")]
        [Required]
        [MaxLength(20)]
        public string? ServiceAccountNumber { get; set;}

        [ForeignKey("Customers")]
        
        public string? CustomerPersonalId { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate { get; set; }= DateTime.Now;

    }
}
