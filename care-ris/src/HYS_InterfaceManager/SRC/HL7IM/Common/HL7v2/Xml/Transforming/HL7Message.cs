using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class HL7Message:HL7baseclass
    {
        private string charSet;
	
        public HL7Message(string sInMsg, int iMessageSyntax)            
        {
            delimeters = null;
            sMessageType = null;
            sHL7Encoding = "";
            sXMLEncoding = "";
            xmlMessage = null;
            characterSet = new HL7CharacterSets();
            message(sInMsg, iMessageSyntax);
        }
        
        public HL7Message(string sInMsg, int iMessageSyntax, string charset)
        {
    	    delimeters = null;
            sMessageType = null;
            sHL7Encoding = "";
            sXMLEncoding = "";
            xmlMessage = null;
            charSet = charset;
            characterSet = new HL7CharacterSets();
            message(sInMsg, iMessageSyntax);
        }

        /*public HL7Message(URL inURL, int iMessageSyntax)           
        {
            delimeters = null;
            sMessageType = null;
            sHL7Encoding = "";
            sXMLEncoding = "";
            xmlMessage = null;
            characterSet = new HL7CharacterSets();
            InputStream inStream = inURL.openStream();
            message(inStream, iMessageSyntax);
            inStream.close();
        }*/

        public HL7Message(Stream inStream, int iMessageSyntax)
           
        {
            delimeters = null;
            sMessageType = null;
            sHL7Encoding = "";
            sXMLEncoding = "";
            xmlMessage = null;
            characterSet = new HL7CharacterSets();
            message(inStream, iMessageSyntax);
        }
        
        public HL7Message(FileInfo file, int iMessageSyntax)
            
        {
            delimeters = null;
            sMessageType = null;
            sHL7Encoding = "";
            sXMLEncoding = "";
            xmlMessage = null;
            characterSet = new HL7CharacterSets();
            FileStream inFile = file.OpenRead();
            message(inFile, iMessageSyntax);
            inFile.Close();
        }
        
        private void message(Stream inStream, int iMessageSyntax)
           
        {
            StringBuilder sMessage = new StringBuilder();
            StreamReader inReader = new StreamReader(inStream);
            //BufferedReader reader = new BufferedReader(inReader);
            for(int c = 0; (c = inReader.Read()) > 0;)
                sMessage.Append((char)c);

            inReader.Close();
            message(sMessage.ToString(), iMessageSyntax);
        }

        private void message(string sInMsg, int iMessageSyntax)
           
        {
            switch(iMessageSyntax)
            {
            case 0: // '\0'
                initializeHL7(sInMsg);
                break;

            case 1: // '\001'
                //xmlMessage = loadXMLDocument(sInMsg);
                //string sHL7Message = convertToHL7(xmlMessage);
                string sHL7Message = convertToHL7(sInMsg);
                initializeHL7(sHL7Message);
                break;
            }
        }

        private void initializeHL7(string sHL7Message)
            
        {
            deMessage = new HL7DataElement(sHL7Message.Trim());
            try
            {
                HL7Segment messageHeader = getMessageHeader();
                HL7Field field = messageHeader.getField(MESSAGE_TYPE_POS);
                HL7FieldItem fieldItem = field.getFieldItem(1);
                HL7Component messageType = fieldItem.getComponent(1);
                string sMessageID = "";
                try
                {
                    HL7Component messageID = fieldItem.getComponent(2);
                    sMessageID = messageID.toString();
                }
                catch(ComponentNotFoundException componentnotfoundexception) { }
                if(sMessageID.Length > 0)
                    sMessageType = messageType.toString()+"_"+sMessageID;
                else
                    sMessageType = messageType.toString();
                try
                {
                    field = messageHeader.getField(MESSAGE_ENCODING_POS);
                    sHL7Encoding = field.toString();
                    sXMLEncoding = "UTF-8";//characterSet.getXML(sHL7Encoding);
                }
                catch(FieldNotFoundException e)
                {
                    //debug("Character set not found in HL7.  Using system defaults.");
                    sHL7Encoding = "";
                    sXMLEncoding = "UTF-8";
                }
            }
            catch(ComponentNotFoundException e)
            {
                throw new InvalidMessageTypeException(e.Message);
            }
            catch(FieldNotFoundException e)
            {
                throw new MessageTypeNotFoundException(e.Message);
            }
            catch(SegmentNotFoundException e)
            {
                throw new MessageHeaderNotFoundException(e.Message);
            }
        }

        private XmlDocument loadXMLDocument(string sXMLMessage)
            
        {
            XmlDocument xMsg = new XmlDocument();
            try
            {
                /*ByteArrayInputStream xmlInputStream = new ByteArrayInputStream(sXMLMessage.getBytes(charSet));
                SAXBuilder builder = new SAXBuilder();
                xMsg = builder.build(xmlInputStream);
                 * */
                xMsg.LoadXml(sXMLMessage);
            }
            catch(IOException e)
            {
                throw new HL7Exception(e.Message);
            }
            catch(Exception e)
            {
                throw new HL7Exception(e.Message);
            }
            return xMsg;
        }

        private string convertToHL7(string xMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xMsg);
            while (doc.FirstChild.NodeType != XmlNodeType.Element)
            {
                doc.RemoveChild(doc.FirstChild);
            }
            
            return convertToHL7(doc);
        }

        private string convertToHL7(XmlDocument xMsg)
            
        {
            String sHL7Message = "";
            XmlElement rootElement = (XmlElement)xMsg.FirstChild;
            XmlElement messageHeaderElement = (XmlElement)rootElement.GetElementsByTagName(MESSAGE_HEADER)[0];
            XmlElement fieldSepartorElement = (XmlElement)messageHeaderElement.GetElementsByTagName(FIELD_SERARATOR_FIELD_POS)[0];
            XmlElement encodingCharactersElement = (XmlElement)messageHeaderElement.GetElementsByTagName(ENCODING_CHARACTER_FIELD_POS)[0];
            string sDel = fieldSepartorElement.InnerText + encodingCharactersElement.InnerText;
            delimeters = new HL7Delimeters(sDel);
            XmlNodeList segmentList = rootElement.ChildNodes;
            int iSegmentTotal = segmentList.Count;
            for(int i = 0; i < iSegmentTotal; i++)
            {
                XmlElement segmentElement = (XmlElement)segmentList[i];
                HL7Segment segment = new HL7Segment(segmentElement, delimeters);
                if(segment.getLength() > 0)
                    sHL7Message = sHL7Message + segment.toString() + SEGMENT_DELIMETER;
            }

            return sHL7Message;
        }

        public string getType()
        {
            return sMessageType;
        }

        private HL7Segment getHL7Segment(int iSegmentIndex)
           
        {
            HL7Segment seg = null;
            try
            {
                String sSegment = deMessage.getString(SEGMENT_DELIMETER, iSegmentIndex);
                seg = new HL7Segment(sSegment, delimeters);
            }
            catch(UnableToGetStringException e)
            {
                throw new InvalidSegmentException(e.Message);
            }
            return seg;
        }

        private HL7Segment getMessageHeader()
            
        {
            string sHeader = "";
            try
            {
                sHeader = deMessage.getString(SEGMENT_DELIMETER, 1);
                if(sHeader.Length > 3)
                {
                    if(sHeader.Substring(0, MESSAGE_HEADER.Length).Equals(MESSAGE_HEADER,StringComparison.CurrentCultureIgnoreCase))
                    {
                        string sDel = sHeader.Substring(DELIMETERS_START, DELIMETERS_END-DELIMETERS_START);
                        delimeters = new HL7Delimeters(sDel);
                    } else
                    {
                        throw new SegmentNotFoundException("Can not find message header.");
                    }
                } else
                {
                    throw new InvalidSegmentException("Message segement name is not valid.");
                }
            }
            catch(UnableToGetStringException e)
            {
                throw new SegmentNotFoundException(e.Message);
            }
            catch(InvalidDelimetersException e)
            {
                throw new SegmentNotFoundException(e.Message);
            }
            return new HL7Segment(sHeader, delimeters);
        }

        private HL7Segment getHL7Segment(String sSegmentName, int iSegmentOccurence)
           
        {
            String sSegment = "";
            HL7Segment seg = null;
            bool bFound = false;
            int iSegmentCount = 1;
            if(iSegmentOccurence == 0)
                iSegmentOccurence = 1;
            try
            {
                int i = 1;
                do
                {
                    if(i > deMessage.getLength(SEGMENT_DELIMETER))
                        break;
                    sSegment = deMessage.getString(SEGMENT_DELIMETER, i);
                    seg = new HL7Segment(sSegment, delimeters);
                    if(sSegmentName.Equals(seg.getName()))
                    {
                        if(iSegmentCount == iSegmentOccurence)
                        {
                            bFound = true;
                            break;
                        }
                        iSegmentCount++;
                    }
                    i++;
                } while(true);
            }
            catch(InvalidSegmentException e)
            {
                throw new SegmentNotFoundException(e.Message);
            }
            catch(UnableToGetStringException e)
            {
                throw new SegmentNotFoundException(e.Message);
            }
            if(!bFound)
                throw new SegmentNotFoundException("");
            else
                return seg;
        }

        public string get(string sSegmentName, int iSegmentOccurence, int iFieldPos, int iFieldOccurence, int iComponentPos, int iSubComponentPos)
           
        {
            String sString = "";
            HL7Segment segment = getHL7Segment(sSegmentName, iSegmentOccurence);
            sString = segment.toString();
            if(iFieldPos > 0)
            {
                HL7Field field = segment.getField(iFieldPos);
                sString = field.toString();
                if(iFieldOccurence > 0)
                {
                    HL7FieldItem fieldItem = field.getFieldItem(iFieldOccurence);
                    sString = fieldItem.toString();
                    if(iComponentPos > 0)
                    {
                        HL7Component component = fieldItem.getComponent(iComponentPos);
                        sString = component.toString();
                        if(iSubComponentPos > 0)
                        {
                            HL7SubComponent subComponent = component.getSubComponent(iSubComponentPos);
                            sString = subComponent.toString();
                        }
                    }
                }
            }
            return sString;
        }

        public int getLength()
        {
            return deMessage.getLength(SEGMENT_DELIMETER);
        }

        private string getHL7Encoding()
        {
            return sHL7Encoding;
        }

        private string getXMLEncoding()
        {
            return sXMLEncoding;
        }

        private XmlDocument convertToXML()
            
        {
            xmlMessage = new XmlDocument();
            HL7Segment segment = new HL7Segment("", delimeters);
            XmlElement rootElement = xmlMessage.CreateElement(getType());
            for(int i = 1; i <= getLength(); i++)
            {
                segment = getHL7Segment(i);
                if (!segment.getName().Trim().Equals(""))
                segment.buildXML(xmlMessage,rootElement);
            }
            xmlMessage.AppendChild(rootElement);
            return xmlMessage;
        }

        public void outputHL7(Stream outStream)
            
        {
            /*
            PrintStream pStream = new PrintStream(outStream);
            pStream.println(get());
             */
            string str = get();
            outStream.SetLength(str.Length);
            outStream.Position = 0;
            StreamWriter sw = new StreamWriter(outStream);
            sw.Write(str);
            sw.Flush();

        }

        public void outputXML(Stream outStream)
            
        {
            string sXML = "";
            try
            {
                if (xmlMessage == null)
                {
                    convertToXML();
                }
                /*if(xmlMessage != null)
                {
                    String sIndent = "  ";
                    bool bNewLine = true;
                    Format f = Format.getPrettyFormat();
                    f.setLineSeparator(System.getProperty("line.separator"));
                    if(sXMLEncoding.trim().length() > 0)
                        f.setEncoding(sXMLEncoding);
                    XMLOutputter xOut = new XMLOutputter(f);
                    xOut.output(xmlMessage, outStream);
                }*/                
                outStream.SetLength(xmlMessage.InnerXml.Length);                
                StreamWriter sw = new StreamWriter(outStream);
                sw.Write(xmlMessage.InnerXml);
                sw.Flush();                

            }
            catch(HL7Exception hl7exception) { }
        }

        public string getXML()
        {
            string sXML = "";
            try
            {
                if (xmlMessage == null)
                {
                    convertToXML();
                }
                /*
                if(xmlMessage != null)
                {
                    String sIndent = "  ";
                    bool bNewLine = true;
                    Format f = Format.getPrettyFormat();
                    f.setLineSeparator(System.getProperty("line.separator"));
                    if(sXMLEncoding.trim().length() > 0)
                        f.setEncoding(sXMLEncoding);
                    XMLOutputter xOut = new XMLOutputter(f);
                    sXML = xOut.outputString(xmlMessage);
                }*/

                if (xmlMessage!=null)
                {
                    sXML = xmlMessage.InnerXml;
                }
            }
            catch(Exception exception) { }
            return sXML;
        }

        public string get()
        {
            return deMessage.toString();
        }

        public String getSegment(String sSegmentName, int iSegmentOccurence)
           
        {
            return get(sSegmentName, iSegmentOccurence, 0, 0, 0, 0);
        }

        public String getField(String sSegmentName, int iSegmentOccurence, int iField, int iFieldOccurence)
            
        {
            return get(sSegmentName, iSegmentOccurence, iField, iFieldOccurence, 0, 0);
        }

        public String getComponent(String sSegmentName, int iSegmentOccurence, int iField, int iFieldOccurence, int iComponent)
            
        {
            return get(sSegmentName, iSegmentOccurence, iField, iFieldOccurence, iComponent, 0);
        }

        public String getSubComponent(String sSegmentName, int iSegmentOccurence, int iField, int iFieldOccurence, int iComponent, int iSubComponent)
           
        {
            return get(sSegmentName, iSegmentOccurence, iField, iFieldOccurence, iComponent, iSubComponent);
        }

        private void debug(String sStr)
        {
            /*
            String sDateFormat = "yyyy-MM-dd hh:mm:ss:SSS";
            Date dCurrent = new Date(System.currentTimeMillis());
            SimpleDateFormat sdfApp = new SimpleDateFormat(sDateFormat);
            sdfApp.applyPattern(sDateFormat);
            String sLogDate = sdfApp.format(dCurrent);
            println(String.valueOf(String.valueOf((new StringBuffer(String.valueOf(String.valueOf(sLogDate)))).append("   ").append(sStr))));
             */
        }

        public static int HL7_SYNTAX = 0;
        public static int XML_SYNTAX = 1;
        private HL7DataElement deMessage;
        private HL7Delimeters delimeters;
        private string sMessageType;
        private string sHL7Encoding;
        private string sXMLEncoding;
        private XmlDocument xmlMessage;
        private HL7CharacterSets characterSet;
    }
}
