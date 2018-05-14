using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;


namespace OutboundDBInstall
{
    #region EventType related class

    public class EventType : XObject
    {
        private string _ETName;
        [ReadOnly(true), Description("Event Type name")]
        public string ETName
        {
            get { return _ETName; }
            set { _ETName = value; }
        }


        private string _ETValue;
        [ReadOnly(true), Description("Event Type value")]
        public string ETValue
        {
            get { return _ETValue; }
            set { _ETValue = value; }
        }

        private bool _Enabled;
        [ReadOnly(false), Description("Whether to treat the event")]
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        private bool _Merging = false;
        public bool Merging
        {
            get { return _Merging; }
            set { _Merging = value; }
        }

        #region PKField List
        //PKFields _PKFields = new PKFields();
        //public PKFields PKFields
        //{
        //    get { return _PKFields; }
        //    set { _PKFields = value; }
        //}

        private MatchFieldTree _matchCriteria = new MatchFieldTree();
        public MatchFieldTree MatchCriteria { get { return _matchCriteria; } set { _matchCriteria = value; } }

        #endregion

        #region MergeField List
        //MergeFields _MergeFields = new MergeFields();
        //public MergeFields MergeFields
        //{
        //    get { return _MergeFields; }
        //    set { _MergeFields = value; }
        //}

        private MergeFieldMappings _mfms = new MergeFieldMappings();
        public MergeFieldMappings MergeFieldMappings
        {
            get { return _mfms; }
            set { _mfms = value; }
        }

        #endregion

        #region Filter List
        private FilterFieldList _ffList = new FilterFieldList();
        public FilterFieldList FilterList
        {
            get { return _ffList; }
            set { _ffList = value; }

        }
        #endregion

        public EventType Clone()
        {
            EventType et = new EventType();
            et.ETName = this.ETName;
            et.ETValue = this.ETValue;
            et.Enabled = this.Enabled;
            et.Merging = this.Merging;

            //et.PKFields = this.PKFields.Clone();
            //et.MergeFields = this.MergeFields.Clone();
            et.MatchCriteria = this.MatchCriteria.Clone();
            et.MergeFieldMappings = this.MergeFieldMappings.Clone();

            return et;
        }
    }

    public class EventTypeList : XObjectCollection
    {
        public EventTypeList()
            : base(typeof(EventType))
        {

        }

        public EventType FindEventTypeByValue(string sValue)
        {
            foreach (EventType et in this)
            {
                if (et.ETValue.Trim().ToUpper() == sValue.Trim().ToUpper())
                    return et;
            }
            return null;
        }
        public EventType FindEventTypeByName(string sName)
        {
            foreach (EventType et in this)
            {
                if (et.ETName.Trim().ToUpper() == sName.Trim().ToUpper())
                    return et;
            }
            return null;
        }

        public EventTypeList Clone()
        {
            EventTypeList etl = new EventTypeList();
            foreach (EventType et in this)
                etl.Add(et.Clone());

            return etl;
        }
    }

    #endregion

    public class IOChannel : XObject
    {
        private string _INameInbound;
        public string INameInbound
        {
            get { return _INameInbound; }
            set { _INameInbound = value; }
        }

        private string _EventTypeListStrInbound = "";
        public string EventTypeListStrInbound
        {
            get
            {
                string r = "";
                foreach (EventType item in EventTypeList)
                {
                    r = r + ";" + item.ETName + "," + item.ETValue.ToString() + "," + item.Enabled.ToString();
                }
                if (r.Length > 1)
                    r = r.Substring(1, r.Length - 1);
                _EventTypeListStrInbound = r;

                return _EventTypeListStrInbound;
            }
            set
            {

                _EventTypeListStrInbound = value;
                if (_EventTypeList.ToString() == "")
                {
                    EventTypeList.Clear();
                    return;
                }

                EventTypeList tmpEventTypeList = new EventTypeList();

                string[] ets = value.Split((";").ToCharArray());
                foreach (string item in ets)
                {
                    if (item == "") continue;
                    string[] nve = item.Split((",").ToCharArray());

                    EventType cEventType = new EventType();

                    cEventType.ETName = nve[0];
                    cEventType.ETValue = nve[1];
                    //  if saved string have not Enable value, set it default value =true
                    if (nve.Length > 2)
                        cEventType.Enabled = Boolean.Parse(nve[2]);
                    else
                        cEventType.Enabled = false;

                    EventType tmpET = EventTypeList.FindEventTypeByValue(cEventType.ETValue);
                    if (tmpET != null)
                        tmpEventTypeList.Add(cEventType);
                    else
                        tmpEventTypeList.Add(cEventType);
                }

                EventTypeList = tmpEventTypeList;

            }
        }

        private bool _checkRedundancy;
        public bool CheckRedundancy
        {
            get { return _checkRedundancy; }
            set { _checkRedundancy = value; }
        }

        private EventTypeList _EventTypeList = new EventTypeList();
        public EventTypeList EventTypeList
        {
            get { return _EventTypeList; }
            set { _EventTypeList = value; }

        }

        public IOChannel Clone()
        {
            IOChannel ch = new IOChannel();

            ch.INameInbound = this.INameInbound;
            ch.EventTypeListStrInbound = this.EventTypeListStrInbound;

            ch.EventTypeList = this.EventTypeList.Clone();

            return ch;
        }

    }

    public class IOChannels : XObjectCollection
    {
        public IOChannels()
            : base(typeof(IOChannel))
        {
        }

        public IOChannel FindChannel(string InboundIFName)
        {
            for (int i = 0; i < this.Count; i++)
                if (((IOChannel)this[i]).INameInbound.Trim().ToUpper() == InboundIFName.Trim().ToUpper())
                    return (IOChannel)this[i];

            return null;
        }

        public IOChannels Clone()
        {
            IOChannels chs = new IOChannels();

            foreach (IOChannel ch in this)
            {
                chs.Add(ch.Clone());
            }

            return chs;
        }
    }

    #region --old pkfield --

    //public class PKField : XObject
    //{
    //    public PKField(string ATable, string AField, string AOperator)
    //    {
    //        _Table = ATable;
    //        _Field = AField;
    //        _Operator = AOperator;
    //    }

    //    public PKField()
    //    {

    //    }

    //    private string _Table;
    //    public string Table
    //    {
    //        get { return _Table; }
    //        set { _Table = value; }
    //    }

    //    private string _Field;
    //    public string FieldName
    //    {
    //        get { return _Field; }
    //        set { _Field = value; }
    //    }

    //    private string _Operator = "AND";
    //    public string Operator
    //    {
    //        get { return _Operator; }
    //        set { _Operator = value; }
    //    }



    //    public PKField Clone()
    //    {
    //        PKField pkf = new PKField();
    //        pkf.Table = this.Table;
    //        pkf.FieldName = this.FieldName;
    //        pkf.Operator = this.Operator;

    //        return pkf;
    //    }

    //}

    //public class PKFields : XObjectCollection
    //{
    //    public PKFields()
    //        : base(typeof(PKField))
    //    {
    //    }

    //    public PKField FindField(string ATableName, string AFieldName)
    //    {
    //        foreach (PKField f in this)
    //        {
    //            if ((f.Table.Trim().ToUpper() == ATableName.Trim().ToUpper())
    //                && (f.FieldName.Trim().ToUpper() == AFieldName.Trim().ToUpper()))
    //            {
    //                return f;
    //            }
    //        }

    //        return null;
    //    }

    //    public PKFields Clone()
    //    {
    //        PKFields pkfs = new PKFields();
    //        foreach (PKField pkf in this)
    //            pkfs.Add(pkf.Clone());

    //        return pkfs;
    //    }
    //}

    #endregion

    public class MatchField : XObject
    {
        public string TableName { get; set; }

        public string FieldName { get; set; }

        public LogicOperator LogicOperator { get; set; }

        public MatchFieldValue FixValue { get; set; }

        public string GetSQLStatmenet(string strOutbountViewName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'[").Append(strOutbountViewName).Append("].")
                .Append(GWDataDB.GetTableName((GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), TableName))).Append("_").Append(FieldName)
                .Append(GetLogicOperatorString())
                .Append("'+")
                .Append(FixValue.GetValue());

            return sb.ToString();
        }

        private string GetLogicOperatorString()
        {
            switch (LogicOperator)
            {
                case LogicOperator.Equal:
                    return " = ";
                case LogicOperator.NotEqual:
                    return " <> ";
                case LogicOperator.LargerThan:
                    return " > ";
                case LogicOperator.LargerEqualThan:
                    return " >= ";
                case LogicOperator.SmallerThan:
                    return " < ";
                case LogicOperator.SmallerEqualThan:
                    return " <= ";
                case LogicOperator.Like:
                    return " LIKE ";
                case LogicOperator.In:
                    return " IN ";
                default:
                    return string.Empty;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(TableName).Append(".").Append(FieldName).Append("(Out)")
                .Append(GetLogicOperatorString())
                .Append(FixValue.ToString());

            return sb.ToString();
        }

        public MatchField Clone()
        {
            MatchField f = new MatchField();
            f.FieldName = this.FieldName;
            f.LogicOperator = this.LogicOperator;
            f.TableName = this.TableName;
            f.FixValue = this.FixValue.Clone();

            return f;
        }
    }

    public class MatchFieldValue : XObject
    {
        public FieldValueType ValueType { get; set; }

        private string _inboundTableName;
        public string InboundTableName
        {
            get { return _inboundTableName; }
            set { _inboundTableName = value; }
        }

        private string _inboundFieldName;
        public string InboundFieldName
        {
            get { return _inboundFieldName; }
            set { _inboundFieldName = value; }
        }

        public string Value { get; set; }

        public string GetValue()
        {
            if (ValueType == FieldValueType.FixValue)
            {
                return "'"+Value.Replace("'", "''")+"'";
            }

            if (ValueType == FieldValueType.InboundTableField)
            {
                return "''''+ISNULL([" + GWDataDB.GetTableName((GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), _inboundTableName)) + "].[" + _inboundFieldName + "]+'''','''''')";
            }

            return base.ToString();
        }

        public override string ToString()
        {
            if (ValueType == FieldValueType.FixValue)
            {
                return Value;
            }

            if (ValueType == FieldValueType.InboundTableField)
            {
                return _inboundTableName + "." + _inboundFieldName;
            }

            return base.ToString();
        }

