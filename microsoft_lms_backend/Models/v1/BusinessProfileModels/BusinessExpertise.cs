using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.BusinessProfileModels
{
    public class BusinessExpertise
    {
        [Key]
        public int Id { get; set; }
        public string Expertises { get; set; } 
        public virtual BusinessProfile BusinessProfile  {get; set;}
        public DateTime DateCreated { get; set; }
    }

    // public class Expertises
    // {
    //     [Key]
    //     public int Id { get; set; }
    //     public string  Experience { get; set; }
    //     public BusinessExpertise BusinessExpertise { get; set; }
    //     public DateTime DateUpdate { get; set; }
    //     public DateTime DateCreated { get; set; }
    // }
}
