using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Helper;
using microsoft_lms_backend.InputModels.v1.ContentManagementInputModel;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.ContentManagementModel;

namespace microsoft_lms_backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        //Dependency injection of Course service
        private readonly ICourse _courseService;

        //CourseController constructor
        public CourseController(ICourse courseService)
        {
            _courseService = courseService;
        }

        //Creating New Course post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Courses>>> CreateNewCourse([FromBody] CourseInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var courses = new Courses
                    {
                        CourseTitle = input.CourseTitle,
                        CourseDescription = input.CourseDescription,
                        DateCreated = DateTime.Now,
                        IsSaving = input.IsSaving,
                        IsPublihing = input.IsPublihing,
                        Id = input.LearningTrackId
                    };
                    //Creating Course using the service
                    var newCourses = await _courseService.CreateNewCourseAsync(courses);

                    //checking for operation failure
                    if (newCourses.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newCourses);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<Courses>
                        {
                            Data = newCourses.Data,
                            Message = newCourses.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<Courses>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = false

                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Courses>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All Courses Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Courses>>>> GetAllCourses()
        {
            try
            {
                //Getting all Course using the service
                var courses = await _courseService.GetAllCourseAsync();

                //checks for operation failure
                if (courses.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, courses);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<Courses>>
                    {
                        Data = courses.Data,
                        Message = courses.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<Courses>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //Single Course get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<Courses>>> GetCourseById(int Id)
        {
            try
            {
                //Getting a single Course by Id using the service
                var courses = await _courseService.GetCoursebyIdAsync(Id);

                //checks for operation failure
                if (courses.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, courses);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<Courses>
                    {
                        Data = courses.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Courses>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Courses>>> UpdateCourse(int Id, CourseInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<Courses>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting Course using the Id
                    var coursesFromDatabase = await _courseService.GetCoursebyIdAsync(Id);

                    //checks for operation failure
                    if (!coursesFromDatabase.Success)
                    {
                        return new GenericResponse<Courses>
                        {
                            Data = null,
                            Message = "Course to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //changing the course to the new instance
                        coursesFromDatabase.Data.CourseTitle = Input.CourseTitle;
                        coursesFromDatabase.Data.CourseDescription = Input.CourseDescription;
                        coursesFromDatabase.Data.DateCreated = coursesFromDatabase.Data.DateCreated;
                        coursesFromDatabase.Data.DateUpdated = DateTime.Now;
                        coursesFromDatabase.Data.IsSaving = Input.IsSaving;
                        coursesFromDatabase.Data.IsPublihing = Input.IsPublihing;
                        coursesFromDatabase.Data.Id = Input.LearningTrackId;

                        //Updating the Course using the service
                        var newCourses = await _courseService.UpdateCourseAsync(Id, coursesFromDatabase.Data);

                        //checks for operation failure
                        if (!newCourses.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, coursesFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<Courses>
                            {
                                Data = coursesFromDatabase.Data,
                                Message = newCourses.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Courses>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Course Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<Courses>>> RemoveItemFromCourses(int Id)
        {
            try
            {
                //deleting a Course using the service
                var isDeleted = await _courseService.RemoveFromCourseAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<Courses>
                    {
                        Data = isDeleted.Data,
                        Message = isDeleted.Message,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Courses>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

        //All Courses by learning track Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Courses>>>> GetAllCourseByLearningTrack(int LearningTrackId)
        {
            try
            {
                //Getting all Course by learning track Id using the service
                var courses = await _courseService.GetAllCourseByLearningTrackAsync(LearningTrackId);

                //checks for operation failure
                if (courses.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, courses);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<Courses>>
                    {
                        Data = courses.Data,
                        Message = courses.Message,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
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