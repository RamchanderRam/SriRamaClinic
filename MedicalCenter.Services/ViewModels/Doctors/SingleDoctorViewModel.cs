using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.ViewModels.Doctors
{

    public class SingleDoctorViewModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }
        public string Biography { get; set; }

        public string Specialty { get; set; }

        public string ImagePath { get; set; }

        public bool IsImageApproved { get; set; }
    }
}
