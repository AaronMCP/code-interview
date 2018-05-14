using Hys.CrossCutting.Common.Interfaces;

namespace Hys.Consultation.Application.Dtos
{
    public class UserSearchCriteriaDto : Pagination
    {

        public string Name { get; set; }
        public string HospitalID { get; set; }
        public string RoleID { get; set; }
        public string DepartmentID { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public bool ShowAllUser { get; set; }
        public bool IncludeMobile { get; set; }
        public int? IsInCenter { get; set; }
    }
}
