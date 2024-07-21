using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Data.DataConstants;

namespace MedicalCenter.Services.ViewModels.Patients
{
    public class FindPatientEGNFormModel
    {

        [MaxLength(PatientEGNMaxLen, ErrorMessage = "{0} cannot be greater than {1} characters")]
        [RegularExpression(PatientEGNPattern, ErrorMessage = "{0} must be in format 0000000000")]
        public string EGN { get; set; }

        
    }
}
