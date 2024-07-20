using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Data.DataConstants;

namespace MedicalCenter.Data.Data.Models
{
    public class Hour
    {
        [Key]
        public int Id { get; set; }

        public DateTime FreeHour { get; set; }


        [MaxLength(HourReasonMaxLen)]
        public string Reason { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsFree { get; set; } = true;

        public int ScheduleId { get; set; }

        public virtual Schedule Schedule { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
