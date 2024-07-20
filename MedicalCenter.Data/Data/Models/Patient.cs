using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Data.DataConstants;

namespace MedicalCenter.Data.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            this.Id = Guid.NewGuid().ToString();
            this.BloodTests = new HashSet<BloodTest>();
            this.BloodTestsPatients = new HashSet<BloodTestsPatients>();
            this.MedicalExaminations = new HashSet<MedicalExamination>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(PatientCountryMaxLen)]
        public string Country { get; set; }

        [Required]
        [MaxLength(PatientTownMaxLen)]
        public string Town { get; set; }

        [Required]
        [MaxLength(PatientAddressMaxLen)]
        public string Address { get; set; }

        [Required]
        [MaxLength(PatientEGNMaxLen)]
        public string EGN { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<BloodTest> BloodTests { get; set; }
        public virtual ICollection<BloodTestsPatients> BloodTestsPatients { get; set; }
        public virtual ICollection<MedicalExamination> MedicalExaminations { get; set; }
    }
}
