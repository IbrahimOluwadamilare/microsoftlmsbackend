using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.ContentManagementModel
{
    public class CourseModule
    {
        [Key]
        public int Id { get; set; }
        public string ModuleTitle { get; set; }
        public virtual Courses Courses { get; set; }
    }
}
