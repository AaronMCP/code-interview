using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Device;
using HYS.Adapter.Base;
using DemoAdapter.Configuration;
using DemoAdapter.Controlers;
using HYS.Common.Objects.Rule;

namespace DemoAdapter
{
    [AdapterConfigEntry("Demo_Adapter", "Demo Adapter for GC Gateway")]
    [AdapterServiceEntry("Demo_Adapter", "Demo Adapter for GC Gateway")]
    public class AdapterMain : IInboundAdapterService, IInboundAdapterConfig
    {
        private DeviceDir directory;
        private DemoControler controler;

        #region IInBoundAdapter Members

        public bool Initialize(string[] arguments)
        {
            controler = new DemoControler();
            return true;
        }

        public bool Exit(string[] arguments)
        {
            if (controler.IsRunning) controler.Stop();
            return true;
        }

        public AdapterStatus Status
        {
            get
            {
                if (controler.IsRunning) return AdapterStatus.Running;
                return AdapterStatus.Stopped;
            }
        }

        public AdapterOption Option
        {
            get
            {
                AdapterOption option = new AdapterOption();
                option.EnableCombination = true;
                option.EnableDataProcess = true;
                option.EnableGarbageCollection = true;
                return option;
            }
        }

        public bool Start(string[] arguments)
        {
            controler.Start();
            return true;
        }

        public bool Stop(string[] arguments)
        {
            controler.Stop();
            return true;
        }

        public event DataReceiveEventHandler OnDataReceived;

        public IConfigUI[] GetConfigUI()
        {
            return new IConfigUI[] { new FormConfigAgent() };
        }

        public DeviceDir GetInfor()
        {
            if (directory == null)
            {
                DeviceDirManager dirMgt = new DeviceDirManager();
                dirMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
                if( dirMgt.LoadDeviceDir()) directory = dirMgt.DeviceDirInfor;
            }

            return directory;
        }

        public Exception LastError
        {
            get
            {
                return null;
            }
        }

        public string GetConfigurationSummary()
        {
            return "";
        }

        #endregion

        #region IInboundAdapterConfig Members

        public HYS.Common.Objects.Rule.IInboundRule[] GetRules()
        {
            //MappingItem item = new MappingItem();
            //item.SourceField = "3paryCode";
            //item.TargetField = "EventType";
            //item.Translating.LutName = table1.TableName;
            //item.Translating.Type = TranslatingType.LookUpTable;

            //MappingItem item = new MappingItem();
            //item.SourceField = "3paryCode";
            //item.TargetField = "ExamStaus";
            //item.Translating.LutName = table2.TableName;
            //item.Translating.Type = TranslatingType.LookUpTable;

            return null;
        }

        public HYS.Common.Objects.Rule.LookupTable[] GetLookupTables()
        {
            List<LookupTable> list = new List<LookupTable>();
            
            LookupTable table1 = new LookupTable();
            table1.DisplayName = "3partyCode_2_EventType";
            table1.Table.Add(new LookupItem("8", "18"));
            table1.Table.Add(new LookupItem("7", "19"));
            list.Add(table1);

            LookupTable table2 = new LookupTable();
            table2.DisplayName = "3partyCode_2_ExamStaus";
            table2.Table.Add(new LookupItem("8", "18"));
            table2.Table.Add(new LookupItem("7", "19"));
            list.Add(table2);

            return list.ToArray();
        }

        #endregion
    }
}
