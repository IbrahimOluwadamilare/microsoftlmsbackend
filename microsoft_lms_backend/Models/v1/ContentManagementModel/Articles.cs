using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.ContentManagementModel
{
    public class Articles
    {
        [Key]
        public int Id { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }
        public string Banner { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsSaving { get; set; }
        public bool IsPublihing { get; set; }
        public virtual ArticleCategory ArticleCategory { get; set; }
    }
}
