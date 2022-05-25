using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1
{
    public class TemplateInput
    {
       // public int templateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TemplateURL { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
