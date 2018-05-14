using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace HYS.SocketAdapter.Command
{
     /// <summary>
     /// Include some normal static information
     /// </summary>
    public class CommandBase
    {
        static int _PackageHeadLength = 7;
        static public int PackageHeadLength
        {
            get { return _PackageHeadLength; }
            set { _PackageHeadLength = value; }
        }
                
        public enum CommandPacketTypeEnum
        {
            cptSendData = 1,
            cptRespSendData = 2,            
            cptRespGetResult =3,
            cptGetResult = 4,
        }

        public enum CommandTypeEnum
        {
            PATIENT_ADD = 1,
            PATIENT_DEL = 2,
            PATIENT_UPDATE =3,

            ORDER_ADD = 4,
            ORDER_DEL = 5,
            ORDER_UPDATE = 6,

            IMAGE_ARRIVAL = 8,

            #region NO USE Command Type
            //REPORT_STATUS = 7,   

            //USER_ADD = 9,
            //USER_DEL = 10,
            //USER_UPDATE = 11,
            
            //MODIALITY_ADD = 12,
            //MODIALITY_DEL = 13,
            //MODIALITY_UPDATE = 14,

            //PACSQC_NOTIFY = 15,
            //STUDY_DEL = 16
            #endregion
        }

        public enum CommandStatusEnum
        {
            NEW_COMMAND = 0,
            SENT_COMMAND = 1,
            DONE_COMMAND = 2
        }

        public enum CommandResultEnum
        {
            PENDING = 0,
            SUCESS = 1,
            E_COMMAND = 2,
            E_PERIORITY = 3,
            E_STATUS = 4,
            E_PARAMETER = 5,
            E_DATABASE = 6,
            E_NORECORD = 7,
            E_UNKNOW = 8
        }

        //filter &, =, % which exists in paramvalues
        static public string FilterParamValue(string str)
        {
	        string strOld;
	        string strNew;

            //replace '&' with "&38;"
            strOld = "&";
            strNew = "&38;";
            str = str.Replace(strOld, strNew);

	        //replace '=' with "&61;"
	        strOld = "=";
	        strNew = "&61;";
	        str = str.Replace(strOld,strNew);

	        //replace '%' with "&37;"
	        strOld = "%";
	        strNew = "&37;";
	        str = str.Replace(strOld,strNew);

	        //replace '<' with "&60;"
	        strOld = "<";
	        strNew = "&60;";
	        str = str.Replace(strOld,strNew);

	        //replace '>' with "&62;"
	        strOld = ">";
	        strNew = "&62;";
	        str = str.Replace(strOld,strNew);

            

            return str;
        }

        //Transfer "&37;", "&61;", "&38;" back to '%', '=', '&'
        //and change character from "'" to "''"
        static public string AntiFilterParameterValue(string str)
        {
	        string strOld;
	        string strNew;

	        //replace "&37;" with '%'
	        strOld = "&37;";
	        strNew = "%";
            str = str.Replace(strOld, strNew);

	        //replace "&61;" with '='
	        strOld = "&61;";
	        strNew = "=";
            str = str.Replace(strOld, strNew);

	       
	        //replace ' with ''
	        strOld = "'";
	        strNew = "''";
            str = str.Replace(strOld, strNew);

	        //replace "&60;" with '<'
	        strOld = "&60;";
	        strNew = "<";
            str = str.Replace(strOld, strNew);

	        //replace "&62;" with '>'
	        strOld = "&62;";
	        strNew = ">";
            str = str.Replace(strOld, strNew);

            //replace "&38;" with '&'
            strOld = "&38;";
            strNew = "&";
            str = str.Replace(strOld, strNew);


            return str;
        }

        static public string EncodePacketHead(PacketHead ph)
        {
            StringBuilder sbHead = new StringBuilder();
            
            sbHead.Append("<PacketType=" + ((int)ph.PacketType).ToString());            
            sbHead.Append("%Souce=" + ph.SourceIP + "|" + ph.SourcePort.ToString());
            sbHead.Append("%Destination=" + ph.DestinationIP + "|" + ph.DestinationPort.ToString());
            sbHead.Append(">");

            return sbHead.ToString();
        }

        static public PacketHead DecodePacketHead(string str)
        {
            int bPos, ePos;
            string sHead;            
            bPos = str.IndexOf("PacketType");
            if (bPos >= 0)
            {
                ePos = str.Substring(bPos).IndexOf(">");
                sHead = str.Substring(bPos, ePos);
            }
            else //Used to treat response send data which have not 'PacketTye...', hope it will be added later
                sHead = "PacketType=2%Source=127.0.0.1|9000%Destination=127.0.0.1|9001";
           
            PacketHead ph;
            //PacketType
            bPos = sHead.IndexOf("=");
            ePos = sHead.IndexOf("%");
            string sPocketType = sHead.Substring(bPos + 1, ePos-bPos-1);
            ph.PacketType = (CommandPacketTypeEnum)Convert.ToInt32(sPocketType);
            
            //Source %xxx.xxx.xxx.xxx|xxxx%
            bPos = ePos ;
            bPos = sHead.IndexOf("=", bPos + 1);
            ePos = sHead.IndexOf("|", bPos + 1);
            ph.SourceIP = sHead.Substring(bPos + 1, ePos - bPos - 1);

            bPos = ePos;            
            ePos = sHead.IndexOf("%", bPos + 1);
            ph.SourcePort = Convert.ToInt32(sHead.Substring(bPos + 1, ePos - bPos - 1).Trim());
            //Destination
            bPos = ePos;
            bPos = sHead.IndexOf("=", bPos + 1);
            ePos = sHead.IndexOf("|", bPos + 1);
            ph.DestinationIP = sHead.Substring(bPos + 1, ePos - bPos - 1);

            bPos = ePos;
            ePos = sHead.Length;
            ph.DestinationPort = Convert.ToInt32(sHead.Substring(bPos + 1, ePos - bPos - 1).Trim());

            return ph;
        }


        /// <summary>
        /// Extract value from string, which is start from index "startIndex" and betwwen "=" and "%" or ">"
        /// 
        /// only used for this program
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>        
        /// <returns></returns>        
        static public string ExtractValue(string str, int startIndex)
        {
            int bPos, ePos;

            bPos = str.IndexOf("=", startIndex);
            if (bPos < 0)
                throw new Exception("Message format error! Cannot find '='");
            for (ePos = bPos + 1; ePos < str.Length; ePos++)
            {
                if (str[ePos] == '%'|| str[ePos] == '>' )
                    break;
            }
            return str.Substring(bPos + 1, ePos - bPos - 1);
        }
    }

    public struct PacketHead
    {
        public CommandBase.CommandPacketTypeEnum PacketType;

        public string   SourceIP;
        public int      SourcePort;
        //public string   Source;
                
        public string   DestinationIP;
        public int      DestinationPort;
        //public string   Destination;
        
    }
}
   



