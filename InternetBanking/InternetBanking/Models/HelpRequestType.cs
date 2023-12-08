using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Model
{
    public class HelpRequestType
    {
        [Key]
        public int? RequestTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ServiceName { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set;}

        public ICollection<HelpRequest>? HelpRequests { get; set; }
    }
}
