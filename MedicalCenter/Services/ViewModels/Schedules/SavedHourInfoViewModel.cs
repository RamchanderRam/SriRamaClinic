using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.ViewModels.Schedules
{
    public class SavedHourInfoViewModel
    {
        public string PatientId { get; set; }

        public string FreeHour { get; set; }

        public string Day { get; set; }
        public string Reason { get; set; }

        public string PatientFullName { get; set; }
    }
}
