using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Services.ViewModelsConstants;

namespace MedicalCenter.Services.ViewModels.Doctors
{
    public class AddDoctorFormModel
    {
        [Required]
        [MaxLength(DoctorSpecialtyMaxLen,ErrorMessage ="{0} can not be more than {1} characters long.")]
        [MinLength(DoctorSpecialtyMinLen,ErrorMessage ="{0} must at least {1} characters long.")]
        public string Specialty { get; set; }

        [Required]
        [MinLength(DoctorBiographyMinLen, ErrorMessage = "{0} must at least {1} characters long.")]
        public string Biography { get; set; }

        public IFormFile Image { get; set; }
    }
}
