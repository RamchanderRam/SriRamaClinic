using System;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Services.ViewModelsConstants;

namespace MedicalCenter.Services.ViewModels.Admin
{
    public class CreateDoctorFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(DoctorPasswordMinLen,ErrorMessage = "{0} must be at least {1} characters long.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MinLength(DoctorFirstNameMinLen, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(DoctorFirstNameMaxLen)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MinLength(DoctorLastNameMinLen, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(DoctorLastNameMaxLen)]
        public string LastName { get; set; }
    }
}
