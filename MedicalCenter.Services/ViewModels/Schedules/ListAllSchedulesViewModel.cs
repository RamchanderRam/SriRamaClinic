
using System.Collections.Generic;

namespace MedicalCenter.Services.ViewModels.Schedules
{
    public class ListAllSchedulesViewModel
    {
        public string NameOfDay { get; set; }

        public List<ListAllHoursViewModel> Hours { get; set; }
    }
}
