using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Models.v1.BusinessProfileModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface IBusinessProfile
    {
        Task<GenericResponse<IEnumerable<BusinessProfile>>> GetAllBusinessProfilesAsync();
        Task<GenericResponse<BusinessProfile>> GetBusinessProfileByIdAsync(int Id);
        Task<GenericResponse<BusinessProfile>> UpdateBusinessProfileAsync(BusinessProfile businessProfile);
        Task<GenericResponse<BusinessProfile>> DeleteBusinessProfileAsync(int Id);
        Task<GenericResponse<BusinessProfile>> CreateNewBusinessProfileAsync(BusinessProfile businessProfile);

    }



}
