﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Models
{
    public class Deposit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public double Amount { get; set; }

        [ForeignKey("Employees")]
        public string EmployeeId { get; set; }

        [ForeignKey("Accounts")]
        [Required]
        [MaxLength(20)]
        public string AccountNumber { get; set; }

        [ForeignKey("Customers")]
        public string CustomerId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IssueDate { get; set; } = DateTime.Now;

        public Account Account { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
    }
}
