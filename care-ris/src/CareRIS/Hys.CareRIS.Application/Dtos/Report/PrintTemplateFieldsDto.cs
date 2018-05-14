using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class PrintTemplateFieldsDto
    {
        public string UniqueID { get; set; }
        public string FieldName { get; set; }
        public int? Type { get; set; }
        public string SubType { get; set; }
        public string Domain { get; set; }
    }
}
