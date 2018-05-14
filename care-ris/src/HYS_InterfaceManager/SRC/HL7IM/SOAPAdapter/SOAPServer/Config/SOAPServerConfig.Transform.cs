using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using System.IO;
using HYS.IM.Messaging.Base;
using System.Diagnostics;
using HYS.IM.Messaging.Mapping.Transforming;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config
{
    public partial class SOAPServerConfig : EntityConfigBase
    {
        //public const string XSLTFileName_XDSGatewayMessageToSOAPMessage = "XDSGW2SOAP.xslt";
        //public const string XSLTFileName_SOAPMessageToXDSGatewayMessage = "SOAP2XDSGW.xslt";
        public const string XSLTFileName_XDSGatewayMessageToSOAPMessage = "TransportTemplates\\XDSGW2SOAP.xslt";
        public const string XSLTFileName_SOAPMessageToXDSGatewayMessage = "TransportTemplates\\SOAP2XDSGW.xslt";

        internal string GetXSLTFileFullPath_XDSGatewayMessageToSOAPMessage()
        {
            if (Path.IsPathRooted(XSLTFileName_XDSGatewayMessageToSOAPMessage)) return XSLTFileName_XDSGatewayMessageToSOAPMessage;
            return GetFullPath(XSLTFileName_XDSGatewayMessageToSOAPMessage);
        }
        internal string GetXSLTFileFullPath_SOAPMessageToXDSGatewayMessage()
        {
            if (Path.IsPathRooted(XSLTFileName_SOAPMessageToXDSGatewayMessage)) return XSLTFileName_SOAPMessageToXDSGatewayMessage;
            return GetFullPath(XSLTFileName_SOAPMessageToXDSGatewayMessage);
        }

        //internal void WriteDefaultXSLTFile_XDSGatewayMessageToSOAPMessage()
        //{
        //    using (StreamWriter sw = File.CreateText(GetXSLTFileFullPath_XDSGatewayMessageToSOAPMessage()))
        //    {
        //        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
        //        sw.WriteLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns:myTrans=\"urn:xdsgw:XmlNodeTransformer\" exclude-result-prefixes=\"myTrans\">");
        //        sw.WriteLine(" <xsl:template match=\"/\">");
        //        sw.WriteLine("  <soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
        //        sw.WriteLine("   <soap:Body>");
        //        sw.WriteLine("    <MessageComResponse xmlns=\"http://www.carestreamhealth.com/\">");
        //        sw.WriteLine("      <MessageComResult>0</MessageComResult>");
        //        sw.WriteLine("      <ReturnMessage><xsl:value-of select=\"myTrans:GetEscapingInnerXml('/Message/Body','')\" disable-output-escaping=\"yes\"/></ReturnMessage>");
        //        sw.WriteLine("    </MessageComResponse>");
        //        sw.WriteLine("   </soap:Body>");
        //        sw.WriteLine("  </soap:Envelope>");
        //        sw.WriteLine(" </xsl:template>");
        //        sw.WriteLine("</xsl:stylesheet>");
        //    }
        //}
        //internal void WriteDefaultXSLTFile_SOAPMessageToXDSGatewayMessage()
        //{
        //    using (StreamWriter sw = File.CreateText(GetXSLTFileFullPath_SOAPMessageToXDSGatewayMessage()))
        //    {
        //        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
        //        sw.WriteLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns:myTrans=\"urn:xdsgw:XmlNodeTransformer\" exclude-result-prefixes=\"myTrans\">");
        //        sw.WriteLine("<xsl:template match=\"/\">");
        //        sw.WriteLine(" <Message>");
        //        sw.WriteLine("  <Header/>");
        //        sw.WriteLine("    <Body><xsl:value-of select=\"myTrans:GetDescapingInnerXml(");
        //        sw.WriteLine("    '/soap:Envelope/soap:Body/csh:MessageCom/csh:RequestMessage',");
        //        sw.WriteLine("    'soap|http://schemas.xmlsoap.org/soap/envelope/|csh|http://www.carestreamhealth.com/')\" ");
        //        sw.WriteLine("    disable-output-escaping=\"yes\"/>");
        //        sw.WriteLine("  </Body>");
        //        sw.WriteLine(" </Message>");
        //        sw.WriteLine("</xsl:template>");
        //        sw.WriteLine("</xsl:stylesheet>");
        //    }
        //}

        internal void OpenXSLTFile_XDSGatewayMessageToSOAPMessage()
        {
            string fname = GetXSLTFileFullPath_XDSGatewayMessageToSOAPMessage();
            Process proc = Process.Start("notepad.exe", "\"" + fname + "\"");
            proc.EnableRaisingEvents = false;
        }
        internal void OpenXSLTFile_SOAPMessageToXDSGatewayMessage()
        {
            string fname = GetXSLTFileFullPath_SOAPMessageToXDSGatewayMessage();
            Process proc = Process.Start("notepad.exe", "\"" + fname + "\"");
            proc.EnableRaisingEvents = false;
        }
    }
}
