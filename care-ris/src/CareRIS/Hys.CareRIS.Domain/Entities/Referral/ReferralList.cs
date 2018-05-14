using System;
using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities.Referral
{
    public class ReferralList : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string PatientID { get; set; }
        public string LocalName { get; set; }
        public string EnglishName { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string TelePhone { get; set; }
        public string Address { get; set; }
        public string AccNo { get; set; }
        public string ApplyDoctor { get; set; }
        public DateTime? ApplyDt { get; set; }
        public string ModalityType { get; set; }
        public string ProcedureCode { get; set; }
        public string CheckingItem { get; set; }
        public string HealthHistory { get; set; }
        public string Observation { get; set; }
        public int? Refpurpose { get; set; }
        public int? RefStatus { get; set; }
        public int? RPStatus { get; set; }
        public string ExamDomain { get; set; }
        public string ExamAccNo { get; set; }
        public DateTime? CreateDt { get; set; }
        public string InitialDomain { get; set; }
        public string SourceDomain { get; set; }
        public string TargetDomain { get; set; }
        public string TargetSite { get; set; }
        public int? Direction { get; set; }
        public int? IsExistSnapshot { get; set; }
        public string GetReportDomain { get; set; }
        public DateTime? BookingBeginDt { get; set; }
        public DateTime? BookingEndDt { get; set; }
        public string OriginalBizData { get; set; }
        public string PackagedBizData { get; set; }
        public int? Scope { get; set; }
        public string SourceSite { get; set; }
        public string RefApplication { get; set; }
        public string RefReport { get; set; }
    }
}
