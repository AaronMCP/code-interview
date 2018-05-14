using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class HL7Delimeters
    {
        public HL7Delimeters(string sDelimeters)       
        {
            TOTAL_DELIMETERS = 5;
            DELIMETERS_START = 3;
            FIELD_DELIMETER_LOC = 0;
            COMPONENT_DELIMETER_LOC = 1;
            REPETITION_DELIMETER_LOC = 2;
            ESCAPE_CHARACTER_LOC = 3;
            SUBCOMPONENT_DELIMETER_LOC = 4;
            if(sDelimeters.Length == TOTAL_DELIMETERS)
            {
                cFieldDelimeter = sDelimeters[FIELD_DELIMETER_LOC];
                cComponentDelimeter = sDelimeters[COMPONENT_DELIMETER_LOC];
                cRepetitionDelimeter = sDelimeters[REPETITION_DELIMETER_LOC];
                cEscapeCharacter = sDelimeters[ESCAPE_CHARACTER_LOC];
                cSubComponentDelimeter = sDelimeters[SUBCOMPONENT_DELIMETER_LOC];
            } else
            {
                throw new InvalidDelimetersException("Delimeter number is not "+TOTAL_DELIMETERS.ToString());
            }
            if(cFieldDelimeter == cComponentDelimeter || cFieldDelimeter == cRepetitionDelimeter || cFieldDelimeter == cEscapeCharacter || cFieldDelimeter == cSubComponentDelimeter)
                throw new InvalidDelimetersException("Invalid Field Delimeter "+cFieldDelimeter);
            if(cComponentDelimeter == cRepetitionDelimeter || cComponentDelimeter == cEscapeCharacter || cComponentDelimeter == cSubComponentDelimeter)
                throw new InvalidDelimetersException("Invalid Component Delimeter "+cComponentDelimeter);
            if(cRepetitionDelimeter == cEscapeCharacter || cRepetitionDelimeter == cSubComponentDelimeter)
                throw new InvalidDelimetersException("Invalid Repetiton Delimeter " +cRepetitionDelimeter);
            if(cEscapeCharacter == cSubComponentDelimeter)
                throw new InvalidDelimetersException("Invalid Escape Delimeter "+cEscapeCharacter);
            else
                return;
        }

        public char getFD()
        {
            return cFieldDelimeter;
        }

        public char getCD()
        {
            return cComponentDelimeter;
        }

        public char getRD()
        {
            return cRepetitionDelimeter;
        }

        public char getEC()
        {
            return cEscapeCharacter;
        }

        public char getSD()
        {
            return cSubComponentDelimeter;
        }

        public string toString()
        {
            return char.ToString(cFieldDelimeter) + char.ToString(cComponentDelimeter) + char.ToString(cRepetitionDelimeter) + char.ToString(cEscapeCharacter) + char.ToString(cSubComponentDelimeter);
            
        }

        private int TOTAL_DELIMETERS;
        private int DELIMETERS_START;
        private int FIELD_DELIMETER_LOC;
        private int COMPONENT_DELIMETER_LOC;
        private int REPETITION_DELIMETER_LOC;
        private int ESCAPE_CHARACTER_LOC;
        private int SUBCOMPONENT_DELIMETER_LOC;
        private char cFieldDelimeter;
        private char cComponentDelimeter;
        private char cRepetitionDelimeter;
        private char cEscapeCharacter;
        private char cSubComponentDelimeter;
     }
}
