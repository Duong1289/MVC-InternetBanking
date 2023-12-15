using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Models
{
    public class ServiceType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ServiceName { get; set; }
  
        public ICollection<Service>? Services { get; set; }
    }
}
