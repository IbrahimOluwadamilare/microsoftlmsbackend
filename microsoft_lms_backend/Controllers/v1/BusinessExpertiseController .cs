using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Models;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.InputModels.v1;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.BusinessProfileModels;

namespace microsoft_lms_backend.Controllers.v1
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class BusinessExpertiseController : Controller
    {
        private readonly IBusinessExpertise _businessExpertiseService;

        public BusinessExpertiseController(IBusinessExpertise businessExpertiseService)
        {
            _businessExpertiseService = businessExpertiseService;
        }

        [HttpPost]
        
        public async Task<ActionResult<GenericResponse<BusinessExpertise>>> CreateNewBusinessExpertise([FromBody]  BusinessExpertiseInput businessExpertiseInput)

        {
            try
            {
                if (ModelState.IsValid)
                {
                    //creates new business expertise details from the model
                    var expertise = new BusinessExpertise
                    {
                        Expertises = businessExpertiseInput.Expertises,
                        DateCreated = DateTime.Now,
                        Id = businessExpertiseInput.BusinessProfileId
                    };


                    var newExpertise = await _businessExpertiseService.CreateNewBusinessExpertiseAsync(expertise);

                    // checks if the creation of the expertise is successful and returns a response
                    if (newExpertise.Success == true)
                    {
                        return new GenericResponse<BusinessExpertise>
                        {
                            Data = newExpertise.Data,
                            Message = "Business expertise successfully created",
                            Success = true
                        };
                    } else
                    {
                        return new GenericResponse<BusinessExpertise>
                        {
                            Data = null,
                            Message = "Business profile has not being created",
                            Success = false
                        };
                    }

                }
                else
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = null,
                        Message = null,
                        Success = false

                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<BusinessExpertise>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

            }
          
        

        [HttpDelete]
        public async Task<ActionResult<GenericResponse<BusinessExpertise>>> DeleteBusinessExpertise(int Id)
        {
            try
            {
                var deleteExpertise = await _businessExpertiseService.DeleteBusinessExpertiseAsync(Id);

                if (deleteExpertise.Success == true)
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = deleteExpertise.Data,
                        Message = $"Business {deleteExpertise.Message} sucessful",
                        Success = true
                    };

                } else
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = null,
                        Message = $"Business {deleteExpertise.Message} not deleted",
                        Success = false
                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<BusinessExpertise>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse<BusinessExpertise>>> UpdateBusinessExpertise(int Id, BusinessExpertiseInput businessExpertiseInput)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var expertiseEdit = await _businessExpertiseService.GetBusinessExpertiseByIdAsync(Id).ConfigureAwait(true);

                    if (expertiseEdit.Success == true)
                    {
                        expertiseEdit.Data.Expertises = businessExpertiseInput.Expertises;

                           var newExpertise = await _businessExpertiseService.UpdateBusinessExpertiseAsync(expertiseEdit.Data);
                    
                        return new GenericResponse<BusinessExpertise>
                        {
                            Data = newExpertise.Data,
                            Message = $"Business {expertiseEdit.Data.BusinessProfile.Name} found",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<BusinessExpertise>
                        {
                            Data = null,
                            Message = $"Business {expertiseEdit.Data.BusinessProfile.Name} not found",
                            Success = false
                        };
                    }

                }
                catch (Exception e)
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = null,
                        Message = e.Message,
                        Success = false
                    };
                }
            }
            else
            {
                return new GenericResponse<BusinessExpertise>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }

        }
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<BusinessExpertise>>>> GetAllBusinessExpertises()
        {
            try
            {
                var allExpertise = await _businessExpertiseService.GetAllBusinessExpertisesAsync();

                if (allExpertise.Success == true)
                {
                    return new GenericResponse<IEnumerable<BusinessExpertise>>
                    {
                        Data = allExpertise.Data,
                        Message = $"Business {allExpertise.Data} found",
                        Success = true
                    };
                } else
                {
                    return new GenericResponse<IEnumerable<BusinessExpertise>>
                    {
                        Data = null,
                        Message = $"Business {allExpertise.Data} not found",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<IEnumerable<BusinessExpertise>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
           
        [HttpGet]

        public async Task<ActionResult<GenericResponse<BusinessExpertise>>> GetBusinessExpertiseById(int Id)
        {
            try
            {
                var expertise = await _businessExpertiseService.GetBusinessExpertiseByIdAsync(Id);

                if (expertise.Success == true)
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = expertise.Data,
                        Message = $"Business {expertise.Data.BusinessProfile.Name} found",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = null,
                        Message = $"Business {expertise.Data.BusinessProfile.Name} not found",
                        Success = false

                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<BusinessExpertise>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
 
        }



    }
           

    
    }




