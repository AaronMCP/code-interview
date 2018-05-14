using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos.Referral
{
    public class ReferralListSearchDto
    {
        public int Count;
        public IEnumerable<ReferralListDto> Referrals { get; set; }
    }
}
