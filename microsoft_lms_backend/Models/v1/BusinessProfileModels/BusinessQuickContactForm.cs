using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.BusinessProfileModels
{
    public class BusinessQuickContactForm
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }

        [Required, MaxLength(80)]
        [Display(Name = "MessageTitle")]
        public string MessageTitle { get; set; }

        [Display(Name = "Message")]

        [Required, MaxLength(200)]
        public string MessageBody { get; set; }
        // public virtual BusinessContact BusinessEmail { get; set; }
    }
}
