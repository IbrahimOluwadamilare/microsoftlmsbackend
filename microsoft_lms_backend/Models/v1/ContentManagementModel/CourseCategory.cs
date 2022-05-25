using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.ContentManagementModel
{
    public class CourseCategory
    {

        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryBanner { get; set; }
        public virtual Category Category { get; set; }
    }
}
