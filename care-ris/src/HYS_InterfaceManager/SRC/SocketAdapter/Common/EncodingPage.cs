using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.SocketAdapter.Common
{
    public class EncodingPage
    {
        static EncodingInfo[] _EIS = System.Text.Encoding.GetEncodings();

        
        static public EncodingInfo[] GetAllCodePages()
        {
            return _EIS;
        }

        static public int GetIndex(int iCodePage)
        {
            
            for (int i = 0; i < _EIS.Length; i++)
            {
                if (iCodePage == _EIS[i].CodePage)
                    return i;
            }
            return -1;
        }

        static public EncodingInfo GetEncodingInfo(int iCodePage)
        {             

            for (int i = 0; i < _EIS.Length; i++)
            {
                if (iCodePage == _EIS[i].CodePage)
                    return _EIS[i];
            }
            return null;
        }

        static public int GetIndex(string sCodePageName)
        {           

            for (int i = 0; i < _EIS.Length; i++)
            {
                if (sCodePageName == _EIS[i].Name)
                    return i;
            }
            return -1;
        }

        static public EncodingInfo GetEncodingInfo(string sCodePageName)
        {
            for (int i = 0; i < _EIS.Length; i++)
            {
                if (sCodePageName == _EIS[i].Name)
                    return _EIS[i];
            }
            return null;
        }

        static public int GetAsciiWithOnTheCodePage(string sCodePageName)
        {
            byte[] buf = System.Text.Encoding.GetEncoding(sCodePageName).GetBytes("A");
            return buf.Length;
        }
    }
}
