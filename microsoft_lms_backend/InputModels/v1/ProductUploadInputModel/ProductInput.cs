using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.InputModels.v1.ProductUploadInputModel
{
    public class ProductInput
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public int ProductTypeId { get; set; }
    }
}
