

using System;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Data.DataConstants;

namespace MedicalCenter.Data.Data.Models
{
    public class MedicalExamination
    {
        public MedicalExamination()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MinLength(ExaminationDiagnoseMinLen)]
        [MaxLength(ExaminationDiagnoseMaxLen)]
        public string Diagnose { get; set; }

        [Required]
        public string PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
