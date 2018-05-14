using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class ReportFileDto
    {
        public string UniqueID { get; set; }
        public string ReportID { get; set; }
        public int? fileType { get; set; }
        public string RelativePath { get; set; }
        public string FileName { get; set; }
        public string BackupMark { get; set; }
        public string BackupComment { get; set; }
        public int? ShowWidth { get; set; }
        public int? ShowHeight { get; set; }
        public int? ImagePosition { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Domain { get; set; }
    }
}
