using microsoft_lms_backend.Helper;
using microsoft_lms_backend.Models.v1.WebinarModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface IWebinar
    {
        //webinar interface class
        Task<GenericResponse<Webinar>> CreateNewWebinarAsync(Webinar Input);
        Task<GenericResponse<IEnumerable<Webinar>>> GetAllWebinarAsync();
        Task<GenericResponse<Webinar>> GetWebinarbyIdAsync(int Id);
        Task<GenericResponse<Webinar>> UpdateWebinarAsync(Webinar Input);
        Task<GenericResponse<Webinar>> RemoveFromWebinarAsync(int Id);
    }
}
