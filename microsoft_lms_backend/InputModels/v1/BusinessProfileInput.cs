using microsoft_lms_backend.Models.v1.BusinessProfileModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1
{
    
    public class BusinessProfileInput
    {
       // public int ProfileId { get; set; }
        public string Banner { get; set; }
        [Required]
        public string Name { get; set; }
        public string Detail { get; set; }
        public string FacebookSocial { get; set; }
        public string TwitterSocial { get; set; }
        public string LinkedInSocial { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
