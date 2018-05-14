using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Diagnostics;
using System.Net;
using System.ServiceProcess;
using System.IO;
using System.Data;
using System.Xml;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Management.Scripts;

namespace HYS.IM.IPMonitor.Policies
{
    class Helper
    {
        private static object locker = new object();
        private static ManagementClass _objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
        private static ManagementClass _objMC2 = new ManagementClass("Win32_NetworkAdapter");
        private static Dictionary<string, string> _serviceValidations = new Dictionary<string, string>();

        public Helper()
        {
        }

        [DllImport("sensapi.dll")]
        public static extern bool IsNetworkAlive(out int flags);

        public static bool IsAdapterConnected(string AdapterName)
        {
            try
            {
                using (ManagementObjectCollection queryCollection = _objMC2.GetInstances())
                {
                    foreach (ManagementObject m in queryCollection)
                    {
                        StringBuilder sb = new StringBuilder();
                        if (m["Caption"].ToString().Trim() == AdapterName)
                        {
                            if (m["NetConnectionStatus"].ToString() == "2")
                            {
                                return true;
                            }
                            else
                            {
                                string status = "";
                                switch (m["NetConnectionStatus"].ToString())
                                {
                                    case "0":
                                        status = "Disconnected";
                                        break;
                                    case "1":
                                        status = "Connecting";
                                        break;
                                    case "2":
                                        status = "Connected";
                                        break;
                                    case "3":
                                        status = "Disconnecting";
                                        break;
                                    case "4":
                                        status = "Hardware not present";
                                        break;
                                    case "5":
                                        status = "Hardware disabled";
                                        break;
                                    case "6":
                                        status = "Hardware malfunction";
                                        break;
                                    case "7":
                                        status = "Media disconnected";
                                        break;
                                    case "8":
                                        status = "Authenticating";
                                        break;
                                    case "9":
                                        status = "Authentication succeeded";
                                        break;
                                    case "10":
                                        status = "Authentication failed";
                                        break;
                                    case "11":
                                        status = "Invalid address";
                                        break;
                                    case "12":
                                        status = "Credentials required ";
                                        break;
                                    default:
                                        status = "Default";
                                        break;
                                }
                                Program.Log.Write(LogType.Error, "the status of current adapter network is " + status);
                            }
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
                return false;
            }
        }

        public static bool IsCurrentIP(string AdapterName, string IP)
        {
            try
            {
                using (ManagementObjectCollection queryCollection = _objMC.GetInstances())
                {
                    foreach (ManagementObject m in queryCollection)
                    {
                        if (!m["Caption"].Equals(AdapterName))
                        {
                            continue;
                        }
                        if (m["IPAddress"] != null)
                        {
                            string[] arr = (string[])m["IPAddress"];
                            if (arr != null)
                            {
                                foreach (string item in arr)
                                {
                                    if (item.Trim() == IP.Trim())
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
                return false;
            }
        }

        public static bool IsServiceRunning(string ServiceName)
        {
            try
            {
                using (ServiceController sc = new ServiceController(ServiceName))
                {
                    if (sc != null)
                    {
                        return sc.Status == ServiceControllerStatus.Running;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
                return false;
            }
        }

        public static bool HasServicesValidated()
        {
            Dictionary<string, Dictionary<string, string>> validations = new Dictionary<string, Dictionary<string, string>>();
            if (!Helper.LoadConfiguration(validations))
            {
                Program.Log.Write(LogType.Error, string.Format("Loading service validation Configuration failed!" ));
                return false;
            }
            try
            {
                foreach(string service in validations.Keys)
                {
                    foreach (string validation in validations[service].Keys)
                    {
                        Type type = Type.GetType(validations[service][validation]);
                        if (type == null)
                        {
                            Program.Log.Write(LogType.Error, string.Format("the Type ({0}) does not exists---({1}-{2})!", validations[service][validation], validation, service));
                            return false;
                        }
                        object obj = Activator.CreateInstance(type);
                        if (obj == null)
                        {
                            Program.Log.Write(LogType.Error, string.Format("the Type ({0}) does not exists---({1}-{2})!", validations[service][validation], validation, service));
                            return false;
                        }
                        else if(!(obj is IVerifitable))
                        {
                            Program.Log.Write(LogType.Error, string.Format("the Type ({0}) is not a proper validation(inherit from IVerifitable)---({1}-{2})!", validations[service][validation], validation, service));
                            return false;
                        }
                        else
                        {
                            if (!(obj as IVerifitable).Validation(service))
                            {
                                Program.Log.Write(LogType.Error, string.Format("the validation ({0}-{1}) failed!",validation, service));
                                return false;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
                return false;
            }
            return true;
        }

        public static bool SetIP(string AdapterName, string ip_address, string subnet_mask, string Gateway, string DnsSearchOrder)
        {
            Program.Log.Write(LogType.Debug, "start to set IP:" + ip_address);
            lock (locker)
            {
                try
                {
                   using( ManagementObjectCollection objMOC = _objMC.GetInstances())
                    {
                        foreach (ManagementObject objMO in objMOC)
                        {
                            if (objMO["Caption"].Equals(AdapterName))
                            {
                                using (ManagementBaseObject newIP = objMO.GetMethodParameters("EnableStatic"),
                                 newGate = objMO.GetMethodParameters("SetGateways"),
                                 newDNS = objMO.GetMethodParameters("SetDNSServerSearchOrder"))
                                {
                                    newIP["IPAddress"] = new string[] { ip_address };
                                    newIP["SubnetMask"] = new string[] { subnet_mask };

                                    using (ManagementObjectCollection queryCollection = _objMC.GetInstances())
                                    {
                                        if (Gateway.Trim() == "")
                                        {
                                            bool b = true;
                                            foreach (ManagementObject m in queryCollection)
                                            {
                                                StringBuilder sb = new StringBuilder();
                                                if (m["Caption"].Equals(AdapterName))
                                                {
                                                    newGate["DefaultIPGateway"] = (string[])m["DefaultIPGateway"];
                                                    b = false;
                                                    break;
                                                }
                                            }
                                            if (b)
                                            {
                                                Program.Log.Write(LogType.Information, "Please check if 'Network Adapter Name' is configured properly and the cable is plugged");
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            newGate["DefaultIPGateway"] = new string[] { Gateway };
                                        }


                                        if (DnsSearchOrder.Trim() == "")
                                        {
                                            bool b = true;
                                            foreach (ManagementObject m in queryCollection)
                                            {
                                                StringBuilder sb = new StringBuilder();
                                                if (m["Caption"].Equals(AdapterName))
                                                {
                                                    newDNS["DNSServerSearchOrder"] = (string[])m["DNSServerSearchOrder"];
                                                    b = false;
                                                    break;
                                                }
                                            }
                                            if (b)
                                            {
                                                Program.Log.Write(LogType.Information, "Please check if 'Network Adapter Name'(" + AdapterName + ") is configured properly and the cable is plugged-in");
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            newDNS["DNSServerSearchOrder"] = new string[] { DnsSearchOrder };
                                        }
                                    }

                                    ManagementBaseObject setIP = objMO.InvokeMethod("EnableStatic", newIP, null);
                                    if (setIP["ReturnValue"].ToString().Trim() != "0")
                                    {
                                        Program.Log.Write(LogType.Information, "failed in resetting IP:" + ip_address + "(Error code:" + setIP["ReturnValue"].ToString() + "), Please check if 'Network Adapter Name'(" + AdapterName + ") is configured properly and the cable is plugged-in");
                                    }
                                    else
                                    {
                                        Program.Log.Write(LogType.Information, "succeed in resetting IP:" + ip_address);
                                    }

                                    ManagementBaseObject setGateways = objMO.InvokeMethod("SetGateways", newGate, null);
                                    if (setGateways["ReturnValue"].ToString().Trim() != "0")
                                    {
                                        Program.Log.Write(LogType.Information, "failed in resetting Gateway(Error code:" + setGateways["ReturnValue"].ToString() + "), Please check if 'Network Adapter Name'(" + AdapterName + ") is configured properly and the cable is plugged-in");
                                    }
                                    else
                                    {
                                        Program.Log.Write(LogType.Information, "succeed in resetting Gateway!");
                                    }

                                    ManagementBaseObject setDNS = objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                                    if (setDNS["ReturnValue"].ToString().Trim() != "0")
                                    {
                                        Program.Log.Write(LogType.Information, "failed in resetting DNS(Error code:" + setDNS["ReturnValue"].ToString() + "), Please check if 'Network Adapter Name'(" + AdapterName + ") is configured properly and the cable is plugged-in");
                                    }
                                    else
                                    {
                                        Program.Log.Write(LogType.Information, "succeed in resetting DNS!");
                                    }
                                }
                                return true;
                            }
                        }
                    Program.Log.Write("There is no IP Enabled devices or" + AdapterName + "not existed");
                    return false;
                    }
                }
                catch (Exception ex)
                {
                    Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
                    return false;
                }
            }
        }

        public static List<string> GetAdapterNames()
        {
            List<string> adapterNames = new List<string>();
            try
            {
                ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\cimv2");
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration where IPEnabled = True");
                ManagementObjectCollection queryCollection;
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    queryCollection = searcher.Get();
                }
                using (queryCollection)
                {
                    foreach (ManagementObject m in queryCollection)
                    {
                        if (m["IPAddress"] == null)
                        {
                            continue;
                        }

                        foreach (var p in m.Properties)
                        {
                            if (p.Name == "Caption")
                            {
                                adapterNames.Add(p.Value.ToString().Trim());
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
            }
            return adapterNames;
        }

        public static Adapter GetAdapterInfo(string AdapterName)
        {
            Adapter adapter = new Adapter();
            try
            {
                adapter.Name = AdapterName;
                using (ManagementObjectCollection queryCollection = _objMC.GetInstances())
                {
                    foreach (ManagementObject m in queryCollection)
                    {
                        if (!m["Caption"].Equals(AdapterName))
                        {
                            continue;
                        }
                        if (m["IPAddress"] != null)
                        {
                            string[] arr = (string[])m["IPAddress"];
                            if (arr != null)
                            {
                                foreach (string item in arr)
                                {
                                    if (item.Trim() != "")
                                    {
                                        adapter.IPAdress = item;
                                        break;
                                    }
                                }
                            }
                        }

                        if (m["IPSubnet"] != null)
                        {
                            string[] arr = (string[])m["IPSubnet"];
                            if (arr != null)
                            {
                                foreach (string item in arr)
                                {
                                    if (item.Trim() != "")
                                    {
                                        adapter.Subnet = item;
                                        break;
                                    }
                                }
                            }
                        }

                        if (m["DefaultIPGateway"] != null)
                        {
                            string[] arr = (string[])m["DefaultIPGateway"];
                            if (arr != null)
                            {
                                foreach (string item in arr)
                                {
                                    if (item.Trim() != "")
                                    {
                                        adapter.Gateway = item;
                                        break;
                                    }
                                }
                            }
                        }

                        if (m["DNSServerSearchOrder"] != null)
                        {
                            string[] arr = (string[])m["DNSServerSearchOrder"];
                            if (arr != null)
                            {
                                foreach (string item in arr)
                                {
                                    if (item.Trim() != "")
                                    {
                                        adapter.DNS = item;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message);
                return adapter;
            }
            return adapter;
        }

        public static DataTable GetServiceList()
        {
            DataTable list = new DataTable();
            list.Columns.Add("Display");
            list.Columns.Add("Value");
            try
            {
                ServiceController[] services = ServiceController.GetServices();
                foreach (ServiceController item in services)
                {
                        list.Rows.Add(item.DisplayName + "  (# " + item.ServiceName + " #)", item.ServiceName);
                }
                list.DefaultView.Sort = "Display";
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
            }
            return list;
        }

        public static bool PingIP(string IP)
        {
            Program.Log.Write(LogType.Debug, "start to ping IP:" + IP);
            Ping ping = new Ping();
            try
            {
                PingReply reply;
                reply = ping.Send(IP, Program.ConfigMgt.Config.PolicyIP.TimeOutInS4Ping * 1000);

                if (reply != null && reply.Status == IPStatus.Success)
                {
                    Program.Log.Write(LogType.Information, "Ping:" + IP + " Reply:" + PingReplyToString(reply) + " Waiting for Public IP Released");
                    return true;
                }
                else if (reply != null)
                {
                    Program.Log.Write(LogType.Information, "Ping:" + IP + " Reply: failed Reason:" + PingReplyToString(reply));
                    return false;
                }
                else
                {
                    Program.Log.Write(LogType.Information, "Ping:" + IP + " Reply: failed Reason: RETURN NULL");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
            }
            finally
            {
                ping.Dispose();
            }
            return false;
        }

        public static void StopFlagService()
        {
            try
            {
                using (ServiceController sc = new ServiceController())
                {

                    if (sc != null)
                    {
                        sc.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
            }
        }

        public static string ValidationsToString(Dictionary<string, List<string>> validations)
        {
            string strXml = @"<Services/>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);
            XmlNode root = doc.SelectSingleNode("//Services");

            foreach (string key in validations.Keys)
            {
                XmlElement xe = doc.CreateElement("Service");
                xe.SetAttribute("Name", key);

                if (validations[key] != null)
                {
                    foreach (string item in validations[key])
                    {
                        XmlElement xeChild = doc.CreateElement("Validation");
                        xeChild.SetAttribute("Name", item);
                        xe.AppendChild(xeChild);
                    }
                }
                root.AppendChild(xe);
            }

            return doc.InnerXml.ToString();
        }

        public static void StringToValidations(Dictionary<string, List<string>> validations, string XML)
        {
            if (XML == null || XML == "") return;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XML);

            XmlNodeList nodes = doc.SelectNodes("//Service");
            List<string> list;
            foreach (XmlNode node in nodes)
            {
                list = new List<string>();
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    list.Add(childnode.Attributes["Name"].Value);
                }
                validations.Add(node.Attributes["Name"].Value, list);
            }
        }

        public static bool LoadConfiguration(Dictionary<string, Dictionary<string,string>> validations)
        {
            try
            {
                Dictionary<string, List<string>> temp = new Dictionary<string, List<string>>();
                Helper.StringToValidations(temp, Program.ConfigMgt.Config.PolicyFlag.ServicesWithValidations);

                try
                {
                    if (_serviceValidations.Count <= 0)
                    {
                        XmlDocument validationConfig = new XmlDocument();
                        XmlReaderSettings settings = new XmlReaderSettings();
                        settings.IgnoreComments = true;
                        XmlReader reader = XmlReader.Create(Path.Combine(Program.ConfigPath, MonitorConfig.ValidationConfigName), settings);
                        Program.Log.Write(Path.Combine(Program.ConfigPath, MonitorConfig.ValidationConfigName));
                        validationConfig.Load(reader);

                        XmlNodeList nodes = validationConfig.SelectNodes("//validation");
                        foreach (XmlNode node in nodes)
                        {
                            if (!_serviceValidations.ContainsKey(node.ChildNodes[0].InnerText))
                            {
                                _serviceValidations.Add(node.ChildNodes[0].InnerText, node.ChildNodes[2].InnerText);
                            }
                            else
                            {
                                Program.Log.Write(LogType.Error, "Please Make sure the name is unique in ServiceValidations.xml file");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Please check if each node in ServiceValidations.xml has proper structure!" + ex.Message);
                }

                foreach (string service in temp.Keys)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach(string item in temp[service])
                    {
                        if (_serviceValidations.ContainsKey(item))
                        {
                            dic.Add(item, _serviceValidations[item]);
                        }
                        else
                        {
                            Program.Log.Write(LogType.Error, "There is no validation named " + item);
                        }
                    }
                    validations.Add(service, dic);
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
                return false;
            }
            return true;
        }

        private static string PingReplyToString(PingReply reply)
        {
            string str = "";
            if (reply != null)
            {
                switch (reply.Status)
                {
                    case IPStatus.Success:
                        str = "Success";
                        break;
                    case IPStatus.BadDestination:
                        str = "BadDestination";
                        break;
                    case IPStatus.BadHeader:
                        str = "BadHeader";
                        break;
                    case IPStatus.BadOption:
                        str = "BadOption";
                        break;
                    case IPStatus.BadRoute:
                        str = "BadRoute";
                        break;
                    case IPStatus.DestinationHostUnreachable:
                        str = "DestinationHostUnreachable";
                        break;
                    case IPStatus.DestinationNetworkUnreachable:
                        str = "DestinationNetworkUnreachable";
                        break;
                    case IPStatus.DestinationPortUnreachable:
                        str = "DestinationPortUnreachable";
                        break;
                    case IPStatus.DestinationProtocolUnreachable:
                        str = "DestinationProtocolUnreachable";
                        break;
                    case IPStatus.DestinationScopeMismatch:
                        str = "DestinationScopeMismatch";
                        break;
                    case IPStatus.DestinationUnreachable:
                        str = "DestinationUnreachable";
                        break;
                    case IPStatus.HardwareError:
                        str = "HardwareError";
                        break;
                    case IPStatus.IcmpError:
                        str = "IcmpError";
                        break;
                    case IPStatus.NoResources:
                        str = "NoResources";
                        break;
                    case IPStatus.PacketTooBig:
                        str = "PacketTooBig";
                        break;
                    case IPStatus.ParameterProblem:
                        str = "ParameterProblem";
                        break;
                    case IPStatus.SourceQuench:
                        str = "SourceQuench";
                        break;
                    case IPStatus.TimedOut:
                        str = "TimedOut";
                        break;
                    case IPStatus.TimeExceeded:
                        str = "TimeExceeded";
                        break;
                    case IPStatus.TtlExpired:
                        str = "TtlExpired";
                        break;
                    case IPStatus.TtlReassemblyTimeExceeded:
                        str = "TtlReassemblyTimeExceeded";
                        break;
                    case IPStatus.UnrecognizedNextHeader:
                        str = "UnrecognizedNextHeader";
                        break;
                    default:
                        str = "Unknow";
                        break;
                }
            }

            return str;
        }

        public struct Adapter
        {
            public string Name;
            public string IPAdress;
            public string Subnet;
            public string Gateway;
            public string DNS; 
        }
        
    }
}
