using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace MedicalCenter.Services.ViewModels.PRos
{
    public class PRo
    {
        public int Id { get; set; }
        public string? HospitalName { get; set; }


        public string Location { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public string Reason { get; set; }




    }
}
