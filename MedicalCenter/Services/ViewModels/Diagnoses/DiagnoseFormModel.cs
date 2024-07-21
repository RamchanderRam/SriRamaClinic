

using System;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Services.ViewModelsConstants;

namespace MedicalCenter.Services.ViewModels.Diagnoses
{
    public class DiagnoseFormModel
    {

        [Required]
        [MinLength(DiagnoseMinLen)]
        [MaxLength(DiagnoseMaxLen)]
        public string Diagnose { get; set; }
    }
}
