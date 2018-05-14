namespace Hys.CareRIS.Domain.Entities
{
    using Hys.Platform.Domain;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ReportTemplateDirec:Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }
        public string UniqueID { get; set; }
        public string ParentID { get; set; }
        public int? Depth { get; set; }
        public string ItemName { get; set; }
        public int? Type { get; set; }
        public int? ItemOrder { get; set; }
        public string UserID { get; set; }
        public string TemplateID { get; set; }
        public int? Leaf { get; set; }
        public string Domain { get; set; }
        public string DirectoryType { get; set; }
    }
}
