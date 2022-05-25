using microsoft_lms_backend.Models.v1;
using microsoft_lms_backend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using microsoft_lms_backend.Models;

namespace microsoft_lms_backend.Interfaces.v1
{
  public interface ITemplateService
    {
        Task<GenericResponse<Templates>> CreateTemplateAsync(Templates templates);
        Task<GenericResponse<Templates>> DeleteTemplateAsync(int Id);
        Task<GenericResponse<Templates>> EditTemplateAsync(Templates templates);
        Task<GenericResponse<Templates>> GetTemplateByIdAsync(int Id);
        Task<GenericResponse<IEnumerable<Templates>>> GetAllTemplatesAsync();

    }
}
 