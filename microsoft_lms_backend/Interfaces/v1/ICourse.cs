using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface ICourse
    {
        //course category
        Task<GenericResponse<CourseCategory>> CreateNewCourseCategoryAsync(CourseCategory Input);
        Task<GenericResponse<IEnumerable<CourseCategory>>> GetAllCourseCategoryAsync();
        Task<GenericResponse<CourseCategory>> GetCourseCategorybyIdAsync(int Id);
        Task<GenericResponse<CourseCategory>> UpdateCourseCategoryAsync(int Id, CourseCategory Input);
        Task<GenericResponse<CourseCategory>> RemoveFromCourseCategoryAsync(int Id);

        //course 
        Task<GenericResponse<Courses>> CreateNewCourseAsync(Courses Input);
        Task<GenericResponse<IEnumerable<Courses>>> GetAllCourseAsync();
        Task<GenericResponse<Courses>> GetCoursebyIdAsync(int Id);
        Task<GenericResponse<Courses>> UpdateCourseAsync(int Id, Courses Input);
        Task<GenericResponse<Courses>> RemoveFromCourseAsync(int Id);
        Task<GenericResponse<IEnumerable<Courses>>> GetAllCourseByLearningTrackAsync(int LearningTrackId);
    }
}
