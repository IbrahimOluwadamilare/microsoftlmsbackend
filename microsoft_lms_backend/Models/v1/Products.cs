using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public string Billing { get; set; }
        public Decimal Price { get; set; }
        public string Category { get; set; }
    }
}
