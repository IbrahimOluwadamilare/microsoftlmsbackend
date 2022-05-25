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
    public class CourseCategoryController : ControllerBase
    {
        //Dependency injection of CourseCategory service
        private readonly ICourse _courseCategoryService;

        //CourseCategoryController constructor
        public CourseCategoryController(ICourse courseCategoryService)
        {
            _courseCategoryService = courseCategoryService;
        }

        //Creating New Course Category post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<CourseCategory>>> CreateNewCourseCategory([FromBody] CourseCategoryInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {

                    var courseCategory = new CourseCategory
                    {
                        CategoryName = input.CategoryName,
                        CategoryBanner = input.CategoryBanner,
                        Id = input.CategoryId
                    };
                    //Creating Course category using the service
                    var newCourseCategory = await _courseCategoryService.CreateNewCourseCategoryAsync(courseCategory);

                    //checking for operation failure
                    if (newCourseCategory.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newCourseCategory);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<CourseCategory>
                        {
                            Data = newCourseCategory.Data,
                            Message = newCourseCategory.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<CourseCategory>
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
                return new GenericResponse<CourseCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All Course Categorys Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<CourseCategory>>>> GetAllCourseCategories()
        {
            try
            {
                //Getting all Course Category using the service
                var CourseCategory = await _courseCategoryService.GetAllCourseCategoryAsync();

                //checks for operation failure
                if (CourseCategory.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, CourseCategory);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<CourseCategory>>
                    {
                        Data = CourseCategory.Data,
                        Message = CourseCategory.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<CourseCategory>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //Single CourseCategory get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<CourseCategory>>> GetCourseCategoryById(int Id)
        {
            try
            {
                //Getting a single Course category by Id using the service
                var CourseCategory = await _courseCategoryService.GetCourseCategorybyIdAsync(Id);

                //checks for operation failure
                if (CourseCategory.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, CourseCategory);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<CourseCategory>
                    {
                        Data = CourseCategory.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<CourseCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<CourseCategory>>> UpdateCourseCategory(int Id, CourseCategoryInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<CourseCategory>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting Course Category using the Id
                    var CourseCategoryFromDatabase = await _courseCategoryService.GetCourseCategorybyIdAsync(Id);

                    //checks for operation failure
                    if (!CourseCategoryFromDatabase.Success)
                    {
                        return new GenericResponse<CourseCategory>
                        {
                            Data = null,
                            Message = "CourseCategory to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //changing the course category to the new instance
                        CourseCategoryFromDatabase.Data.CategoryName = Input.CategoryName;
                        CourseCategoryFromDatabase.Data.CategoryBanner = Input.CategoryBanner;
                        CourseCategoryFromDatabase.Data.Id = Input.CategoryId;

                        //Updating the Course category using the service
                        var newCourseCategory = await _courseCategoryService.UpdateCourseCategoryAsync(Id, CourseCategoryFromDatabase.Data);

                        //checks for operation failure
                        if (!newCourseCategory.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, CourseCategoryFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<CourseCategory>
                            {
                                Data = CourseCategoryFromDatabase.Data,
                                Message = newCourseCategory.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<CourseCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Course Category Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<CourseCategory>>> RemoveItemFromCourseCategory(int Id)
        {
            try
            {
                //deleting a Course Category using the service
                var isDeleted = await _courseCategoryService.RemoveFromCourseCategoryAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<CourseCategory>
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
                return new GenericResponse<CourseCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }
    }
}