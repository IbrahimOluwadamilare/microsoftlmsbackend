using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1
{
    public class SupportTicketsInput
    {
        public string Department { get; set; }
        public int Priority { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string CaseOwner { get; set; }
        public string Attachment { get; set; }
        public string Response { get; set; }
    }
}
