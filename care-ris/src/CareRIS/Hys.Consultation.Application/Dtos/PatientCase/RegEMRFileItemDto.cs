using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos.PatientCase
{
    public class RegEMRFileItemDto
    {
        public string UniqueID { get; set; }
        public int ItemType { get; set; }
        public string DicomPrefix { get; set; }
        public string SrcFilePath { get; set; }
        public Int64 FileSize { get; set; }
        public string SrcInfo { get; set; }
        public string FileName { get; set; }
        public string CreatorID { get; set; }
        public string Description { get; set; }
        public string ParentID { get; set; }
    }
}
