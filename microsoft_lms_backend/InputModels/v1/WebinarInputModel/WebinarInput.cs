using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.WebinarInputModel
{
    public class WebinarInput
    {
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public string VideoURL { get; set; }
        public DateTime EventDate { get; set; }
    }
}
