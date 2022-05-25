using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models
{
    public class Templates
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string  Description { get; set; }
        public string TemplateURL { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
