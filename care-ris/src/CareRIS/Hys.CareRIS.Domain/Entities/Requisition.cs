namespace Hys.CareRIS.Domain.Entities
{
    using Hys.Platform.Domain;
    using System;

    public partial class Requisition : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }
        public string AccNo { get; set; }
        public string RelativePath { get; set; }
        public string FileName { get; set; }
        public DateTime? ScanTime { get; set; }
        public string Domain { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? Uploaded { get; set; }
        public DateTime? CreateTime { get; set; }
        public string ErNo { get; set; }
    }
}
