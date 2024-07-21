using System;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Data.DataConstants;

namespace MedicalCenter.Data.Data.Models
{
    public class Parameter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ParameterNameMaxLen)]
        public string Name { get; set; }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
