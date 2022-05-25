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
    public class OfferListService : IOfferListService
    {
        private readonly ApplicationDbContext _dbcontext;

        public OfferListService()
        {
        }

        public OfferListService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //creation of a new offerlist frm the available offers
        public async Task<GenericResponse<OfferList>> CreateOfferListAsync(OfferList offerList) 
        {
            try
            {
                if (offerList == null)
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = null,
                        Message = "OfferList is null",
                        Success = false
                    };
                } 
                else
                //checks if offer to be included is available
                {
                var offer = await _dbcontext.Offers.FirstOrDefaultAsync(o => o.Id == offerList.Id);

                    //creating an instance of the offerlist
                    var newOfferList = new OfferList
                    {
                        OfferDetail = offerList.OfferDetail,
                        Offer = offer
                    };

                    //creating a new offerlist if the offer is available
                    if (offer != null)
                    {
                        await _dbcontext.OfferList.AddAsync(offerList);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<OfferList>
                        {
                            Data = offerList,
                            Message = "OfferList created successfully",
                            Success = true
                        };
                    } 
                    else
                    {
                        return new GenericResponse<OfferList>
                        {
                            Data = null,
                            Message = "Offerlist not created",
                            Success = false
                        };
                    }
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

    //deletes an offerlist
    public async Task<GenericResponse<OfferList>> DeleteOfferListAsync(int Id)
        {
            try
            {
                //checks for the offerlist by its Id
                var editOfferList = await _dbcontext.Offers.FirstOrDefaultAsync(o => o.Id == Id);
                if (editOfferList == null)
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = null,
                        Message = "OfferList not found",
                        Success = false
                    };
                } else

                {
                    //deletes the offer list and updates the DB
                    _dbcontext.Remove(editOfferList);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<OfferList>
                    {
                        Data = null,
                        Message = "OfferList successfully deleted",
                        Success = true
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

        public async Task<GenericResponse<OfferList>> EditOfferListAsync(OfferList offerList)
        {
           try

            {
                var editOfferList = await _dbcontext.OfferList.FirstOrDefaultAsync(o => o.Id == offerList.Id);
                if (editOfferList != null)
                {
                    _dbcontext.OfferList.Update(offerList);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<OfferList>
                    {
                        Data = offerList,
                        Message = "OfferList suceessfully updated",
                        Success = true

                    };
                } else
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = null,
                        Message = "OfferList not found",
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

        public async Task<GenericResponse<IEnumerable<OfferList>>> GetAllOfferListsAsync()
        {
            try
            {
                //gets all the offerlists availabe and put them in a list
                var offerList = await _dbcontext.OfferList.ToListAsync();

                if (offerList != null)
                {
                    return new GenericResponse<IEnumerable<OfferList>>
                    {
                        Data = offerList,
                        Message = "All offers listed",
                        Success = true

                    };
                } else
                {
                    return new GenericResponse<IEnumerable<OfferList>>
                    {
                        Data = null,
                        Message = "OfferList not found", 
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

        public async Task<GenericResponse<OfferList>> GetOfferListByIdAsync(int Id)
        {
            try
            {
                //gets the  offerlist from the Db by its Id
                var offerList = await _dbcontext.OfferList.SingleOrDefaultAsync(o => o.Id == Id);

                if (offerList != null)
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = offerList,
                        Message = "OfferList found",
                        Success = true

                    };
                } else
                {
                    return new GenericResponse<OfferList>
                    {
                        Data = null,
                        Message = "OfferList not found",
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
