using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Services.v1
{
    public class CourseService : ICourse
    {
        public readonly ApplicationDbContext _dbcontext;

        public CourseService()
        {

        }
        public CourseService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

//course Category
        //Service implementation for creating new course Category
        public async Task<GenericResponse<CourseCategory>> CreateNewCourseCategoryAsync(CourseCategory Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<CourseCategory>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = true
                    };
                }
                else
                {
                    //check if category exit
                    var category = await _dbcontext.Category.FirstOrDefaultAsync(w => w.Id == Input.Id);
                    var courseCategoryName = await _dbcontext.CourseCategory.FirstOrDefaultAsync(w => w.CategoryName == Input.CategoryName);

                    if (category == null)
                    {
                        return new GenericResponse<CourseCategory>
                        {
                            Data = Input,
                            Message = "Category Id does not exit",
                            Success = false
                        };
                    }
                    else if (courseCategoryName != null)
                    {
                        return new GenericResponse<CourseCategory>
                        {
                            Data = Input,
                            Message = $"Course Category Name '{Input.CategoryName}' already exit",
                            Success = false
                        };
                    }
                    else
                    {
                        var courseCategory = new CourseCategory
                        {
                            CategoryName = Input.CategoryName,
                            CategoryBanner = Input.CategoryBanner,
                            Category = category
                        };
                        //If input and category is not null, add to database
                        await _dbcontext.CourseCategory.AddAsync(courseCategory);
                        _dbcontext.SaveChanges();
                        return new GenericResponse<CourseCategory>
                        {
                            Data = courseCategory,
                            Message = "Course Category added successfully",
                            Success = true
                        };
                    }
                    
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<CourseCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all Course Category
        public async Task<GenericResponse<IEnumerable<CourseCategory>>> GetAllCourseCategoryAsync()
        {
            try
            {
                //Getting all Course Category from database
                var courseCategory = await _dbcontext.CourseCategory.ToListAsync();
                if (courseCategory.Count == 0)
                {
                    return new GenericResponse<IEnumerable<CourseCategory>>
                    {
                        Data = null,
                        Message = "Course Category is empty",
                        Success = false
                    };
                }
                //returning all Course Category gotten from database
                return new GenericResponse<IEnumerable<CourseCategory>>
                {
                    Data = courseCategory,
                    Message = $"successfully gets {courseCategory.Count} category(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<CourseCategory>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        //Service implementation for getting a single Course Category
        public async Task<GenericResponse<CourseCategory>> GetCourseCategorybyIdAsync(int Id)
        {
            try
            {
                //find the Course Category by id from the database
                var courseCategory = await _dbcontext.CourseCategory.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (courseCategory == null)
                {
                    return new GenericResponse<CourseCategory>
                    {
                        Data = null,
                        Message = "Course category does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the course category
                    return new GenericResponse<CourseCategory>
                    {
                        Data = courseCategory,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<CourseCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for Updating a Course Category
        public async Task<GenericResponse<CourseCategory>> UpdateCourseCategoryAsync(int Id, CourseCategory Input)
        {
            try
            {
                //find the Course Category by it id from the database
                var courseCategory = await _dbcontext.CourseCategory.FirstOrDefaultAsync(x => x.Id == Id);
                //find the Category by it id given
                var category = await _dbcontext.Category.FirstOrDefaultAsync(x => x.Id == Input.Id);

                //If courseCategory not found
                if (courseCategory == null)
                {
                    return new GenericResponse<CourseCategory>
                    {
                        Data = null,
                        Message = "Course category to be updated does not exist",
                        Success = false
                    };
                }
                //If category not found
                else if (category == null)
                {
                    return new GenericResponse<CourseCategory>
                    {
                        Data = Input,
                        Message = "Category Id does not exit",
                        Success = false
                    };
                }
                else
                {
                    var courseCategoryUpdate = new CourseCategory
                    {
                        CategoryName = Input.CategoryName,
                        CategoryBanner = Input.CategoryBanner,
                        Category = category
                    };
                    //if both found, update with the new changes and save changes
                    var result = _dbcontext.CourseCategory.Update(courseCategoryUpdate);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<CourseCategory>
                    {
                        Data = courseCategoryUpdate,
                        Message = "Course Category Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<CourseCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for deleting a Course Category
        public async Task<GenericResponse<CourseCategory>> RemoveFromCourseCategoryAsync(int Id)
        {
            try
            {
                //find the Course Category by id from the database
                var courseCategoryToBeRemoved = await _dbcontext.CourseCategory.FirstOrDefaultAsync(x => x.Id == Id);

                //If found, remove it from database and save changes
                if (courseCategoryToBeRemoved != null)
                {
                    _dbcontext.Remove(courseCategoryToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<CourseCategory>
                    {
                        Data = null,
                        Message = "Course Category has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<CourseCategory>
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
                return new GenericResponse<CourseCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }


//Course
        //Service implementation for creating new course
        public async Task<GenericResponse<Courses>> CreateNewCourseAsync(Courses Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<Courses>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //check if learning track exit
                    var learningTrack = await _dbcontext.LearningTrack.FirstOrDefaultAsync(w => w.Id == Input.Id);

                    if (learningTrack == null)
                    {
                        return new GenericResponse<Courses>
                        {
                            Data = Input,
                            Message = "Learning track Id does not exit",
                            Success = false
                        };
                    }
                    
                    else
                    {   //If Learning track is not null, create a new instance of course
                        var course = new Courses
                        {
                            CourseTitle =Input.CourseTitle,
                            CourseDescription = Input.CourseDescription,
                            DateCreated = DateTime.Now,
                            IsSaving = Input.IsSaving,
                            IsPublihing = Input.IsPublihing,
                            LearningTrack = learningTrack
                        };
                        //add it to database
                        await _dbcontext.Courses.AddAsync(course);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<Courses>
                        {
                            Data = course,
                            Message = "Course added successfully",
                            Success = true
                        };
                    }
                   
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Courses>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all Course
        public async Task<GenericResponse<IEnumerable<Courses>>> GetAllCourseAsync()
        {
            try
            {
                //Getting all Course from database
                var course = await _dbcontext.Courses.ToListAsync();
                if (course.Count == 0)
                {
                    return new GenericResponse<IEnumerable<Courses>>
                    {
                        Data = null,
                        Message = "Course is empty",
                        Success = true
                    };
                }
                //returning all Course gotten from database
                return new GenericResponse<IEnumerable<Courses>>
                {
                    Data = course,
                    Message = $"successfully gets {course.Count} course(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<Courses>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        //Service implementation for getting a single Course
        public async Task<GenericResponse<Courses>> GetCoursebyIdAsync(int Id)
        {
            try
            {
                //find the Course by id from the database
                var course = await _dbcontext.Courses.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (course == null)
                {
                    return new GenericResponse<Courses>
                    {
                        Data = null,
                        Message = "Course does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the course 
                    return new GenericResponse<Courses>
                    {
                        Data = course,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Courses>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for Updating a Course 
        public async Task<GenericResponse<Courses>> UpdateCourseAsync(int Id, Courses Input)
        {
            try
            {
                //find the Course by it id from the database
                var course = await _dbcontext.Courses.FirstOrDefaultAsync(x => x.Id == Id);
                //find the learning Track by the provided id 
                var learningTrack = await _dbcontext.LearningTrack.FirstOrDefaultAsync(x => x.Id == Input.Id);

                //If Course not found
                if (course== null)
                {
                    return new GenericResponse<Courses>
                    {
                        Data = null,
                        Message = "Course to be updated does not exist",
                        Success = false
                    };
                }
                //If learning Track not found
                else if (learningTrack == null)
                {
                    return new GenericResponse<Courses>
                    {
                        Data = Input,
                        Message = "Learning track Id does not exit",
                        Success = false
                    };
                }
                else
                {
                    var courseUpdate = new Courses
                    {
                        CourseTitle = Input.CourseTitle,
                        CourseDescription = Input.CourseDescription,
                        DateCreated = DateTime.Now,
                        IsSaving = Input.IsSaving,
                        IsPublihing = Input.IsPublihing,
                        LearningTrack = learningTrack
                    };
                    //if both found, update with the new changes and save changes
                    var result = _dbcontext.Courses.Update(courseUpdate);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Courses>
                    {
                        Data = courseUpdate,
                        Message = "Course Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Courses>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for deleting a Course
        public async Task<GenericResponse<Courses>> RemoveFromCourseAsync(int Id)
        {
            try
            {
                //find the Course by id from the database
                var courseToBeRemoved = await _dbcontext.Courses.FirstOrDefaultAsync(x => x.Id == Id);

                //If found, remove it from database and save changes
                if (courseToBeRemoved != null)
                {
                    _dbcontext.Remove(courseToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Courses>
                    {
                        Data = null,
                        Message = "Course has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<Courses>
                    {
                        Data = null,
                        Message = "Not found",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Courses>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all Course by learning track
        public async Task<GenericResponse<IEnumerable<Courses>>> GetAllCourseByLearningTrackAsync(int LearningTrackId)
        {
            try
            {
                //find the courses by learning track Id from the database
                var courses = await _dbcontext.Courses.Where(w => w.LearningTrack.Id == LearningTrackId).ToListAsync();

                //If not found
                if (courses == null)
                {
                    return new GenericResponse<IEnumerable<Courses>>
                    {
                        Data = null,
                        Message = "There is no course for this learning track",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the article
                    return new GenericResponse<IEnumerable<Courses>>
                    {
                        Data = courses,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<Courses>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }
}
