using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.ViewModels.Patients
{
    public class PatientProfileViewModel
    {
        public string FullName { get; set; }

        
        public string Country { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }

       
        public string EGN { get; set; }

        public string DateOfBirth { get; set; }

        public int Age { get; set; }
        public string Id { get; set; }

        public List<PatientProfileViewModel> Patients { get; set; }

    }
}
