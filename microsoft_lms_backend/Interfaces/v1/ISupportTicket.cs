using microsoft_lms_backend.Helpers;
// using microsoft_lms.Helper;
using microsoft_lms_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface ISupportTicket
    {
        Task<GenericResponse<SupportTickets>> CreateSupportTicketAsync(SupportTickets supportTickets);
        Task<GenericResponse<SupportTickets>> DeleteSupportTicketAsync(int Id);
        Task<GenericResponse<SupportTickets>> EditSupportAsync(SupportTickets supportTickets);
        Task<GenericResponse<SupportTickets>> GetSupportTicketByIdAsync(int Id);
        Task<GenericResponse<IEnumerable<SupportTickets>>> GetAllSupportTicketsAsync();
    }
}
