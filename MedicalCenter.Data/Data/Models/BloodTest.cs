using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Data.Data.Models
{
    public class BloodTest
    {
        public BloodTest()
        {
            this.Id = Guid.NewGuid().ToString();
            this.BloodTestsPatients = new HashSet<BloodTestsPatients>();
        }

        [Key]
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsCompleted { get; set; } = false;

        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        [Required]
        public string PatientId { get; set; }

        public virtual Patient Patient{ get; set; }

        public virtual ICollection<BloodTestsPatients> BloodTestsPatients { get; set; }

    }
}
