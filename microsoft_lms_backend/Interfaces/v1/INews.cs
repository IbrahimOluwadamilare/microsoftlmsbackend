using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface INews
    {
        //news
        Task<GenericResponse<News>> CreateNewsAsync(News Input);
        Task<GenericResponse<IEnumerable<News>>> GetAllNewsAsync();
        Task<GenericResponse<News>> GetNewsbyIdAsync(int Id);
        Task<GenericResponse<News>> UpdateNewsAsync(int Id, News Input);
        Task<GenericResponse<News>> RemoveFromNewsAsync(int Id);
    }
}
