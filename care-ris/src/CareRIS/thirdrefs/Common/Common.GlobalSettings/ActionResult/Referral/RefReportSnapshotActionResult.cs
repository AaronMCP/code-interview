using System;
using System.Collections.Generic;
using System.Text;
using CommonGlobalSettings;

namespace Common.ActionResult.Referral
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(RefReportSnapshotModel))]
    public class RefReportSnapshotActionResult : ReferralBaseActionResult
    {
        private List<RefReportSnapshotModel> rptSts = null;

        public List<RefReportSnapshotModel> ReportSnapshots
        {
            get { return rptSts; }
            set { rptSts = value; }
        }
    }
}
