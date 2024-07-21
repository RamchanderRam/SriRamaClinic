using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalCenter.Data.Data.Models
{
    public class Schedule
    {
        public Schedule()
        {
            this.Hours = new HashSet<Hour>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Hour> Hours { get; set; }
    }
}
