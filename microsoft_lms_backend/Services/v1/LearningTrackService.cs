using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Helper;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Services.v1
{
    public class LearningTrackService : ILearningTrack
    {
        public readonly ApplicationDbContext _dbcontext;

        public LearningTrackService()
        {

        }
        public LearningTrackService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //Service implementation for creating new learning track
        public async Task<GenericResponse<LearningTrack>> CreateNewLearningTrackAsync(LearningTrack Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<LearningTrack>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //check if Course Category exist
                    var courseCategory = await _dbcontext.CourseCategory.FirstOrDefaultAsync(w => w.Id == Input.Id);
                    var learningTrackName = await _dbcontext.LearningTrack.FirstOrDefaultAsync(w => w.TrackName == Input.TrackName);

                    if (courseCategory == null)
                    {
                        return new GenericResponse<LearningTrack>
                        {
                            Data = Input,
                            Message = "Course Category Id does not exit",
                            Success = false
                        };
                    }
                    else if (learningTrackName != null)
                    {
                        return new GenericResponse<LearningTrack>
                        {
                            Data = Input,
                            Message = $"Learning track name '{Input.TrackName}' already exit",
                            Success = false
                        };
                    }
                    else
                    {
                        var learningTrack = new LearningTrack
                        {
                            TrackName = Input.TrackName,
                            TrackDescription = Input.TrackDescription,
                            TrackBanner = Input.TrackBanner,
                            CourseCategory = courseCategory
                        };
                        //If input and course category is not null, add to database
                        await _dbcontext.LearningTrack.AddAsync(learningTrack);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<LearningTrack>
                        {
                            Data = learningTrack,
                            Message = "Learning track added successfully",
                            Success = true
                        };
                    }
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<LearningTrack>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all learning track
        public async Task<GenericResponse<IEnumerable<LearningTrack>>> GetAllLearningTrackAsync()
        {
            try
            {
                //Getting all learning track from database
                var learningTrack = await _dbcontext.LearningTrack.ToListAsync();
                if (learningTrack.Count == 0)
                {
                    return new GenericResponse<IEnumerable<LearningTrack>>
                    {
                        Data = null,
                        Message = "Learning Track is empty",
                        Success = true
                    };
                }
                //returning all learning track gotten from database
                return new GenericResponse<IEnumerable<LearningTrack>>
                {
                    Data = learningTrack,
                    Message = $"successfully gets {learningTrack.Count} learning track(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<LearningTrack>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        //Service implementation for getting a learning track
        public async Task<GenericResponse<LearningTrack>> GetLearningTrackbyIdAsync(int Id)
        {
            try
            {
                //find the learning track by id from the database
                var learningTrack = await _dbcontext.LearningTrack.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (learningTrack == null)
                {
                    return new GenericResponse<LearningTrack>
                    {
                        Data = null,
                        Message = "Learning track does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the learning track
                    return new GenericResponse<LearningTrack>
                    {
                        Data = learningTrack,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<LearningTrack>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for Updating a learning track
        public async Task<GenericResponse<LearningTrack>> UpdateLearningTrackAsync(int Id, LearningTrack Input)
        {
            try
            {   //find the learning track by it id from the database
                var learningTrack = await _dbcontext.LearningTrack.FirstOrDefaultAsync(x => x.Id == Id);

                //find the course category by the provided Id from the database
                var courseCategory = await _dbcontext.CourseCategory.FirstOrDefaultAsync(x => x.Id == Input.Id);

                //If learning track not found
                if (learningTrack == null)
                {
                    return new GenericResponse<LearningTrack>
                    {
                        Data = null,
                        Message = "Learning track to be updated does not exist",
                        Success = false
                    };
                }
                //If course category not found
                else if (courseCategory == null)
                {
                    return new GenericResponse<LearningTrack>
                    {
                        Data = Input,
                        Message = "Course Category Id does not exit",
                        Success = false
                    };
                }
                else
                {
                    var learningTrackUpdate = new LearningTrack
                    {
                        TrackName = Input.TrackName,
                        TrackDescription = Input.TrackDescription,
                        TrackBanner = Input.TrackBanner,
                        CourseCategory = courseCategory
                    };
                    //if learning track and course category found, update with the new changes and save changes
                    var result = _dbcontext.LearningTrack.Update(learningTrackUpdate);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<LearningTrack>
                    {
                        Data = learningTrackUpdate,
                        Message = "Learning Track Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<LearningTrack>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for deleting a learning track
        public async Task<GenericResponse<LearningTrack>> RemoveFromLearningTrackAsync(int Id)
        {
            try
            {
                //find the learning track by id from the database
                var learningTrackToBeRemoved = await _dbcontext.LearningTrack.FirstOrDefaultAsync(x => x.Id == Id);

                //If found, remove it from database and save changes
                if (learningTrackToBeRemoved != null)
                {
                    _dbcontext.Remove(learningTrackToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<LearningTrack>
                    {
                        Data = null,
                        Message = "Learning track has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<LearningTrack>
                    {
                        Data = null,
                        Message = "Not found",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<LearningTrack>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<LearningTrack>>> GetAllLearningTrackByCourseCategoryAsync(int CourseCategoryId)
        {
            try
            {
                //find the article by learning track Id from the database
                var learningTrack = await _dbcontext.LearningTrack.Where(w => w.CourseCategory.Id == CourseCategoryId).ToListAsync();

                //If not found
                if (learningTrack == null)
                {
                    return new GenericResponse<IEnumerable<LearningTrack>>
                    {
                        Data = null,
                        Message = "There is no learning track for this course category",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the article
                    return new GenericResponse<IEnumerable<LearningTrack>>
                    {
                        Data = learningTrack,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<LearningTrack>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }
}
