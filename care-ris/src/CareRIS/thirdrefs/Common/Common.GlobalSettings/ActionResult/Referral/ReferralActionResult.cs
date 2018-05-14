using System;
using System.Collections.Generic;
using System.Text;
using CommonGlobalSettings;

namespace Common.ActionResult.Referral
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(ReferralModel))]
    public class ReferralActionResult : ReferralBaseActionResult
    {
        private List<ReferralViewModel> _referlist = null;
        private int _total = 0;

        public List<ReferralViewModel> ReferralList
        {
            get
            {
                return _referlist;
            }
            set
            {
                _referlist = value;
            }
        }

        public int TotalCount
        {
            get { return _total; }
            set { _total = value; }
        }
    }
}
