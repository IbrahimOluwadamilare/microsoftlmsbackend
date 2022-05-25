using microsoft_lms_backend.Helper;
using microsoft_lms_backend.Models.v1.WebinarModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface IWebinarAttendee
    {
        Task<GenericResponse<WebinarAttendee>> CreateNewWebinarAttendeeAsync(WebinarAttendee Input);
        Task<GenericResponse<IEnumerable<WebinarAttendee>>> GetAllWebinarAttendeeAsync();
        Task<GenericResponse<WebinarAttendee>> GetWebinarAttendeebyIdAsync(int Id);
        Task<GenericResponse<WebinarAttendee>> RemoveFromWebinarAttendeeAsync(int Id);
        Task<GenericResponse<IEnumerable<WebinarAttendee>>> GetAllWebinarAttendeeByWebinarIdAsync( int WebinarAttendeeId);

    }
}
