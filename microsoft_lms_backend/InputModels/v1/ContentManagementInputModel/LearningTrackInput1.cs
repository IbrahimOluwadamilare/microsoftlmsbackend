using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.ContentManagementInputModel
{
    public class LearningTrackInput
    {
        public string TrackName { get; set; }
        public string TrackDescription { get; set; }
        public string TrackBanner { get; set; }
        public int CourseCategoryId { get; set; }
    }
}
