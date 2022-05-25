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
    public class ModuleListItemController : ControllerBase
    {
        //Dependency injection of ModuleListItem service
        private readonly IModule _moduleService;

        //ModuleListItemController constructor
        public ModuleListItemController(IModule moduleService)
        {
            _moduleService = moduleService;
        }

        //Creating New Module list item post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<ModuleListItem>>> CreateNewModuleListItem([FromBody] ModuleListItemInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var moduleListItem = new ModuleListItem
                    {
                        Title = input.Title,
                        Detail = input.Detail,
                        CourseVideo = input.CourseVideo,
                        Id = input.CourseModuleId
                    };
                    //creating Module list item using the service
                    var newModuleListItem = await _moduleService.CreateNewModuleListItemAsync(moduleListItem);

                    //checking for operation failure
                    if (newModuleListItem.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newModuleListItem);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<ModuleListItem>
                        {
                            Data = newModuleListItem.Data,
                            Message = newModuleListItem.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<ModuleListItem>
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
                return new GenericResponse<ModuleListItem>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All Module list item Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<ModuleListItem>>>> GetAllModuleListItems()
        {
            try
            {
                //Getting all Module list item using the service
                var moduleListItem = await _moduleService.GetAllModuleListItemAsync();

                //checks for operation failure
                if (moduleListItem.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, moduleListItem);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<ModuleListItem>>
                    {
                        Data = moduleListItem.Data,
                        Message = moduleListItem.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<ModuleListItem>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //single Module list item get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<ModuleListItem>>> GetModuleListItemById(int Id)
        {
            try
            {
                //Getting a single Module list item by Id using the service
                var moduleListItem = await _moduleService.GetModuleListItembyIdAsync(Id);

                //checks for operation failure
                if (moduleListItem.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, moduleListItem);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<ModuleListItem>
                    {
                        Data = moduleListItem.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<ModuleListItem>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<ModuleListItem>>> UpdateModuleListItem(int Id, ModuleListItemInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<ModuleListItem>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting a Module list item using the Id
                    var moduleListItemFromDatabase = await _moduleService.GetModuleListItembyIdAsync(Id);

                    //checks for operation failure
                    if (!moduleListItemFromDatabase.Success)
                    {
                        return new GenericResponse<ModuleListItem>
                        {
                            Data = null,
                            Message = "Module list item to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //Changing a Module list item to the new instance
                        moduleListItemFromDatabase.Data.Title = Input.Title;
                        moduleListItemFromDatabase.Data.Detail = Input.Detail;
                        moduleListItemFromDatabase.Data.CourseVideo = Input.CourseVideo;
                        moduleListItemFromDatabase.Data.Id = Input.CourseModuleId;


                        //Updating the Module List Item using the service
                        var newModuleListItem = await _moduleService.UpdateModuleListItemAsync(Id, moduleListItemFromDatabase.Data);

                        //checks for operation failure
                        if (!newModuleListItem.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, moduleListItemFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<ModuleListItem>
                            {
                                Data = moduleListItemFromDatabase.Data,
                                Message = newModuleListItem.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<ModuleListItem>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Module list item Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<ModuleListItem>>> RemoveItemFromModuleListItem(int Id)
        {
            try
            {
                //deleting a Module list item using the service
                var isDeleted = await _moduleService.RemoveFromModuleListItemAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<ModuleListItem>
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
                return new GenericResponse<ModuleListItem>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }


        //Module list item by module Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<ModuleListItem>>>> GetAllModuleListItemByModule(int ModuleId)
        {
            try
            {
                //Getting all Module list items by module Id using the service
                var moduleListItem = await _moduleService.GetAllModuleListItemByModuleAsync(ModuleId);

                //checks for operation failure
                if (moduleListItem.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, moduleListItem);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<ModuleListItem>>
                    {
                        Data = moduleListItem.Data,
                        Message = moduleListItem.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<ModuleListItem>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

    }
}