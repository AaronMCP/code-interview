using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.StorageServer.Dicom;
using HYS.DicomAdapter.StorageServer.Forms;
using HYS.DicomAdapter.StorageServer.Objects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Dicom;
using HYS.Common.Xml;
using HYS.DicomAdapter.MWLServer.Dicom;

namespace HYS.DicomAdapter.StorageServer.Adapter
{
    [AdapterServiceEntry("DICOM Storage Adapter Service", DirectionType.INBOUND, "Storage adapter for GC Gateway 2.0")]
    public class ServiceMain : IInboundAdapterService
    {
        private StorageSCPService _storageSCP;
        internal XCollection<StorageItem> StorageList = new XCollection<StorageItem>();

        #region IInboundAdapterService Members

        public event DataReceiveEventHandler OnDataReceived;
        internal bool SaveData(IInboundRule rule, DataSet data)
        {
            if (OnDataReceived == null) return false;
            return OnDataReceived(rule, data);
        }

        #endregion

        #region IAdapterService Members

        public bool Start(string[] arguments)
        {
            _storageSCP = new StorageSCPService(this);
            return _storageSCP.Start();
        }

        public AdapterStatus Status
        {
            get
            {
                return AdapterStatus.Stopped;
            }
        }

        public bool Stop(string[] arguments)
        {
            if (_storageSCP == null) return false;
            return _storageSCP.Stop();
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
            Program.Log.Write(Program.AppName + " is running in Adapter.Service host.");

            if (arguments != null && arguments.Length > 0)
            {
                string str = arguments[0];
                //Program.Log.Write("GWDataDB connection: " + str); //contains db pw
                Program.ConfigMgt.Config.GWDataDBConnection = str;
            }

            DicomMappingHelper.SQLMatchChar = "";
            DicomMappingHelper.PreperatMappingList<StorageItem>(StorageList, Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList);

            DicomMappingHelper.CleanQRMappingList
                <StorageItem>(Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList);

            return true;
        }

        public Exception LastError
        {
            get { return null; }
        }

        #endregion
    }
}
