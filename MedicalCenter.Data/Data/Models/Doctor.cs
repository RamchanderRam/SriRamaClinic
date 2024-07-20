using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Data.DataConstants;

namespace MedicalCenter.Data.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
            this.Schedules = new HashSet<Schedule>();
            this.MedicalExaminations = new HashSet<MedicalExamination>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DoctorSpecialtyMaxLen)]
        public string Specialty { get; set; }

        [Required]
        public string Biography { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsDeleted { get; set; }

      
        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<MedicalExamination> MedicalExaminations { get; set; }
    }
}
