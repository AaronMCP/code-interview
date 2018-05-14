using System;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using HYS.XmlAdapter.Common.Objects;

namespace HYS.XmlAdapter.Outbound.Controlers
{
    public class XIMWrapper
    {
        public string XIMDocument;      // used only in merge SPS
        public XmlDocument Document;
        public List<string> GUIDList;
        public XIMWrapper(string xim)
        {
            XIMDocument = xim;
            GUIDList = new List<string>();
            if (XIMDocument == null) return;
            XIMDocument = XIMDocument.Replace(XIMTransformHelper.TransactionID, XIMTransformHelper.GetTransactionID());
        }
    }
}
