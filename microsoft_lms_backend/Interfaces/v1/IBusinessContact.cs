using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Models.v1.BusinessProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
   public interface IBusinessContact
    {
        Task<GenericResponse<IEnumerable<BusinessContact>>> GetAllBusinessContactsAsync();
        Task<GenericResponse<BusinessContact>> GetBusinessContactByIdAsync(int Id);
        Task<GenericResponse<BusinessContact>> UpdateBusinessContactAsync(BusinessContact businessContact);
        Task<GenericResponse<BusinessContact>> DeleteBusinessContactAsync(int Id);
        Task<GenericResponse<BusinessContact>> CreateNewBusinessContactAsync (BusinessContact businessContact);
    }
}
