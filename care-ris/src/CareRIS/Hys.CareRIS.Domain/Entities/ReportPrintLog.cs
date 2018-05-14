using Hys.Platform.Domain;
using System;

namespace Hys.CareRIS.Domain.Entities
{
    public class ReportPrintLog : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string ReportID { get; set; }
        public string TemplateID { get; set; }
        public string Printer { get; set; }
        public DateTime? PrintDate { get; set; }
        public string Type { get; set; }
        public int? Counts { get; set; }
        public string Comments { get; set; }
        public string BackupMark { get; set; }
        public string BackupComment { get; set; }
        public string SnapShotSrvPath { get; set; }
        public string Domain { get; set; }
    }
}
