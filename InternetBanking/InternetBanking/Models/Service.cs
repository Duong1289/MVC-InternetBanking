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
        // >= 0
        public double? Amount { get; set; }


         [ForeignKey("Employee")]
         [Column("EmpId")]
         [Required]
         public string? EmployeeId { get; set; }


        [ForeignKey("Accounts")]
        [Required]
        [MaxLength(20)]
        public string? ServiceAccountNumber { get; set;}


        [ForeignKey("Customer")]
        [Column("PersonalId")]
        [Required]
        public string? CustomerPersonalId { get; set; }
        
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate { get; set; }= DateTime.Now;

    }
}
