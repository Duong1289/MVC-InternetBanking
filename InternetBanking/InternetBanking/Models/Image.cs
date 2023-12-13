using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Models
{
    public class Image
    {
        [Key]
        public string? Id { get; set; }
        
        [ForeignKey("Accounts")]
        [MaxLength(20)]
        public required string AccountId {  get; set; }

        [ForeignKey("Customer")]
        [Column("PersonalId")]
        public string? CustomerId { get; set; }
        
        [ForeignKey("Employee")]
        [Column("EmpId")]
        public string? EmployeeId { get; set; }

        [ForeignKey("HelpRequests")]
        [MaxLength(20)]
        public string? RequestId { get; set; }

        [MaxLength(1000)]
        public required string Path { get; set; }

        public bool Avatar {  get; set; }

    }
}
