using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Model
{
    public class ServiceType
    {
        [Key]
        [Required]
        public int? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ServiceName { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set;}
        
        public ICollection<Service>? Services { get; set; }
    }
}
