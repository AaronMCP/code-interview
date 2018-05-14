using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.SocketAdapter.Command
{
    public class CommandRespSendData
    {
        public const CommandBase.CommandPacketTypeEnum PacketType = CommandBase.CommandPacketTypeEnum.cptRespSendData;

        public CommandRespSendData()
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

       

        string _SendResult;
        public string SendResult
        {
            get { return _SendResult; }
            set { _SendResult = value; }
        }

        #endregion



        public string EncodePackage()
        {
            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append("><");

            #region Packet Head
            //sbMsg.Append(CommandBase.EncodePacketHead(PacketHead));
            #endregion

            
            #region Command Head
            sbMsg.Append("<");
            sbMsg.Append("CommandGUID=" + CommandGUID);
            sbMsg.Append("%Send result=" + _SendResult);
            sbMsg.Append(">");
            #endregion

            string sMsg = sbMsg.ToString();

            return sMsg;
        }

        public bool DecodePackagea(string sMsg)
        {
            
            int bPos = 0;
            // Decode Packet Head
            PacketHead = CommandBase.DecodePacketHead(sMsg);

            if (PacketHead.PacketType != PacketType) return false;

           
            //CommandGUID, Result

            bPos = sMsg.IndexOf("CommandGUID", bPos);
            if (bPos < 0) return false;
            _CommandGUID = CommandBase.ExtractValue(sMsg, bPos);

            bPos = sMsg.IndexOf("Send result", bPos);
            if (bPos < 0) return false;
            _SendResult = CommandBase.ExtractValue(sMsg, bPos);

            //><<CommandGUID=771D3A76-8A8D-49CF-8CB6-0E19CD6B27F2%Send result=1>

            return true;
        }

    }
}
