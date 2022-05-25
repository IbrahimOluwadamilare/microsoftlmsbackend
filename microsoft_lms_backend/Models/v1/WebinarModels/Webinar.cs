using System;
using System.ComponentModel.DataAnnotations;

namespace microsoft_lms_backend.Models.v1.WebinarModels
{
    public class Webinar
    {
        [Key]
        public int Id { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public string VideoURL { get; set; } 
        public DateTime EventDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
