using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class HL7SubComponent
    {
        public HL7SubComponent(string sSubComp, HL7Delimeters delim)
        {
            deSubComponent = new HL7DataElement(sSubComp);
            delimeters = delim;
        }

        public HL7SubComponent(XmlElement subComponentElement, HL7Delimeters delim)
        {
            string sText = subComponentElement.InnerText;
            delimeters = delim;
            deSubComponent = new HL7DataElement(sText);
        }

        public string toString()
        {
            return deSubComponent.toString();
        }

        public int getLength()
        {
            return deSubComponent.getLength();
        }

        public void buildXML(XmlElement subComponentElement)
        {
            subComponentElement.InnerText = this.toString();
        }

        private string replace(string sSource, char cSearch, string sReplace)
        {
            StringBuilder sb = new StringBuilder();
            int iLastPos = 0;
            for (int iPos = sSource.IndexOf(cSearch, 0); iPos > 0; iPos = sSource.IndexOf(cSearch, iPos + 1))
            {
                sb.Append(sSource.Substring(iLastPos, iPos-iLastPos));
                sb.Append(sReplace);
                iLastPos = iPos + 1;
            }

            sb.Append(sSource.Substring(iLastPos));
            string st = sb.ToString();
            return sb.ToString();
        }

        private HL7DataElement deSubComponent;
        private HL7Delimeters delimeters;
    }
}
