namespace Hys.CareRIS.Domain.Entities
{
    using Hys.Platform.Domain;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ReportFile : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }
        public string ReportID { get; set; }
        /// <summary>
        /// 0£ºrtf,1:html,2:jpg,3:txt,4:xml,5:pdf
        /// </summary>
        public int? fileType { get; set; }
        public string RelativePath { get; set; }
        public string FileName { get; set; }
        public string BackupMark { get; set; }
        public string BackupComment { get; set; }
        public int? ShowWidth { get; set; }
        public int? ShowHeight { get; set; }
        public int? ImagePosition { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Domain { get; set; }
    }
}
