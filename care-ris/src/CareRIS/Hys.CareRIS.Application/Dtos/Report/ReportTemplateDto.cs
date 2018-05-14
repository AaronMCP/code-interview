﻿using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class ReportTemplateDto
    {
        public string UniqueID { get; set; }

        public string TemplateName { get; set; }

        public string ModalityType { get; set; }

        public string BodyPart { get; set; }

        public byte[] WYS { get; set; }

        public string WYSText { get; set; }

        public byte[] WYG { get; set; }

        public string WYGText { get; set; }

        public byte[] AppendInfo { get; set; }

        public byte[] TechInfo { get; set; }

        public string CheckItemName { get; set; }

        public string DoctorAdvice { get; set; }

        public string ShortcutCode { get; set; }

        public string ACRCode { get; set; }

        public string ACRAnatomicDesc { get; set; }

        public string ACRPathologicDesc { get; set; }

        public string BodyCategory { get; set; }

        public string Domain { get; set; }

        public int? IsPositive { get; set; }

        public string Gender { get; set; }

        public string UserID { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public string ParentID { get; set; }
        public int  Type { get; set; }
    }
}
