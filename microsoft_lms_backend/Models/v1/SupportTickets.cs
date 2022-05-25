using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models
{
    public class SupportTickets
    {
        [Key]
        public int Id { get; set; }
        public string Department { get; set; }
        public int Priority { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        //public DateTime DateCreated { get; set; }
        //public DateTime DateUpdated { get; set; }
        //public DateTime DateResolved { get; set; }
        public string CaseOwner { get; set; }
        public string Attachment { get; set; }
        public string Response { get; set; }

    }
}
