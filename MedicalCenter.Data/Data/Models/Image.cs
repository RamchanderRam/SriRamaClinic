using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalCenter.Data.Data.Models
{
    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        public bool IsApproved { get; set; } = false;

        public DateTime CreatedOn { get; set; }

        [Required]
        public string Extension { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
