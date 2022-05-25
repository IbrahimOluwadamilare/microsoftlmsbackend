using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.ContentManagementModel
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsSaving { get; set; }
        public bool IsPublihing { get; set; }
        public virtual LearningTrack LearningTrack { get; set; } = null;
    }
}
