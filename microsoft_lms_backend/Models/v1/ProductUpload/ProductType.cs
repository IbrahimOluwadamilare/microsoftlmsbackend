using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models.v1.ProductUpload
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }

    }
}
