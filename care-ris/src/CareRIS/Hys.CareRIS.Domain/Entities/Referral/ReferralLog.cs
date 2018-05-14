using System;
using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities.Referral
{
    public class ReferralLog : Entity
    {
        public string ReferralID { get; set; }
        public string SourceDomain { get; set; }
        public string TargetDomain { get; set; }
        public string OperatorGuid { get; set; }
        public string OperatorName { get; set; }
        public DateTime OperateDt { get; set; }
        public string Memo { get; set; }
        public string EventDesc { get; set; }
        public DateTime? CreateDt { get; set; }
        public int? RefPurpose { get; set; }
        public string SourceSite { get; set; }
        public string TargetSite { get; set; }
    }
}
