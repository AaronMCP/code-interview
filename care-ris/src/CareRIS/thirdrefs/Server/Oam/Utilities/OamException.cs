using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using CommonGlobalSettings;

namespace Server.Utilities.Oam
{
    public class OamException : GCRISException
    {
        public OamException()
            : base()
        {

        }

        public OamException(string s)
            : base(s)
        {

        }

        protected OamException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public OamException(string s, System.Exception e)
            : base(s, e)
        {

        }
    }

    public class DuplicateDescrpException : OamException
    {
        public DuplicateDescrpException()
            : base()
        {

        }
        public DuplicateDescrpException(string s)
            : base(s)
        {

        }
        public DuplicateDescrpException(string s, System.Exception e)
            : base(s, e)
        {

        }
    }
}
