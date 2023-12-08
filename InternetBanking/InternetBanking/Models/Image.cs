using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Model
{
    public class Image
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("Customers")]
        [MaxLength(20)]
        public string? CustomerId { get; set; }

        [ForeignKey("Employees")]
        [MaxLength(20)]
        public int? EmployeeId { get; set; }

        [ForeignKey("HelpRequests")]
        [MaxLength(20)]
        public string? RequestId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Path { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Type { get; set; }

       
    }
}
