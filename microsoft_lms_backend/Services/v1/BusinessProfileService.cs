using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.BusinessProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Services.v1
{
    public class BusinessProfileService : IBusinessProfile
    {
        private readonly ApplicationDbContext _dbcontext;

        public BusinessProfileService()
        {
        }

        public BusinessProfileService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // creates a new business profile
        public async Task<GenericResponse<BusinessProfile>> CreateNewBusinessProfileAsync(BusinessProfile businessProfile)
        {
            try
            {
                if (businessProfile == null)
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = null,
                        Message = "Business profile not created ",
                        Success = false
                    };
                }
                else
                {
                    //adds the newly created profile to the DB and saves it
                     await _dbcontext.BusinessProfile.AddAsync(businessProfile).ConfigureAwait(true);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<BusinessProfile>
                    {
                        Data = businessProfile,
                        Message = $"Business {businessProfile.Name} created successfully",
                        Success = true
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

        //deletes the business profile
        public async Task<GenericResponse<BusinessProfile>> DeleteBusinessProfileAsync(int Id)
        {
            try
            {
                var businessProfile = await _dbcontext.BusinessProfile.FirstOrDefaultAsync(b => b.Id == Id).ConfigureAwait(false);
                if (businessProfile == null)
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = null,
                        Message = "Business probile not found",
                        Success = false
                    };
                }
                else

                {
                    _dbcontext.Remove(businessProfile);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<BusinessProfile>
                    {
                        Data = null,
                        Message = $"Business {businessProfile.Name} deleted successfully",
                        Success = true
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

        public async Task<GenericResponse<IEnumerable<BusinessProfile>>> GetAllBusinessProfilesAsync()
        {
            try
            {
                //creates a list of all the available business profiles
                var businessProfile = await _dbcontext.BusinessProfile.ToListAsync();

                if (businessProfile != null)
                {
                    return new GenericResponse<IEnumerable<BusinessProfile>>
                    {
                        Data = businessProfile,
                        Message = $"Businesses {businessProfile} listed successfully",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<IEnumerable<BusinessProfile>>
                    {
                        Data = null,
                        Message = "Business profile not found",
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

        public async Task<GenericResponse<BusinessProfile>> GetBusinessProfileByIdAsync(int Id)
        {
            try
            {
                //gets the business profile by using its Id
                var businessProfile = await _dbcontext.BusinessProfile.SingleOrDefaultAsync(b => b.Id == Id);

                if (businessProfile != null)
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = businessProfile,
                        Message = $"Business {businessProfile.Name} successfully found",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = null,
                        Message = "Business profile not found",
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
    
        //updates the business profile 
        public async Task<GenericResponse<BusinessProfile>> UpdateBusinessProfileAsync(BusinessProfile businessProfile)
        {
            try

            {
                //gets the profile to be selected from the DB
                var profile = await _dbcontext.BusinessProfile.FirstOrDefaultAsync(b => b.Id == businessProfile.Id);
                if (profile != null)
                {
                    _dbcontext.BusinessProfile.Update(profile);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<BusinessProfile>
                    {
                        Data = profile,
                        Message = $"Business {businessProfile.Name} successfully updated",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<BusinessProfile>
                    {
                        Data = null,
                        Message = "Business profile not found",
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

