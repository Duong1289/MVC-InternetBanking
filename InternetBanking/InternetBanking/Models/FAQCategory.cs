using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Models
{
    public class FAQCategory
    {
        [Key]
        public int? Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string? CategoryName { get; set; }
        public ICollection<FAQ> FAQ { get; set; }
    }
}
