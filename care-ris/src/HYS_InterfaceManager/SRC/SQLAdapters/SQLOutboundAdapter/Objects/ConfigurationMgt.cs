using System;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.SQLOutboundAdapterObjects;


namespace SQLOutboundAdapter.Objects
{
//tz no sense
    //public class ConfigurationMgt
    //{
    //    public static string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
    //    public static string FileName = "SQLOutboundAdapter.xml";

    //    public static DynamicLibrary.SQLOutAdapterConfig Config = new Configuration();
    //    public static Exception LastError;

    //    public static bool Load()
    //    {
    //        try
    //        {
    //            using (StreamReader sr = File.OpenText(FileName))
    //            {
    //                string strXml = sr.ReadToEnd();
    //                Config = XObjectManager.CreateObject(strXml, typeof(Configuration)) as Configuration;
    //                return (Config != null);
    //            }
    //        }
    //        catch (Exception err)
    //        {
    //            LastError = err;
    //            return false;
    //        }
    //    }
    //    public static bool Save()
    //    {
    //        try
    //        {
    //            if (Config == null) return false;
    //            using (StreamWriter sw = File.CreateText(FileName))
    //            {
    //                string strXml = XMLHeader + Config.ToXMLString();
    //                sw.Write(strXml);
    //                return true;
    //            }
    //        }
    //        catch (Exception err)
    //        {
    //            LastError = err;
    //            return false;
    //        }
    //    }
    //}
}
