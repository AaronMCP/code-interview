using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class HL7FieldItem:HL7baseclass
    {
        public HL7FieldItem(string sFld, HL7Delimeters delim)
        {
            deField = new HL7DataElement(sFld);
            delimeters = delim;
            if (deField.getLength(delimeters.getRD()) > 1 && !delimeters.toString().Equals(deField.toString(), StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidFieldException("");
            else
                return;
        }

        public HL7FieldItem(XmlElement fieldItemElement, HL7Delimeters delim)            
        {
            delimeters = delim;
            string sFieldItem = "";
            int iLastPopulatedPos = 0;
            XmlNodeList componentList = fieldItemElement.ChildNodes;
            if(componentList.Count == 0)
            {
                HL7Component component = new HL7Component(fieldItemElement, delimeters);
                sFieldItem =sFieldItem +component.toString();
                
            } else
            {
                if (componentList.Count == 1 && componentList[0].NodeType == XmlNodeType.Text)
                {
                    HL7Component component = new HL7Component(fieldItemElement, delimeters);
                    sFieldItem = sFieldItem + component.toString();
                    if (component.toString().Length > 0)
                        iLastPopulatedPos = sFieldItem.Length;
                }
                else
                {
                    for (int i = 0; i < componentList.Count; i++)
                    {
                        XmlElement componentElement = (XmlElement)componentList[i];
                        HL7Component component = new HL7Component(componentElement, delimeters);
                        sFieldItem = sFieldItem + component.toString() + delimeters.getCD();
                        if (component.toString().Length > 0)
                            iLastPopulatedPos = sFieldItem.Length - 1;
                    }

                    
                }

                sFieldItem = sFieldItem.Substring(0, iLastPopulatedPos);
            }
            deField = new HL7DataElement(sFieldItem);
        }

        public HL7Component getComponent(int iComponentPos)            
        {
            string sString;
            try
            {
                sString = deField.getString(delimeters.getCD(), iComponentPos);
            }
            catch(UnableToGetStringException e)
            {
                throw new ComponentNotFoundException(e.Message+", can not get component "+iComponentPos.ToString()+" in field "+deField.toString());
            }
            HL7Component comp = new HL7Component(sString, delimeters);
            return comp;
        }

        public String toString()
        {
            return deField.toString();
        }

        public int getLength()
        {
            return deField.getLength(delimeters.getCD());
        }

        public void buildXML(XmlDocument doc,XmlElement fieldItemElement)            
        {
            HL7Component component = new HL7Component("", delimeters);
            int iNumberComponents = getLength();
            for(int i = 1; i <= iNumberComponents; i++)
            {
                component = getComponent(i);
                XmlElement componentElement = doc.CreateElement("COMPONENT."+i.ToString());
                fieldItemElement.AppendChild(componentElement);
                component.buildXML(doc,componentElement);
            }

        }

        private HL7DataElement deField;
        private HL7Delimeters delimeters;
    }
}
