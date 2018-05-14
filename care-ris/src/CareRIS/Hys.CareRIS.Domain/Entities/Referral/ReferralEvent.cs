using System;
using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities.Referral
{
    public class ReferralEvent : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string ReferralID { get; set; }
        public string SourceDomain { get; set; }
        public string TargetDomain { get; set; }
        public string Memo { get; set; }
        public int? Event { get; set; }
        public int? Status { get; set; }
        public string ExamDomain { get; set; }
        public string ExamAccNo { get; set; }
        public string OperatorGuid { get; set; }
        public string OperatorName { get; set; }
        public DateTime? OperateDt { get; set; }
        public int? Tag { get; set; }
        public string Content { get; set; }
        public int? Scope { get; set; }
        public string SourceSite { get; set; }
        public string TargetSite { get; set; }
    }
}
