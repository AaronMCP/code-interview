using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationReportHistoryDto : AuditDto
    {
        public string ReportID  { get; set; }
        public string ConsultationAdvice { get; set; }
        public string Writer { get; set; }
    }
}
