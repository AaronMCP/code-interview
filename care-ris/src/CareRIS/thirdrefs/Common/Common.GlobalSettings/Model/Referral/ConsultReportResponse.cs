using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CommonGlobalSettings
{
    public class ConsultReportResponse
    {
        protected string _domain = "";
        protected string _reportId = "";
        protected string _memo = "";
        protected string _createrName = "";
        protected int _positive = 0;

        protected Dictionary<string, string> _dicRptAdivce =
            new Dictionary<string, string>();

        public string Domain
        {
            get { return _domain; }
            set { this._domain = value; }
        }

        public Dictionary<string, string> DicReportAdvice
        {
            get { return _dicRptAdivce; }
        }


        public void Add(string reportId, string consultAdvice)
        {
            if (!_dicRptAdivce.ContainsKey(reportId))
            {
                _dicRptAdivce.Add(reportId, consultAdvice);
            }
        }

        public XElement SerializeXML()
        {
            XElement element = new XElement("ConsultAdviceResponse");

            foreach (var key in _dicRptAdivce.Keys)
            {
                element.Add(
                    new XElement("ConsultAdvice",
                        new XElement("ReportID", key),
                        new XElement("ConsultContent", Convert.ToBase64String(Encoding.Unicode.GetBytes(_dicRptAdivce[key])))));
            }

            return element;
        }

        public void DeSerializeXML(XElement element)
        {
            if (element == null) return;

            var elements = element.Descendants("ConsultAdvice");
            foreach (var advice in elements)
            {
                var e = (from node in advice.Descendants("ReportID")
                         select node).SingleOrDefault();
                if (!_dicRptAdivce.ContainsKey(e.Value))
                {

                    var content = (from node in advice.Descendants("ConsultContent")
                                   select node).SingleOrDefault();

                    _dicRptAdivce.Add(e.Value, Encoding.Unicode.GetString(Convert.FromBase64String(content.Value)));
                }
            }
        }

        public static ConsultReportResponse Parse(string strXml)
        {
            XDocument doc = null;
            try
            {
                doc = XDocument.Parse(strXml);
            }
            catch
            {
                return null;
            }
            var e = (from node in doc.Descendants("ConsultAdviceResponse")
                     select node).SingleOrDefault();

            ConsultReportResponse consultAd = new ConsultReportResponse();
            consultAd.DeSerializeXML(e);

            return consultAd;
        }
    }
}
