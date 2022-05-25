using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.ContentManagementModel
{
    public class LearningTrack
    {
        [Key]
        public int Id { get; set; }
        public string TrackName { get; set; }
        public string TrackDescription { get; set; }
        public string TrackBanner { get; set; }
        public virtual CourseCategory CourseCategory { get; set; }
    }
}
