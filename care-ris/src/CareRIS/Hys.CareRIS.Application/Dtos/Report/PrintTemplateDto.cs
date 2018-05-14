using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class PrintTemplateDto
    {
        public string UniqueID { get; set; }
        public int? Type { get; set; }
        public string TemplateName { get; set; }
        public byte[] TemplateInfo { get; set; }
        public bool? IsDefaultByType { get; set; }
        public int? Version { get; set; }
        public string ModalityType { get; set; }
        public bool? IsDefaultByModality { get; set; }
        public string Domain { get; set; }
        public string PropertyTag { get; set; }
        public string Site { get; set; }
    }
}
