using Microsoft.Extensions.Logging;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Interfaces.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using microsoft_lms_backend.Helper;
using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Models.v1.WebinarModels;

namespace microsoft_lms_backend.Services.v1
{
    public class WebinarService: IWebinar
    {

        public readonly ApplicationDbContext _dbcontext;
        //private readonly ILogger<WebinarService> _logger;

        public WebinarService()
        {
           
        }
        public WebinarService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            //_logger = logger;
        }

        //Service implementation for creating new webinar
        public async Task<GenericResponse<Webinar>> CreateNewWebinarAsync(Webinar Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<Webinar>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //If input is not null, add it to database
                    await _dbcontext.Webinar.AddAsync(Input);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Webinar>
                    {
                        Data = Input,
                        Message = "Webinar added successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Webinar>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all webinar
        public async Task<GenericResponse<IEnumerable<Webinar>>> GetAllWebinarAsync()
        {
            try
            {
                //Getting all webinar from database
                var webinars = await _dbcontext.Webinar.ToListAsync();
                if (webinars.Count == 0)
                {
                    return new GenericResponse<IEnumerable<Webinar>>
                    {
                        Data = null,
                        Message = "Webinar is empty",
                        Success = true
                    };
                }
                //returning all webinar gotten from database
                return new GenericResponse<IEnumerable<Webinar>>
                {
                    Data = webinars,
                    Message = $"successfully gets {webinars.Count} webinar(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<Webinar>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        //Service implementation for getting a single webinar
        public async Task<GenericResponse<Webinar>> GetWebinarbyIdAsync(int Id)
        {
            try
            {
                //find the webinar by id from the database
                var webinar = await _dbcontext.Webinar.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (webinar == null)
                {
                    return new GenericResponse<Webinar>
                    {
                        Data = null,
                        Message = "Webinar does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the webinar
                    return new GenericResponse<Webinar>
                    {
                        Data = webinar,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Webinar>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for Updating a webinar
        public async Task<GenericResponse<Webinar>> UpdateWebinarAsync(Webinar Input)
        {
            try
            {
                //find the webinar by it id from the database
                var webinar = await _dbcontext.Webinar.FirstOrDefaultAsync(x => x.Id == Input.Id);

                //If not found
                if (webinar == null)
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
                    //if found, update with the new changes from input and save changes
                    var result = _dbcontext.Webinar.Update(Input);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Webinar>
                    {
                        Data = null,
                        Message = "webinars Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Webinar>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for deleting a webinar
        public async Task<GenericResponse<Webinar>> RemoveFromWebinarAsync(int Id)
        {
            try
            {
                //find the webinar by id from the database
                var webinarToBeRemoved = await _dbcontext.Webinar.FirstOrDefaultAsync(x => x.Id == Id);
                
                //If found, remove it from database and save changes
                if (webinarToBeRemoved != null)
                {
                    _dbcontext.Remove(webinarToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Webinar>
                    {
                        Data = null,
                        Message = "webinar has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<Webinar>
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
