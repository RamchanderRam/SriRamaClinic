using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static MedicalCenter.Data.DataConstants;
#nullable disable


namespace MedicalCenter.Data.Data.Models
{
    [Table("PRos")]
    public class PRos
    {
        [Key]
        public int Id { get; set; }
        public string? Hospital { get; set; }
        public string Location { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Reason { get; set; }
        public string UserId { get; set; }

    }
}
