using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SocketAdapter.Configuration
{
    public class SocketInboundAdapterConfig : XObject
    {
        private SocketInGeneralParams _InGeneralParams = new SocketInGeneralParams();
        public SocketInGeneralParams InGeneralParams
        {
            get { return _InGeneralParams; }
            set { _InGeneralParams = value; }
        }

        private ServerSocketParams _ListenServerSocketParams = new ServerSocketParams();
        public ServerSocketParams ListenServerSocketParams
        {
            get { return _ListenServerSocketParams; }
            set { _ListenServerSocketParams = value; }
        }

        private XCollection<SocketInChannel> _inboundChanels = new XCollection<SocketInChannel>();
        public XCollection<SocketInChannel> InboundChanels
        {
            get { return _inboundChanels; }
            set { _inboundChanels = value; }
        }

        private XCollection<ThrPartyDBParamter> _thrPartyDBParamter = new XCollection<ThrPartyDBParamter>();
        public XCollection<ThrPartyDBParamter> ThrPartyDBParamters
        {
            get { return _thrPartyDBParamter; }
            set { _thrPartyDBParamter = value; }
        }

        private XCollection<LookupTable> _LookupTables = new XCollection<LookupTable>();
        public XCollection<LookupTable> LookupTables
        {
            get { return _LookupTables; }
            set { _LookupTables = value; }
        }

    }


}
