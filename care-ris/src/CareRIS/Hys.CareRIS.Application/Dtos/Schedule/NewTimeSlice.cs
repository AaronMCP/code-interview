using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class NewTimeSlice
    {
        public string ModalityType { get; set; }
        public string Modality { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int[] DateTypes { get; set; }
        public DateTime AvailableDate { get; set; }
        public int? Interval { get; set; }
    }
}
