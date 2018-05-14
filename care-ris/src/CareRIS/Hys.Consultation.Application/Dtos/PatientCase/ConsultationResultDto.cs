using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos.PatientCase
{
    public class ConsultationResultDto
    {
        public string UniqueID { get; set; }
        public string ConsultationNo { get; set; }
        public int Status { get; set; }
        public int IsDeleted { get; set; }
        public string RequestHospital { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ConsultationDate { get; set; }
        public string Advice { get; set; }
        public string Description { get; set; }
    }
}
