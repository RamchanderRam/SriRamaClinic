
using System.ComponentModel.DataAnnotations;

namespace MedicalCenter.Services.ViewModels.Patients
{
    public class FindPatientNameFormModel
    {
        [Required]
        public string FullName { get; set; }
    }
}
