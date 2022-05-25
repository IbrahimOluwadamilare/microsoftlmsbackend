using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.ContentManagementInputModel
{
    public class CourseInput
    {
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public bool IsSaving { get; set; }
        public bool IsPublihing { get; set; }
        public int LearningTrackId { get; set; }
    }
}
