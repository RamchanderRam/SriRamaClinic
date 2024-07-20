
using MedicalCenter.Services.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalCenter.Services.ViewModels.Schedules
{
    public class InputScheduleFormModel
    {
        [Required]
        [Display(Name = "Pick Up Day And Hour")]
        [DateAttribute(ErrorMessage = "Invalid date")]
        public DateTime Date { get; set; }

    }
}
