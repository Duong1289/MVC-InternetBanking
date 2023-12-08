using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Model
{
    public class FAQ
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string? Question { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Answer { get; set; }


        [ForeignKey("FAQCategories")]
        [Required]
        public int? FAQCategoryId { get; set; }
    }
}
