using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Common.ActionResult;
using CommonGlobalSettings;

namespace Common.ActionResult.Referral
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(ReferralActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(ReferralLogActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(RefReportSnapshotActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(ReferralBookingActionResult))]
    public class ReferralBaseActionResult : BaseActionResult
    {        
    }
}
