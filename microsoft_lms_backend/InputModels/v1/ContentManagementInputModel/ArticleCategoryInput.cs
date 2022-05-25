using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.ContentManagementInputModel
{
    public class ArticleCategoryInput
    {
        public string CategoryName { get; set; }
        public string CategoryBanner { get; set; }
        public int CategoryId { get; set; }
    }
}
