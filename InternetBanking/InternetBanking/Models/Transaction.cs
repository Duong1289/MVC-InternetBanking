using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBanking.Model
{
    public class Transaction
    {
        [Key]
        [Required]
        //Ma giao dich tu sinh TTDH-yyyymmdd_radom text...
        public string? Id {  get; set; }
        
        [ForeignKey("Accounts")]
        [Required]
        [MaxLength(20)]
        public string? SenderAccountNumber { get; set; }

        [ForeignKey("Accounts")]
        [Required]
        [MaxLength(20)]
        public string? ReceiverAccountNumber { get; set; }

        [Required]
        public double? Amount { get; set;}

        [Required]
        [MaxLength(1000)]
        public string? Content { get; set;}

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        public bool Validation { get; set; }

    }
}
