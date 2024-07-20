using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MedicalCenter.Services.ViewModelsConstants;

namespace MedicalCenter.Services.ViewModels.Doctors
{
    public class PreviewDoctorProfileViewModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(DoctorBiographyMinLen, ErrorMessage = "{0} must at least {1} characters long.")]
        public string Biography { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public bool IsImageApproved { get; set; }
    }
}
