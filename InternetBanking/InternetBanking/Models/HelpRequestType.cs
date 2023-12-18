﻿using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Models
{
    public class HelpRequestType
    {
        [Key]
        public int? RequestTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? TypeName { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set;}

        public ICollection<HelpRequest>? HelpRequests { get; set; }
    }
}
