using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using System.IO;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Objects.RequestModel;
using System.Diagnostics;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Config
{
    public partial class SOAPClientConfig : EntityConfigBase
    {
        public const string SOAPClientWCFConfigFileName = "SOAPClientConfig.WCF.xml";
        public const string XSLTFileName_XDSGatewayMessageToSOAPMessage = "TransportTemplates\\XDSGW2SOAP.xslt";
        public const string XSLTFileName_SOAPMessageToXDSGatewayMessage = "TransportTemplates\\SOAP2XDSGW.xslt";

        internal ProgramContext _context;

        private string GetFullPath(string relativePath)
        {
            string fullPath = ConfigHelper.GetFullPath(Path.Combine(_context.AppArgument.ConfigFilePath, relativePath));
            return fullPath;
        }

        internal string GetWCFConfigFileNameWithFullPath()
        {
            return GetFullPath(SOAPClientWCFConfigFileName);
        }
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

        internal void WriteDefaultWCFConfigFile()
        {
            using (StreamWriter sw = File.CreateText(GetWCFConfigFileNameWithFullPath()))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sw.WriteLine("<configuration>");
                sw.WriteLine("  <system.serviceModel>");
                sw.WriteLine("    <extensions>");
                sw.WriteLine("      <bindingElementExtensions>");
                sw.WriteLine("        <add name=\"swaMessageEncoding\"");
                sw.WriteLine("          type=\"HYS.IM.Common.WCFHelper.SwA.SwaMessageEncodingElement, HYS.IM.Common.WCFHelper\" />");
                sw.WriteLine("      </bindingElementExtensions>");
                sw.WriteLine("    </extensions>");
                sw.WriteLine("    <bindings>");
                sw.WriteLine("      <customBinding>");
                sw.WriteLine("        <binding name=\"SwaBindingConfiguration\">");
                sw.WriteLine("          <swaMessageEncoding innerMessageEncoding=\"textMessageEncoding\" />");
                sw.WriteLine("          <httpTransport maxReceivedMessageSize=\"62914560\" authenticationScheme=\"Anonymous\"");
                sw.WriteLine("            maxBufferSize=\"62914560\" proxyAuthenticationScheme=\"Anonymous\"");
                sw.WriteLine("            useDefaultWebProxy=\"true\" />");
                sw.WriteLine("        </binding>");
                sw.WriteLine("      </customBinding>");
                sw.WriteLine("    </bindings>");
                sw.WriteLine("    <client>");
                sw.WriteLine("      <endpoint binding=\"customBinding\" bindingConfiguration=\"SwaBindingConfiguration\"");
                sw.WriteLine("            contract=\"HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Controler.IAbstractContract\" name=\"ABSTRACT_CLIENT_ENDPOINT\"/>");
                sw.WriteLine("    </client>");
                sw.WriteLine("  </system.serviceModel>");
                sw.WriteLine("</configuration>");
            }
        }
        internal void WriteDefaultXSLTFile_XDSGatewayMessageToSOAPMessage()
        {
            using (StreamWriter sw = File.CreateText(GetXSLTFileFullPath_XDSGatewayMessageToSOAPMessage()))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                sw.WriteLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns:myTrans=\"urn:xdsgw:XmlNodeTransformer\" exclude-result-prefixes=\"myTrans\">");
                sw.WriteLine(" <xsl:template match=\"/\">");
                sw.WriteLine("  <soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
                sw.WriteLine("   <soap:Body>");
                sw.WriteLine("    <MessageCom xmlns=\"http://www.carestreamhealth.com/\">");
                sw.WriteLine("      <RequestMessage><xsl:value-of select=\"myTrans:GetEscapingInnerXml('/Message/Body','')\" disable-output-escaping=\"yes\"/></RequestMessage>");
                sw.WriteLine("    </MessageCom>");
                sw.WriteLine("   </soap:Body>");
                sw.WriteLine("  </soap:Envelope>");
                sw.WriteLine(" </xsl:template>");
                sw.WriteLine("</xsl:stylesheet>");
            }
        }
        internal void WriteDefaultXSLTFile_SOAPMessageToXDSGatewayMessage()
        {
            using (StreamWriter sw = File.CreateText(GetXSLTFileFullPath_SOAPMessageToXDSGatewayMessage()))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                sw.WriteLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns:myTrans=\"urn:xdsgw:XmlNodeTransformer\" exclude-result-prefixes=\"myTrans\">");
                sw.WriteLine("<xsl:template match=\"/\">");
                sw.WriteLine(" <Message>");
                sw.WriteLine("  <Header/>");
                sw.WriteLine("    <Body><xsl:value-of select=\"myTrans:GetDescapingInnerXml(");
                sw.WriteLine("    '/soap:Envelope/soap:Body/csh:MessageComResponse/csh:ReturnMessage',");
                sw.WriteLine("    'soap|http://schemas.xmlsoap.org/soap/envelope/|csh|http://www.carestreamhealth.com/')\" ");
                sw.WriteLine("    disable-output-escaping=\"yes\"/>");
                sw.WriteLine("  </Body>");
                sw.WriteLine(" </Message>");
                sw.WriteLine("</xsl:template>");
                sw.WriteLine("</xsl:stylesheet>");
            }
        }

        internal void OpenWCFConfigFile()
        {
            string fname = GetWCFConfigFileNameWithFullPath();
            Process proc = Process.Start("notepad.exe", "\"" + fname + "\"");
            proc.EnableRaisingEvents = false;
        }
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
