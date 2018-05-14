using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.FileAdapter.Configuration
{
   
    public class FileOutboundAdapterConfig : XObject
    {
        private FileOutGeneralParams _OutGeneralParams = new FileOutGeneralParams();
        public FileOutGeneralParams OutGeneralParams
        {
            get { return _OutGeneralParams; }
            set { _OutGeneralParams = value; }
        }

       
        private XCollection<FileOutChannel> _inboundChanels = new XCollection<FileOutChannel>();
        public XCollection<FileOutChannel> OutboundChanels
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
