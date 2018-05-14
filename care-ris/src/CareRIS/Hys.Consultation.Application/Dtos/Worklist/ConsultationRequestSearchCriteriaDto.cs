using Hys.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.CrossCutting.Common.Interfaces;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationRequestSearchCriteriaDto : Pagination
    {
        public string PatientName { get; set; }
        public string PatientNo { get; set; }
        public string InsuranceNumber { get; set; }
        public string IdentityCard { get; set; }
        public ConsultationRequestStatus[] StatusList { get; set; }
        public DateTime? ConsultationStartDate { get; set; }
        public DateTime? ConsultationEndDate { get; set; }
        public DateTime? RequestStartDate { get; set; }
        public DateTime? RequestEndDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public SearchType SearchType { get; set; }
        public bool IncludeDeleted { get; set; }
    }
}
