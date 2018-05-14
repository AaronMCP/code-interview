using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class HL7Field:HL7baseclass
    {
        public HL7Field(string sFld, HL7Delimeters delim)
        {
            deField = new HL7DataElement(sFld);
            delimeters = delim;
        }

        public HL7Field(XmlElement fieldElement, HL7Delimeters delim)
        {
            delimeters = delim;
            int iLastPopulatedPos = 0;
            String sField = "";
            XmlNodeList fieldItemList = fieldElement.ChildNodes;
            if(fieldItemList.Count > 0)
            {
                for(int i = 0; i < fieldItemList.Count; i++)
                {
                    XmlElement fieldItemElement = (XmlElement)fieldItemList[i];
                    HL7FieldItem fieldItem = new HL7FieldItem(fieldItemElement, delimeters);
                    
                    sField = sField + fieldItem.toString() + Char.ToString(delimeters.getRD());
                    if (fieldItem.toString().Length > 0)
                        iLastPopulatedPos = sField.Length - 1;
                }

                sField = sField.Substring(0, iLastPopulatedPos);
            }
            deField = new HL7DataElement(sField);
        }

        public HL7FieldItem getFieldItem(int iFieldOccurence)           
        {
            HL7FieldItem field = null;
            try
            {
                string sField = deField.getString(delimeters.getRD(), iFieldOccurence);
                field = new HL7FieldItem(sField, delimeters);
            }
            catch(UnableToGetStringException e)
            {
                throw new FieldNotFoundException(e.Message);
            }
            return field;
        }

        public int getLength()
        {
            return deField.getLength(delimeters.getRD());
        }

        public String toString()
        {
            return deField.toString();
        }

        public void buildXML(XmlDocument doc,XmlElement fieldElement)           
        {
            int iNumberFieldItems = getLength();
            HL7FieldItem fieldItem = new HL7FieldItem("", delimeters);
            for(int i = 1; i <= iNumberFieldItems; i++)
            {
                fieldItem = getFieldItem(i);
                XmlElement fieldItemElement = doc.CreateElement("FIELD_ITEM");
                fieldElement.AppendChild(fieldItemElement);
                fieldItem.buildXML(doc,fieldItemElement);
            }

        }

        private HL7DataElement deField;
        private HL7Delimeters delimeters;
    }
}
