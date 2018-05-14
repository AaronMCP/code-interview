using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos.Referral
{
    public class ManualReferralDto
    {
        public string OrderID { get; set; }
        public string TargetSite { get; set; }
        public string Memo { get; set; }
    }
}
