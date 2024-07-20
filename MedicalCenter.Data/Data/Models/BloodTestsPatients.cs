using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Data.Data.Models
{
    public class BloodTestsPatients
    {
        public string BloodTestId { get; set; }

        public virtual BloodTest BloodTest { get; set; }

        public string PatientId { get; set; }

        public virtual Patient Patient{ get; set; }

        public int ParameterId { get; set; }

        public virtual Parameter Paramater { get; set; }

        public double? Value { get; set; }
    }
}
