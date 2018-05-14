using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Adapter.Service.Services;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Service
{
    static class Program
    {
        public static AdapterAgent<IOutboundAdapterService, AdapterServiceEntryAttribute> OutAdapter;
        public static AdapterAgent<IInboundAdapterService, AdapterServiceEntryAttribute> InAdapter;
        public static AdapterAgent<IBidirectionalAdapterService, AdapterServiceEntryAttribute> BiAdapter;
        public const string AppName = "Adapter Windows Service";
        public static AdapterServiceCfgMgt ConfigMgt;
        public static DeviceDirManager DeviceMgt;
        public static Logging Log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // initialize logging
            Log = new Logging(Application.StartupPath + "\\AdapterService.log");
            LoggingHelper.EnableApplicationLogging(Log);
            LoggingHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);

            // load DeviceDir
            DeviceMgt = new DeviceDirManager();
            DeviceMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
            if (!DeviceMgt.LoadDeviceDir())
            {
                Log.Write(LogType.Error, "Load DeviceDir failed. " + ConfigMgt.FileName);
                goto exit;
            }

            // initialize config
            ConfigMgt = new AdapterServiceCfgMgt();
            ConfigMgt.FileName = Application.StartupPath + "\\" + ConfigMgt.FileName;
            if (ConfigMgt.Load())
            {
                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
            }
            else
            {
                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                goto exit;
            }

            // log config parameters
            string serviceName = ConfigMgt.Config.ServiceName;
            string adapterFileName = ConfigMgt.Config.AdapterFileName;
            DirectionType adapterDirection = ConfigMgt.Config.AdapterDirection;

            Log.Write("Service name: " + serviceName, false);
            Log.Write("Adapter filename: " + adapterFileName, false);
            Log.Write("Adapter direction: " + adapterDirection, false);
            //Log.Write("GWConfigDB connection: " + ConfigMgt.Config.ConfigDBConnection, false);    //contains db pw
            //Log.Write("GWDataDB connection: " + ConfigMgt.Config.DataDBConnection, false);    //contains db pw
            Log.Write("Dump data: " + ConfigMgt.Config.DumpData.ToString(), false);

            // run service
            switch (adapterDirection)
            {
                case DirectionType.INBOUND:
                    {
                        // initialize inbound adapter agent
                        InAdapter = new AdapterAgent<IInboundAdapterService, AdapterServiceEntryAttribute>(adapterFileName, Log);

                        ServiceBase[] ServicesToRun = new ServiceBase[] { new InboundService() };
                        ServiceBase.Run(ServicesToRun);
                        break;
                    }
                case DirectionType.OUTBOUND:
                    {
                        // initialize outbound adapter agent
                        OutAdapter = new AdapterAgent<IOutboundAdapterService, AdapterServiceEntryAttribute>(adapterFileName, Log);

                        ServiceBase[] ServicesToRun = new ServiceBase[] { new OutboundService() };
                        ServiceBase.Run(ServicesToRun);
                        break;
                    }
                case DirectionType.BIDIRECTIONAL:
                    {
                        // initialize bidiretional adapter agent
                        BiAdapter = new AdapterAgent<IBidirectionalAdapterService, AdapterServiceEntryAttribute>(adapterFileName, Log);

                        ServiceBase[] ServicesToRun = new ServiceBase[] { new BidirectionalService() };
                        ServiceBase.Run(ServicesToRun);
                        break;
                    }
            }

        exit:
            // exit
            Log.WriteAppExit(AppName);
        }
    }
}