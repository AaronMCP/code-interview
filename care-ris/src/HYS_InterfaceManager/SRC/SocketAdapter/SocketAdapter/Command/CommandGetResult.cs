using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.SocketAdapter.Command
{
    public class CommandGetResult
    {
        public const CommandBase.CommandPacketTypeEnum PacketType = CommandBase.CommandPacketTypeEnum.cptGetResult;

        public CommandGetResult()
        {
            _PacketHead.PacketType = PacketType;
        }

        #region Property
        PacketHead _PacketHead;
        public PacketHead PacketHead
        {
            get { return _PacketHead; }
            set { _PacketHead = value; }
        }
                      
        string _CommandGUID;
        public string CommandGUID
        {
            get { return _CommandGUID; }
            set { _CommandGUID = value; }
        }

        string _ComeFrom;
        public string ComeFrom
        {
            get { return _ComeFrom; }
            set { _ComeFrom = value; }
        }
     
        #endregion

        public string EncodePackage()
        {
            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append("<>");

            #region Packet Head
            sbMsg.Append(CommandBase.EncodePacketHead(PacketHead));
            #endregion

            #region Command Head
            sbMsg.Append("<");
            sbMsg.Append("CommandGUID=" + CommandGUID);
            sbMsg.Append("%From=" + _ComeFrom);
            sbMsg.Append(">");
            #endregion

            string sMsg = sbMsg.ToString();

            return sMsg;
            
        }

        public bool DecodePackage(string sMsg)
        {            
            int bPos;
            // Decode Packet Head
            PacketHead = CommandBase.DecodePacketHead(sMsg);
            if (PacketHead.PacketType != PacketType) return false;

            // Decode Command Type           
            bPos = sMsg.IndexOf("CommandGUID");
            _CommandGUID = CommandBase.ExtractValue(sMsg, bPos);

            // From
            bPos = sMsg.IndexOf("From");
            _ComeFrom = CommandBase.ExtractValue(sMsg, bPos).Trim();

            return true;
        }
    }
}
