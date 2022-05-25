using microsoft_lms_backend.Models.v1.ProductUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1
{
    public class OfferInput
    {
        public string OfferName { get; set; }
        public string Description { get; set; }
        public int ProductInputId { get; set; }
    }
}
