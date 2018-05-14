namespace Hys.CareRIS.Domain.Entities
{
    using Hys.Platform.Domain;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class PrintTemplate : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }
        public int? Type { get; set; }
        public string TemplateName { get; set; }
        public byte[] TemplateInfo { get; set; }
        public int? IsDefaultByType { get; set; }
        public int? Version { get; set; }
        public string ModalityType { get; set; }
        public int? IsDefaultByModality { get; set; }
        public string Domain { get; set; }
        public string PropertyTag { get; set; }
        public string Site { get; set; }

    }
}
