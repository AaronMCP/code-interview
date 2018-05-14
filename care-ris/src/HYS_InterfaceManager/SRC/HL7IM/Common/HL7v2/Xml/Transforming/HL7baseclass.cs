using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public abstract class HL7baseclass
    {
        public HL7baseclass()
        {
            TOTAL_DELIMETERS = 5;
            DELIMETERS_START = 3;
            DELIMETERS_END = 8;
            FIELD_DELIMETER_LOC = 0;
            COMPONENT_DELIMETER_LOC = 1;
            REPETITION_DELIMETER_LOC = 2;
            ESCAPE_CHARACTER_LOC = 3;
            SUBCOMPONENT_DELIMETER_LOC = 4;
            MESSAGE_HEADER = "MSH";
            MESSAGE_TYPE_POS = 9;
            SEGMENT_NAME_LENGTH = 3;
            SEGMENT_DELIMETER = ((char)0x0d).ToString();// "\r";
            MESSAGE_ENCODING_POS = 18;
            FIELD_SERARATOR_FIELD_POS = "FIELD.1";
            ENCODING_CHARACTER_FIELD_POS = "FIELD.2";
        }

        protected int TOTAL_DELIMETERS;
        protected int DELIMETERS_START;
        protected int DELIMETERS_END;
        protected int FIELD_DELIMETER_LOC;
        protected int COMPONENT_DELIMETER_LOC;
        protected int REPETITION_DELIMETER_LOC;
        protected int ESCAPE_CHARACTER_LOC;
        protected int SUBCOMPONENT_DELIMETER_LOC;
        protected string MESSAGE_HEADER;
        protected int MESSAGE_TYPE_POS;
        protected int SEGMENT_NAME_LENGTH;
        protected string SEGMENT_DELIMETER;
        protected int MESSAGE_ENCODING_POS;
        protected string FIELD_SERARATOR_FIELD_POS;
        protected string ENCODING_CHARACTER_FIELD_POS;
    }
}
