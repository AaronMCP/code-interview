using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationDetailDto : ConsultationRequestDto
    {
        public string ConsultationAdvice { get; set; }

        public string ConsultationRemark { get; set; }

        public string ConsultationReportID { get; set; }

        public string Writer { get; set; }

        public string InsuranceNumber { get; set; }

        public string IdentityCard { get; set; }

        public string RequestUser { get; set; }

        public string RequestHospitalName { get; set; }

        public string Telephone { get; set; }

        public DateTime? Birthday { get; set; }

        public string RequestPurpose { get; set; }

        public string RequestRequirement { get; set; }

        public string History { get; set; }

        public string ClinicalDiagnosis { get; set; }

        public string ServiceTypeName { get; set; }

        public string ServiceTypeID { get; set; }

        public string OtherReason { get; set; }

        public string ChangeReasonType { get; set; }

        public string AssignedDescription { get; set; }

        public string AssignExpertIDs { get; set; }
        public string AssignExpertNames { get; set; }
        public string HostID { get; set; }
        public string HostName { get; set; }

        /// <summary>
        /// Delete info
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        public string DeleteReason { get; set; }
        public string DeleteUser { get; set; }
        public string DeleteUserName { get; set; }

        public IEnumerable<ConsultationReportHistoryDto> ReportHistories { get; set; }
    }

  
}
