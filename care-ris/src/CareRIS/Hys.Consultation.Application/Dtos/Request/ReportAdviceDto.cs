using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos
{
    public class ReportAdviceDto : AuditDto
    {
        public string RequestID { get; set; }
        public string ConsultationReportID { get; set; }

        public string ConsultationAdvice { get; set; }

        public string ConsultationRemark { get; set; }

        public string Writer { get; set; }
    }
}
