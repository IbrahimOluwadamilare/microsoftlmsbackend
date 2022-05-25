using microsoft_lms_backend.Models.v1;
using microsoft_lms_backend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using microsoft_lms_backend.Models;

namespace microsoft_lms_backend.Interfaces.v1
{
  public interface IOfferListService
    {
        Task<GenericResponse<OfferList>> CreateOfferListAsync(OfferList offerList);
        Task<GenericResponse<OfferList>> DeleteOfferListAsync(int Id);
        Task<GenericResponse<OfferList>> EditOfferListAsync(OfferList offerList);
        Task<GenericResponse<OfferList>> GetOfferListByIdAsync(int Id);
        Task<GenericResponse<IEnumerable<OfferList>>> GetAllOfferListsAsync();

    }
}
 