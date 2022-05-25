using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Helper;
using microsoft_lms_backend.InputModels.v1.WebinarInputModel;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.WebinarModels;

namespace microsoft_lms_backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class WebinarController : ControllerBase
    {
        //Dependency injection of webinar service
        private readonly IWebinar _webinarService;

        //WebinarController constructor
        public WebinarController(IWebinar webinarService)
        {
            _webinarService = webinarService;
        }

        //Creating New Webinar post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Webinar>>> CreateNewWebinar([FromBody] WebinarInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var webinar = new Webinar
                    {
                        EventTitle = input.EventTitle,
                        EventDescription = input.EventDescription,
                        EventDate = input.EventDate,
                        DateCreated = DateTime.Now,
                    };
                    //creating webinar using the service
                    var newWebinar = await _webinarService.CreateNewWebinarAsync(webinar);

                    //checking for operation failure
                    if (newWebinar.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newWebinar);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<Webinar>
                        {
                            Data = newWebinar.Data,
                            Message = newWebinar.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<Webinar>
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
                return new GenericResponse<Webinar>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }
           
        }

        //All Webinars Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Webinar>>>> GetAllWebinars()
        {
            try
            {                  
                //Getting all webinar using the service
                var webinar = await _webinarService.GetAllWebinarAsync();

                //checks for operation failure
                if (webinar.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, webinar);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<Webinar>>
                    {
                        Data = webinar.Data,
                        Message = webinar.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<Webinar>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //single Webinar get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<Webinar>>> GetWebinarById(int Id)
        {
            try
            {
                //Getting a single webinar by Id using the service
                var webinar = await _webinarService.GetWebinarbyIdAsync(Id);

                //checks for operation failure
                if (webinar.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, webinar);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<Webinar>
                    {
                        Data = webinar.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Webinar>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Webinar>>> UpdateWebinar(int Id, WebinarInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<Webinar>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting webinar using the Id
                    var WebinarFromDatabase = await _webinarService.GetWebinarbyIdAsync(Id);

                    //checks for operation failure
                    if (!WebinarFromDatabase.Success)
                    {
                        return new GenericResponse<Webinar>
                        {
                            Data = null,
                            Message = "Webinar to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //changing the webinar to the new instance
                        WebinarFromDatabase.Data.EventTitle = Input.EventTitle;
                        WebinarFromDatabase.Data.EventDescription = Input.EventDescription;
                        WebinarFromDatabase.Data.EventDate = Input.EventDate;
                        WebinarFromDatabase.Data.DateCreated = WebinarFromDatabase.Data.DateCreated;
                        WebinarFromDatabase.Data.DateUpdated = DateTime.Now;

                        //Updating the webinar using the service
                        var newWebinar = await _webinarService.UpdateWebinarAsync(WebinarFromDatabase.Data);
                        
                        //checks for operation failure
                        if (!newWebinar.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, WebinarFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<Webinar>
                            {
                                Data = WebinarFromDatabase.Data,
                                Message = newWebinar.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Webinar>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Webinar Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<Webinar>>> RemoveItemFromWebinar(int Id)
        {
            try
            {
                //deleting a webinar using the service
                var isDeleted = await _webinarService.RemoveFromWebinarAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<Webinar>
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
                return new GenericResponse<Webinar>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }
    }
}