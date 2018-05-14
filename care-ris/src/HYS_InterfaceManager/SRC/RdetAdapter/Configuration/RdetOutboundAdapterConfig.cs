using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.RdetAdapter.Configuration
{
   
    public class RdetOutboundAdapterConfig : XObject
    {
        private RdetOutGeneralParams _OutGeneralParams = new RdetOutGeneralParams();
        public RdetOutGeneralParams OutGeneralParams
        {
            get { return _OutGeneralParams; }
            set { _OutGeneralParams = value; }
        }

        private string _GWDataDBConnection = "";
        public string GWDataDBConnection
        {
            get { return _GWDataDBConnection; }
            set { _GWDataDBConnection = value; }
        }

        private ClientRdetParams _ClientRdetParams = new ClientRdetParams();
        public ClientRdetParams ClientRdetParams
        {
            get { return _ClientRdetParams; }
            set { _ClientRdetParams = value; }
        }

        private XCollection<RdetOutChannel> _outboundChanels = new XCollection<RdetOutChannel>();
        public XCollection<RdetOutChannel> OutboundChanels
        {
            get { return _outboundChanels; }
            set { _outboundChanels = value; }
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
