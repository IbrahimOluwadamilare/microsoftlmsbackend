using microsoft_lms_backend.Models.v1.ProductUpload;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models
{
    public class Offers
    {
     [Key]
        public int Id { get; set; }
        public string OfferName { get; set; }
        public string Description { get; set; }
        public virtual Product Product { get; set; }

    }
}
