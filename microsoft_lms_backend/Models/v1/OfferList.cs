using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models
{
    public class OfferList
    {
        [Key]
        public int Id { get; set; }
        public string OfferDetail { get; set; }
        public virtual Offers Offer { get; set; }
    }
}
