using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.IdentityModels
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set;  }
    }
}
