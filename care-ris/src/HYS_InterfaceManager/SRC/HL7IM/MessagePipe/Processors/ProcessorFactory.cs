using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Processor;
using HYS.Common.Logging;
using HYS.MessageDevices.MessagePipe.Processors.Schema;
using HYS.MessageDevices.MessagePipe.Processors.XSLT;

namespace HYS.MessageDevices.MessagePipe.Processors
{
    public class ProcessorFactory
    {
        public static string[] ProcessorRegistry = new string[]{
            SchemaValidatorConfig.DEVICE_NAME,
            XSLTConfig.DEVICE_NAME,
        };

        public static IProcessor CreateProcessor(string deviceName, ILog log)
        {
            try
            {
                switch (deviceName)
                {
                    case SchemaValidatorConfig.DEVICE_NAME: return new SchemaValidatorImpl();
                    case XSLTConfig.DEVICE_NAME: return new XSLTImpl();
                }

                return null;
            }
            catch (Exception e)
            {
                if (log != null) log.Write(e);
                return null;
            }
        }

        public static IProcessorConfig CreateProcessorConfig(string deviceName, ILog log)
        {
            try
            {
                switch (deviceName)
                {
                    case SchemaValidatorConfig.DEVICE_NAME: return new SchemaValidatorImplCfg();
                    case XSLTConfig.DEVICE_NAME: return new XSLTImplCfg();
                }

                return null;
            }
            catch (Exception e)
            {
                if (log != null) log.Write(e);
                return null;
            }
        }
    }
}
