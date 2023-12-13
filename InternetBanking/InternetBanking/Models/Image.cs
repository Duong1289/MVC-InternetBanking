using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Models
{
    public class Image
    {
        [Key]
        public string? Id { get; set; }

<<<<<<< HEAD
        [ForeignKey("Customers")]
        [MaxLength(20)]
        public string? CustomerId { get; set; }

        [ForeignKey("Employees")]
        public string? EmployeeId { get; set; }
=======
        // [ForeignKey("Customer")]
        // [Column("CustomerId")]
        // [MaxLength(20)]
        // public string? CustomerId { get; set; }
        //
        // [ForeignKey("Employee")]
        // [Column("EmployeeId")]
        // public string? EmployeeId { get; set; }
>>>>>>> parent of c480796 (update models)

        [ForeignKey("HelpRequests")]
        [MaxLength(20)]
        public string? RequestId { get; set; }

        [MaxLength(1000)]
        public required string Path { get; set; }

        public bool Avatar {  get; set; }

    }
}
