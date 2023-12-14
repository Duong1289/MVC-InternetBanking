﻿using System;
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
        public string? FirstName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }


        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }  
    }
}
