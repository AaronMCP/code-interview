using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.FileAdapter.Configuration
{
    public class FileInboundAdapterConfig : XObject
    {
        private FileInGeneralParams _InGeneralParams = new FileInGeneralParams();
        public FileInGeneralParams InGeneralParams
        {
            get { return _InGeneralParams; }
            set { _InGeneralParams = value; }
        }

        
        private XCollection<FileInChannel> _inboundChanels = new XCollection<FileInChannel>();
        public XCollection<FileInChannel> InboundChanels
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
