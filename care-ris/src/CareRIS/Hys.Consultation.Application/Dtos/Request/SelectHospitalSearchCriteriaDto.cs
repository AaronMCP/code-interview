using Hys.CrossCutting.Common.Interfaces;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultHospitalSearchCriteriaDto : Pagination
    {

        public string Name { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
    }
}
