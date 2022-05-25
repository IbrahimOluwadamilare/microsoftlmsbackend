using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Models;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Interfaces.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Services.v1
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _dbcontext;

        public OfferService()
        {
        }

        public OfferService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        

        // creation of a newoffer from the products
        public async Task<GenericResponse<Offers>> CreateOfferAsync(Offers offers) 
        {
            try
            {
                if (offers == null)
                {
                    return new GenericResponse<Offers>
                    {
                        Data = null,
                        Message = "Offer is null",
                        Success = false
                    };
                } else
                {
                    //checks if the products to be included in the offer is availabe
                    var product = await _dbcontext.Product.FirstOrDefaultAsync(p => p.Id == offers.Id);

                    //creating an instance of the offer 
                    var productToInclude = new Offers
                    {
                        OfferName = offers.OfferName,
                        Description = offers.Description,
                        Product = product
                    };

                    //creating a new offer if the product is available
                    if (product != null)
                    {
                        await _dbcontext.Offers.AddAsync(productToInclude);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<Offers>
                        {
                            Data = offers,
                            Message = "Offer created successfully",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<Offers>
                        {
                            Data = null,
                            Message = "Product not available",
                            Success = false
                        };

                    }
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

        //deleting an offer
        public async Task<GenericResponse<Offers>> DeleteOfferAsync(int Id)
        {
            try
            {
                // checks the offer by its ID
                var offer = await _dbcontext.Offers.FirstOrDefaultAsync(o => o.Id == Id);

                if (offer == null)
                {
                    return new GenericResponse<Offers>
                    {
                        Data = null,
                        Message = "Offer not found",
                        Success = false
                    };
                } 
                //removes the offer if found
                else
                {
                    _dbcontext.Remove(offer);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Offers>
                    {
                        Data = null,
                        Message = "Offer successfully deleted",
                        Success = true
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
    
        //to make changes to the offer
        public async Task<GenericResponse<Offers>> EditOfferAsync(Offers offers)
        {
           try

            {
                //checks the offer to be edited by its Id
                var editOffer = await _dbcontext.Offers.FirstOrDefaultAsync(o => o.Id == offers.Id);
                if (editOffer != null)
                {
                    _dbcontext.Offers.Update(offers);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Offers>
                    {
                        Data = offers,
                        Message = "Offer suceessfully updated",
                        Success = true

                    };
                } else
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

        //gets all the offers available
        public async Task<GenericResponse<IEnumerable<Offers>>> GetAllOffersAsync()
        {
            try
            {
                //gets all the offers and put them in a list
                var offers = await _dbcontext.Offers.ToListAsync();

                if (offers != null)
                {
                    return new GenericResponse<IEnumerable<Offers>>
                    {
                        Data = offers,
                        Message = "All offers listed",
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

        public async Task<GenericResponse<Offers>> GetOfferByIdAsync(int Id)
        {
            try
            {
                //fetches the offer from the DB by its ID
                var offer = await _dbcontext.Offers.SingleOrDefaultAsync(o => o.Id == Id);

                if (offer != null)
                {
                    return new GenericResponse<Offers>
                    {
                        Data = offer,
                        Message = "Offer found",
                        Success = true

                    };
                } else
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
