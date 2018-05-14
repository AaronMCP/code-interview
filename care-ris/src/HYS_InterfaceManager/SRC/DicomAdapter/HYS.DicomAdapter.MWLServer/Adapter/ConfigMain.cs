using System;
using System.Data;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Forms;
using HYS.DicomAdapter.MWLServer.Dicom;
using HYS.DicomAdapter.MWLServer.Objects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;

namespace HYS.DicomAdapter.MWLServer.Adapter
{
    [AdapterConfigEntry("DICOM Modality Worklist Adapter Configuration", DirectionType.OUTBOUND,"Modality Worklist Adapter for GC Gateway 2.0")]
    public class ConfigMain : IOutboundAdapterConfig
    {
        #region IOutboundAdapterConfig Members

        public IOutboundRule[] GetRules()
        {
            DicomMappingHelper.CleanMappingList
                <MWLQueryCriteriaItem, MWLQueryResultItem>(Program.ConfigMgt.Config.Rule);
            WorklistSCPHelper.ModifyQCMappingList_CS
                <MWLQueryCriteriaItem>(Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList, true);
            DicomMappingHelper.ModifyQCMappingList_DateTime
                <MWLQueryCriteriaItem>(Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList, true);
            DicomMappingHelper.ModifyQCMappingList_FixValue
                <MWLQueryCriteriaItem>(Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList);
            DicomMappingHelper.SetDataIDMapping
                <MWLQueryResultItem>(Program.ConfigMgt.Config.Rule.QueryResult.MappingList);
            WorklistSCPHelper.SetAdditionalQueryCriteria
                <MWLQueryCriteriaItem>(Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList,
                                       Program.ConfigMgt.Config.AdditionalQueryCriteria,
                                       Program.ConfigMgt.Config.AdditionalQueryCriteriaJoinType);

            return new IOutboundRule[] { Program.ConfigMgt.Config.Rule };
        }

        #endregion

        #region IAdapterConfig Members

        public string GetConfigurationSummary()
        {
            return Program.ConfigMgt.Config.SCPConfig.GetSummary();
        }

        public IConfigUI[] GetConfigUI()
        {
            return new IConfigUI[] { new FormSCP(Program.ConfigMgt, Program.Log), new FormQueryCriteria(), new FormQueryResult() };
        }

        public LookupTable[] GetLookupTables()
        {
            return null;
        }

        public AdapterOption Option
        {
            get { return new AdapterOption(DirectionType.OUTBOUND); }
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
                //Program.Log.Write("GWDataDB connection: " + str); //contains db pw
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
