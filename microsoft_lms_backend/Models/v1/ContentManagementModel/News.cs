using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.ContentManagementModel
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string NewsTitle { get; set; }
        public string NewsBanner { get; set; }
        public string NewsCategory { get; set; }
        public string PublishedBy { get; set; }
        public DateTime PublishingDate { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsSaving { get; set; }
        public bool IsPublihing { get; set; }
        public virtual Category Category { get; set; }
    }
}
