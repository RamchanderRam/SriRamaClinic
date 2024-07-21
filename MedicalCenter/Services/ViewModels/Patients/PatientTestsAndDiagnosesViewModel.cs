using MedicalCenter.Services.ViewModels.BloodTests;
using MedicalCenter.Services.ViewModels.Diagnoses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.ViewModels.Patients
{
    public class PatientTestsAndDiagnosesViewModel
    {
        public string PatientId { get; set; }

        public ICollection<AllBloodTestsViewModel> BloodTests { get; set; }

        public ICollection<ListAllMedicalExaminationsViewModel> AllMedicalExaminations { get; set; }
    }
}
