using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.WebinarInputModel
{
    public class WebinarAttendeeInput
    {
        public string AttendeeName { get; set; }
        public string AttendeePhoneNumber { get; set; }
        public string AttendeeEmail { get; set; }
        public string AttendeeOcupation { get; set; }
        public int WebinarId { get; set; }
    }
}
