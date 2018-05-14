using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class ScanningTech : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }
        public string UniqueID { get; set; }
        public string ScanningTechName { get; set; }
        public string ParentId { get; set; }
        public string ModalityType { get; set; }
        
        public string Domain { get; set; }
        public string Site { get; set; }
    }
}
