using microsoft_lms_backend.Helper;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface ILearningTrack
    {
        //Learnig Track
        Task<GenericResponse<LearningTrack>> CreateNewLearningTrackAsync(LearningTrack Input);
        Task<GenericResponse<IEnumerable<LearningTrack>>> GetAllLearningTrackAsync();
        Task<GenericResponse<LearningTrack>> GetLearningTrackbyIdAsync(int Id);
        Task<GenericResponse<LearningTrack>> UpdateLearningTrackAsync(int Id, LearningTrack Input);
        Task<GenericResponse<LearningTrack>> RemoveFromLearningTrackAsync(int Id);
        Task<GenericResponse<IEnumerable<LearningTrack>>> GetAllLearningTrackByCourseCategoryAsync(int CourseCategoryId);

    }
}
