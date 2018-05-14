using System;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.Config
{
    public class AdapterServiceCfg : ConfigBase 
    {
        private string _adapterFileName = "";
        [Category("1. Should be modified when composing a device.")]
        [Description("Adapter NT service implementation assembly. Should better be a indirect path.")]
        public string AdapterFileName
        {
            get { return _adapterFileName; }
            set { _adapterFileName = value; }
        }

        private DirectionType _adapterDirection = DirectionType.UNKNOWN;
        [Category("1. Should be modified when composing a device.")]
        [Description("Adapter direction.")]
        public DirectionType AdapterDirection
        {
            get { return _adapterDirection; }
            set { _adapterDirection = value; }
        }

        private string _serviceName = "";
        [Category("3. Will be modified by IM when installing an interface. Don't need to be modified when composing a device.")]
        [Description("Interface name which is inputed by use, when he or she is installing an interface on IM. This name is also be used as NT service name.")]
        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }

        private string _dataDBConnection = "";
        [Category("3. Will be modified by IM when installing an interface. Don't need to be modified when composing a device.")]
        [Description("OLEDB connection string of GWDataDB")]
        public string DataDBConnection
        {
            get { return _dataDBConnection; }
            set { _dataDBConnection = value; }
        }

        private string _configDBConnection = "";
        [Category("3. Will be modified by IM when installing an interface. Don't need to be modified when composing a device.")]
        [Description("OLEDB connection string of GWConfigDB")]
        public string ConfigDBConnection
        {
            get { return _configDBConnection; }
            set { _configDBConnection = value; }
        }

        private string _iemWindowCaption = ConfigHelper.IMDefaultCaption;
        [Category("3. Will be modified by IM when installing an interface. Don't need to be modified when composing a device.")]
        [Description("IM window caption, by used by Adapter.Service.exe to find IM window handle, in order to send service status to IM via windows massage.")]
        public string IMWindowCaption
        {
            get { return _iemWindowCaption; }
            set { _iemWindowCaption = value; }
        }

        private bool _notifyStatusToIM;
        [Category("3. Will be modified by IM when installing an interface. Don't need to be modified when composing a device.")]
        [Description("Whether to send service status to IM via windows massage. When installing an interface, IM will always set this value as True.")]
        public bool NotifyStatusToIM
        {
            get { return _notifyStatusToIM; }
            set { _notifyStatusToIM = value; }
        }

        private bool _dumpData;
        [Category("2. Should be modified when composing a device. However, better remain its default value.")]
        [Description("Whether to let Adapter.Service.exe dump DataSet to Temp folder when processing inbound and outbound data")]
        public bool DumpData
        {
            get { return _dumpData; }
            set { _dumpData = value; }
        }

        //private int _mutexTimeOut = 60000;  //ms
        private int _mutexTimeOut = 600000;  //ms
        [Category("2. Should be modified when composing a device. However, better remain its default value.")]
        [Description("Time span (in ms.) to waiting for a mutex when Adapter.Service.exe wants to access GWDataDB. This mutex is used to synchronize data processing and garbage collection. When any thread meets a time out when waiting for the mutex, it will cancel its desire to access GWDataDB, in order to prevent dead lock.")]
        public int MutexTimeOut
        {
            get { return _mutexTimeOut; }
            set { _mutexTimeOut = value; }
        }

        private GarbageRule _garbageCollection = new GarbageRule();
        [Browsable(false)]
        public GarbageRule GarbageCollection
        {
            get { return _garbageCollection; }
            set { _garbageCollection = value; }
        }

        private Chinese2PinyinRule _chinese2Pinyin = new Chinese2PinyinRule();
        [Browsable(false)]
        public Chinese2PinyinRule Chinese2Pinyin
        {
            get { return _chinese2Pinyin; }
            set { _chinese2Pinyin = value; }
        }

        private ReplacementRule _replacement = new ReplacementRule();
        [Browsable(false)]
        public ReplacementRule Replacement
        {
            get { return _replacement; }
            set { _replacement = value; }
        }

        private ComposingRule _composing = new ComposingRule();
        [Browsable(false)]
        public ComposingRule Composing
        {
            get { return _composing; }
            set { _composing = value; }
        }

        private bool _enableTransaction = false;
        [Category("2. Should be modified when composing a device. However, better remain its default value.")]
        [Description("Whether to use transaction when inserting inbound data.")]
        public bool EnableTransaction
        {
            get { return _enableTransaction; }
            set { _enableTransaction = value; }
        }

        private Level3KanJiReplacementRule _l3kanjiReplacement = new Level3KanJiReplacementRule();
        [Browsable(false)]
        public Level3KanJiReplacementRule L3KanJiReplacement
        {
            get { return _l3kanjiReplacement; }
            set { _l3kanjiReplacement = value; }
        }

    }
}
