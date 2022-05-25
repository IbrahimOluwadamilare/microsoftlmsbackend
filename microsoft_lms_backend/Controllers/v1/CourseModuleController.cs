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
    public class CourseModuleController : ControllerBase
    {
        //Dependency injection of CourseModule service
        private readonly IModule _courseModuleService;

        //CourseModuleController constructor
        public CourseModuleController(IModule courseModuleService)
        {
            _courseModuleService = courseModuleService;
        }

        //Creating New Course Module post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<CourseModule>>> CreateNewCourseModule([FromBody] CourseModuleInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var courseModule = new CourseModule
                    {
                        ModuleTitle = input.ModuleTitle,
                        Id = input.CourseId
                    };
                    //creating Course Module using the service
                    var newCourseModule = await _courseModuleService.CreateNewModuleAsync(courseModule);

                    //checking for operation failure
                    if (newCourseModule.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newCourseModule);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<CourseModule>
                        {
                            Data = newCourseModule.Data,
                            Message = newCourseModule.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<CourseModule>
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
                return new GenericResponse<CourseModule>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All Modules Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<CourseModule>>>> GetAllCourseModule()
        {
            try
            {
                //Getting all Course Module using the service
                var courseModule = await _courseModuleService.GetAllModuleAsync();

                //checks for operation failure
                if (courseModule.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, courseModule);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<CourseModule>>
                    {
                        Data = courseModule.Data,
                        Message = courseModule.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<CourseModule>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //single Module get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<CourseModule>>> GetCourseModuleById(int Id)
        {
            try
            {
                //Getting a single Course Module by Id using the service
                var courseModule = await _courseModuleService.GetModulebyIdAsync(Id);

                //checks for operation failure
                if (courseModule.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, courseModule);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<CourseModule>
                    {
                        Data = courseModule.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<CourseModule>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<CourseModule>>> UpdateCourseModule(int Id, CourseModuleInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<CourseModule>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting a Course Module using the Id
                    var courseModuleFromDatabase = await _courseModuleService.GetModulebyIdAsync(Id);

                    //checks for operation failure
                    if (!courseModuleFromDatabase.Success)
                    {
                        return new GenericResponse<CourseModule>
                        {
                            Data = null,
                            Message = "Course module to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //Changing the Module to the new instance
                        courseModuleFromDatabase.Data.ModuleTitle = Input.ModuleTitle;
                        courseModuleFromDatabase.Data.Id = Input.CourseId;

                        //Updating the Course Module using the service
                        var newCourseModule = await _courseModuleService.UpdateModuleAsync(Id, courseModuleFromDatabase.Data);

                        //checks for operation failure
                        if (!newCourseModule.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, courseModuleFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<CourseModule>
                            {
                                Data = courseModuleFromDatabase.Data,
                                Message = newCourseModule.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<CourseModule>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Module Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<CourseModule>>> RemoveItemFromCourseModule(int Id)
        {
            try
            {
                //deleting a Course Module using the service
                var isDeleted = await _courseModuleService.RemoveFromModuleAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<CourseModule>
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
                return new GenericResponse<CourseModule>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
        //All Modules by course Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<CourseModule>>>> GetAllModuleByCourseAsync(int CourseId)
        {
            try
            {
                //Getting all Module by course Id using the service
                var courseModule = await _courseModuleService.GetAllModuleByCourseAsync(CourseId);

                //checks for operation failure
                if (courseModule.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, courseModule);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<CourseModule>>
                    {
                        Data = courseModule.Data,
                        Message = courseModule.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<CourseModule>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

    }
}