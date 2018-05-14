using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CommonGlobalSettings
{
    public class CancelResponse
    {
        protected bool _canCancel = false;
        protected string _reason = "";

        public bool CanCancel
        {
            get { return _canCancel; }
            set { _canCancel = value; }
        }

        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        public XElement SerializeXML()
        {
            XElement element = new XElement("CancelReponse",
                new XElement("CanCancel", this._canCancel ? 1 : 0),
               new XElement("Reason", this._reason));

            return element;
        }

        public void DeSerializeXML(XElement element)
        {
            if (element == null) return;

            var e = (from node in element.Descendants("CanCancel")
                     select node).SingleOrDefault();
            this._canCancel = e.Value == "1" ? true : false;


            e = (from node in element.Descendants("Reason")
                 select node).SingleOrDefault();
            this._reason = e.Value;

        }

        public static CancelResponse Parse(string xml)
        {
            XDocument doc = null;
            try
            {
                doc = XDocument.Parse(xml);
            }
            catch
            {
                return null;
            }

            var e = (from node in doc.Descendants("CancelReponse")
                     select node).SingleOrDefault();
            CancelResponse response = new CancelResponse();
            response.DeSerializeXML(e);

            return response;
        }
    }
}
