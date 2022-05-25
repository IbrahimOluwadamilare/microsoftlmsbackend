using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Models.v1.BusinessProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
   public interface IBusinessExpertise
    {
        Task<GenericResponse<IEnumerable<BusinessExpertise>>> GetAllBusinessExpertisesAsync();
        Task<GenericResponse<BusinessExpertise>> GetBusinessExpertiseByIdAsync(int Id);
        Task<GenericResponse<BusinessExpertise>> UpdateBusinessExpertiseAsync(BusinessExpertise businessExpertise);
        Task<GenericResponse<BusinessExpertise>> DeleteBusinessExpertiseAsync(int Id);
        Task<GenericResponse<BusinessExpertise>> CreateNewBusinessExpertiseAsync(BusinessExpertise businessExpertise);
    }
}
