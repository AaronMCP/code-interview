using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationRequestBaseDto : PatientBaseDto
    {
        public string RequestId { get; set; }
        public DateTime? ConsultationDate { get; set; }
        public DateTime? RequestCreateDate { get; set; }
        public DateTime? RequestCompleteDate { get; set; }
        public string ConsultationStartTime { get; set; }
        public string ConsultationEndTime { get; set; }
        public string ExpectedTimeRange { get; set; }
        public DateTime? ExpectedDate { get; set; }

        public string ReceiveHospitalID { get; set; }
        public string receiveHospitalName { get; set; }
        public int IsDeleted { get; set; }
        public string Experts { get; set; }
        // for consultation overdure
        public bool? IsOverdue { get; set; }

        public string ConsultationTimeInfo
        {
            get
            {
                if (ConsultationDate.HasValue)
                {
                    var result = ConsultationDate.Value.ToString("yyyy/dd/MM dddd");
                    if (!string.IsNullOrEmpty(ConsultationStartTime))
                    {
                        result += "<br/>" + ConsultationStartTime + " - " + ConsultationEndTime;
                    }
                    return result;
                }
                return string.Empty;
            }
        }
    }
}
