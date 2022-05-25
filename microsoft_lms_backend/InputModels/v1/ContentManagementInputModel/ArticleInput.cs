using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.ContentManagementInputModel
{
    public class ArticleInput
    {
        public string ArticleTitle { get; set; }
        public string Banner { get; set; }
        public string ArticleContent { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsSaving { get; set; }
        public bool IsPublihing { get; set; }
        public int ArticleCategoryId { get; set; }
    }
}
