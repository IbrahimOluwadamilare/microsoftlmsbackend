using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.ContentManagementInputModel
{
    public class NewsInput
    {
        public string NewsTitle { get; set; }
        public string NewsBanner { get; set; }
        public string NewsCategory { get; set; }
        public string PublishedBy { get; set; }
        public DateTime PublishingDate { get; set; }
        public bool IsSaving { get; set; }
        public bool IsPublihing { get; set; }
        public int CategoryId { get; set; }
    }
}
