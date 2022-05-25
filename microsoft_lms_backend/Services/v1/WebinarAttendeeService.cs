using microsoft_lms_backend.Data;
using microsoft_lms_backend.Interfaces.v1;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using microsoft_lms_backend.Helper;
using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Models.v1.WebinarModels;
using System.Linq;

namespace microsoft_lms_backend.Services.v1
{
    public class WebinarAttendeeService : IWebinarAttendee
    {

        public readonly ApplicationDbContext _dbcontext;

        public WebinarAttendeeService()
        {

        }
        public WebinarAttendeeService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //Service implementation for creating new webinar attendee
        public async Task<GenericResponse<WebinarAttendee>> CreateNewWebinarAttendeeAsync(WebinarAttendee Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<WebinarAttendee>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //find the webinar by provided id 
                    var webinar = await _dbcontext.Webinar.FirstOrDefaultAsync(x => x.Id == Input.Id);

                    //If webinar not found
                    if (webinar == null)
                    {
                        return new GenericResponse<WebinarAttendee>
                        {
                            Data = null,
                            Message = "Webinar Id does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        var webinarAttendee = new WebinarAttendee
                        {
                            AttendeeName = Input.AttendeeName,
                            AttendeePhoneNumber = Input.AttendeePhoneNumber,
                            AttendeeEmail = Input.AttendeeEmail,
                            AttendeeOcupation = Input.AttendeeOcupation,
                            Webinar = webinar,
                        };
                        //If input and webinar is not null, add to database
                        await _dbcontext.WebinarAttendee.AddAsync(webinarAttendee);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<WebinarAttendee>
                        {
                            Data = webinarAttendee,
                            Message = "WebinarAttendee added successfully",
                            Success = true
                        };
                    }
                    
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<WebinarAttendee>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all webinar attendee
        public async Task<GenericResponse<IEnumerable<WebinarAttendee>>> GetAllWebinarAttendeeAsync()
        {
            try
            {
                //Getting all webinar attendee from database
                var webinarAttendees = await _dbcontext.WebinarAttendee.ToListAsync();
                if (webinarAttendees.Count == 0)
                {
                    return new GenericResponse<IEnumerable<WebinarAttendee>>
                    {
                        Data = null,
                        Message = "WebinarAttendee is empty",
                        Success = true
                    };
                }
                //returning all webinar attendee gotten from database
                return new GenericResponse<IEnumerable<WebinarAttendee>>
                {
                    Data = webinarAttendees,
                    Message = $"successfully gets {webinarAttendees.Count} WebinarAttendee(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<WebinarAttendee>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        //Service implementation for getting a single webinar attendee
        public async Task<GenericResponse<WebinarAttendee>> GetWebinarAttendeebyIdAsync(int Id)
        {
            try
            {
                //find the webinar attendee by id from the database
                var webinarAttendee = await _dbcontext.WebinarAttendee.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (webinarAttendee == null)
                {
                    return new GenericResponse<WebinarAttendee>
                    {
                        Data = null,
                        Message = "WebinarAttendee does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the webinar attendee
                    return new GenericResponse<WebinarAttendee>
                    {
                        Data = webinarAttendee,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<WebinarAttendee>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for deleting a webinar attendee
        public async Task<GenericResponse<WebinarAttendee>> RemoveFromWebinarAttendeeAsync(int Id)
        {
            try
            {
                //find the webinar attendee by id from the database
                var webinarAttendeeToBeRemoved = await _dbcontext.WebinarAttendee.FirstOrDefaultAsync(x => x.Id == Id);

                //If found, remove it from database and save changes
                if (webinarAttendeeToBeRemoved != null)
                {
                    _dbcontext.Remove(webinarAttendeeToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<WebinarAttendee>
                    {
                        Data = null,
                        Message = "Webinar attendee has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<WebinarAttendee>
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
                return new GenericResponse<WebinarAttendee>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<WebinarAttendee>>> GetAllWebinarAttendeeByWebinarIdAsync(int WebinarId)
        {
            try
            {
                //find the webinar attendees by webinar Id from the database
                var webinarAttendees = await _dbcontext.WebinarAttendee.Where(w => w.Webinar.Id == WebinarId).ToListAsync();

                //If not found
                if (webinarAttendees == null)
                {
                    return new GenericResponse<IEnumerable<WebinarAttendee>>
                    {
                        Data = null,
                        Message = "There is no webinar attendees for this webinar.",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the article
                    return new GenericResponse<IEnumerable<WebinarAttendee>>
                    {
                        Data = webinarAttendees,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
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
