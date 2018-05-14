using System;
using Hys.CrossCutting.Common.Interfaces;

namespace Hys.Consultation.Application.Dtos
{
    public class PatientCaseSearchCriteriaDto : Pagination
    {
        public string PatientName { get; set; }
        public string PatientNo { get; set; }
        public string InsuranceNumber { get; set; }
        public string IdentityCard { get; set; }
        public PatientCaseStatus[] StatusList { get; set; }
        public bool IncludeDeleted { get; set; }
    }
}
