using microsoft_lms_backend.Models;
using microsoft_lms_backend.Models.v1.ProductUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1
{
    public class OfferListInput
    {
        public string OfferDetail { get; set; }
        public int OfferInputId { get; set; }
    }
}
