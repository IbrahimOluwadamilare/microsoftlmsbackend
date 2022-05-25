using microsoft_lms_backend.Models.v1.BusinessProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1
{
    public class BusinessContactInput
    {
        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public string BusinessEmail { get; set; }

        public int BusinessPhoneNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
        public int BusinessProfileId { get; set; }
    }
}
