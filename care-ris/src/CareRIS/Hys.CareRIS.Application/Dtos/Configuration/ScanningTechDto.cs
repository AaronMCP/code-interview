using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ScanningTechDto 
    {
        public string UniqueID { get; set; }
        public string ScanningTechName { get; set; }
        public string ParentId { get; set; }
        public string ModalityType { get; set; }

        public string Domain { get; set; }
        public string Site { get; set; }
    }
}
