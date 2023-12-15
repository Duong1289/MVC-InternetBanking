using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Models
{
    public class FAQ
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(300)]
        public required string Question { get; set; }

        [MaxLength(1000)]
        public required string Answer { get; set; }


        [ForeignKey("FAQCategories")]
        public required int FAQCategoryId { get; set; }
    }
}
