using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class HL7DataElement
    {
        private string sData;

        public HL7DataElement(string s)
        {            
            sData = s;           
        }

        public string getString(char cDelimeter, int iPosition)        
        {
            return getString(Char.ToString(cDelimeter), iPosition);
        }

        public string getString(string sDelimeter, int iPosition)
        {
            int iFirstDelimeter = 0;
            int iLastDelimeter = 0;
            string sReturn = sData;
            if(sData.Length < 0 || sDelimeter.Equals(" ") || iPosition < 0)
                throw new UnableToGetStringException("get string error");
            if(iPosition > 0)
            {
                for(int i = 1; i < iPosition; i++)
                {
                    if(i == 1)
                        iFirstDelimeter = sData.IndexOf(sDelimeter, 0);
                    else
                        iFirstDelimeter = sData.IndexOf(sDelimeter, iFirstDelimeter + sDelimeter.Length);
                    if(iFirstDelimeter < 0)
                        throw new UnableToGetStringException("get string error");
                }

                if(iFirstDelimeter >= 0)
                {
                    int iStringStart = 0;
                    if(iFirstDelimeter > 0 || iPosition > 1)
                        iStringStart = iFirstDelimeter + sDelimeter.Length;
                    iLastDelimeter = sData.IndexOf(sDelimeter, iStringStart);
                    if(iLastDelimeter < 0)
                        iLastDelimeter = sData.Length;
                    sReturn = sData.Substring(iStringStart, iLastDelimeter-iStringStart);
                }
            }
            return sReturn;
        }

    public string toString()
    {
        return sData;
    }

    public int getLength()
    {
        return getLength("");
    }

    public int getLength(char cDelimeter)
    {
        return getLength(Char.ToString(cDelimeter));
    }

    public int getLength(string sDelimeter)
    {
        int iCounter = 0;
        int iPos = 0;
        if(sData.Length > 0)
        {
            iCounter = 1;
            if(sDelimeter.Length > 0)
                for(iPos = sData.IndexOf(sDelimeter, 0); iPos >= 0;)
                {
                    iPos = sData.IndexOf(sDelimeter, iPos + 1);
                    iCounter++;
                }

        }
        return iCounter;
    }
    }
}
