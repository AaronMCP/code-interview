using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ModalityShareDto
    {
        public string UniqueID { get; set; }
        public string TimeSliceGuid { get; set; }
        public string ShareTarget { get; set; }
        public int? TargetType { get; set; }
        public int? MaxCount { get; set; }
        public int? AvailableCount { get; set; }
        public string GroupId { get; set; }
        public DateTime? Date { get; set; }
        
        //from modality timeslice
        public string ModalityType { get; set; }
        public string Modality { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
        public string Description { get; set; }
        public int? MaxNumber { get; set; }
        public string Domain { get; set; }
        public int? DateType { get; set; }
        public DateTime? AvailableDate { get; set; }
    }
}
