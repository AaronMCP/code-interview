using Hys.Platform.Domain;
using System;

namespace Hys.Consultation.Domain.Entities
{
    public partial class EMRItem : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string PatientCaseID { get; set; }
        public string PatientNo { get; set; }
        public string EMRItemType { get; set; }
        public string EMRName { get; set; }
        public string ExamDate { get; set; }
        public string ReportDate { get; set; }
        public string BodyPart { get; set; }
        public string ExamModality { get; set; }
        public string AccessionNo { get; set; }
        public string ProcedureCode { get; set; }
        public string ExamSection { get; set; }
        public string ExamDescription { get; set; }
        public string Observation { get; set; }
        public string Conclusion { get; set; }
        public string AuthorDoctor { get; set; }
        public string Authenticator { get; set; }
        public int Progress { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
