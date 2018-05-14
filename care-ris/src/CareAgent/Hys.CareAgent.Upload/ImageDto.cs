using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hys.CareAgent.Upload
{
    public class ImageDto
    {
        public string UniqueID { get; set; }
        public string SeriesInstanceUID { get; set; }
        public string ObjectFile { get; set; }
        public int Status { get; set; }
        public string SrcFilePath { get; set; }
        public Int64 FileSize { get; set; }
        public string SrcInfo { get; set; }
        public string CreatorID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UploadedTime { get; set; }
        public int Progress { get; set; }
        public string Description { get; set; }
        public string ExtraConfigID { get; set; }
    }
}
