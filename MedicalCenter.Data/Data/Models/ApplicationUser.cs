using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Data.DataConstants;

namespace MedicalCenter.Data.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(UserFirstNameMaxLen)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(UserLastNameMaxLen)]
        public string LastName { get; set; }
    }
}
