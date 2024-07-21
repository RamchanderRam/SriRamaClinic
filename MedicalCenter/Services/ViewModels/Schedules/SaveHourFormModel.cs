

using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Services.ViewModelsConstants;

namespace MedicalCenter.Services.ViewModels.Schedules
{
    public class SaveHourFormModel
    {
        [Required]
        [MinLength(SaveHourReasonMinLen)]
        [MaxLength(SaveHourReasonMaxLen)]
        public string Reason { get; set; }
    }
}
