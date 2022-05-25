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
    public class BusinessContactService : IBusinessContact
    {
        private readonly ApplicationDbContext _dbcontext;
        public BusinessContactService()
        {
        }

        public BusinessContactService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //creation of a business contact by checking if the profile has already being created first
        public async Task<GenericResponse<BusinessContact>> CreateNewBusinessContactAsync(BusinessContact businessContact)
        {
            try
            {
                if (businessContact == null)
                {
                    return new GenericResponse<BusinessContact>
                    {
                        Data = null,
                        Message = "Unable to create business contact",
                        Success = false
                    };
                }
                else
                {
                    // verifying if business profile has already been created
                    var contact = await _dbcontext.BusinessProfile.FirstOrDefaultAsync(c => c.Id == businessContact.Id);

                    var BusinessprofiletoUpload = new BusinessContact
                    {
                        Longitude = businessContact.Longitude,
                        Latitude = businessContact.Latitude,
                        BusinessEmail = businessContact.BusinessEmail,
                        BusinessPhoneNumber = businessContact.BusinessPhoneNumber,
                        DateCreated = DateTime.Now,
                        BusinessProfile = contact
                    };
                    //creates the contact if the profile has already been created
                    if (contact != null)
                    {
                        await _dbcontext.BusinessContact.AddAsync(BusinessprofiletoUpload);

                        var result = _dbcontext.SaveChanges();

                        return new GenericResponse<BusinessContact>
                        {
                            Data = BusinessprofiletoUpload,
                            Message = $"{contact.Name} Business contact created successfully",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<BusinessContact>
                        {
                            Data = null,
                            Message = "Business profile has not been created",
                            Success = true
                        };

                    }
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<BusinessContact>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

        //deletes a business contact 
        public async Task<GenericResponse<BusinessContact>> DeleteBusinessContactAsync(int Id)
        {
            try
            {
                // checks the contact by its ID
                var businessContact = await _dbcontext.BusinessContact.FirstOrDefaultAsync(c => c.Id == Id);
                if (businessContact == null)
                {
                    return new GenericResponse<BusinessContact>
                    {
                        Data = null,
                        Message = $"Business {businessContact.BusinessProfile.Name} not found",
                        Success = false
                    };
                }
                //removes the contact if found
                else
                {
                    _dbcontext.Remove(businessContact);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<BusinessContact>
                    {
                        Data = null,
                        Message = $"Business {businessContact.BusinessProfile.Name} deleted successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<BusinessContact>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //retrives all the business contacts available
        public async Task<GenericResponse<IEnumerable<BusinessContact>>> GetAllBusinessContactsAsync()
        {
            try
            {
                //creates a list of all business contact
                var businessContact = await _dbcontext.BusinessContact.ToListAsync();

                if (businessContact != null)
                {
                    return new GenericResponse<IEnumerable<BusinessContact>>
                    {
                        Data = businessContact,
                        Message = $"Businesses {businessContact} listed successfully",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<IEnumerable<BusinessContact>>
                    {
                        Data = null,
                        Message = $"Business {businessContact} not found",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<IEnumerable<BusinessContact>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //gets a contact by its Id
        public async Task<GenericResponse<BusinessContact>> GetBusinessContactByIdAsync(int Id)
        {
            try
            {
                var businessContact = await _dbcontext.BusinessContact.SingleOrDefaultAsync(c => c.Id == Id);

                if (businessContact != null)
                {
                    return new GenericResponse<BusinessContact>
                    {
                        Data = businessContact,
                        Message = $"Business {businessContact.BusinessProfile.Name} successfully found",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<BusinessContact>
                    {
                        Data = null,
                        Message = "Business contact not found",
                        Success = false

                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<BusinessContact>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<BusinessContact>> UpdateBusinessContactAsync(BusinessContact businessContact)
        {
            try

            {
                var contact = await _dbcontext.BusinessContact.FirstOrDefaultAsync(c => c.Id == businessContact.Id);
                if (contact != null)
                {
                    //if the contact is retrieved updates it and save it to the db
                    _dbcontext.BusinessContact.Update(contact);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<BusinessContact>
                    {
                        Data = contact,
                        Message = $"Business {businessContact.BusinessProfile.Name} successfully updated",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<BusinessContact>
                    {
                        Data = null,
                        Message = "Business profile not created",
                        Success = false

                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<BusinessContact>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

    }


}

