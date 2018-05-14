using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Linq;

namespace CommonGlobalSettings
{
    /// <summary>
    /// Referral Base model
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(ReferralModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ReferralListModel))]
    [System.Xml.Serialization.XmlInclude(typeof(RefPatientModel))]
    [System.Xml.Serialization.XmlInclude(typeof(RefOrderModel))]
    [System.Xml.Serialization.XmlInclude(typeof(RefProcedureModel))]
    [System.Xml.Serialization.XmlInclude(typeof(BookingRefModel))]
    [System.Xml.Serialization.XmlInclude(typeof(RefEventModel))]
    [System.Xml.Serialization.XmlInclude(typeof(RefLogModel))]
    [System.Xml.Serialization.XmlInclude(typeof(RefReportSnapshotModel))]
    [System.Xml.Serialization.XmlInclude(typeof(RefReportSnapshotListModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ERequisitionModel))]
    [System.Xml.Serialization.XmlInclude(typeof(SendingRefContext))]
    public abstract class ReferralBaseModel : BaseModel
    {
        //public abstract XElement SerializeXML();

        //public abstract void DeSerializeXML(string xml);
    }
}

