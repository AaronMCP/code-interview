using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.HL7Objects.Types;

namespace HYS.Common.HL7Objects.Segments
{
    public class MSH : XObject
    {
        public string FieldSeparator = "|";
        public string EncodingCharacters = @"^~\&amp;";
        public string MessageControlID;

        public MSH()
        {
            SendingApplication = new EI();
            SendingFacility = new EI();
            ReceivingApplication = new EI();
            ReceivingFacility = new EI();
            MessageType = new MSG();
            ProcessingID = new PT();
            VersionID = new VID();
        }

        public EI SendingApplication { get; set; }
        public EI SendingFacility { get; set; }
        public EI ReceivingApplication { get; set; }
        public EI ReceivingFacility { get; set; }
        public MSG MessageType { get; set; }
        public PT ProcessingID { get; set; }
        public VID VersionID { get; set; }
    }
}
