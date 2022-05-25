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
    public class WebinarAttendeeController : ControllerBase
    {
        //Dependency injection of WebinarAttendee service
        private readonly IWebinarAttendee _WebinarAttendeeService;

        //WebinarAttendeeController constructor
        public WebinarAttendeeController(IWebinarAttendee WebinarAttendeeService)
        {
            _WebinarAttendeeService = WebinarAttendeeService;
        }

        //Creating New WebinarAttendee post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<WebinarAttendee>>> CreateNewWebinarAttendee([FromBody] WebinarAttendeeInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var webinarAttendee = new WebinarAttendee
                    {
                        AttendeeName = input.AttendeeName,
                        AttendeePhoneNumber = input.AttendeePhoneNumber,
                        AttendeeEmail = input.AttendeeEmail,
                        AttendeeOcupation = input.AttendeeOcupation,
                        Id = input.WebinarId,
                    };
                    //creating WebinarAttendee using the service
                    var newWebinarAttendee = await _WebinarAttendeeService.CreateNewWebinarAttendeeAsync(webinarAttendee);

                    //checking for operation failure
                    if (newWebinarAttendee.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newWebinarAttendee);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<WebinarAttendee>
                        {
                            Data = newWebinarAttendee.Data,
                            Message = newWebinarAttendee.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<WebinarAttendee>
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
                return new GenericResponse<WebinarAttendee>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All Webinar Attendees Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<WebinarAttendee>>>> GetAllWebinarAttendees()
        {
            try
            {
                //Getting all WebinarAttendee using the service
                var webinarAttendees = await _WebinarAttendeeService.GetAllWebinarAttendeeAsync();

                //checks for operation failure
                if (webinarAttendees.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, webinarAttendees);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<WebinarAttendee>>
                    {
                        Data = webinarAttendees.Data,
                        Message = webinarAttendees.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<WebinarAttendee>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //single webinar attendee get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<WebinarAttendee>>> GetWebinarAttendeeById(int Id)
        {
            try
            {
                //Getting a single webinar attendee by Id using the service
                var webinarAttendee = await _WebinarAttendeeService.GetWebinarAttendeebyIdAsync(Id);

                //checks for operation failure
                if (webinarAttendee.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, webinarAttendee);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<WebinarAttendee>
                    {
                        Data = webinarAttendee.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<WebinarAttendee>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }


        //Webinar attendee Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<WebinarAttendee>>> RemoveItemFromWebinarAttendee(int Id)
        {
            try
            {
                //deleting a WebinarAttendee using the service
                var isDeleted = await _WebinarAttendeeService.RemoveFromWebinarAttendeeAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<WebinarAttendee>
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
                return new GenericResponse<WebinarAttendee>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
        //All webinar attendee by webinar Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<WebinarAttendee>>>> GetAllModuleByCourseAsync(int WebinarId)
        {
            try
            {
                //Getting all webinar attendee by webinar Id using the service
                var webinarAttendees = await _WebinarAttendeeService.GetAllWebinarAttendeeByWebinarIdAsync(WebinarId);

                //checks for operation failure
                if (webinarAttendees.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, webinarAttendees);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<WebinarAttendee>>
                    {
                        Data = webinarAttendees.Data,
                        Message = webinarAttendees.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<WebinarAttendee>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }
    }
}