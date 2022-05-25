using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.ProductUploadInputModel
{
    public class ProductTypeInput
    {
        public int Id { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
    }
}
