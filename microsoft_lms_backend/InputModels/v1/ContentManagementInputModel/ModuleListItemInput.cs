using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.ContentManagementInputModel
{
    public class ModuleListItemInput
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string CourseVideo { get; set; }
        public int CourseModuleId { get; set; }
    }
}
