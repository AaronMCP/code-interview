using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class Modality : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string ModalityType { get; set; }
        public string ModalityName { get; set; }
        public string Room { get; set; }
        public string IPAddress { get; set; }
        public string Description { get; set; }
        public string Site { get; set; }
        public string Domain { get; set; }
        //
        public int BookingShowMode { get; set; }
        public int MaxLoad { get; set; }
        public int ApplyHaltPeriod { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
        public string WorkStationIP { get; set; }
        

    }
}
