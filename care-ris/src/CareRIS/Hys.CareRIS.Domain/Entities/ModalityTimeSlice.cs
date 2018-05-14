using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public class ModalityTimeSlice : Entity
    {
        public override object UniqueId { get { return TimeSliceGuid; } }

        public string TimeSliceGuid { get; set; }
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
