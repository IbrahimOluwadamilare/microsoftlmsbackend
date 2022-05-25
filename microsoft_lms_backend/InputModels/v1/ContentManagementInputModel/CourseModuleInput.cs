using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.ContentManagementInputModel
{
    public class CourseModuleInput
    {
        public string ModuleTitle { get; set; }
        public int CourseId { get; set; }
    }
}
