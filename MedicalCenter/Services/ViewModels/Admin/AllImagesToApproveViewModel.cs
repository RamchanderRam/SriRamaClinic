using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.ViewModels.Admin
{
    public class AllImagesToApproveViewModel
    {
        public string Id { get; set; }

        public string Path { get; set; }

        public string CreatedOn { get; set; }

        public string DoctorName { get; set; }
    }
}
