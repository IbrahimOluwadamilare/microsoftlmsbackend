using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.ContentManagementModel
{
    public class ModuleListItem
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string CourseVideo { get; set; }
        public virtual CourseModule CourseModule { get; set; }
    }

}

