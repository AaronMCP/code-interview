using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos.PatientCase
{
    public class VisitEMRFileItemDto
    {
        public string UniqueID { get; set; }
        public string ParentID { get; set; }
        public int ItemType { get; set; }
        public string FileName { get; set; }
        public int FileType { get; set; }
        public Int64 FileSize { get; set; }
        public string DestFilePath { get; set; }
        public VisitFileStatus FileStatus { get; set; }
        public string FileExtension { get; set; }
        public string CreatorID { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public DateTime? UploadedTime { get; set; }
        public string ExtraConfigID { get; set; }
    }

    public enum VisitFileStatus
    {
        UnReg = 0,
        Regstered = 1,
        Uploading = 2,
        Uploaded = 3,
        Forward = 4,
        Forwarded = 5,
        Error = 10,
        UploadError = 11,
        ForwardError = 12
    }
}
