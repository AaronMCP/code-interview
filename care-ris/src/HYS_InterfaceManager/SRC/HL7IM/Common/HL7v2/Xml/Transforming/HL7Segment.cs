using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class HL7Segment:HL7baseclass
    {
        public HL7Segment(string sSeg, HL7Delimeters delim)
        {
            deSegment = new HL7DataElement(sSeg);
            delimeters = delim;
            try
            {
                sName = deSegment.getString(delimeters.getFD(), 1).Trim();
                if (sName.Equals(""))
                {
                    //throw new InvalidSegmentException("Segment name is empty");
                }
            }
            catch(UnableToGetStringException e)
            {
                throw new InvalidSegmentException(e.Message);
            }
            catch(Exception e)
            {
                throw new InvalidSegmentException(e.Message);
            }
        }

        public HL7Segment(XmlElement segmentElement, HL7Delimeters delim)
        {
            delimeters = delim;
            sName = segmentElement.Name;
            int iLastPopulatedPos = 0;
            string sSegment  = (sName) + char.ToString(delimeters.getFD());
            try
            {
                XmlNodeList fieldList = segmentElement.ChildNodes;
                for(int i = 0; i < fieldList.Count; i++)
                {
                    if(sName.Equals(MESSAGE_HEADER,StringComparison.CurrentCultureIgnoreCase) && i == 0)
                        continue;
                    if (sName.Equals(MESSAGE_HEADER, StringComparison.CurrentCultureIgnoreCase) && i == 1)
                    {
                        sSegment =sSegment + (delimeters.toString().Substring(1)) + char.ToString(delimeters.getFD());
                        continue;
                    }
                    XmlElement fieldElement = (XmlElement)fieldList[i];
                    HL7Field field = new HL7Field(fieldElement, delimeters);
                    sSegment = sSegment +field.toString() + char.ToString(delimeters.getFD());
                    if(field.toString().Length > 0)
                        iLastPopulatedPos = sSegment.Length - 1;
                }

                deSegment = new HL7DataElement(sSegment.Substring(0, iLastPopulatedPos));
            }
            catch(InvalidFieldException e)
            {
                throw new InvalidSegmentException(e.Message);
            }
        }

        public string getName()
        {
            return sName;
        }

        public HL7Field getField(int iFieldPos)
        {
            HL7Field field = null;
            try
            {
                String sField = "";
                if(sName.Equals(MESSAGE_HEADER,StringComparison.CurrentCultureIgnoreCase))
                {
                    if(iFieldPos == 1)
                        sField = char.ToString(delimeters.getFD());
                    else
                    if(iFieldPos == 2)
                        sField = delimeters.toString().Substring(1);
                    else
                        sField = deSegment.getString(delimeters.getFD(), iFieldPos);
                } else
                {
                    sField = deSegment.getString(delimeters.getFD(), iFieldPos + 1);
                }
                field = new HL7Field(sField, delimeters);
            }
            catch(UnableToGetStringException e)
            {
                throw new FieldNotFoundException(e.Message);
            }
            return field;
        }

        public String toString()
        {
            return deSegment.toString();
        }

        public int getLength()
        {
            int len;
            if(sName.Equals(MESSAGE_HEADER,StringComparison.CurrentCultureIgnoreCase))
                len = deSegment.getLength(delimeters.getFD());
            else
                len = deSegment.getLength(delimeters.getFD()) - 1;
            return len;
        }

        public void buildXML(XmlDocument doc,XmlElement messageElement)
        {
            
            int iNumberFields = getLength();
            HL7Field field = new HL7Field("", delimeters);
            XmlElement segmentElement = doc.CreateElement(sName);
            messageElement.AppendChild(segmentElement);
            for(int i = 1; i <= iNumberFields; i++)
            {
                XmlElement fieldElement = doc.CreateElement("FIELD."+i.ToString());
                segmentElement.AppendChild(fieldElement);
                if(sName.Equals(MESSAGE_HEADER,StringComparison.CurrentCultureIgnoreCase) && i == 1)
                {
                    fieldElement.InnerText=char.ToString(delimeters.getFD());
                    continue;
                }
                if(sName.Equals(MESSAGE_HEADER,StringComparison.CurrentCultureIgnoreCase) && i == 2)
                {
                    fieldElement.InnerText=(delimeters.toString().Substring(1));
                } else
                {
                    field = getField(i);
                    field.buildXML(doc,fieldElement);
                }
            }

        }

        private HL7DataElement deSegment;
        private HL7Delimeters delimeters;
        private string sName;
    }
}
