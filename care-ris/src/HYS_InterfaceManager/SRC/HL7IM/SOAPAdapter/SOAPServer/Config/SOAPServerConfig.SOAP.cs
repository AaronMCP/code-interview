using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using System.IO;
using HYS.IM.Messaging.Base;
using System.Diagnostics;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config
{
    public partial class SOAPServerConfig : EntityConfigBase
    {
        public const string SOAPServerWCFConfigFileName = "SOAPServerConfig.WCF.xml";
        public const string SOAPErrorMessageFileName = "SOAPErrorMessage.xml";

        internal string GetWCFConfigFileNameWithFullPath()
        {
            return GetFullPath(SOAPServerWCFConfigFileName);
        }
        internal string GetSOAPErrorMessageFileFullPath()
        {
            if (Path.IsPathRooted(SOAPErrorMessageFileName)) return SOAPErrorMessageFileName;
            return GetFullPath(SOAPErrorMessageFileName);
        }

        internal void WriteDefaultWCFConfigFile()
        {
            using (StreamWriter sw = File.CreateText(GetWCFConfigFileNameWithFullPath()))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sw.WriteLine("<configuration>");
                sw.WriteLine("  <system.serviceModel>");
                sw.WriteLine("    <extensions>");
                sw.WriteLine("      <behaviorExtensions>");
                sw.WriteLine("        <add name=\"actionMappingEndpointBehavior\"");
                sw.WriteLine("             type=\"HYS.IM.Common.WCFHelper.Filter.ActionMappingEndpointBehaviorExtension, HYS.IM.Common.WCFHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\" />");
                sw.WriteLine("      </behaviorExtensions>");
                sw.WriteLine("    </extensions>");
                sw.WriteLine("    <bindings>");
                sw.WriteLine("      <basicHttpBinding>");
                sw.WriteLine("        <binding name=\"PIXServiceSoap\" closeTimeout=\"00:01:00\" openTimeout=\"00:01:00\"");
                sw.WriteLine("            receiveTimeout=\"00:10:00\" sendTimeout=\"00:01:00\" allowCookies=\"false\"");
                sw.WriteLine("            bypassProxyOnLocal=\"false\" hostNameComparisonMode=\"StrongWildcard\"");
                sw.WriteLine("            maxBufferSize=\"65536\" maxBufferPoolSize=\"524288\" maxReceivedMessageSize=\"65536\"");
                sw.WriteLine("            messageEncoding=\"Text\" textEncoding=\"utf-8\" transferMode=\"Buffered\"");
                sw.WriteLine("            useDefaultWebProxy=\"true\">");
                sw.WriteLine("          <readerQuotas maxDepth=\"32\" maxStringContentLength=\"8192\" maxArrayLength=\"16384\"");
                sw.WriteLine("              maxBytesPerRead=\"4096\" maxNameTableCharCount=\"16384\" />");
                sw.WriteLine("          <security mode=\"None\">");
                sw.WriteLine("            <transport clientCredentialType=\"None\" proxyCredentialType=\"None\"");
                sw.WriteLine("                realm=\"\" />");
                sw.WriteLine("            <message clientCredentialType=\"UserName\" algorithmSuite=\"Default\" />");
                sw.WriteLine("          </security>");
                sw.WriteLine("        </binding>");
                sw.WriteLine("      </basicHttpBinding>");
                sw.WriteLine("    </bindings>");
                sw.WriteLine("    <services>");
                sw.WriteLine("      <service behaviorConfiguration=\"metadataBehavior\"");
                sw.WriteLine("               name=\"HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler.AbstractService\">");
                sw.WriteLine("        <!--<host>");
                sw.WriteLine("          <baseAddresses>");
                sw.WriteLine("            <add baseAddress=\"http://localhost:8080/PIXService\"/>");
                sw.WriteLine("          </baseAddresses>");
                sw.WriteLine("        </host>-->");
                sw.WriteLine("        <endpoint binding=\"basicHttpBinding\" bindingConfiguration=\"PIXServiceSoap\"");
                sw.WriteLine("                  contract=\"HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler.IAbstractContract\"");
                sw.WriteLine("                  behaviorConfiguration=\"actionMappingBehavior\" />");
                sw.WriteLine("      </service>");
                sw.WriteLine("    </services>");
                sw.WriteLine("    <behaviors>");
                sw.WriteLine("      <serviceBehaviors>");
                sw.WriteLine("        <behavior name=\"metadataBehavior\">");
                sw.WriteLine("          <serviceMetadata httpGetEnabled=\"false\"/>");
                sw.WriteLine("        </behavior>");
                sw.WriteLine("      </serviceBehaviors>");
                sw.WriteLine("      <endpointBehaviors>");
                sw.WriteLine("        <behavior name=\"actionMappingBehavior\">");
                sw.WriteLine("          <actionMappingEndpointBehavior fromAction=\"*\" toAction=\"ProcessMessage\" />");
                sw.WriteLine("        </behavior>");
                sw.WriteLine("      </endpointBehaviors>");
                sw.WriteLine("    </behaviors>");
                sw.WriteLine("  </system.serviceModel>");
                sw.WriteLine("</configuration>");
            }
        }
        internal void WriteDefaultSOAPErrorMessageFile()
        {
            using (StreamWriter sw = File.CreateText(GetSOAPErrorMessageFileFullPath()))
            {
                sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                sw.Write("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
                sw.Write("  <soap:Header>");
                sw.Write("    <soap12:Upgrade xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">");
                sw.Write("      <soap12:SupportedEnvelope qname=\"soap:Envelope\"></soap12:SupportedEnvelope>");
                sw.Write("      <soap12:SupportedEnvelope qname=\"soap12:Envelope\"></soap12:SupportedEnvelope>");
                sw.Write("    </soap12:Upgrade>");
                sw.Write("  </soap:Header>");
                sw.Write("  <soap:Body>");
                sw.Write("    <soap:Fault>");
                sw.Write("      <faultcode>soap:Server</faultcode>");
                sw.Write("      <faultstring>Cannot process this SOAP message in current application configuraiton.</faultstring>");
                sw.Write("      <detail/>");
                sw.Write("    </soap:Fault>");
                sw.Write("  </soap:Body>");
                sw.Write("</soap:Envelope>");
            }
        }

        internal void OpenWCFConfigFile()
        {
            string fname = GetWCFConfigFileNameWithFullPath();
            Process proc = Process.Start("notepad.exe", "\"" + fname + "\"");
            proc.EnableRaisingEvents = false;
        }

        private string _soapErrorMessageContent;
        internal string GetSOAPErrorMessageContent()
        {
            if (_soapErrorMessageContent == null)
            {
                try
                {
                    _soapErrorMessageContent = File.ReadAllText(GetSOAPErrorMessageFileFullPath());
                }
                catch (Exception e)
                {
                    _context.Log.Write(e);
                }
            }
            return _soapErrorMessageContent;
        }
    }
}
