using System;
using System.Collections.Generic;
using System.Text;

using CommonGlobalSettings;

namespace Common.ActionResult.Referral
{
    [Serializable()]
    public class ReferralLogActionResult : ReferralBaseActionResult
    {
        private List<RefLogModel> _refLoglist = null;

        public List<RefLogModel> RefLogList
        {
            get
            {
                return _refLoglist;
            }
            set
            { 
                _refLoglist = value; 
            }
        }
    }
}
