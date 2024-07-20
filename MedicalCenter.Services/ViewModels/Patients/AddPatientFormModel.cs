using System;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Data.DataConstants;

namespace MedicalCenter.Services.ViewModels.Patients
{
    public class AddPatientFormModel
    {
        [Required]
        [MaxLength(PatientCountryMaxLen, ErrorMessage = "{0} cannot be greater than {1} characters")]
        [MinLength(PatientCountryMinLen, ErrorMessage = "{0} cannot be less than {1} characters")]
        public string Country { get; set; }

        [Required]
        [MaxLength(PatientTownMaxLen, ErrorMessage = "{0} cannot be greater than {1} characters")]
        [MinLength(PatientTownMinLen,ErrorMessage = "{0} cannot be less than {1} characters")]
        public string Town { get; set; }

        [Required]
        [MaxLength(PatientAddressMaxLen, ErrorMessage = "{0} cannot be greater than {1} characters")]
        [MinLength(PatientAddressMinLen, ErrorMessage = "{0} cannot be less than {1} characters")]
        public string Address { get; set; }

        [Required]
        [MaxLength(PatientEGNMaxLen, ErrorMessage = "{0} cannot be greater than {1} characters")]
        [RegularExpression(PatientEGNPattern, ErrorMessage = "{0} must be in format 0000000000")]
        public string EGN { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
    }
}
