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
    public class BusinessExpertiseService : IBusinessExpertise
    {
        private readonly ApplicationDbContext _dbcontext;
        public BusinessExpertiseService() 
        {
        }

        public BusinessExpertiseService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<GenericResponse<BusinessExpertise>> CreateNewBusinessExpertiseAsync(BusinessExpertise businessExpertise)
        {
            try
            {
                if (businessExpertise == null)
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = null,
                        Message = "Unable to create business contact",
                        Success = false
                    };
                }
                else
                {
                     // verifying if business profile has already been created
                    var expertise = await _dbcontext.BusinessProfile.FirstOrDefaultAsync(c => c.Id == businessExpertise.Id);
                    var BusinessprofiletoUpload = new BusinessExpertise
                    {
                        Expertises = businessExpertise.Expertises,
                        DateCreated = DateTime.Now,
                        BusinessProfile = expertise
                    };

                    //creates the expertise if the profile has already been created
                    if (expertise != null) {
                        await _dbcontext.BusinessExpertise.AddAsync(businessExpertise);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<BusinessExpertise>
                        {
                            Data = BusinessprofiletoUpload,
                            Message = $"Business {expertise.Name} created successfully",
                            Success = true
                        };
                        }
                    else
                    {
                        return new GenericResponse<BusinessExpertise>
                        {
                            Data = null,
                            Message = $"Business profile has not been created",
                            Success = true
                        };

                    }
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

        public async Task<GenericResponse<BusinessExpertise>> DeleteBusinessExpertiseAsync(int Id)
        {
            try
            {
                var businessExpertise = await _dbcontext.BusinessExpertise.FirstOrDefaultAsync(c => c.Id == Id);
                if (businessExpertise == null)
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = null,
                        Message = $"Business {businessExpertise.BusinessProfile.Name} not found",
                        Success = false
                    };
                }
                else

                {
                    _dbcontext.Remove(businessExpertise);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = null,
                        Message = $"Business {businessExpertise.BusinessProfile.Name} deleted successfully",
                        Success = true
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

        public async Task<GenericResponse<IEnumerable<BusinessExpertise>>> GetAllBusinessExpertisesAsync()
        {
            try
            {
                var businessExpertise = await _dbcontext.BusinessExpertise.ToListAsync();

                if (businessExpertise != null)
                {
                    return new GenericResponse<IEnumerable<BusinessExpertise>>
                    {
                        Data = businessExpertise,
                        Message = $"Businesses {businessExpertise} listed successfully",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<IEnumerable<BusinessExpertise>>
                    {
                        Data = null,
                        Message = $"Business {businessExpertise} not found",
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

        public async Task<GenericResponse<BusinessExpertise>> GetBusinessExpertiseByIdAsync(int Id)
        {
            try
            {
                var businessExpertise = await _dbcontext.BusinessExpertise.SingleOrDefaultAsync(c => c.Id == Id);

                if (businessExpertise != null)
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = businessExpertise,
                        Message = $"Business {businessExpertise.BusinessProfile.Name} successfully found",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = null,
                        Message = $"Business {businessExpertise.BusinessProfile.Name} not found",
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

    public async Task<GenericResponse<BusinessExpertise>> UpdateBusinessExpertiseAsync(BusinessExpertise businessExpertise)
    {
            try

            {
                var expertise = await _dbcontext.BusinessExpertise.FirstOrDefaultAsync(c => c.Id == businessExpertise.Id);
                if (expertise != null)
                {
                    _dbcontext.BusinessExpertise.Update(expertise);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = expertise,
                        Message = $"Business {businessExpertise.BusinessProfile.Name} successfully updated",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<BusinessExpertise>
                    {
                        Data = null,
                        Message = $"Business {businessExpertise.BusinessProfile.Name} not found",
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

