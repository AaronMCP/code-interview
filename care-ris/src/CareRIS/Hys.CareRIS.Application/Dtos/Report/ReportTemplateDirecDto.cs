﻿using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class ReportTemplateDirecDto
    {
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
        // not mapped tp database
        public ReportTemplateDto ReportTemplateDto { get; set; }
        public bool HasChildren { get { return Leaf == 0; } }
    }
}
