using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class HL7Component:HL7baseclass
    {
        public HL7Component(string sComp, HL7Delimeters delim)
        {
            deComponent = new HL7DataElement(sComp);
            delimeters = delim;
        }

        public HL7Component(XmlElement componentElement, HL7Delimeters delim)
        {
            delimeters = delim;
            string sComponent = "";
            int iLastPopulatedPos = 0;
            XmlNodeList subComponentList = componentElement.ChildNodes;
            if(subComponentList.Count ==0)
            {
                HL7SubComponent subComponent = new HL7SubComponent(componentElement, delimeters);
                sComponent = sComponent + subComponent.toString();                
                
            } else
            {
                if (subComponentList.Count ==1 && componentElement.ChildNodes[0].NodeType == XmlNodeType.Text)
                {
                    
                        HL7SubComponent subComponent = new HL7SubComponent(componentElement, delimeters);
                        sComponent = sComponent + subComponent.toString();
                        if (subComponent.toString().Length > 0)
                            iLastPopulatedPos = sComponent.Length;
                    
                }
                else
                {
                    for (int i = 0; i < subComponentList.Count; i++)
                    {
                        XmlElement subComponentElement = (XmlElement)subComponentList[i];
                        HL7SubComponent subComponent = new HL7SubComponent(subComponentElement, delimeters);
                        sComponent = sComponent + subComponent.toString() + char.ToString(delimeters.getSD());
                        if (subComponent.toString().Length > 0)
                            iLastPopulatedPos = sComponent.Length - 1;
                    }
                }

                sComponent = sComponent.Substring(0, iLastPopulatedPos);
            }
            deComponent = new HL7DataElement(sComponent);
        }

        public HL7SubComponent getSubComponent(int iSubComponentPos)           
        {
            string sString = "";
            try
            {
                sString = deComponent.getString(delimeters.getSD(), iSubComponentPos);
            }
            catch(UnableToGetStringException e)
            {
                throw new SubComponentNotFoundException(e.Message);
            }
            HL7SubComponent subComp = new HL7SubComponent(sString, delimeters);
            return subComp;
        }

        public string toString()
        {
            return deComponent.toString();
        }

        public int getLength()
        {
            return deComponent.getLength(delimeters.getSD());
        }

        public void buildXML(XmlDocument doc,XmlElement componentElement)
        {
            HL7SubComponent subComponent = new HL7SubComponent("", delimeters);
            int iNumberSubComponents = getLength();
            if(iNumberSubComponents == 1)
            {
                subComponent = getSubComponent(1);
                subComponent.buildXML(componentElement);
            } else
            {              
                
                for(int i = 1; i <= iNumberSubComponents; i++)
                {
                    subComponent = getSubComponent(i);

                    XmlElement subComponentElement = doc.CreateElement("SUBCOMPONENT."+i.ToString());
                    componentElement.AppendChild(subComponentElement);
                    subComponent.buildXML(subComponentElement);
                }

            }
        }

        private HL7DataElement deComponent;
        private HL7Delimeters delimeters;
    }
}
