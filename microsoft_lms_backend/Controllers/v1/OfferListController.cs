using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Models;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.InputModels.v1;
using microsoft_lms_backend.Interfaces.v1;


namespace microsoft_lms_backend.Controllers.v1
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class OfferListController : Controller
    {
        private readonly IOfferListService _offerListService;

        public OfferListController(IOfferListService offerListService)
        {
            _offerListService = offerListService;
        }

        [HttpPost]
        
        public async Task<ActionResult<GenericResponse<OfferList>>> CreateOfferList([FromBody] OfferListInput offerListInput)

        {
            if (ModelState.IsValid) {
                try
                {
                    //creates  a new offerlist from the model
                    var offerList = new OfferList
                    {
                        OfferDetail = offerListInput.OfferDetail,
                        Id = offerListInput.OfferInputId

                    };

                    var newOfferList = await _offerListService.CreateOfferListAsync(offerList);

                    //creates an offerlist if the above operation is successful
                    if (newOfferList.Success == true)
                        { 
                        return new GenericResponse<OfferList>
                        {
                            Data = newOfferList.Data,
                            Message = "Offer List created successfully",
                            Success = true
                        };
                    } else
                    {
                        return new GenericResponse<OfferList>
                        {
                            Data = null,
                            Message = "Offer List not created successfully",
                            Success = false
                        };
                    }


                }
                catch (Exception e)
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = null,
                        Message = e.Message,
                        Success = false
                    };
                }

            }
            else
            {
                //returns the following code if the state of the model is not valid
                return new GenericResponse<OfferList>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }
        }

        [HttpDelete]
        public async Task<ActionResult<GenericResponse<OfferList>>> DeleteOfferList(int Id)
        {
            try
            {
                var deleteOfferList = await _offerListService.DeleteOfferListAsync(Id);

                if (deleteOfferList.Success == true)
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = deleteOfferList.Data,
                        Message = "Offer List deleted successfully",
                        Success = true
                    };

                } else
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = null,
                        Message = "Unable to delete offer List",
                        Success = false
                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<OfferList>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse<OfferList>>> EditOfferList(int Id, OfferListInput offerListInput)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var offerListEdit = await _offerListService.GetOfferListByIdAsync(Id);

                    if (offerListEdit.Success == true)
                   {
                        offerListEdit.Data.OfferDetail = offerListInput.OfferDetail;

                        var newOfferList = await _offerListService.EditOfferListAsync(offerListEdit.Data);
                    
                        return new GenericResponse<OfferList>
                        {
                            Data = newOfferList.Data,
                            Message = "Offer List updated successfully",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<OfferList>
                        {
                            Data = null,
                            Message = "Offer List not updated",
                            Success = false
                        };
                    }

                }
                catch (Exception e)
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = null,
                        Message = e.Message,
                        Success = false
                    };
                }
            }
            else
            {
                return new GenericResponse<OfferList>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }

        }
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<OfferList>>>> GetAllOfferLists()
        {
            try
            {
                var allOfferLists = await _offerListService.GetAllOfferListsAsync();

                if (allOfferLists.Success == true)
                {
                    return new GenericResponse<IEnumerable<OfferList>>
                    {
                        Data = allOfferLists.Data,
                        Message = "Offer Lists listed",
                        Success = true
                    };
                } else
                {
                    return new GenericResponse<IEnumerable<OfferList>>
                    {
                        Data = null,
                        Message = "Offer Lists not found",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<IEnumerable<OfferList>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
           
        [HttpGet]

        public async Task<ActionResult<GenericResponse<OfferList>>> GetOfferListById(int Id)
        {
            try
            {
                var offerList = await _offerListService.GetOfferListByIdAsync(Id);

                if (offerList.Success == true)
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = offerList.Data,
                        Message = "Offer List found",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = null,
                        Message = "Offer List not found",
                        Success = false

                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<OfferList>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
 
        }



    }
           

    
    }




