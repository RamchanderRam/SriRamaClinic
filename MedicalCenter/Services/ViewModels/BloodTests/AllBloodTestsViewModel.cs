using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.ViewModels.BloodTests
{
    public class AllBloodTestsViewModel
    {
        public string Id { get; set; }
        public string CreatedOn { get; set; }

        public string DoctorFullName { get; set; }

        public bool IsCompleted { get; set; }
    }
}
