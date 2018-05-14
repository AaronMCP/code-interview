using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos.Referral
{
    public class ReferralListDto
    {
        public string ReferralID { get; set; }
        public string LocalName { get; set; }
        public string Gender { get; set; }
        public string AccNo { get; set; }
        public string ModalityType { get; set; }
        public string ProcedureCode { get; set; }
        public int Refpurpose { get; set; }
        public int RefStatus { get; set; }
        public int RPStatus { get; set; }
        public DateTime? CreateDt { get; set; }
        public string TargetSite { get; set; }
        public string CurrentAge { get; set; }
        public string ProcedureID { get; set; }
        public string SiteName { get; set; }
    }
}
