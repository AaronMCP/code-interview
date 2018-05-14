using System;
using HYS.Adapter.Base;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.StorageServer.Forms;
using HYS.DicomAdapter.StorageServer.Objects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;

namespace HYS.DicomAdapter.StorageServer.Adapter
{
    [AdapterConfigEntry("DICOM Storage Adapter Configuration", DirectionType.INBOUND, "Storage adapter for HYS 1.0.0.0")]
    public class ConfigMain : IInboundAdapterConfig
    {
        #region IInboundAdapterConfig Members

        public IInboundRule[] GetRules()
        {
            DicomMappingHelper.CleanQRMappingList
                <StorageItem>(Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList);

            AddEventTypeItem(Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList, GWEventType.PACSImageArrival.Code);  //14

            return new IInboundRule[] { Program.ConfigMgt.Config.StorageRule };
        }

        private static void AddEventTypeItem(XCollection<StorageItem> itemList, string value)
        {
            if (itemList == null) return;
            StorageItem item = new StorageItem();
            item.GWDataDBField = GWDataDBField.i_EventType.Clone();
            item.Translating.Type = TranslatingType.FixValue;
            item.Translating.ConstValue = value;
            itemList.Add(item);
        }

        #endregion

        #region IAdapterConfig Members

        public string GetConfigurationSummary()
        {
            return Program.ConfigMgt.Config.SCPConfig.GetSummary();
        }

        public IConfigUI[] GetConfigUI()
        {
            //return new IConfigUI[] { new FormSCP(Program.ConfigMgt, Program.Log), new FormStorage(), new FormMapping() };
            return Program.ConfigMgt.Config.SOAPEnable ?
                (new IConfigUI[] {new FormSOAPConfig(), new FormSCP(Program.ConfigMgt, Program.Log), new FormStorage() }) :
                (new IConfigUI[] { new FormSCP(Program.ConfigMgt, Program.Log), new FormStorage(), new FormMapping() });

        }

        public LookupTable[] GetLookupTables()
        {
            return null;
        }

        public AdapterOption Option
        {
            get 
            {
                AdapterOption adp = new AdapterOption(DirectionType.INBOUND);

                if (Program.ConfigMgt.Config.SOAPEnable)
                {
                    adp.EnableCombination = false;
                    adp.EnableDataProcess = false;
                    adp.EnableGarbageCollection = false;
                    adp.EnableLogConfig = true;
                }

                return adp;
            }
        }

        #endregion

        #region IAdapterBase Members

        public bool Exit(string[] arguments)
        {
            Program.BeforeExit();
            return true;
        }

        public bool Initialize(string[] arguments)
        {
            Program.PreLoading();
            Program.Log.Write(Program.AppName + " is running in Adapter.Config host.");

            if (arguments != null && arguments.Length > 0)
            {
                string str = arguments[0];
                //Program.Log.Write("GWDataDB connection: " + str);     //contains db pw
                Program.ConfigMgt.Config.GWDataDBConnection = str;
            }

            return true;
        }

        public Exception LastError
        {
            get { return null; }
        }

        #endregion
    }
}
