using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface ICategory
    {
        //Category category
        Task<GenericResponse<Category>> CreateNewCategoryAsync(Category Input);
        Task<GenericResponse<IEnumerable<Category>>> GetAllCategoryAsync();
        Task<GenericResponse<Category>> GetCategorybyIdAsync(int Id);
        Task<GenericResponse<Category>> UpdateCategoryAsync(Category Input);
        Task<GenericResponse<Category>> RemoveFromCategoryAsync(int Id);
    }
}

