using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using InternetBanking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InternetBanking.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the InternetBankingUser class
    public class InternetBankingUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string? PersonalId { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string? FirstName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string? Address { get; set; }
        [PersonalData]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OpenDate { get; set; }
        [PersonalData]
        [DefaultValue(false)]
        public bool? Status { get; set; }
        [PersonalData]
        [ForeignKey("Branches")]
        public int? BranchId { get; set; }
        [PersonalData]
        public ICollection<Account>? Accounts { get; set; }
        [PersonalData]
        public ICollection<HelpRequest>? HelpRequests { get; set; }
        [PersonalData]
        public ICollection<Models.Service> Services { get; set; }
        [PersonalData]
        public ICollection<Image>? Images { get; set; }
        [PersonalData]
        public ICollection<Loan>? Loans { get; set; }
    }
}
