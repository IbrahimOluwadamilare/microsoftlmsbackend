using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.BusinessProfileModels
{
    public class BusinessContact
    {
        [Key]
        public int Id { get; set; } 
        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public string BusinessEmail { get; set; }

        public int BusinessPhoneNumber { get; set;  }

        public DateTime DateCreated { get; set; }
        public virtual BusinessProfile BusinessProfile { get; set; }
    }
}
