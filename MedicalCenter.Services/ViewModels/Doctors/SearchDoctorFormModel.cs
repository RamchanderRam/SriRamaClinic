using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Services.ViewModelsConstants;

namespace MedicalCenter.Services.ViewModels.Doctors
{
    public class SearchDoctorFormModel
    {
        [Required]
        [MinLength(DoctorFirstNameMinLen)]
        public string DoctorName { get; set; }
    }
}
