using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ModalityTimeSliceDto
    {
        public string UniqueID { get; set; }
        public string ModalityType { get; set; }
        public string Modality { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
        public string Description { get; set; }
        public int? MaxNumber { get; set; }
        public string Domain { get; set; }
        public int? DateType { get; set; }
        public DateTime? AvailableDate { get; set; }
        //caculate
        public int TotalPrivateQuota = 0;
        public int TotalSharedQuota = 0;
        public int TotalUsedQuota = 0;
        public int TotalAvailableQuota = 0;

        public bool IsShared = false;
    }
}
