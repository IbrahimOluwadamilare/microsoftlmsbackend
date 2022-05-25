using microsoft_lms_backend.Helper;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface IModule
    {
        //course module
        Task<GenericResponse<CourseModule>> CreateNewModuleAsync(CourseModule Input);
        Task<GenericResponse<IEnumerable<CourseModule>>> GetAllModuleAsync();
        Task<GenericResponse<CourseModule>> GetModulebyIdAsync(int Id);
        Task<GenericResponse<CourseModule>> UpdateModuleAsync(int Id, CourseModule Input);
        Task<GenericResponse<CourseModule>> RemoveFromModuleAsync(int Id);
        Task<GenericResponse<IEnumerable<CourseModule>>> GetAllModuleByCourseAsync(int CourseId);


        //module List item
        Task<GenericResponse<ModuleListItem>> CreateNewModuleListItemAsync(ModuleListItem Input);
        Task<GenericResponse<IEnumerable<ModuleListItem>>> GetAllModuleListItemAsync();
        Task<GenericResponse<ModuleListItem>> GetModuleListItembyIdAsync(int Id);
        Task<GenericResponse<ModuleListItem>> UpdateModuleListItemAsync(int Id, ModuleListItem Input);
        Task<GenericResponse<ModuleListItem>> RemoveFromModuleListItemAsync(int Id);
        Task<GenericResponse<IEnumerable<ModuleListItem>>> GetAllModuleListItemByModuleAsync(int ModuleId);

    }
}
