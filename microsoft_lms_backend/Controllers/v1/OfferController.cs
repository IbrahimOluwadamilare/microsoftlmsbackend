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
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpPost]
        
        public async Task<ActionResult<GenericResponse<Offers>>> CreateOffer([FromBody] OfferInput offerInput)

        {
            if (ModelState.IsValid) {
                try
                {
                    //creates a new offer from the model
                    var offer = new Offers
                    {
                        OfferName = offerInput.OfferName,
                        Description = offerInput.Description,
                        Id = offerInput.ProductInputId

                    };

                    var newOffer = await _offerService.CreateOfferAsync(offer);

                    //ceates the offer successfully if the above operation is successfull
                    if (newOffer.Success == true)
                        { 
                        return new GenericResponse<Offers>
                        {
                            Data = newOffer.Data,
                            Message = "Offer created successfully",
                            Success = true
                        };
                    } else
                    {
                        return new GenericResponse<Offers>
                        {
                            Data = null,
                            Message = "Offer not created successfully",
                            Success = false
                        };
                    }


                }
                catch (Exception e)
                {
                    return new GenericResponse<Offers>
                    {
                        Data = null,
                        Message = e.Message,
                        Success = false
                    };
                }

            }
            else
            {
                // returns ths if the model state is not valid
                return new GenericResponse<Offers>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }
        }

        //an end point that deletes the offer 
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<Offers>>> DeleteOffer(int Id)
        {
            try
            {
                //implements the delete offer in the offer service
                var deleteOffer = await _offerService.DeleteOfferAsync(Id);

                //checks if the above operation is successfull and returns the following code
                if (deleteOffer.Success == true)
                {
                    return new GenericResponse<Offers>
                    {
                        Data = deleteOffer.Data,
                        Message = "Offer deleted successfully",
                        Success = true
                    };

                } else
                {
                    return new GenericResponse<Offers>
                    {
                        Data = null,
                        Message = "Unable to delete offer",
                        Success = false
                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<Offers>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

        //an end pont to allow the offer to be updated
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Offers>>> EditOffer(int Id, OfferInput offerInput)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //retrieves the offer by its Id
                    var offerEdit = await _offerService.GetOfferByIdAsync(Id);

                    //if operation is succesful return the following code
                    if (offerEdit.Success == true)
                   {
                        offerEdit.Data.OfferName = offerInput.OfferName;
                        offerEdit.Data.Description = offerInput.Description;

                        var newOffer = await _offerService.EditOfferAsync(offerEdit.Data);
                    
                        return new GenericResponse<Offers>
                        {
                            Data = newOffer.Data,
                            Message = "Offer updated successfully",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<Offers>
                        {
                            Data = null,
                            Message = "Offer not updated",
                            Success = false
                        };
                    }

                }
                catch (Exception e)
                {
                    return new GenericResponse<Offers>
                    {
                        Data = null,
                        Message = e.Message,
                        Success = false
                    };
                }
            }
            else
            {
                //return this if the state of the model isnt valid
                return new GenericResponse<Offers>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }

        }

        //an endpoint that gets all the offers from the db
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Offers>>>> GetAllOffers()
        {
            try
            {
                var allOffers = await _offerService.GetAllOffersAsync();

                if (allOffers.Success == true)
                {
                    return new GenericResponse<IEnumerable<Offers>>
                    {
                        Data = allOffers.Data,
                        Message = "Offers listed",
                        Success = true
                    };
                } else
                {
                    return new GenericResponse<IEnumerable<Offers>>
                    {
                        Data = null,
                        Message = "Offers not found",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<IEnumerable<Offers>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
         
        
        //an endpoint that gets the offer by its Id
        [HttpGet]
        public async Task<ActionResult<GenericResponse<Offers>>> GetOfferById(int Id)
        {
            try
            {
                var offer = await _offerService.GetOfferByIdAsync(Id);

                if (offer.Success == true)
                {
                    return new GenericResponse<Offers>
                    {
                        Data = offer.Data,
                        Message = "Offer found",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<Offers>
                    {
                        Data = null,
                        Message = "Offer not found",
                        Success = false

                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<Offers>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
 
        }



    }
           

    
    }




