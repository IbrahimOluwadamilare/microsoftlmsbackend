using microsoft_lms_backend.Models.v1.BusinessProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1
{
    public class BusinessExpertiseInput
    {
        public string Expertises { get; set; }
        public int BusinessProfileId  { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
