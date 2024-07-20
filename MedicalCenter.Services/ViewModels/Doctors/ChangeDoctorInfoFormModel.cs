using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Services.ViewModelsConstants;

namespace MedicalCenter.Services.ViewModels.Doctors
{
    public class ChangeDoctorInfoFormModel
    {
        [Required]
        [MinLength(DoctorBiographyMinLen, ErrorMessage = "{0} must at least {1} characters long.")]
        public string Biography { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
