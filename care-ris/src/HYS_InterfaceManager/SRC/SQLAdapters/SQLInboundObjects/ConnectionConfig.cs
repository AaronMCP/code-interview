using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLInboundAdapterObjects
{
    public class ConnectionConfig : XObject
    {
        private InteractType _interactType = InteractType.Active;
        public InteractType InteractType {
            get { return _interactType; }
            set { _interactType = value; }
        }
        
        private ThirdDBConnection _connectionParameter = new ThirdDBConnection();
        public ThirdDBConnection ConnectionParameter
        {
            get { return _connectionParameter; }
            set { _connectionParameter = value; }
        }
        
        private int _timerInterval = 1000;
        public int TimerInterval
        {
            get { return _timerInterval; }
            set { _timerInterval = value; }
        }

        private bool _timerEnable = false;
        public bool TimerEnable
        {
            get { return _timerEnable; }
            set { _timerEnable = value; }
        }

        private bool _shareConnectionAmongChannels = false;
        public bool ShareConnectionAmongChannels
        {
            get { return _shareConnectionAmongChannels; }
            set { _shareConnectionAmongChannels = value; }
        }

        public bool OracleDriver
        {
            set;
            get;            
        }

    }
}
