namespace Hys.Consultation.Domain.Entities
{
    using Hys.Platform.Domain;
    using System;

    public partial class ConsultatReportTemplate:Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }

        public string TemplateName { get; set; }

        public string ModalityType { get; set; }

        public string BodyPart { get; set; }

        public byte[] WYS { get; set; }

        public byte[] WYG { get; set; }

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

    }
}
