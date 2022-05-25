using microsoft_lms_backend.Models.v1;
using microsoft_lms_backend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using microsoft_lms_backend.Models;

namespace microsoft_lms_backend.Interfaces.v1
{
  public interface IOfferService
    {
        Task<GenericResponse<Offers>> CreateOfferAsync(Offers offers);
        Task<GenericResponse<Offers>> DeleteOfferAsync(int Id);
        Task<GenericResponse<Offers>> EditOfferAsync(Offers offers);
        Task<GenericResponse<Offers>> GetOfferByIdAsync(int Id);
        Task<GenericResponse<IEnumerable<Offers>>> GetAllOffersAsync();

    }
}
 