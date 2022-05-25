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
    public class BusinessContactController : Controller
    {
        private readonly IBusinessContact _businessContactService;

        public BusinessContactController(IBusinessContact businessContactService)
        {
            _businessContactService = businessContactService;
        }

        [HttpPost]

        public async Task<ActionResult<GenericResponse<BusinessContact>>> CreateNewBusinessContact([FromBody] BusinessContactInput businessContactInput)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    //creates new business contact details from the model
                    var contact = new BusinessContact
                    {
                        Longitude = businessContactInput.Longitude,
                        Latitude = businessContactInput.Latitude,
                        BusinessEmail = businessContactInput.BusinessEmail,
                        BusinessPhoneNumber = businessContactInput.BusinessPhoneNumber,
                        DateCreated = DateTime.Now,
                        Id = businessContactInput.BusinessProfileId
                    };


                    var newContact = await _businessContactService.CreateNewBusinessContactAsync(contact);

                    // checks if the creation of the contact is successful
                    if (newContact.Success == true)
                    {
                        return new GenericResponse<BusinessContact>
                        {
                            Data = newContact.Data,
                            Message = "Business contact successfully created",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<BusinessContact>
                        {
                            Data = null,
                            Message = "Business contact not created",
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
            else
            {
                return new GenericResponse<BusinessContact>
                {
                    Data = null,
                    Message = null,
                    Success = false

                };
            }
        }

        [HttpDelete]
        public async Task<ActionResult<GenericResponse<BusinessContact>>> DeleteBusinessContact(int Id)
        {
            try
            {
                var deleteContact = await _businessContactService.DeleteBusinessContactAsync(Id);

                if (deleteContact.Success == true)
                {
                    return new GenericResponse<BusinessContact>
                    {
                        Data = deleteContact.Data,
                        Message = "Business contact deleted sucessfully",
                        Success = true
                    };

                }
                else
                {
                    return new GenericResponse<BusinessContact>
                    {
                        Data = null,
                        Message = $"Business {deleteContact.Message} not deleted",
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

        [HttpPost]
        public async Task<ActionResult<GenericResponse<BusinessContact>>> UpdateBusinessContact(int Id, BusinessContactInput businessContactInput)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var contactEdit = await _businessContactService.GetBusinessContactByIdAsync(Id);

                    if (contactEdit.Success == true)
                    {
                        contactEdit.Data.Longitude = businessContactInput.Longitude;
                        contactEdit.Data.Latitude = businessContactInput.Latitude;
                        contactEdit.Data.BusinessEmail = businessContactInput.BusinessEmail;
                        contactEdit.Data.BusinessPhoneNumber = businessContactInput.BusinessPhoneNumber;

                        var newContact = await _businessContactService.UpdateBusinessContactAsync(contactEdit.Data);

                        return new GenericResponse<BusinessContact>
                        {
                            Data = newContact.Data,
                            Message = $"Business {contactEdit.Data.BusinessProfile.Name} found",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<BusinessContact>
                        {
                            Data = null,
                            Message = $"Business {contactEdit.Data.BusinessProfile.Name} not found",
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
            else
            {
                return new GenericResponse<BusinessContact>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }

        }
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<BusinessContact>>>> GetAllBusinessContacts()
        {
            try
            {
                var allContacts = await _businessContactService.GetAllBusinessContactsAsync();

                if (allContacts.Success == true)
                {
                    return new GenericResponse<IEnumerable<BusinessContact>>
                    {
                        Data = allContacts.Data,
                        Message = $"Business {allContacts.Data} found",
                        Success = true
                    };
                }
                else
                {
                    return new GenericResponse<IEnumerable<BusinessContact>>
                    {
                        Data = null,
                        Message = $"Business {allContacts.Data} not found",
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

        [HttpGet]

        public async Task<ActionResult<GenericResponse<BusinessContact>>> GetBusinessContactById(int Id)
        {
            try
            {
                var contact = await _businessContactService.GetBusinessContactByIdAsync(Id);

                if (contact.Success == true)
                {
                    return new GenericResponse<BusinessContact>
                    {
                        Data = contact.Data,
                        Message = $"Business {contact.Data.BusinessProfile.Name} found",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<BusinessContact>
                    {
                        Data = null,
                        Message = $"Business {contact.Data.BusinessProfile.Name} not found",
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




