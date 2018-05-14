using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Common.Logging;

namespace HYS.IM.Common.HL7v2.Xml
{
    public static class XmlTransformerFactory
    {
        public static string[] TransformerRegistry = new string[]{
            NHL7ToolkitTransformer.DEVICE_NAME,
            //HL7ToolkitTransformer.DEVICE_NAME,
            //NHApiTransformer.DEVICE_NAME,
        };

        public static XmlTransformerBase CreateTransformer(string deviceName, ILog log)
        {
            try
            {
                switch (deviceName)
                {
                    default:
                    case NHL7ToolkitTransformer.DEVICE_NAME: return new NHL7ToolkitTransformer(log);
                    //case HL7ToolkitTransformer.DEVICE_NAME: return new HL7ToolkitTransformer(log);
                    //case NHApiTransformer.DEVICE_NAME: return new NHApiTransformer(log);
                }
            }
            catch (Exception e)
            {
                log.Write(e);
                return null;
            }
        }
    }
}
