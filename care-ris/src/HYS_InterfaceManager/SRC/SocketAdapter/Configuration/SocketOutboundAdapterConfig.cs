using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SocketAdapter.Configuration
{
   
    public class SocketOutboundAdapterConfig : XObject
    {
        private SocketOutGeneralParams _OutGeneralParams = new SocketOutGeneralParams();
        public SocketOutGeneralParams OutGeneralParams
        {
            get { return _OutGeneralParams; }
            set { _OutGeneralParams = value; }
        }

        private ClientSocketParams _ClientSocketParams = new ClientSocketParams();
        public ClientSocketParams ClientSocketParams
        {
            get { return _ClientSocketParams; }
            set { _ClientSocketParams = value; }
        }

        private XCollection<SocketOutChannel> _inboundChanels = new XCollection<SocketOutChannel>();
        public XCollection<SocketOutChannel> OutboundChanels
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
