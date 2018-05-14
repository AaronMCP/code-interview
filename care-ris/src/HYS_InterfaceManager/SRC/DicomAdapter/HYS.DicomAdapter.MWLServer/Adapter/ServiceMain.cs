using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Dicom;
using HYS.DicomAdapter.MWLServer.Forms;
using HYS.DicomAdapter.MWLServer.Objects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule; 
using HYS.Common.Dicom;
using HYS.Common.Xml;

namespace HYS.DicomAdapter.MWLServer.Adapter
{
    [AdapterServiceEntry("DICOM Modality Worklist Adapter Service", DirectionType.OUTBOUND, "Modality Worklist Adapter for HYS 1.0.0.0")]
    public class ServiceMain : IOutboundAdapterService 
    {
        private WorkListSCPService _worklistSCP;
        internal XCollection<MWLQueryResultItem> QRList = new XCollection<MWLQueryResultItem>();
        internal XCollection<MWLQueryCriteriaItem> QCList = new XCollection<MWLQueryCriteriaItem>();

        #region IOutboundAdapterService Members

        public event DataDischargeEventHanlder OnDataDischarge;
        internal bool DischargeData(string[] guidList)
        {
            if (OnDataDischarge == null) return false;
            return OnDataDischarge(guidList);
        }

        public event DataRequestEventHanlder OnDataRequest;
        internal DataSet RequestData(IOutboundRule rule, DataSet criteria)
        {
            if (OnDataRequest == null) return null;
            return OnDataRequest(rule, criteria);
        }

        #endregion

        #region IAdapterService Members

        public bool Start(string[] arguments)
        {
            _worklistSCP = new WorkListSCPService(this);
            return _worklistSCP.Start();
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
            if (_worklistSCP == null) return false;
            return _worklistSCP.Stop();
        }

        #endregion

        #region IAdapterBase Members

        public bool Exit(string[] arguments)
        {
            //Program.BeforeExit();
            return true;
        }

        public bool Initialize(string[] arguments)
        {
            Program.PreLoading();
            Program.Log.Write(Program.AppName + " is running in Adapter.Service host.");


            DicomMappingHelper.PersonNameRule = Program.ConfigMgt.Config.PersonNameRule;

            WorklistSCPHelper.ModifyQCMappingList_CS
                <MWLQueryCriteriaItem>(Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList, false);
            DicomMappingHelper.ModifyQCMappingList_DateTime
                <MWLQueryCriteriaItem>(Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList, false);

            DicomMappingHelper.PreperatMappingList<MWLQueryResultItem>(QRList, Program.ConfigMgt.Config.Rule.QueryResult.MappingList);
            DicomMappingHelper.PreperatMappingList<MWLQueryCriteriaItem>(QCList, Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList);


            DicomMappingHelper.CleanMappingList
                <MWLQueryCriteriaItem, MWLQueryResultItem>(Program.ConfigMgt.Config.Rule);

            return true;
        }

        public Exception LastError
        {
            get { return null; }
        }

        #endregion

    }
}
