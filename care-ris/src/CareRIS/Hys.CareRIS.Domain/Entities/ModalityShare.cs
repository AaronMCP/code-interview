using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public class ModalityShare : Entity
    {
        public override object UniqueId { get { return Guid; } }

        public string Guid { get; set; }
        public string TimeSliceGuid { get; set; }
        public string ShareTarget { get; set; }
        public int? TargetType { get; set; }
        public int? MaxCount { get; set; }
        public int? AvailableCount { get; set; }
        public string GroupId { get; set; }
        public DateTime? Date { get; set; }

        private ModalityTimeSlice _TimeSlice = new ModalityTimeSlice();
        public ModalityTimeSlice TimeSlice
        {
            get
            {
                if (_TimeSlice == null)
                    _TimeSlice = new ModalityTimeSlice();
                return _TimeSlice;

            }
            set { _TimeSlice = value; }
        }
    }
}
