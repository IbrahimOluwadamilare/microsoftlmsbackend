using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microsoft_lms_backend.Models.v1.WebinarModels
{
    public class WebinarAttendee
    {
        [Key]
        public int Id { get; set; }
        public string AttendeeName { get; set; }
        public string AttendeePhoneNumber { get; set; }
        public string AttendeeEmail { get; set; }
        public string AttendeeOcupation { get; set; }
        public virtual Webinar Webinar { get; set; }
    }
}
