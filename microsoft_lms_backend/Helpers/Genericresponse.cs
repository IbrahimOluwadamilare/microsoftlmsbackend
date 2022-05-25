using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Helpers
{
    public class GenericResponse<T>
    { 
            public bool Success { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }
        
    }
}
