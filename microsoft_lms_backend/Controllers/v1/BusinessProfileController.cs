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
    public class BusinessProfileController : Controller
    {
        private readonly IBusinessProfile _businessProfileService;

        public BusinessProfileController(IBusinessProfile businessProfileService)
        {
            _businessProfileService = businessProfileService;
        }

        [HttpPost]
        
        public async Task<ActionResult<GenericResponse<BusinessProfile>>> CreateNewBusinessProfile([FromBody]  BusinessProfileInput businessProfileInput)

        {
            try
             {
                if (ModelState.IsValid)
                {
                    var profile = new BusinessProfile
                    {
                        Banner = businessProfileInput.Banner,
                        Name = businessProfileInput.Name,
                        Detail = businessProfileInput.Detail,
                        FacebookSocial = businessProfileInput.FacebookSocial,
                        TwitterSocial = businessProfileInput.TwitterSocial,
                        LinkedInSocial = businessProfileInput.LinkedInSocial,
                        DateCreated = DateTime.Now,

                    };

                    var newProfile = await _businessProfileService.CreateNewBusinessProfileAsync(profile);

                    if (newProfile.Success == true)
                    {
                        return new GenericResponse<BusinessProfile>
                        {
                            Data = newProfile.Data,
                            Message = $"Business {profile.Name} successfully created",
                            // Message = "Business profilec created sucessfully", 
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<BusinessProfile>
                        {
                            Data = null,
                            Message = $"Business {profile.Name} not found",
                            // Message = "Business profile not created",
                            Success = false
                        };
                    }

                }
                else
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = null,
                        Message = "Invalid Operation",
                        Success = false

                    };
                }
            }
                catch (Exception e)
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = null,
                        Message = e.Message,
                        Success = false
                    };
                }

            }
        

        [HttpDelete]
        public async Task<ActionResult<GenericResponse<BusinessProfile>>> DeleteBusinessProfile(int Id)
        {
            try
            {
                var deleteProfile = await _businessProfileService.DeleteBusinessProfileAsync(Id);

                if (deleteProfile.Success == true)
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = deleteProfile.Data,
                        Message = $"Business {deleteProfile.Message} sucessful",
                        Success = true
                    };

                } else
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = null,
                        Message = $"Business {deleteProfile.Message} not deleted",
                        Success = false
                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<BusinessProfile>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse<BusinessProfile>>> UpdateBusinessProfile(int Id, BusinessProfileInput businessProfileInput)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var profileEdit = await _businessProfileService.GetBusinessProfileByIdAsync(Id).ConfigureAwait(true);

                    if (profileEdit.Success == true)
                   {
                        profileEdit.Data.Banner = businessProfileInput.Banner;
                        profileEdit.Data.Name = businessProfileInput.Name;
                        profileEdit.Data.DateCreated = businessProfileInput.DateCreated;
                        profileEdit.Data.DateUpdated = businessProfileInput.DateUpdated;
                        profileEdit.Data.Detail = businessProfileInput.Detail;
                        profileEdit.Data.TwitterSocial = businessProfileInput.TwitterSocial;
                        profileEdit.Data.FacebookSocial = businessProfileInput.FacebookSocial;
                        profileEdit.Data.LinkedInSocial = businessProfileInput.LinkedInSocial;

                        var newProfile = await _businessProfileService.UpdateBusinessProfileAsync(profileEdit.Data);
                    
                        return new GenericResponse<BusinessProfile>
                        {
                            Data = newProfile.Data,
                            Message = $"Business {profileEdit.Data.Name} found",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<BusinessProfile>
                        {
                            Data = null,
                            Message = $"Business {profileEdit.Data.Name} not found",
                            Success = false
                        };
                    }

                }
                catch (Exception e)
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = null,
                        Message = e.Message,
                        Success = false
                    };
                }
            }
            else
            {
                return new GenericResponse<BusinessProfile>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }

        }
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<BusinessProfile>>>> GetAllBusinessProfiles()
        {
            try
            {
                var allProfiles = await _businessProfileService.GetAllBusinessProfilesAsync();

                if (allProfiles.Success == true)
                {
                    return new GenericResponse<IEnumerable<BusinessProfile>>
                    {
                        Data = allProfiles.Data,
                        Message = $"Business {allProfiles.Data} found",
                        Success = true
                    };
                } else
                {
                    return new GenericResponse<IEnumerable<BusinessProfile>>
                    {
                        Data = null,
                        Message = $"Business {allProfiles.Data} not found",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<IEnumerable<BusinessProfile>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
           
        [HttpGet]

        public async Task<ActionResult<GenericResponse<BusinessProfile>>> GetBusinessProfileById(int Id)
        {
            try
            {
                var profile = await _businessProfileService.GetBusinessProfileByIdAsync(Id);

                if (profile.Success == true)
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = profile.Data,
                        Message = $"Business {profile.Data.Name} found",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = null,
                        Message = $"Business {profile.Data.Name} not found",
                        Success = false

                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<BusinessProfile>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
 
        }



    }
           

    
    }




