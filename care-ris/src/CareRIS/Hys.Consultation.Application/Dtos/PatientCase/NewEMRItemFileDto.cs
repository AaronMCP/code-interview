using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos.PatientCase
{
    public class NewEMRItemFileDto
    {
        public string UniqueID { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string SrcInfo { get; set; }
        public string DetailedId { get; set; }
        public bool? IsFromRis { get; set; }
        public string PacsPatientId { get; set; }
        public string PacsAccessionNo { get; set; }
    }
}
