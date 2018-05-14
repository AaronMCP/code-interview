using System;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.Common.Objects.Config
{
    public class GarbageRule : XObject
    {
        private bool _enable;
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Whether to enbale garbage collection.")]
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        private bool _startAtParticularTime;
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Whether to start garbage collection at a particular time, or according to a time interval from the NT service start.")]
        public bool StartAtParticularTime
        {
            get { return _startAtParticularTime; }
            set { _startAtParticularTime = value; }
        }

        private ParticularTime _particularTime = new ParticularTime();
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Particular time for garbage collection.")]
        public ParticularTime ParticularTime
        {
            get { return _particularTime; }
            set { _particularTime = value; }
        }

        private int _interval = 60 * 1000; //ms
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Time interval for garbage collection.")]
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        private bool _checkExpireTime;
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Whether to check expire time to determine which data should be delete.")]
        public bool CheckExpireTime
        {
            get { return _checkExpireTime; }
            set { _checkExpireTime = value; }
        }

        private TimeSpan _expireTime = new TimeSpan(1,0,0,0,0);
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("When the value of property CheckExpireTime is True, ExpireTime is used to determine which data is expired and should be delete.")]
        public TimeSpan ExpireTime
        {
            get { return _expireTime; }
            set { _expireTime = value; }
        }

        private bool _checkProcessFlag;
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Whether to check process flag to determine which data should be delete.")]
        public bool CheckProcessFlag
        {
            get { return _checkProcessFlag; }
            set { _checkProcessFlag = value; }
        }

        private int _sqlCommandTimeOutInSecond = 1200; //s          20min
        [Description("The timeout (in second) for the SQL command to execute garbage collection storage procedure.")]
        public int SqlCommandTimeOutInSecond
        {
            get { return _sqlCommandTimeOutInSecond; }
            set { _sqlCommandTimeOutInSecond = value; }
        }
        //US29442:Action1:修改清理SP脚本生成代码，减少单次事务的执行量    private int _maxRecordCountLimitation = 30000;
        #region
        private int _maxRecordCountLimitation = 500;
        #endregion
        [Description("Max record count limitation in one garbage collection.")]
        public int MaxRecordCountLimitation
        {
            get { return _maxRecordCountLimitation; }
            set { _maxRecordCountLimitation = value; }
        }

        private XCollection<QueryCriteriaItem> _additionalCriteria = new XCollection<QueryCriteriaItem>();
        [Description("Additional query criteria beside process flag and expire time for finding garbage data.")]
        public XCollection<QueryCriteriaItem> AdditionalCriteria
        {
            get { return _additionalCriteria; }
            set { _additionalCriteria = value; }
        }
    }
}
