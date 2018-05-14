using System.Collections.Generic;
using Hys.Consultation.Application.Dtos;
using Hys.CrossCutting.Common.Interfaces;

namespace Hys.Consultation.Application.Services
{
    public interface IUserManagementService
    {
        PaginationResult SearchUsers(UserSearchCriteriaDto searchCriteria);
        string GetRoleRelatedUserName(string roleID);
        bool SaveUser(UserDto user);
        IEnumerable<HospitalProfileDto> GetHospitals(bool isCenter);
        UserDto GetUser(string userID);
        bool SaveHospital(HospitalProfileDto hospitalDto);
        bool UpdateUser(string userID, Dictionary<string, object> properties);
    }
}