        public MatchFieldValue Clone()
        {
            MatchFieldValue v = new MatchFieldValue();
            v.InboundFieldName = this.InboundFieldName;
            v.InboundTableName = this.InboundTableName;
            v.Value = this.Value;
            v.ValueType = this.ValueType;

            return v;
        }
    }

    public enum FieldValueType
    {
        FixValue,
        InboundTableField
    }

    public enum JoinOperator
    {
        AND,
        OR
    }

    public enum LogicOperator
    {
        Equal,
        NotEqual,
        LargerThan,
        LargerEqualThan,
        SmallerThan,
        SmallerEqualThan,
        Like,
        In
    }

    public class MatchFieldNode : XObject
    {
        public JoinOperator JoinOperator { get; set; }

        private XCollection<MatchFieldNode> _childNodes = new XCollection<MatchFieldNode>();
        public XCollection<MatchFieldNode> ChildNodes
        {
            get { return _childNodes; }
            set { _childNodes = value; }
        }

        private XCollection<MatchField> _childMatchField = new XCollection<MatchField>();
        public XCollection<MatchField> MatchFields
        {
            get { return _childMatchField; }
            set { _childMatchField = value; }
        }


        public string GetSQLStatement(string strOutbountViewName)
        {
            string strJoinOperator = GetJoinOperatorString();
            StringBuilder sb = new StringBuilder();
            foreach (MatchField field in MatchFields)
            {
                sb.Append(field.GetSQLStatmenet(strOutbountViewName));
                sb.Append("+'" + strJoinOperator + "'+");
            }

            string strNodeSql = string.Empty;
            foreach (MatchFieldNode node in ChildNodes)
            {
                strNodeSql = node.GetSQLStatement(strOutbountViewName);

                if (!string.IsNullOrEmpty(strNodeSql))
                {
                    sb.Append("'('+").Append(strNodeSql).Append("+')'");
                    sb.Append("+'" + strJoinOperator + "'+");
                }
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - ("+'" + strJoinOperator + "'+").Length, ("+'" + strJoinOperator + "'+").Length);
            }

            return sb.ToString();
        }

        private string GetJoinOperatorString()
        {
            switch (JoinOperator)
            {
                case JoinOperator.AND:
                    return " AND ";
                case JoinOperator.OR:
                    return " OR ";
                default:
                    return string.Empty;
            }
        }

        public override string ToString()
        {
            return GetJoinOperatorString();
        }

        public MatchFieldNode Clone()
        {
            MatchFieldNode n = new MatchFieldNode();
            n.JoinOperator = this.JoinOperator;

            foreach (MatchField m in this.MatchFields)
            {
                n.MatchFields.Add(m.Clone());
            }

            foreach (MatchFieldNode node in this.ChildNodes)
            {
                n.ChildNodes.Add(node.Clone());
            }

            return n;
        }
    }

    public class MatchFieldTree : XObject
    {
        private MatchFieldNode root = new MatchFieldNode();

        public MatchFieldNode Root
        {
            get { return root; }
        }

        public MatchFieldTree()
        {
            root.JoinOperator = JoinOperator.AND;
        }

        public MatchFieldTree(JoinOperator joinOperator)
        {
            root.JoinOperator = joinOperator;
        }

        public string GetSQLStatement(string strOutbountViewName)
        {
            return root.GetSQLStatement(strOutbountViewName);
        }


        public MatchFieldTree Clone()
        {
            MatchFieldTree t = new MatchFieldTree();
            t.root = this.root.Clone();

            return t;
        }
    }

    #region --old mergefiled--

    //public class MergeField : XObject
    //{
    //    public MergeField(string ATable, string AField)
    //    {
    //        _Table = ATable;
    //        _Field = AField;
    //    }

    //    public MergeField()
    //    {

    //    }

    //    private string _Table;
    //    public string Table
    //    {
    //        get { return _Table; }
    //        set { _Table = value; }
    //    }

    //    private string _Field;
    //    public string FieldName
    //    {
    //        get { return _Field; }
    //        set { _Field = value; }
    //    }

    //    public MergeField Clone()
    //    {
    //        MergeField mf = new MergeField();
    //        mf.Table = this.Table;
    //        mf.FieldName = this.FieldName;
    //        return mf;
    //    }

    //}

    //public class MergeFields : XObjectCollection
    //{
    //    public MergeFields()
    //        : base(typeof(MergeField))
    //    {
    //    }

    //    public MergeField FindField(string ATableName, string AFieldName)
    //    {
    //        foreach (MergeField f in this)
    //        {
    //            if ((f.Table.Trim().ToUpper() == ATableName.Trim().ToUpper())
    //                && (f.FieldName.Trim().ToUpper() == AFieldName.Trim().ToUpper()))
    //            {
    //                return f;
    //            }
    //        }

    //        return null;
    //    }

    //    public MergeFields Clone()
    //    {
    //        MergeFields mfs = new MergeFields();
    //        foreach (MergeField mf in this)
    //            mfs.Add(mf.Clone());
    //        return mfs;
    //    }

    //}

    #endregion

    public class MergeFieldMapping : XObject
    {
        public string OutboundTable { get; set; }

        public string OutboundField { get; set; }

        public string InboundTable { get; set; }

        public string InboundField { get; set; }

        public MergeFieldMapping Clone()
        {
            MergeFieldMapping mfm = new MergeFieldMapping();
            mfm.InboundField = InboundField;
            mfm.InboundTable = InboundTable;
            mfm.OutboundField = OutboundField;
            mfm.OutboundTable = OutboundTable;

            return mfm;
        }
    }

    public class MergeFieldMappings : XObjectCollection
    {
        public MergeFieldMappings() : base(typeof(MergeFieldMapping)) { }

        public MergeFieldMapping FindMapping(string outboundTable, string outboundField)
        {
            foreach (MergeFieldMapping map in this)
            {
                if (map.OutboundTable.Equals(outboundTable) && map.OutboundField.Equals(outboundField))
                {
                    return map;
                }
            }

            return null;
        }

        public bool HasMappings(string outboundTalble)
        {
            foreach (MergeFieldMapping map in this)
            {
                if (map.OutboundTable.Equals(outboundTalble))
                {
                    return true;
                }
            }

            return false;
        }

        public MergeFieldMappings Clone()
        {
            MergeFieldMappings mfms = new MergeFieldMappings();
            foreach (MergeFieldMapping item in this)
            {
                mfms.Add(item.Clone());
            }

            return mfms;
        }
    }

    public class FilterField : XObject
    {
        public FilterField(string ATable, string AFiled, string AOperator, string ALogic, string ALogicValue)
        {
            _Table = ATable;
            _Field = ATable;
            _Operator = AOperator;
            _Logic = ALogic;
            _LogicValue = ALogicValue;
        }

        public FilterField()
        {
        }


        private string _Table;
        public string Table
        {
            get { return _Table; }
            set { _Table = value; }
        }

        private string _Field;
        public string Field
        {
            get { return _Field; }
            set { _Field = value; }
        }

        private string _Operator = "OR";
        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }

        private string _Logic = "=";
        [XCData(true)]
        public string Logic
        {
            get { return _Logic; }
            set { _Logic = value; }
        }

        private string _LogicValue;
        [XCData(true)]
        public string LogicValue
        {
            get { return _LogicValue; }
            set { _LogicValue = value; }
        }

        public FilterField Clone()
        {
            FilterField fField = new FilterField();
            fField.Table = this.Table;
            fField.Field = this.Field;
            fField.Operator = this.Operator;
            fField.Logic = this.Logic;
            fField.LogicValue = this.LogicValue;
            return fField;
        }

    }

    public class SubFilterFieldList : XObjectCollection
    {
        public SubFilterFieldList()
            : base(typeof(FilterField))
        {
        }

        public SubFilterFieldList Clone()
        {
            SubFilterFieldList sFFList = new SubFilterFieldList();
            foreach (FilterField ff in this)
            {
                sFFList.Add(ff.Clone());
            }

            return sFFList;
        }

    }

    public class FilterFieldList : XObjectCollection
    {
        public FilterFieldList()
            : base(typeof(SubFilterFieldList))
        {
        }

        public FilterFieldList Clone()
        {
            FilterFieldList FFList = new FilterFieldList();

            foreach (SubFilterFieldList sFFlist in this)
            {
                FFList.Add(sFFlist.Clone());
            }

            return FFList;
        }

    }

    public class OutboundConfig : XObject
    {
        #region Copy Contructor
        public OutboundConfig Clone()
        {
            OutboundConfig oc = new OutboundConfig();
            oc.Device_ID = this.Device_ID;
            oc.INameOutbound = this.INameOutbound;

            oc.InstallConfigDBScript = this.InstallConfigDBScript;
            oc.InstallTriggerScript = this.InstallTriggerScript;
            oc.UninstallConfigDBScript = this.UninstallConfigDBScript;
            oc.UninstallTriggerScript = this.UninstallTriggerScript;

            oc.ScriptIsHijacked = this.ScriptIsHijacked;

            oc.IOChannels = this.IOChannels.Clone();

            return oc;
        }
        #endregion

        #region Outbound Database objects

        private int _DEVICE_ID = 1;
        [Bindable(true)]
        public int Device_ID
        {
            get { return _DEVICE_ID; }
            set { _DEVICE_ID = value; }
        }

        private string _IName;
        [Category("Outbound interface name"), Description("Name of Outbound interface ")]
        [ReadOnly(true)]
        public string INameOutbound
        {
            get { return _IName; }
            set { _IName = value; }
        }

        #endregion


        bool _ScriptIsHijacked = false;
        public bool ScriptIsHijacked
        {
            get { return _ScriptIsHijacked; }
            set { _ScriptIsHijacked = value; }
        }

        #region IOChannel List

        IOChannels _IOChannels = new IOChannels();
        public IOChannels IOChannels
        {
            get { return _IOChannels; }
            set { IOChannels = value; }
        }
        #endregion

        #region User Changed Script, if user not changed, it save script which system produced

        string _InstallTriggerScript = "";
        [XCData(true)]
        public string InstallTriggerScript
        {
            get { return _InstallTriggerScript; }
            set
            {
                _InstallTriggerScript = value;
            }
        }

        string _UninstallTriggerScript = "";
        [XCData(true)]
        public string UninstallTriggerScript
        {
            get { return _UninstallTriggerScript; }
            set
            {
                _UninstallTriggerScript = value;
            }
        }

        string _InstallConfigDBScript = "";
        [XCData(true)]
        public string InstallConfigDBScript
        {
            get { return _InstallConfigDBScript; }
            set
            {
                _InstallConfigDBScript = value;
            }
        }

        string _UninstallConfigDBScript = "";
        [XCData(true)]
        public string UninstallConfigDBScript
        {
            get { return _UninstallConfigDBScript; }
            set
            {
                _UninstallConfigDBScript = value;
            }
        }

        public bool BuildScriptBySetting()
        {
            _InstallTriggerScript = BuildInstllTriggerScript();
            _UninstallTriggerScript = BuildUninstallTriggerScript();

            _InstallConfigDBScript = BuildInstallConfigDBScript();
            _UninstallConfigDBScript = BuildUnInstallConfigDBScript();

            _ScriptIsHijacked = false;
            return true;
        }

        #endregion

        #region Function To Build Script fragment

        public string View_Outbound_InstallScript()//IOChannel ch)
        {
            StringBuilder sb = new StringBuilder();

            string sVName = "v_" + OutboundDBConfigMgt.Config.INameOutbound;

            #region Drop V_xxx

            sb.Append(View_Outbound_UninstallScript());

            #endregion

            #region Create View
            sb.AppendLine("Create View [dbo].[" + sVName + "]");
            sb.AppendLine("AS");
            sb.AppendLine("Select ");

            #region Field List

            GWDataDBField[] DataIndex_Fields = GWDataDBField.GetFields(GWDataDBTable.Index);
            GWDataDBField[] Patient_Fields = GWDataDBField.GetFields(GWDataDBTable.Patient);
            GWDataDBField[] Order_Fields = GWDataDBField.GetFields(GWDataDBTable.Order);
            GWDataDBField[] Report_Fields = GWDataDBField.GetFields(GWDataDBTable.Report);

            string sIndexTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Index);
            string sPatientTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Patient);
            string sOrderTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Order);
            string sReportTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Report);

            foreach (GWDataDBField item in DataIndex_Fields)
            {
                sb.AppendLine("dbo." + sIndexTable + "." + item.FieldName + " as " + GWDataDB.GetTableName(item.Table) + "_" + item.FieldName + ",");
            }

            sb.AppendLine("");

            foreach (GWDataDBField item in Patient_Fields)
            {
                sb.AppendLine("dbo." + sPatientTable + "." + item.FieldName + " as " + GWDataDB.GetTableName(item.Table) + "_" + item.FieldName + ",");
            }

            sb.AppendLine("");

            foreach (GWDataDBField item in Order_Fields)
            {
                sb.AppendLine("dbo." + sOrderTable + "." + item.FieldName + " as " + GWDataDB.GetTableName(item.Table) + "_" + item.FieldName + ",");
            }

            sb.AppendLine("");

            foreach (GWDataDBField item in Report_Fields)
            {
                sb.AppendLine("dbo." + sReportTable + "." + item.FieldName + " as " + GWDataDB.GetTableName(item.Table) + "_" + item.FieldName + ",");
            }

            sb.Remove(sb.Length - 3, 3);

            #endregion

            #region FROM LEFT OUTER JOIN
            sb.AppendLine(" from " + sIndexTable);
            sb.AppendLine(" LEFT OUTER JOIN " + sPatientTable + " ON " + sIndexTable + "." + GWDataDBField.i_IndexGuid.FieldName + " = " + sPatientTable + "." + GWDataDBField.p_DATA_ID.FieldName);
            sb.AppendLine(" LEFT OUTER JOIN " + sOrderTable + " ON " + sIndexTable + "." + GWDataDBField.i_IndexGuid.FieldName + " = " + sOrderTable + "." + GWDataDBField.o_DATA_ID.FieldName);
            sb.AppendLine(" LEFT OUTER JOIN " + sReportTable + " ON " + sIndexTable + "." + GWDataDBField.i_IndexGuid.FieldName + " = " + sReportTable + "." + GWDataDBField.r_DATA_ID.FieldName);
            #endregion

            sb.AppendLine("GO\r\n");

            #endregion

            return sb.ToString();
        }

        public string View_Outbound_UninstallScript()//IOChannel ch)
        {
            StringBuilder sb = new StringBuilder();

            string sVName = "v_" + OutboundDBConfigMgt.Config.INameOutbound;

            #region Drop V_xxx

            sb.AppendLine("IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[" + sVName + "]'))");
            sb.AppendLine("DROP VIEW [dbo].[" + sVName + "]");
            sb.AppendLine(" GO\r\n");
            #endregion

            return sb.ToString();
        }

        public string Procedure_IsRedundant_InstallScript(IOChannel ch)
        {
            StringBuilder sb = new StringBuilder();

            string sPName = ch.INameInbound + "_" + OutboundDBConfigMgt.Config.INameOutbound + "_IsRedundant";
            string sInbound_Index_Table = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Index);
            string sInbound_Patient_Table = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Patient);
            string sInbound_Order_Table = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Order);
            string sInbound_Report_Table = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Report);

            #region Drop IsRedundant

            sb.Append(Procedure_IsRedundant_UninstallScript(ch));

            #endregion

            #region IsRedundant declaration
            sb.AppendLine("CREATE procedure " + sPName);
            sb.AppendLine("(");
            sb.AppendLine("    @In_Data_ID  varchar(max) ");
            sb.AppendLine("   ,@Out_Data_ID varchar(max) output ");
            sb.AppendLine("   ,@ret bit output  ");
            sb.AppendLine(")");
            sb.AppendLine("AS");
            #endregion

            #region _IsRedundant BEGIN
            sb.AppendLine("BEGIN");
            #endregion

            #region Query Criteria

            GWDataDBField[] Index_Fields = GWDataDBField.GetFields(GWDataDBTable.Index);
            GWDataDBField[] Patient_Fields = GWDataDBField.GetFields(GWDataDBTable.Patient);
            GWDataDBField[] Order_Fields = GWDataDBField.GetFields(GWDataDBTable.Order);
            GWDataDBField[] Report_Fields = GWDataDBField.GetFields(GWDataDBTable.Report);

            sb.AppendLine("declare @stm nvarchar(max)");
            sb.AppendLine("declare @where nvarchar(max)");
            sb.AppendLine("");
            sb.AppendLine("-- Build @where string -----begin--");
            sb.AppendLine("--set @where = N'process_flag=1'	");
            sb.AppendLine("--SELECT * FROM %Inbound_IFName%_DATAINDEX");
            sb.AppendLine("DECLARE @S1 NVARCHAR(MAX) -- DATAINDEX");
            sb.AppendLine("DECLARE @S2 NVARCHAR(MAX) -- PATIENT");
            sb.AppendLine("DECLARE @S3 NVARCHAR(MAX) -- ORDER");
            sb.AppendLine("DECLARE @S4 NVARCHAR(MAX) -- REPORT");

            #region Index Criteria

            sb.AppendLine(" SELECT @S1=");

            for (int i = 0, j = 0; i < Index_Fields.Length; i++)
            {
                if (Index_Fields[i] == GWDataDBField.i_IndexGuid)
                    continue;
                if (Index_Fields[i] == GWDataDBField.i_DataDateTime)
                    continue;
                if (Index_Fields[i] == GWDataDBField.i_PROCESS_FLAG)
                    continue;

                if (j > 0)
                    sb.Append("+ N' AND '+");
                j++;
                string sVField = GWDataDB.GetTableName(GWDataDBTable.Index) + "_" + Index_Fields[i].FieldName;
                string sTField = Index_Fields[i].FieldName;

                sb.AppendLine(" N'" + sVField + "'");
                sb.AppendLine("+CASE WHEN " + sTField + " IS NULL THEN N' IS NULL '");
                sb.AppendLine(" ELSE N'='+" + "''''+" + sTField + "+''''");
                sb.AppendLine(" END ");
                sb.Append(" ");
            }

            sb.AppendLine(" FROM " + sInbound_Index_Table);
            sb.AppendLine(" WHERE " + GWDataDBField.i_IndexGuid.FieldName + " = @IN_DATA_ID");
            sb.AppendLine("");
            #endregion

            #region Patient Criteria

            sb.AppendLine(" SELECT @S2=");

            for (int i = 0, j = 0; i < Patient_Fields.Length; i++)
            {
                if (Patient_Fields[i] == GWDataDBField.p_DATA_ID)
                    continue;
                if (Patient_Fields[i] == GWDataDBField.p_DATA_DT)
                    continue;

                if (j > 0)
                    sb.Append("+");
                j++;
                string sVField = GWDataDB.GetTableName(GWDataDBTable.Patient) + "_" + Patient_Fields[i].FieldName;
                string sTField = Patient_Fields[i].FieldName;

                sb.AppendLine(" N' AND " + sVField + "'");
                sb.AppendLine("+CASE WHEN " + sTField + " IS NULL THEN N' IS NULL '");
                sb.AppendLine(" ELSE N'='+" + "''''+" + sTField + "+''''");
                sb.AppendLine(" END ");
                sb.Append(" ");
            }

            sb.AppendLine(" FROM " + sInbound_Patient_Table);
            sb.AppendLine(" WHERE " + GWDataDBField.p_DATA_ID.FieldName + " = @IN_DATA_ID");
            sb.AppendLine("");
            #endregion

            #region Order Criteria

            sb.AppendLine(" SELECT @S3=");

            for (int i = 0, j = 0; i < Order_Fields.Length; i++)
            {
                if (Order_Fields[i] == GWDataDBField.o_DATA_ID)
                    continue;
                if (Order_Fields[i] == GWDataDBField.o_DATA_DT)
                    continue;
                if (j > 0)
                    sb.Append("+");
                j++;

                string sVField = GWDataDB.GetTableName(GWDataDBTable.Order) + "_" + Order_Fields[i].FieldName;
                string sTField = Order_Fields[i].FieldName;

                sb.AppendLine(" N' AND " + sVField + "'");
                sb.AppendLine("+CASE WHEN " + sTField + " IS NULL THEN N' IS NULL '");
                sb.AppendLine(" ELSE N'='+" + "''''+" + sTField + "+''''");
                sb.AppendLine(" END ");
            }

            sb.AppendLine(" FROM " + sInbound_Order_Table);
            sb.AppendLine(" WHERE " + GWDataDBField.o_DATA_ID.FieldName + " = @IN_DATA_ID");
            sb.AppendLine("");
            #endregion

            #region Report Criteria

            sb.AppendLine(" SELECT @S4=");

            for (int i = 0, j = 0; i < Report_Fields.Length; i++)
            {
                if (Report_Fields[i] == GWDataDBField.r_DATA_ID)
                    continue;
                if (Report_Fields[i] == GWDataDBField.r_DATA_DT)
                    continue;
                if (j > 0)
                    sb.Append("+");
                j++;
                string sVField = GWDataDB.GetTableName(GWDataDBTable.Report) + "_" + Report_Fields[i].FieldName;
                string sTField = Report_Fields[i].FieldName;

                sb.AppendLine(" N' AND " + sVField + "'");
                sb.AppendLine("+CASE WHEN " + sTField + " IS NULL THEN N' IS NULL '");
                sb.AppendLine(" ELSE N'='+" + "''''+" + sTField + "+''''");
                sb.AppendLine(" END ");
            }

            sb.AppendLine(" FROM " + sInbound_Report_Table);
            sb.AppendLine(" WHERE " + GWDataDBField.r_DATA_ID.FieldName + " = @IN_DATA_ID");
            sb.AppendLine("");

            #endregion

            #region Execute SQL
            sb.AppendLine("SET @WHERE = ''");
            sb.AppendLine("   IF @S1 IS NOT NULL");
            sb.AppendLine("      SET @WHERE = @WHERE+@S1");
            sb.AppendLine("   IF @S2 IS NOT NULL");
            sb.AppendLine("      SET @WHERE = @WHERE+@S2");
            sb.AppendLine("   IF @S3 IS NOT NULL");
            sb.AppendLine("      SET @WHERE = @WHERE+@S3");
            sb.AppendLine("   IF @S4 IS NOT NULL");
            sb.AppendLine("      SET @WHERE = @WHERE+@S4");
            sb.AppendLine(" ");
            sb.AppendLine("   IF @WHERE != ''");
            sb.AppendLine("      SET @WHERE = ' WHERE '+@WHERE");
            sb.AppendLine("");
            sb.AppendLine("   ------------BUILD WHERE End----");
            sb.AppendLine("   ");
            sb.AppendLine("   set @stm = N'select @out_data_id =" + GWDataDB.GetTableName(GWDataDBTable.Index) + "_" + GWDataDBField.i_IndexGuid.FieldName + "  from V_" + OutboundDBConfigMgt.Config.INameOutbound + "'+ @where ");
            sb.AppendLine("   declare @temp varchar(max)");
            sb.AppendLine("   exec sp_executesql @stm,");
            sb.AppendLine("        N'@out_data_id as varchar(max) output',");
            sb.AppendLine("        @out_data_id = @temp output");
            sb.AppendLine("   ");
            sb.AppendLine("   if @temp is null");
            sb.AppendLine("   begin");
            sb.AppendLine("     Set @ret = 0");
            sb.AppendLine("   end	");
            sb.AppendLine("   else begin");
            sb.AppendLine("     set @out_data_id = @temp");
            sb.AppendLine("     set @ret = 1");
            sb.AppendLine("   end   ");
            #endregion

            #endregion

            #region  END
            sb.AppendLine("END");
            sb.AppendLine("GO\r\n");
            #endregion

            return sb.ToString();
        }

        public string Procedure_IsRedundant_UninstallScript(IOChannel ch)
        {
            StringBuilder sb = new StringBuilder();
            string sPName = ch.INameInbound + "_" + OutboundDBConfigMgt.Config.INameOutbound + "_IsRedundant";
            #region Drop IsRedundant

            sb.AppendLine("IF OBJECT_ID ( '" + sPName + "', 'P' ) IS NOT NULL ");
            sb.AppendLine("DROP PROCEDURE " + sPName);
            sb.AppendLine("GO\r\n");

            #endregion

            return sb.ToString();
        }


        #region --New--

        private string Procedure_PKIsExisted_InstallScript(IOChannel ch)
        {

            StringBuilder sb = new StringBuilder();

            string sPName = ch.INameInbound + "_" + OutboundDBConfigMgt.Config.INameOutbound + "_PKIsExisted";

            #region Drop _PKIsExisted

            sb.AppendLine("IF OBJECT_ID ( '" + sPName + "', 'P' ) IS NOT NULL ");
            sb.AppendLine("DROP PROCEDURE " + sPName);
            sb.AppendLine("GO\r\n");

            #endregion

            #region _PKIsExisted declaration

            sb.AppendLine("CREATE procedure " + sPName);
            sb.AppendLine("(");
            sb.AppendLine("    @In_Data_ID  varchar(max) ");
            sb.AppendLine("   ,@Out_Data_ID varchar(max) output ");
            sb.AppendLine("   ,@ret bit output  ");
            sb.AppendLine(")");
            sb.AppendLine("AS");

            #endregion

            #region _PKIsExisted BEGIN

            sb.AppendLine("BEGIN");

            #endregion

            #region Query Criteria

            #region PreTreat

            sb.AppendLine("");
            sb.AppendLine("DECLARE @EVENT_TYPE varchar(max) ");
            string sInbound_IndexTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Index);
            string sInbound_PatientTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Patient);
            string sInbound_OrderTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Order);
            string sInbound_ReportTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Report);

            string sOutbound_IndexTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Index);
            string sOutbound_PatientTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Patient);
            string sOutbound_OrderTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Order);
            string sOutbound_ReportTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Report);

            sb.AppendLine("SELECT @EVENT_TYPE=" + GWDataDBField.i_EventType.FieldName + " from " + sInbound_IndexTable);
            sb.AppendLine(" WHERE " + GWDataDBField.i_IndexGuid.FieldName + "=@In_Data_ID");


            sb.AppendLine("declare @stm nvarchar(max)");
            sb.AppendLine("declare @temp varchar(max)");
            sb.AppendLine("declare @where nvarchar(max)");
            sb.AppendLine("DECLARE @S1 NVARCHAR(MAX)");

            #endregion

            string sVName = "v_" + OutboundDBConfigMgt.Config.INameOutbound;
            foreach (EventType et in ch.EventTypeList)
            {
                sb.AppendLine(" IF @EVENT_TYPE='" + et.ETValue.Trim() + "'");
                sb.AppendLine(" BEGIN");

                #region --Prepare Where Sql--

                string strIndexTableNameShort = GWDataDB.GetTableName(GWDataDBTable.Index);
                string strPatientTableNameShort = GWDataDB.GetTableName(GWDataDBTable.Patient);
                string strOrderTableNameShort = GWDataDB.GetTableName(GWDataDBTable.Order);
                string strReportTableNameShort = GWDataDB.GetTableName(GWDataDBTable.Report);

                string strCriteriaSql = et.MatchCriteria.GetSQLStatement(sVName);
                if (!string.IsNullOrEmpty(strCriteriaSql))
                {
                    sb.AppendLine(" select @S1 = ").Append(et.MatchCriteria.GetSQLStatement(sVName)).AppendLine()
                        .Append(" from ").Append(sInbound_IndexTable).Append(" as [").Append(strIndexTableNameShort).AppendLine("]")
                        .Append(" left join ").Append(sInbound_PatientTable).Append(" as [").Append(strPatientTableNameShort).Append("] ON [").Append(strIndexTableNameShort).Append("].DATA_ID = [").Append(strPatientTableNameShort).Append("].DATA_ID").AppendLine()
                        .Append(" left join ").Append(sInbound_OrderTable).Append(" as [").Append(strOrderTableNameShort).Append("] ON [").Append(strIndexTableNameShort).Append("].DATA_ID = [").Append(strOrderTableNameShort).Append("].DATA_ID").AppendLine()
                        .Append(" left join ").Append(sInbound_ReportTable).Append(" as [").Append(strReportTableNameShort).Append("] ON [").Append(strIndexTableNameShort).Append("].DATA_ID = [").Append(strReportTableNameShort).Append("].DATA_ID").AppendLine()
                        .Append(" WHERE [").Append(strIndexTableNameShort).Append("].DATA_ID = @In_Data_ID ;").AppendLine();
                }                

                sb.AppendLine("   SET @WHERE = ''");
                sb.AppendLine("   IF @S1 IS NOT NULL");
                sb.AppendLine("      SET @WHERE = @WHERE+@S1");
                sb.AppendLine("   IF @WHERE != ''");
                sb.AppendLine("      SET @WHERE = ' WHERE '+@WHERE");
                sb.AppendLine();
                sb.AppendLine("   -----BUILD WHERE End----");
                sb.AppendLine();
                sb.AppendLine("   set @stm = N'select @out_data_id =" + GWDataDB.GetTableName(GWDataDBTable.Index) + "_" + GWDataDBField.i_IndexGuid.FieldName + "  from V_" + OutboundDBConfigMgt.Config.INameOutbound + "'+ @where ");

                #endregion

                #region Execute SQL

                sb.AppendLine("   exec sp_executesql @stm,");
                sb.AppendLine("        N'@out_data_id as varchar(max) output',");
                sb.AppendLine("        @out_data_id = @temp output");
                sb.AppendLine();
                sb.AppendLine("   if @temp is null");
                sb.AppendLine("   begin");
                sb.AppendLine("     Set @ret = 0");
                sb.AppendLine("   end	");
                sb.AppendLine("   else begin");
                sb.AppendLine("     set @out_data_id = @temp");
                sb.AppendLine("     set @ret = 1");
                sb.AppendLine("   end   ");
                sb.AppendLine(" return");
                #endregion

                sb.AppendLine("End");

            }
            #endregion

            #region  END
            sb.AppendLine("END");
            sb.AppendLine("GO\r\n");
            #endregion

            return sb.ToString();
        }

        public string Procedure_MergeFields_InstallScript(IOChannel ch)
        {

            StringBuilder sb = new StringBuilder();

            string sPName = ch.INameInbound + "_" + OutboundDBConfigMgt.Config.INameOutbound + "_MergeFields";

            #region Drop MergeFields

            sb.AppendLine("IF OBJECT_ID ( '" + sPName + "', 'P' ) IS NOT NULL ");
            sb.AppendLine("DROP PROCEDURE " + sPName);
            sb.AppendLine("GO\r\n");
            #endregion

            #region _MergeFields declaration
            sb.AppendLine("CREATE procedure " + sPName);
            sb.AppendLine("(");
            sb.AppendLine("    @In_Data_ID  varchar(max) ");
            sb.AppendLine("   ,@Out_Data_ID varchar(max) ");
            sb.AppendLine("   ,@ret bit output  ");
            sb.AppendLine(")");
            sb.AppendLine("AS");
            #endregion

            #region _MergeFields BEGIN
            sb.AppendLine("BEGIN");
            #endregion

            sb.AppendLine("declare @stm nvarchar(max)");
            sb.AppendLine("declare @where nvarchar(max)");
            sb.AppendLine("DECLARE @S1 NVARCHAR(MAX)");
            sb.AppendLine("create table #match_data_ids(Data_ID nvarchar(max));");

            #region Merge Fields
            sb.AppendLine();
            sb.AppendLine("DECLARE @EVENT_TYPE varchar(max) ");

            string sInbound_IndexTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Index);
            string sInbound_PatientTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Patient);
            string sInbound_OrderTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Order);
            string sInbound_ReportTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Report);

            sb.AppendLine("SELECT @EVENT_TYPE=" + GWDataDBField.i_EventType.FieldName + " from " + sInbound_IndexTable);
            sb.AppendLine(" WHERE " + GWDataDBField.i_IndexGuid.FieldName + "=@In_Data_ID");

            sb.AppendLine("--Merging Fields");

            string sVName = "v_" + OutboundDBConfigMgt.Config.INameOutbound;
            foreach (EventType et in ch.EventTypeList)
            {
                if ((!et.Merging)
                    || string.IsNullOrEmpty(et.MatchCriteria.GetSQLStatement("View")) 
                    || et.MergeFieldMappings.Count == 0) 
                    continue;

                string strIndexTableNameShort = GWDataDB.GetTableName(GWDataDBTable.Index);
                string strPatientTableNameShort = GWDataDB.GetTableName(GWDataDBTable.Patient);
                string strOrderTableNameShort = GWDataDB.GetTableName(GWDataDBTable.Order);
                string strReportTableNameShort = GWDataDB.GetTableName(GWDataDBTable.Report);

                sb.AppendLine("IF @EVENT_TYPE='" + et.ETValue + "'");
                sb.AppendLine("BEGIN");
                sb.AppendLine("");

                sb.AppendLine(" select @S1 = ").Append(et.MatchCriteria.GetSQLStatement(sVName)).AppendLine()
                    .Append(" from ").Append(sInbound_IndexTable).Append(" as [").Append(strIndexTableNameShort).AppendLine("]")
                    .Append(" left join ").Append(sInbound_PatientTable).Append(" as [").Append(strPatientTableNameShort).Append("] ON [").Append(strIndexTableNameShort).Append("].DATA_ID = [").Append(strPatientTableNameShort).Append("].DATA_ID").AppendLine()
                    .Append(" left join ").Append(sInbound_OrderTable).Append(" as [").Append(strOrderTableNameShort).Append("] ON [").Append(strIndexTableNameShort).Append("].DATA_ID = [").Append(strOrderTableNameShort).Append("].DATA_ID").AppendLine()
                    .Append(" left join ").Append(sInbound_ReportTable).Append(" as [").Append(strReportTableNameShort).Append("] ON [").Append(strIndexTableNameShort).Append("].DATA_ID = [").Append(strReportTableNameShort).Append("].DATA_ID").AppendLine()
                    .Append(" WHERE [").Append(strIndexTableNameShort).Append("].DATA_ID = @In_Data_ID ;").AppendLine();

                sb.AppendLine("   SET @WHERE = ''");
                sb.AppendLine("   IF @S1 IS NOT NULL");
                sb.AppendLine("      SET @WHERE = @WHERE+@S1");
                sb.AppendLine("   IF @WHERE != ''");
                sb.AppendLine("      SET @WHERE = ' WHERE '+@WHERE");
                sb.AppendLine("");

                sb.AppendLine("   SET @stm = N'insert #match_data_ids select DATAINDEX_DATA_ID from " + sVName + "' + @WHERE;");
                sb.AppendLine("   exec sp_executesql @stm;");

                #region --Update Index --

                sb.Append("  UPDATE ").Append(sVName).AppendLine();
                sb.AppendLine("\t SET DATAINDEX_PROCESS_FLAG = 0");
                foreach (MergeFieldMapping map in et.MergeFieldMappings)
                {
                    if (map.OutboundTable == GWDataDBTable.Index.ToString())
                    {
                        sb.AppendLine("\t," + GWDataDB.GetTableName(GWDataDBTable.Index) + "_" + map.OutboundField + "=(select " + map.InboundField + " from " + GWDataDB.GetTableName(ch.INameInbound, (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), map.InboundTable)) + " where DATA_ID = @In_Data_ID)");
                    }                    
                }

                sb.AppendLine(" WHERE DATAINDEX_DATA_ID in (select Data_ID from #match_data_ids)");
                sb.AppendLine();

                #endregion

                #region --Update Patient--

                if (et.MergeFieldMappings.HasMappings(GWDataDBTable.Patient.ToString()))
                {
                    sb.Append("  UPDATE ").Append(sVName).AppendLine();
                    sb.AppendLine("\t SET ");

                    foreach (MergeFieldMapping map in et.MergeFieldMappings)
                    {
                        if (map.OutboundTable == GWDataDBTable.Patient.ToString())
                        {
                            sb.AppendLine("\t" + GWDataDB.GetTableName(GWDataDBTable.Patient) + "_" + map.OutboundField + "=(select " + map.InboundField + " from " + GWDataDB.GetTableName(ch.INameInbound, (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), map.InboundTable)) + " where DATA_ID = @In_Data_ID),");
                        }
                    }
                    sb.Remove(sb.Length - 3, 3);
                    sb.AppendLine();
                    sb.AppendLine(" WHERE DATAINDEX_DATA_ID in (select Data_ID from #match_data_ids)");
                }
                
                #endregion

                #region --Update ORDER--

                if (et.MergeFieldMappings.HasMappings(GWDataDBTable.Order.ToString()))
                {
                    sb.Append("  UPDATE ").Append(sVName).AppendLine();
                    sb.AppendLine("\t SET ");

                    foreach (MergeFieldMapping map in et.MergeFieldMappings)
                    {
                        if (map.OutboundTable == GWDataDBTable.Order.ToString())
                        {
                            sb.AppendLine("\t" + GWDataDB.GetTableName(GWDataDBTable.Order) + "_" + map.OutboundField + "=(select " + map.InboundField + " from " + GWDataDB.GetTableName(ch.INameInbound, (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), map.InboundTable)) + " where DATA_ID = @In_Data_ID),");
                        }
                    }
                    sb.Remove(sb.Length - 3, 3);
                    sb.AppendLine();
                    sb.AppendLine(" WHERE DATAINDEX_DATA_ID in (select Data_ID from #match_data_ids)");
                }

                #endregion

                #region --Update Report--

                if (et.MergeFieldMappings.HasMappings(GWDataDBTable.Report.ToString()))
                {
                    sb.Append("  UPDATE ").Append(sVName).AppendLine();
                    sb.AppendLine("\t SET ");

                    foreach (MergeFieldMapping map in et.MergeFieldMappings)
                    {
                        if (map.OutboundTable == GWDataDBTable.Report.ToString())
                        {
                            sb.AppendLine("\t" + GWDataDB.GetTableName(GWDataDBTable.Report) + "_" + map.OutboundField + "=(select " + map.InboundField + " from " + GWDataDB.GetTableName(ch.INameInbound, (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), map.InboundTable)) + " where DATA_ID = @In_Data_ID),");
                        }
                    }
                    sb.Remove(sb.Length - 3, 3);
                    sb.AppendLine();
                    sb.AppendLine(" WHERE DATAINDEX_DATA_ID in (select Data_ID from #match_data_ids)");
                }

                #endregion
                sb.AppendLine(" drop table #match_data_ids;");
                sb.AppendLine(" return");
                sb.AppendLine("END");
            }

            #endregion

            #region _MergeFields END
            sb.AppendLine("END");
            sb.AppendLine("GO\r\n");
            #endregion
            return sb.ToString();
        }

            #endregion

        #region --OLD--

        //private string Procedure_PKIsExisted_InstallScript(IOChannel ch)
        //{

        //    StringBuilder sb = new StringBuilder();

        //    string sPName = ch.INameInbound + "_" + OutboundDBConfigMgt.Config.INameOutbound + "_PKIsExisted";
        //    string sInbound_Index_Table = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Index);
        //    string sInbound_Patient_Table = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Patient);
        //    string sInbound_Order_Table = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Order);
        //    string sInbound_Report_Table = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Report);

        //    #region Drop _PKIsExisted

        //    sb.AppendLine("IF OBJECT_ID ( '" + sPName + "', 'P' ) IS NOT NULL ");
        //    sb.AppendLine("DROP PROCEDURE " + sPName);
        //    sb.AppendLine("GO\r\n");

        //    #endregion

        //    #region _PKIsExisted declaration
        //    sb.AppendLine("CREATE procedure " + sPName);
        //    sb.AppendLine("(");
        //    sb.AppendLine("    @In_Data_ID  varchar(max) ");
        //    sb.AppendLine("   ,@Out_Data_ID varchar(max) output ");
        //    sb.AppendLine("   ,@ret bit output  ");
        //    sb.AppendLine(")");
        //    sb.AppendLine("AS");
        //    #endregion

        //    #region _PKIsExisted BEGIN
        //    sb.AppendLine("BEGIN");
        //    #endregion

        //    #region Query Criteria
        //    #region PreTreat

        //    sb.AppendLine("");
        //    sb.AppendLine("DECLARE @EVENT_TYPE varchar(max) ");
        //    string sInbound_IndexTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Index);
        //    string sInbound_PatientTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Patient);
        //    string sInbound_OrderTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Order);
        //    string sInbound_ReportTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Report);

        //    string sOutbound_IndexTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Index);
        //    string sOutbound_PatientTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Patient);
        //    string sOutbound_OrderTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Order);
        //    string sOutbound_ReportTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Report);

        //    sb.AppendLine("SELECT @EVENT_TYPE=" + GWDataDBField.i_EventType.FieldName + " from " + sInbound_IndexTable);
        //    sb.AppendLine(" WHERE " + GWDataDBField.i_IndexGuid.FieldName + "=@In_Data_ID");

        //    List<PKField> Index_Fields = new List<PKField>();
        //    List<PKField> Patient_Fields = new List<PKField>();
        //    List<PKField> Order_Fields = new List<PKField>();
        //    List<PKField> Report_Fields = new List<PKField>();

        //    Index_Fields.Clear();
        //    Patient_Fields.Clear();
        //    Order_Fields.Clear();
        //    Report_Fields.Clear();

        //    sb.AppendLine("declare @stm nvarchar(max)");
        //    sb.AppendLine("   declare @temp varchar(max)");
        //    sb.AppendLine("declare @where nvarchar(max)");
        //    sb.AppendLine("DECLARE @S1 NVARCHAR(MAX) -- DATAINDEX");
        //    sb.AppendLine("DECLARE @S2 NVARCHAR(MAX) -- PATIENT");
        //    sb.AppendLine("DECLARE @S3 NVARCHAR(MAX) -- ORDER");
        //    sb.AppendLine("DECLARE @S4 NVARCHAR(MAX) -- REPORT");

        //    #endregion

        //    foreach (EventType et in ch.EventTypeList)
        //    {
        //        sb.AppendLine(" IF @EVENT_TYPE='" + et.ETValue.Trim()+"'");
        //        sb.AppendLine(" BEGIN");

        //        foreach (PKField f in et.PKFields)
        //        {
        //            if (f.Table.Trim().ToUpper() == GWDataDB.GetTableName(GWDataDBTable.Index).ToUpper())
        //                Index_Fields.Add(f);
        //            else if (f.Table.Trim().ToUpper() == GWDataDB.GetTableName(GWDataDBTable.Patient).ToUpper())
        //                Patient_Fields.Add(f);
        //            else if (f.Table.Trim().ToUpper() == GWDataDB.GetTableName(GWDataDBTable.Order).ToUpper())
        //                Order_Fields.Add(f);
        //            else if (f.Table.Trim().ToUpper() == GWDataDB.GetTableName(GWDataDBTable.Report).ToUpper())
        //                Report_Fields.Add(f);
        //        }



        //        sb.AppendLine("");
        //        sb.AppendLine("-- Build @where string -----begin--");
        //        sb.AppendLine("--set @where = N'process_flag=1'	");
        //        sb.AppendLine("--SELECT * FROM %Inbound_IFName%_DATAINDEX");


        //        bool bHaveFirst = false;

        //        string sVField;
        //        string sTField;

        //        #region Index Criteria


        //        if (Index_Fields.Count > 0)
        //        {
        //            sb.AppendLine(" SELECT @S1=");

        //            for (int i = 0; i < Index_Fields.Count; i++)
        //            {
        //                if (i > 0)
        //                    sb.Append("+");
        //                if (bHaveFirst == true)
        //                    sb.Append(" N' AND '+ ");
        //                else
        //                    bHaveFirst = true;

        //                sVField = GWDataDB.GetTableName(GWDataDBTable.Index) + "_" + Index_Fields[i].FieldName;
        //                sTField = Index_Fields[i].FieldName;

        //                //sb.AppendLine(" N' AND " + sVField + "'");
        //                sb.AppendLine(" N'  " + sVField + "'");
        //                sb.AppendLine("+CASE WHEN " + sTField + " IS NULL THEN N' IS NULL '");
        //                sb.AppendLine(" ELSE N'='+" + "''''+" + sTField + "+''''");
        //                sb.AppendLine(" END ");
        //                sb.AppendLine("");
        //            }

        //            //add eventtype criteria
        //            //if (i > 0)
        //            //    sb.Append("+");
        //            //if (bHaveFirst == true)
        //            //    sb.Append(" N' AND '+ ");
        //            //else
        //            //    bHaveFirst = true;

        //            //sVField = GWDataDB.GetTableName(GWDataDBTable.Index) + "_" + GWDataDBField.i_EventType.FieldName;
        //            //sTField = GWDataDBField.i_EventType.FieldName;
        //            //sb.AppendLine(" N'  " + sVField + "'");
        //            //sb.AppendLine("+CASE WHEN " + sTField + " IS NULL THEN N' IS NULL '");
        //            //sb.AppendLine(" ELSE N'='+" + "''''+" + sTField + "+''''");
        //            //sb.AppendLine(" END ");
        //            //sb.AppendLine("");

        //            sb.AppendLine(" FROM " + sInbound_Index_Table);
        //            sb.AppendLine(" WHERE " + GWDataDBField.i_IndexGuid.FieldName + " = @IN_DATA_ID");
        //            sb.AppendLine("");
        //        }
        //        #endregion

        //        #region Patient Criteria

        //        if (Patient_Fields.Count > 0)
        //        {
        //            sb.AppendLine(" SELECT @S2=");

        //            for (int i = 0; i < Patient_Fields.Count; i++)
        //            {
        //                if (i > 0)
        //                    sb.Append("+");
        //                if (bHaveFirst == true)
        //                    sb.Append(" N' AND '+ ");
        //                else
        //                    bHaveFirst = true;

        //                sVField = GWDataDB.GetTableName(GWDataDBTable.Patient) + "_" + Patient_Fields[i].FieldName;
        //                sTField = Patient_Fields[i].FieldName;

        //                //sb.AppendLine(" N' AND " + sVField + "'");
        //                sb.AppendLine(" N'  " + sVField + "'");
        //                sb.AppendLine("+CASE WHEN " + sTField + " IS NULL THEN N' IS NULL '");
        //                sb.AppendLine(" ELSE N'='+" + "''''+" + sTField + "+''''");
        //                sb.AppendLine(" END ");
        //                sb.AppendLine("");
        //            }

        //            sb.AppendLine(" FROM " + sInbound_Patient_Table);
        //            sb.AppendLine(" WHERE " + GWDataDBField.p_DATA_ID.FieldName + " = @IN_DATA_ID");
        //            sb.AppendLine("");
        //        }
        //        #endregion

        //        #region Order Criteria

        //        if (Order_Fields.Count > 0)
        //        {
        //            sb.AppendLine(" SELECT @S3=");

        //            for (int i = 0; i < Order_Fields.Count; i++)
        //            {
        //                if (i > 0)
        //                    sb.Append("+");
        //                if (bHaveFirst == true)
        //                    sb.Append(" N' AND '+ ");
        //                else
        //                    bHaveFirst = true;

        //                sVField = GWDataDB.GetTableName(GWDataDBTable.Order) + "_" + Order_Fields[i].FieldName;
        //                sTField = Order_Fields[i].FieldName;

        //                sb.AppendLine(" N'  " + sVField + "'");
        //                sb.AppendLine("+CASE WHEN " + sTField + " IS NULL THEN N' IS NULL '");
        //                sb.AppendLine(" ELSE N'='+" + "''''+" + sTField + "+''''");
        //                sb.AppendLine(" END ");
        //            }

        //            sb.AppendLine(" FROM " + sInbound_Order_Table);
        //            sb.AppendLine(" WHERE " + GWDataDBField.o_DATA_ID.FieldName + " = @IN_DATA_ID");
        //            sb.AppendLine("");
        //        }
        //        #endregion

        //        #region Report Criteria

        //        if (Report_Fields.Count > 0)
        //        {
        //            sb.AppendLine(" SELECT @S4=");

        //            for (int i = 0; i < Report_Fields.Count; i++)
        //            {
        //                if (i > 0)
        //                    sb.Append("+");
        //                if (bHaveFirst == true)
        //                    sb.Append(" N' AND '+ ");
        //                else
        //                    bHaveFirst = true;

        //                sVField = GWDataDB.GetTableName(GWDataDBTable.Report) + "_" + Report_Fields[i].FieldName;
        //                sTField = Report_Fields[i].FieldName;

        //                sb.AppendLine(" N' " + sVField + "'");
        //                sb.AppendLine("+CASE WHEN " + sTField + " IS NULL THEN N' IS NULL '");
        //                sb.AppendLine(" ELSE N'='+" + "''''+" + sTField + "+''''");
        //                sb.AppendLine(" END ");
        //            }

        //            sb.AppendLine(" FROM " + sInbound_Report_Table);
        //            sb.AppendLine(" WHERE " + GWDataDBField.r_DATA_ID.FieldName + " = @IN_DATA_ID");
        //            sb.AppendLine("");
        //        }
        //        #endregion

        //        #region Execute SQL
        //        sb.AppendLine("SET @WHERE = ''");
        //        sb.AppendLine("   IF @S1 IS NOT NULL");
        //        sb.AppendLine("      SET @WHERE = @WHERE+@S1");
        //        sb.AppendLine("   IF @S2 IS NOT NULL");
        //        sb.AppendLine("      SET @WHERE = @WHERE+@S2");
        //        sb.AppendLine("   IF @S3 IS NOT NULL");
        //        sb.AppendLine("      SET @WHERE = @WHERE+@S3");
        //        sb.AppendLine("   IF @S4 IS NOT NULL");
        //        sb.AppendLine("      SET @WHERE = @WHERE+@S4");
        //        sb.AppendLine(" ");
        //        sb.AppendLine("   IF @WHERE != ''");
        //        sb.AppendLine("      SET @WHERE = ' WHERE '+@WHERE");
        //        sb.AppendLine("");
        //        sb.AppendLine("   ------------BUILD WHERE End----");
        //        sb.AppendLine("   ");
        //        sb.AppendLine("   set @stm = N'select @out_data_id =" + GWDataDB.GetTableName(GWDataDBTable.Index) + "_" + GWDataDBField.i_IndexGuid.FieldName + "  from V_" + OutboundDBConfigMgt.Config.INameOutbound + "'+ @where ");

        //        sb.AppendLine("   exec sp_executesql @stm,");
        //        sb.AppendLine("        N'@out_data_id as varchar(max) output',");
        //        sb.AppendLine("        @out_data_id = @temp output");
        //        sb.AppendLine("   ");
        //        sb.AppendLine("   if @temp is null");
        //        sb.AppendLine("   begin");
        //        sb.AppendLine("     Set @ret = 0");
        //        sb.AppendLine("   end	");
        //        sb.AppendLine("   else begin");
        //        sb.AppendLine("     set @out_data_id = @temp");
        //        sb.AppendLine("     set @ret = 1");
        //        sb.AppendLine("   end   ");
        //        sb.AppendLine(" return");
        //        #endregion

        //        sb.AppendLine("End");

        //    }
        //    #endregion

        //    #region  END
        //    sb.AppendLine("END");
        //    sb.AppendLine("GO\r\n");
        //    #endregion

        //    return sb.ToString();
        //}

        //public string Procedure_MergeFields_InstallScript(IOChannel ch)
        //{

        //    StringBuilder sb = new StringBuilder();

        //    string sPName = ch.INameInbound + "_" + OutboundDBConfigMgt.Config.INameOutbound + "_MergeFields";

        //    #region Drop MergeFields

        //    sb.AppendLine("IF OBJECT_ID ( '" + sPName + "', 'P' ) IS NOT NULL ");
        //    sb.AppendLine("DROP PROCEDURE " + sPName);
        //    sb.AppendLine("GO\r\n");
        //    #endregion

        //    #region _MergeFields declaration
        //    sb.AppendLine("CREATE procedure " + sPName);
        //    sb.AppendLine("(");
        //    sb.AppendLine("    @In_Data_ID  varchar(max) ");
        //    sb.AppendLine("   ,@Out_Data_ID varchar(max) ");
        //    sb.AppendLine("   ,@ret bit output  ");
        //    sb.AppendLine(")");
        //    sb.AppendLine("AS");
        //    #endregion

        //    #region _MergeFields BEGIN
        //    sb.AppendLine("BEGIN");
        //    #endregion

        //    #region Merge Fields
        //    sb.AppendLine("");
        //    sb.AppendLine("DECLARE @EVENT_TYPE varchar(max) ");

        //    string sInbound_IndexTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Index);
        //    string sInbound_PatientTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Patient);
        //    string sInbound_OrderTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Order);
        //    string sInbound_ReportTable = GWDataDB.GetTableName(ch.INameInbound, GWDataDBTable.Report);

        //    string sOutbound_IndexTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Index);
        //    string sOutbound_PatientTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Patient);
        //    string sOutbound_OrderTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Order);
        //    string sOutbound_ReportTable = GWDataDB.GetTableName(OutboundDBConfigMgt.Config.INameOutbound, GWDataDBTable.Report);

        //    sb.AppendLine("SELECT @EVENT_TYPE=" + GWDataDBField.i_EventType.FieldName + " from " + sInbound_IndexTable);
        //    sb.AppendLine(" WHERE " + GWDataDBField.i_IndexGuid.FieldName + "=@In_Data_ID");

        //    sb.AppendLine("--Merging Fields");

        //    foreach (EventType et in ch.EventTypeList)
        //    {
        //        if (!et.Merging) continue;

        //        sb.AppendLine("IF @EVENT_TYPE='" + et.ETValue+"'");
        //        sb.AppendLine("BEGIN");
        //        sb.AppendLine("");

        //        List<MergeField> Index_Fields = new List<MergeField>();
        //        List<MergeField> Patient_Fields = new List<MergeField>();
        //        List<MergeField> Order_Fields = new List<MergeField>();
        //        List<MergeField> Report_Fields = new List<MergeField>();

        //        Index_Fields.Clear();
        //        Patient_Fields.Clear();
        //        Order_Fields.Clear();
        //        Report_Fields.Clear();
        //        foreach (MergeField f in et.MergeFields)
        //        {
        //            if (f.Table.Trim().ToUpper() == GWDataDB.GetTableName(GWDataDBTable.Index))
        //                Index_Fields.Add(f);
        //            else if (f.Table.Trim().ToUpper() == GWDataDB.GetTableName(GWDataDBTable.Patient))
        //                Patient_Fields.Add(f);
        //            else if (f.Table.Trim().ToUpper() == GWDataDB.GetTableName(GWDataDBTable.Order))
        //                Order_Fields.Add(f);
        //            else if (f.Table.Trim().ToUpper() == GWDataDB.GetTableName(GWDataDBTable.Report))
        //                Report_Fields.Add(f);
        //        }


        //        #region Merge Index
        //        if (Index_Fields.Count > 0)
        //        {
        //            sb.AppendLine(" update " + sOutbound_IndexTable + " SET ");

        //            foreach (MergeField f in Index_Fields)
        //            {
        //                sb.AppendLine("\t" + f.FieldName + "=(select " + f.FieldName + " from " + sInbound_IndexTable + " where " + GWDataDBField.i_IndexGuid.FieldName + "=@In_Data_ID),");
        //            }

        //            sb.Remove(sb.Length - 3, 3); //remove ",\r\n"

        //            sb.AppendLine("\r\n WHERE " + GWDataDBField.i_IndexGuid.FieldName + "=@Out_Data_ID");
        //            sb.AppendLine("");
        //        }
        //        #endregion

        //        #region Merge Patient
        //        if (Patient_Fields.Count > 0)
        //        {
        //            sb.AppendLine(" IF(Exists(select * from " + sOutbound_PatientTable + " where " + GWDataDBField.p_DATA_ID.FieldName + "=@Out_Data_ID))");
        //            sb.AppendLine(" BEGIN ");
        //            sb.AppendLine(" update " + sOutbound_PatientTable + " SET ");
        //            foreach (MergeField f in Patient_Fields)
        //            {
        //                sb.AppendLine("\t" + f.FieldName + "=(select " + f.FieldName + " from " + sInbound_PatientTable + " where " + GWDataDBField.p_DATA_ID.FieldName + "=@In_Data_ID),");
        //            }

        //            sb.Remove(sb.Length - 3, 3); //remove ",\r\n"

        //            sb.AppendLine("\r\n WHERE " + GWDataDBField.p_DATA_ID.FieldName + "=@Out_Data_ID");
        //            sb.AppendLine(" END");

        //            sb.AppendLine("ELSE");
        //            sb.AppendLine("   INSERT INTO " + sOutbound_PatientTable + " SELECT * FROM " + sInbound_PatientTable + " where " + GWDataDBField.p_DATA_ID.FieldName + "=@In_Data_ID");
        //        }
        //        #endregion

        //        #region Merge order
        //        if (Order_Fields.Count > 0)
        //        {
        //            sb.AppendLine(" IF(Exists(select * from " + sOutbound_OrderTable + " where " + GWDataDBField.o_DATA_ID.FieldName + "=@Out_Data_ID))");

        //            sb.AppendLine(" BEGIN ");

        //            sb.AppendLine(" update " + sOutbound_OrderTable + " SET ");

        //            foreach (MergeField f in Order_Fields)
        //            {
        //                sb.AppendLine("\t" + f.FieldName + "=(select " + f.FieldName + " from " + sInbound_OrderTable + " where " + GWDataDBField.o_DATA_ID.FieldName + "=@In_Data_ID),");
        //            }

        //            sb.Remove(sb.Length - 3, 3); //remove ",\r\n"

        //            sb.AppendLine("\r\n WHERE " + GWDataDBField.o_DATA_ID.FieldName + "=@Out_Data_ID");

        //            sb.AppendLine(" END");

        //            sb.AppendLine("ELSE");

        //            sb.AppendLine("   INSERT INTO " + sOutbound_OrderTable + " SELECT * FROM " + sInbound_OrderTable + " where " + GWDataDBField.o_DATA_ID.FieldName + "=@In_Data_ID");

        //        }
        //        #endregion

        //        #region Merge Report

        //        if (Report_Fields.Count > 0)
        //        {
        //            sb.AppendLine(" IF(Exists(select * from " + sOutbound_ReportTable + " where " + GWDataDBField.r_DATA_ID.FieldName + "=@Out_Data_ID))");

        //            sb.AppendLine(" BEGIN ");

        //            sb.AppendLine(" update " + sOutbound_ReportTable + " SET ");

        //            foreach (MergeField f in Report_Fields)
        //            {
        //                sb.AppendLine("\t" + f.FieldName + "=(select " + f.FieldName + " from " + sInbound_ReportTable + " where " + GWDataDBField.r_DATA_ID.FieldName + "=@In_Data_ID),");
        //            }

        //            sb.Remove(sb.Length - 3, 3); //remove ",\r\n"

        //            sb.AppendLine("\r\n WHERE " + GWDataDBField.r_DATA_ID.FieldName + "=@Out_Data_ID");

        //            sb.AppendLine(" END");

        //            sb.AppendLine("ELSE");

        //            sb.AppendLine("   INSERT INTO " + sOutbound_ReportTable + " SELECT * FROM " + sInbound_ReportTable + " where " + GWDataDBField.r_DATA_ID.FieldName + "=@In_Data_ID");

        //        }
        //        #endregion

        //        #region Update Index
        //        sb.AppendLine("update " + sOutbound_IndexTable + " set ");

        //        sb.AppendLine("\t" + GWDataDBField.i_PROCESS_FLAG.FieldName + "=0 ");

        //        //sb.AppendLine("\t" + GWDataDBField.i_EventType.FieldName + "=(select " + GWDataDBField.i_EventType.FieldName + " from " + sInbound_IndexTable + " where " + GWDataDBField.i_IndexGuid.FieldName + "=@In_Data_ID)");

        //        sb.AppendLine(" where " + GWDataDBField.i_IndexGuid.FieldName + "=@Out_Data_ID");
        //        sb.AppendLine("");
        //        #endregion

        //        sb.AppendLine("");
        //        sb.AppendLine("END");
        //    }

        //    #endregion

        //    #region _MergeFields END
        //    sb.AppendLine("END");
        //    sb.AppendLine("GO\r\n");
        //    #endregion
        //    return sb.ToString();
        //}

        #endregion

        public string Procedure_InsertRecord_InstallScript(IOChannel ch)
        {
            #region Assistant
            string sInbound_IFName = ch.INameInbound;
            string sOutbound_IFName = OutboundDBConfigMgt.Config.INameOutbound;
            StringBuilder sb = new StringBuilder();
            #endregion

            #region Drop
            sb.Append(Procedure_InsertRecord_UninstallScript(ch));
            #endregion

            #region Declaration
            sb.AppendLine("CREATE procedure " + sInbound_IFName + "_" + sOutbound_IFName + "_InsertRecord ");
            sb.AppendLine("(");
            sb.AppendLine("	-- Add the parameters for the function here");
            sb.AppendLine("	@Data_ID varchar(max)");
            sb.AppendLine(",   @ret bit output");
            sb.AppendLine(")");
            sb.AppendLine("AS");
            sb.AppendLine("");
            #endregion

            #region BEGIN
            sb.AppendLine("BEGIN");
            #endregion

            #region Insert Record
            sb.AppendLine("INSERT INTO " + GWDataDB.GetTableName(sOutbound_IFName, GWDataDBTable.Report) + " SELECT * FROM " + GWDataDB.GetTableName(sInbound_IFName, GWDataDBTable.Report) + " WHERE " + GWDataDBField.i_IndexGuid.FieldName + " = @data_ID ");
            sb.AppendLine("INSERT INTO " + GWDataDB.GetTableName(sOutbound_IFName, GWDataDBTable.Order) + " SELECT * FROM " + GWDataDB.GetTableName(sInbound_IFName, GWDataDBTable.Order) + " WHERE " + GWDataDBField.i_IndexGuid.FieldName + " = @data_ID ");
            sb.AppendLine("INSERT INTO " + GWDataDB.GetTableName(sOutbound_IFName, GWDataDBTable.Patient) + " SELECT * FROM " + GWDataDB.GetTableName(sInbound_IFName, GWDataDBTable.Patient) + " WHERE " + GWDataDBField.i_IndexGuid.FieldName + " = @data_ID ");
            sb.AppendLine("INSERT INTO " + GWDataDB.GetTableName(sOutbound_IFName, GWDataDBTable.Index) + " SELECT * FROM " + GWDataDB.GetTableName(sInbound_IFName, GWDataDBTable.Index) + " WHERE " + GWDataDBField.i_IndexGuid.FieldName + " = @data_ID ");
            sb.AppendLine("set @ret = 1 ");
            #endregion

            #region End
            sb.AppendLine("End");
            sb.AppendLine("GO\r\n");
            #endregion

            return sb.ToString();
        }

        public string Procedure_InsertRecord_UninstallScript(IOChannel ch)
        {

            StringBuilder sb = new StringBuilder();

            string sPName = ch.INameInbound + "_" + OutboundDBConfigMgt.Config.INameOutbound + "_InsertRecord";

            #region Drop InsertRecord

            sb.AppendLine("IF OBJECT_ID ( '" + sPName + "', 'P' ) IS NOT NULL ");
            sb.AppendLine("DROP PROCEDURE " + sPName);
            sb.AppendLine("GO\r\n");
            #endregion

            return sb.ToString();
        }

        public string Trigger_InstallScript(IOChannel ch)
        {
            string sInbound_IFName = ch.INameInbound;
            string sOutbound_IFName = OutboundDBConfigMgt.Config.INameOutbound;

            StringBuilder sb = new StringBuilder();

            #region Uninstall Trigger
            sb.Append(Trigger_UninstallScript(ch));
            #endregion

            #region Declaration
            sb.AppendLine("CREATE TRIGGER dbo.[" + sInbound_IFName + "_" + sOutbound_IFName + "_Trigger]");
            sb.AppendLine("   ON  dbo." + GWDataDB.GetTableName(sInbound_IFName, GWDataDBTable.Index) + "  AFTER INSERT");
            sb.AppendLine("AS ");
            #endregion

            #region BEGIN
            sb.AppendLine("BEGIN ");
            #endregion

            #region @In_Data_ID,@Event_Type
            sb.AppendLine("-- SET NOCOUNT ON added to prevent extra result sets from ");
            sb.AppendLine("-- interfering with SELECT statements. ");
            sb.AppendLine("SET NOCOUNT ON; ");
            sb.AppendLine(" ");
            sb.AppendLine("DECLARE @In_Data_ID varchar(max) ");
            sb.AppendLine("DECLARE @OUT_Data_ID varchar(max) ");
            sb.AppendLine("DECLARE @Event_Type varchar(max) ");
            sb.AppendLine("-- Insert statements for trigger here ");
            sb.AppendLine("select @In_Data_ID = inserted.Data_id,@Event_Type=rtrim(ltrim(inserted.event_type)) from inserted ");

            StringBuilder sb_select = new StringBuilder();
            #region Add JOIN
            List<String> TableList = new List<string>();
            TableList.Add("INDEX");
            if (ch.EventTypeList.Count > 0)
            {

                for (int i = 0; i < ch.EventTypeList.Count; i++)
                {
                    EventType et = (EventType)ch.EventTypeList[i];
                    if (!et.Enabled)
                    {
                        continue;
                    }

                    if (et.FilterList.Count > 0)
                    {
                        foreach (SubFilterFieldList sffl in et.FilterList)
                        {
                            foreach (FilterField ff in sffl)
                            {
                                if (TableList.IndexOf(ff.Table) < 0)
                                {
                                    TableList.Add(ff.Table);
                                }

                            }
                        }
                    }
                }

                if (TableList.Count > 1)
                {

                    foreach (String s in TableList)
                    {
                        if (!s.ToUpper().Equals("INDEX"))
                        {
                            sb_select.Append(" FULL JOIN ");
                            sb_select.Append(sInbound_IFName + "_" + GWDataDB.GetTableName((GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), s)) + " ON inserted.DATA_ID = " + sInbound_IFName + "_" + GWDataDB.GetTableName((GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), s)) + ".DATA_ID ");
                        }
                    }


                }
            #endregion

                #region Add Where
                sb_select.Append(" WHERE ");
                int EventNo = 0;
                for (int i = 0; i < ch.EventTypeList.Count; i++)
                {
                    EventType et = (EventType)ch.EventTypeList[i];
                    if (!et.Enabled)
                    {
                        continue;
                    }
                    EventNo++;

                    if (EventNo > 1)
                    {
                        sb_select.Append(" OR ");
                    }

                    sb_select.Append("( inserted.Event_Type = N'" + et.ETValue + "' ");
                    if (et.FilterList.Count > 0)
                    {
                        sb_select.Append(" AND (");
                        for (int j = 0; j < et.FilterList.Count; j++)
                        {
                            if (j > 0)
                            {
                                sb_select.Append(" AND (");
                            }
                            SubFilterFieldList sffl = (SubFilterFieldList)et.FilterList[j];

                            if (sffl.Count > 0)
                            {
                                for (int k = 0; k < sffl.Count; k++)
                                {
                                    if (k > 0)
                                    {
                                        sb_select.Append(" OR ");
                                    }

                                    FilterField ff = (FilterField)sffl[k];
                                    if (!ff.Table.ToUpper().Equals("INDEX"))
                                    {
                                        sb_select.Append(sInbound_IFName + "_" + GWDataDB.GetTableName((GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), ff.Table)) + "." + ff.Field + " " + ff.Logic + "'" + ff.LogicValue + "' ");
                                    }
                                    else
                                    {
                                        sb_select.Append("inserted." + ff.Field + " " + ff.Logic + "'" + ff.LogicValue + "' ");
                                    }                                    
                                }
                            }

                            if (j > 0)
                                sb_select.Append(")");
                        }

                        sb_select.Append(")");
                    }
                    sb_select.Append(")");
                }


            }
            sb.AppendLine(sb_select.ToString());
                #endregion

            sb.AppendLine(" ");
            #endregion

            /*
            #region EventType is need treat?

            string EventTypeMatch = "";
            foreach (EventType item in ch.EventTypeList)
            {
                if (item.Enabled)
                    EventTypeMatch = EventTypeMatch + " @Event_Type='" + item.ETValue.Trim() + "' or ";
            }
            if (EventTypeMatch.Length > 4)
                EventTypeMatch = EventTypeMatch.Substring(0, EventTypeMatch.Length - 4);
                        
            sb.AppendLine("-- Current Event Type need treat ? ");            
            sb.AppendLine("if not( "+EventTypeMatch+" ) ");
            sb.AppendLine("begin ");
            sb.AppendLine("  return ");
            sb.AppendLine("end ");
            sb.AppendLine(" ");
            #endregion
            */

            #region IsRedundant

            string rem = null;
            if (ch.CheckRedundancy) rem = "";
            else rem = "--";

            sb.AppendLine("--IsRedundant");
            sb.AppendLine("declare @ret bit ");
            sb.Append(rem).AppendLine("exec dbo." + sInbound_IFName + "_" + sOutbound_IFName + "_IsRedundant @In_Data_ID,@Out_Data_ID output,@ret output ");
            sb.AppendLine(" ");
            sb.Append(rem).AppendLine("if @ret=1 ");
            sb.Append(rem).AppendLine("begin ");
            sb.Append(rem).AppendLine("  return ");
            sb.Append(rem).AppendLine("end ");
            sb.AppendLine(" ");
            #endregion

            #region IsMerge
            StringBuilder sbIsMerging = new StringBuilder();
            //string IsMerging=""; //? @eventtype=1 or @eventtype=2 or...
            foreach (EventType et in ch.EventTypeList)
            {
                if (et.Merging)
                    sbIsMerging.Append(" @Event_Type=" + "'" + et.ETValue + "'" + " or ");
            }
            if (sbIsMerging.Length > 0)
                sbIsMerging.Remove(sbIsMerging.Length - 4, 4);
            else
                sbIsMerging.Append(" 1 = 0 ");

            sb.AppendLine("--IsMerging");
            sb.AppendLine("IF " + sbIsMerging.ToString());
            sb.AppendLine("BEGIN ");
            sb.AppendLine("    -- PKIsExisted ? ");
            sb.AppendLine("    exec dbo." + sInbound_IFName + "_" + sOutbound_IFName + "_PKIsExisted @In_Data_ID, @Out_Data_ID output, @ret output ");
            sb.AppendLine("    if @ret=1 ");
            sb.AppendLine("	      exec dbo." + sInbound_IFName + "_" + sOutbound_IFName + "_MergeFields @In_Data_ID, @Out_Data_ID,@ret output ");
            sb.AppendLine("    ELSE ");
            sb.AppendLine("       exec dbo." + sInbound_IFName + "_" + sOutbound_IFName + "_InsertRecord @In_Data_ID,@ret output ");
            sb.AppendLine("END ");
            sb.AppendLine("ELSE ");
            sb.AppendLine("BEGIN ");
            sb.AppendLine("exec dbo." + sInbound_IFName + "_" + sOutbound_IFName + "_InsertRecord @In_Data_ID,@ret output ");
            sb.AppendLine("END ");
            sb.AppendLine(" ");
            #endregion

            #region Update ProcessFlag

            sb.AppendLine("UPDATE " + GWDataDB.GetTableName(sInbound_IFName, GWDataDBTable.Index) + " SET " + GWDataDBField.i_PROCESS_FLAG.FieldName + "=1 WHERE " + GWDataDBField.i_IndexGuid.FieldName + "=@In_DATA_ID ");
            sb.AppendLine("UPDATE " + GWDataDB.GetTableName(sOutbound_IFName, GWDataDBTable.Index) + " SET " + GWDataDBField.i_PROCESS_FLAG.FieldName + "=0 WHERE " + GWDataDBField.i_IndexGuid.FieldName + "=@In_DATA_ID ");
            sb.AppendLine(" ");

            #endregion

            #region END
            sb.AppendLine("END ");
            sb.AppendLine("GO\r\n");
            #endregion

            return sb.ToString();
        }

        public string Trigger_UninstallScript(IOChannel ch)
        {
            StringBuilder sb = new StringBuilder();

            string sTriggerName = ch.INameInbound + "_" + OutboundDBConfigMgt.Config.INameOutbound + "_Trigger";

            sb.AppendLine("-- Create Trigger for " + ch.INameInbound + GWDataDB.GetTableName(GWDataDBTable.Index));
            sb.AppendLine("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + sTriggerName + "]') )");
            sb.AppendLine("DROP Trigger [dbo].[" + sTriggerName + "]");
            sb.AppendLine("GO\r\n");

            return sb.ToString();
        }

        private string Procedure_PKIsExisted_UninstallScript(IOChannel ch)
        {

            StringBuilder sb = new StringBuilder();

            string sPName = ch.INameInbound + "_" + OutboundDBConfigMgt.Config.INameOutbound + "_PKIsExisted";

            #region Drop PKIsExisted

            sb.AppendLine("IF OBJECT_ID ( '" + sPName + "', 'P' ) IS NOT NULL ");
            sb.AppendLine("DROP PROCEDURE " + sPName);
            sb.AppendLine("GO\r\n");
            #endregion

            return sb.ToString();
        }

        private string Procedure_MergeFields_UninstallScript(IOChannel ch)
        {

            StringBuilder sb = new StringBuilder();

            string sPName = ch.INameInbound + "_" + OutboundDBConfigMgt.Config.INameOutbound + "_MergeFields";

            sb.AppendLine("IF OBJECT_ID ( '" + sPName + "', 'P' ) IS NOT NULL ");
            sb.AppendLine("DROP PROCEDURE " + sPName);
            sb.AppendLine("GO\r\n");
            return sb.ToString();
        }
        #endregion

        #region Function To Build Script File
        private string BuildInstllTriggerScript()
        {
            StringBuilder sbScript = new StringBuilder();

            #region File  Header
            sbScript.AppendLine("-- ============================================= ");
            sbScript.AppendLine(" ");
            sbScript.AppendLine("-- Author:		HYS");
            sbScript.AppendLine("-- Create date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            sbScript.AppendLine("-- Description:	Script used to create outbound database objects, include  ");
            sbScript.AppendLine("--				View,procedure, trigger ");
            sbScript.AppendLine(" ");
            sbScript.AppendLine("-- ============================================= ");
            #endregion

            #region Use xxx
            sbScript.AppendLine(GWDataDB.GetUseDataBaseSql());
            sbScript.AppendLine("GO\r\n");
            #endregion

            #region View
            sbScript.Append(View_Outbound_InstallScript());
            #endregion

            foreach (IOChannel ch in OutboundDBConfigMgt.Config.IOChannels)
            {

                #region IsRedundant
                sbScript.Append(Procedure_IsRedundant_InstallScript(ch));
                #endregion

                #region PKIsExisted
                sbScript.Append(Procedure_PKIsExisted_InstallScript(ch));
                #endregion

                #region InsertRecord
                sbScript.Append(Procedure_InsertRecord_InstallScript(ch));
                #endregion

                #region MergeFiedls
                sbScript.Append(Procedure_MergeFields_InstallScript(ch));
                #endregion

                #region Trigger
                sbScript.Append(Trigger_InstallScript(ch));
                #endregion

            }

            return sbScript.ToString();

        }

        private string BuildUninstallTriggerScript()
        {

            StringBuilder sbScript = new StringBuilder();

            #region File  Header
            sbScript.AppendLine("-- ============================================= ");
            sbScript.AppendLine(" ");
            sbScript.AppendLine("-- Author:		HYS");
            sbScript.AppendLine("-- Create date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            sbScript.AppendLine("-- Description:	Script used to drop outbound database objects, include  ");
            sbScript.AppendLine("--				View,procedure, trigger ");
            sbScript.AppendLine(" ");
            sbScript.AppendLine("-- ============================================= ");
            #endregion

            #region Use xxx
            sbScript.AppendLine(GWDataDB.GetUseDataBaseSql());
            sbScript.AppendLine("GO\r\n");
            #endregion

            foreach (IOChannel ch in OutboundDBConfigMgt.Config.IOChannels)
            {

                #region Trigger
                sbScript.Append(Trigger_UninstallScript(ch));
                #endregion

                #region MergeFiedls
                sbScript.Append(Procedure_MergeFields_UninstallScript(ch));
                #endregion

                #region InsertRecord
                sbScript.Append(Procedure_InsertRecord_UninstallScript(ch));
                #endregion

                #region PKIsExisted
                sbScript.Append(Procedure_PKIsExisted_UninstallScript(ch));
                #endregion

                #region IsRedundant
                sbScript.Append(Procedure_IsRedundant_UninstallScript(ch));
                #endregion

                #region View
                sbScript.Append(View_Outbound_UninstallScript());
                #endregion
            }

            return sbScript.ToString();

        }

        private string BuildInstallConfigDBScript()
        {
            StringBuilder sb = new StringBuilder();
            #region Header
            sb.AppendLine("-- ======================================================================== ");
            sb.AppendLine(" ");
            sb.AppendLine("-- Author:		HYS");
            sb.AppendLine("-- Create date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            sb.AppendLine("-- Description:	Script used to install outbound adapter,  ");
            sb.AppendLine("--				delete special record in GWConfigDB ");
            sb.AppendLine(" ");
            sb.AppendLine("-- ========================================================================= ");
            sb.AppendLine(" ");
            #endregion

            #region Use xx
            sb.AppendLine("Use GWConfigDB");
            sb.AppendLine("GO\r\n");
            #endregion

            #region Each channel has it's own script
            foreach (IOChannel ch in OutboundDBConfigMgt.Config.IOChannels)
            {

                sb.AppendLine("UPDATE interface  ");
                sb.AppendLine("SET    EVENT_TYPE     = '" + ch.EventTypeListStrInbound + "'");
                sb.AppendLine("WHERE  Interface_name = '" + OutboundDBConfigMgt.Config.INameOutbound + "'");
                sb.AppendLine(" ");
                sb.AppendLine("GO\r\n");
                sb.AppendLine("-- Delete record from table combination ");
                sb.AppendLine("DELETE FROM COMBINATION  ");
                sb.AppendLine("WHERE datain = '" + ch.INameInbound + "' ");
                sb.AppendLine("AND   dataout= '" + OutboundDBConfigMgt.Config.INameOutbound + "' ");
                sb.AppendLine(" ");
                sb.AppendLine("GO\r\n");
                sb.AppendLine("-- Add New record, on secord ");
                sb.AppendLine("INSERT INTO COMBINATION ");
                sb.AppendLine("(datain,dataout,data_Mapping_File) ");
                sb.AppendLine("values ");
                sb.AppendLine("('" + ch.INameInbound + "','" + OutboundDBConfigMgt.Config.INameOutbound + "','') ");

                sb.AppendLine("");
                sb.AppendLine("GO\r\n");
            }
            #endregion

            return sb.ToString();
        }

        private string BuildUnInstallConfigDBScript()
        {
            StringBuilder sb = new StringBuilder();
            #region Header
            sb.AppendLine("-- ============================================= ");
            sb.AppendLine(" ");
            sb.AppendLine("-- Author:		HYS");
            sb.AppendLine("-- Create date: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            sb.AppendLine("-- Description:	Script used to uninstall outbound adapter,  ");
            sb.AppendLine("--				delete special record in GWConfigDB ");
            sb.AppendLine(" ");
            sb.AppendLine("-- ============================================= ");
            sb.AppendLine(" ");

            #endregion

            #region Use xx
            sb.AppendLine("Use [GWConfigDB]");
            sb.AppendLine("GO\r\n");
            #endregion

            #region Each channel has it's own script
            foreach (IOChannel ch in OutboundDBConfigMgt.Config.IOChannels)
            {

                sb.AppendLine("DELETE FROM COMBINATION ");
                sb.AppendLine("WHERE datain = '" + ch.INameInbound + "'");
                sb.AppendLine("AND   dataout= '" + OutboundDBConfigMgt.Config.INameOutbound + "'");
                sb.AppendLine("");
                sb.AppendLine("GO\r\n");

            }
            #endregion

            return sb.ToString();
        }

        #endregion

        #region Old Function To Build Script File
        //private string BuildInstllTriggerScript()
        //{
        //    string fname;
        //    fname = Application.StartupPath + "\\" + InstallTriggerTemplate;

        //    StreamReader sr = File.OpenText(fname);

        //    try
        //    {
        //        string str = sr.ReadToEnd();

        //        Regex rgInbound_IFName = new Regex("%Inbound_IFName%", RegexOptions.IgnoreCase);
        //        Regex rgOutbound_IFName = new Regex("%Outbound_IFName%", RegexOptions.IgnoreCase);

        //        StringBuilder sb = new StringBuilder();
        //        foreach (IOChannel ch in IOChannels)
        //        {

        //            string sScript;
        //            sScript = rgInbound_IFName.Replace(str, ch.InboundConfig.IName);

        //            sScript = rgOutbound_IFName.Replace(sScript, IName);



        //            //%IsMerging%, %EventTypeMatch%,%ConflictTreatMethod%
        //            string IsMerging;
        //            if (ch.IOChannelSettings.Merging == true)
        //                IsMerging = "1";
        //            else
        //                IsMerging = "0";

        //            string ConflictTreatMethod = Convert.ToInt32(ch.IOChannelSettings.ConflictTreatMethod).ToString();

        //            string EventTypeMatch = "";
        //            foreach (EventType item in ch.IOChannelSettings.EventTypeList)
        //            {
        //                if (item.Enabled)
        //                    EventTypeMatch = EventTypeMatch + " @Event_Type='" + item.ETValue.Trim() + "' or ";
        //            }
        //            if (EventTypeMatch.Length > 4)
        //                EventTypeMatch = EventTypeMatch.Substring(0, EventTypeMatch.Length - 4);


        //            Regex rgEventTypeMatch = new Regex("%EventTypeMatch%", RegexOptions.IgnoreCase);
        //            sScript = rgEventTypeMatch.Replace(sScript, EventTypeMatch);
        //            Regex rgConflictTreatMethod = new Regex("%ConflictTreatMethod%", RegexOptions.IgnoreCase);
        //            sScript = rgConflictTreatMethod.Replace(sScript, ConflictTreatMethod);
        //            Regex rgIsMerging = new Regex("%IsMerging%", RegexOptions.IgnoreCase);
        //            sScript = rgIsMerging.Replace(sScript, IsMerging);

        //            sb.Append(sScript);
        //        }

        //        return sb.ToString();

        //    }
        //    finally
        //    {
        //        sr.Close();

        //    }
        //}
        //private string BuildUninstallTriggerScript()
        //{
        //    StreamReader sr2 = File.OpenText(Application.StartupPath + "\\" + UninstallTriggerTemplate);

        //    try
        //    {

        //        string sTemplete = sr2.ReadToEnd();
        //        Regex rgInbound_IFName = new Regex("%Inbound_IFName%", RegexOptions.IgnoreCase);
        //        Regex rgOutbound_IFName = new Regex("%Outbound_IFName%", RegexOptions.IgnoreCase);

        //        StringBuilder sb = new StringBuilder();
        //        foreach (IOChannel ch in IOChannels)
        //        {
        //            string sScript;

        //            sScript = rgInbound_IFName.Replace(sTemplete, ch.InboundConfig.IName );

        //            sScript = rgOutbound_IFName.Replace(sScript, IName);

        //            sb.Append(sScript);
        //        }

        //        return sb.ToString(); ;


        //    }
        //    finally
        //    {
        //        sr2.Close();                
        //    }
        //}

        //private string BuildInstallConfigDBScript()
        //{
        //    string fname;
        //    fname = Application.StartupPath+"\\"+InstallConfigTemplate;

        //    StreamReader sr = File.OpenText(fname);

        //    if (File.Exists(InstallConfigFile))
        //        File.Delete(InstallConfigFile);

        //    try
        //    {

        //        string sTemplate = sr.ReadToEnd();
        //        Regex rgInbound_IFName = new Regex("%Inbound_IFName%", RegexOptions.IgnoreCase);
        //        Regex rgOutbound_IFName = new Regex("%Outbound_IFName%", RegexOptions.IgnoreCase);
        //        Regex rgOutbound_Description = new Regex("%Outbound_Description%", RegexOptions.IgnoreCase);
        //        Regex rgOutbound_EventTypeListStr = new Regex("%Outbound_EventTypeListStr%", RegexOptions.IgnoreCase);
        //        Regex rgOutbound_DEVICE_ID = new Regex("%Outbound_DEVICE_ID%", RegexOptions.IgnoreCase);

        //        StringBuilder sb = new StringBuilder();
        //        foreach (IOChannel ch in IOChannels)
        //        {
        //            string sScript;

        //            sScript = rgInbound_IFName.Replace(sTemplate, ch.InboundConfig.IName);

        //            sScript = rgOutbound_IFName.Replace(sScript, IName);

        //            sScript = rgOutbound_Description.Replace(sScript, Description);

        //            sScript = rgOutbound_EventTypeListStr.Replace(sScript, ch.IOChannelSettings.EventTypeListStr);

        //            sScript = rgOutbound_DEVICE_ID.Replace(sScript, Device_ID.ToString());

        //            sb.Append(sScript);
        //        }

        //        return sb.ToString();

        //    }
        //    finally
        //    {
        //        sr.Close();                
        //    }

        //}
        //private string BuildUnInstallConfigDBScript()
        //{
        //    StreamReader sr2 = File.OpenText(Application.StartupPath + "\\" + UninstallConfigTemplate);            
        //    try
        //    {
        //        string sTemplate = sr2.ReadToEnd();
        //        Regex rgInbound_IFName = new Regex("%Inbound_IFName%", RegexOptions.IgnoreCase);
        //        Regex rgOutbound_IFName = new Regex("%Outbound_IFName%", RegexOptions.IgnoreCase);

        //        StringBuilder sb = new StringBuilder();
        //        foreach (IOChannel ch in IOChannels)
        //        {
        //            string sScript;
        //            sScript = rgInbound_IFName.Replace(sTemplate, ch.InboundConfig.IName);
        //            sScript = rgOutbound_IFName.Replace(sScript, IName);                    
        //            sb.Append(sScript);
        //        }                
        //        return sb.ToString();

        //    }
        //    finally
        //    {
        //        sr2.Close();                
        //    }
        //}

        #endregion

    }

    //public class OutboundDBInstallConfig : XObject
    //{

    //    private OutboundConfig _OutboundConfig = new OutboundConfig();
    //    public OutboundConfig OutboundConfig
    //    {
    //        get { return _OutboundConfig; }
    //        set { _OutboundConfig = value; }
    //    }

    //}

    #region GWDatabaseInfo
    /// <summary>
    /// Load Inbound and outbound  inteface lists exist in current GWConfigDB;
    /// </summary>
    public class GWDataBaseInfo
    {
        private System.Collections.Hashtable _InboundInterfaceList = new Hashtable();
        public Hashtable InboundInterfaceList
        {
            get { return _InboundInterfaceList; }
        }

        private System.Collections.Hashtable _OutboundInterfaceList = new Hashtable();
        public Hashtable OutboundInterfaceList
        {
            get { return _OutboundInterfaceList; }
        }

        private System.Data.OleDb.OleDbConnection _conn = new System.Data.OleDb.OleDbConnection();

        private System.Data.DataSet _ds = new System.Data.DataSet();
        private System.Data.OleDb.OleDbDataAdapter _sa = new System.Data.OleDb.OleDbDataAdapter();

        private bool _IsInited = false;
        public bool IsInited
        {
            get { return _IsInited; }
        }

        public void Save(string sDBConnStr)
        {
            /*
            //INSERT INTO interface
            _conn.ConnectionString = sDBConnStr;
            _conn.Open();

            SqlCommand cmdInterface = new SqlCommand();
            cmdInterface.Connection = _conn;
            SqlCommand cmdCombination = new SqlCommand();
            cmdCombination.Connection = _conn;
            SqlTransaction tran = _conn.BeginTransaction();
            cmdInterface.Transaction = tran;
            cmdCombination.Transaction = tran;
            try
            {
                // Insert into a new record to interface from device 
                if (OutboundDBConfigMgt.IsNewOutbound)
                {
                    cmdInterface.CommandText = " INSERT INTO [GWConfigDB].[dbo].[INTERFACE]" +
                                              "([INTERFACE_NAME]" +
                                              ",[INTERFACE_DEVICE_ID]" +
                                              ",[DEVICE_TYPE]" +
                                              ",[INTERFACE_DIRECTION]" +
                                              ",[INTERFACE_DESC]" +
                                              ",[INTERFACE_STATUS]" +
                                              ",[INDEX_FILE]" +
                                              ",[EVENT_TYPE])" +

                                              " SELECT " + "'" + OutboundDBConfigMgt.Config.OutboundConfig.IName + "'" +
                                              " ,DEVICE_ID, DEVICE_TYPE, DEVICE_DIRECT" +
                                              " ,'" + OutboundDBConfigMgt.Config.OutboundConfig.Description + "'" +
                                              ",1, '' , '" + OutboundDBConfigMgt.Config.OutboundConfig.EventTypeListStr + "'" +
                                              " FROM DEVICE " +
                                              " WHERE DEVICE_ID = " + OutboundDBConfigMgt.Config.Outboundbase.Device_ID.ToString();

                    cmdInterface.ExecuteNonQuery();
                }
                else
                {
                    cmdInterface.CommandText = " UPDATE [GWConfigDB].[dbo].[INTERFACE]" +
                                               " SET [EVENT_TYPE] = @EVENT_TYPE" +
                                               " WHERE INTERFACE_NAME = @INTERFACE_NAME";
                    cmdInterface.Parameters.AddWithValue("@Event_Type", OutboundDBConfigMgt.Config.OutboundConfig.EventTypeListStr);
                    cmdInterface.Parameters.AddWithValue("@Interface_Name", OutboundDBConfigMgt.Config.OutboundConfig.IName);
                    cmdInterface.ExecuteNonQuery();
                }

                // add combination table
                if (OutboundDBConfigMgt.IsNewOutbound)
                {
                    cmdCombination.CommandText = " INSERT INTO [GWConfigDB].[dbo].[Combination]" +
                                                "([DataIn],[DataOut] ,[Data_Mapping_File])" +
                                                " VALUES (@DataIn,@DataOut,@Data_Mapping_File)";

                    cmdCombination.Parameters.AddWithValue("@DataIn", OutboundDBConfigMgt.Config.InboundConfig.IName);
                    cmdCombination.Parameters.AddWithValue("@DataOut", OutboundDBConfigMgt.Config.OutboundConfig.IName);
                    cmdCombination.Parameters.AddWithValue("@Data_Mapping_File", "");
                    cmdCombination.ExecuteNonQuery();
                }
                tran.Commit();
            }
            catch (Exception E)
            {
                tran.Rollback();
                throw;
            }
            finally
            {
                _conn.Close();
            }
            */
        }

        public void Init(string sDBConnStr)
        {
            try
            {
                _ds.Clear();
                _InboundInterfaceList.Clear();
                _OutboundInterfaceList.Clear();

                _conn.ConnectionString = sDBConnStr;
                _conn.Open();
                _sa.SelectCommand = new System.Data.OleDb.OleDbCommand("select * from interface", _conn);
                _sa.Fill(_ds);
                if (_ds.Tables[0].Rows.Count < 1)
                {
                    _IsInited = true;
                    return;
                }
                foreach (System.Data.DataRow dr in _ds.Tables[0].Rows)
                {
                    if (dr["DEVICE_DIRECT"].ToString().Trim().ToUpper() == "I")
                    {
                        _InboundInterfaceList.Add(dr["INTERFACE_NAME"].ToString().Trim(), dr["EVENT_TYPE"]);
                    }
                    else
                    {
                        _OutboundInterfaceList.Add(dr["INTERFACE_NAME"].ToString().Trim(), dr["EVENT_TYPE"]);
                    }


                }
                _IsInited = true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(sDBConnStr + "\r\n" + e.ToString());
            }
            finally
            {
                _conn.Close();
            }
        }

    }
    #endregion

}