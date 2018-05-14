using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XmlMessage
    {
        public class Request
        {
            public static XmlElement XISVersion = new XmlElement("/XMLRequestMessage/XISVersion");
            public static XmlElement Name = new XmlElement("/XMLRequestMessage/Name");
            public static XmlElement Qualifier = new XmlElement("/XMLRequestMessage/Qualifier");
            public static XmlElement OriginatingDevice = new XmlElement("/XMLRequestMessage/OriginatingDevice");
            public static XmlElement TransactionID = new XmlElement("/XMLRequestMessage/TransactionID");
            public static XmlElement TargetDevice = new XmlElement("/XMLRequestMessage/TargetDevice[]");
            public static XmlElement XIM = new XmlElement("/XMLRequestMessage/XIM", typeof(XIM));
        }

        public class Response
        {
            public static XmlElement XISVersion = new XmlElement("/XMLResponseMessage/XISVersion");
            public static XmlElement Name = new XmlElement("/XMLResponseMessage/Name");
            public static XmlElement TransactionID = new XmlElement("/XMLResponseMessage/TransactionID");
            public static XmlElement StatusCode = new XmlElement("/XMLResponseMessage/Status/Code");
            public static XmlElement StatusFailureCode = new XmlElement("/XMLResponseMessage/Status/FailureCode");
            public static XmlElement StatusMessage = new XmlElement("/XMLResponseMessage/Status/Message");
            public static XmlElement DeviceResponse = new XmlElement("/XMLResponseMessage/DeviceResponse[]/Device");
            public static XmlElement DeviceResponseStatusCode = new XmlElement("/XMLResponseMessage/DeviceResponse[]/Status/Code");
            public static XmlElement DeviceResponseStatusMessage = new XmlElement("/XMLResponseMessage/DeviceResponse[]/Status/Message");
            public static XmlElement XIM = new XmlElement("/XMLResponseMessage/XIM", typeof(XIM));
        }
    }
}
