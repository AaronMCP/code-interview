using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.SocketAdapter.Command
{
    public class CommandRespGetResult
    {
        public const CommandBase.CommandPacketTypeEnum PacketType = CommandBase.CommandPacketTypeEnum.cptRespGetResult;

        public CommandRespGetResult()
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


        string _CommandResult;
        public string Result
        {
            get { return _CommandResult; }
            set { _CommandResult = value; }
        }

        #endregion



        public string EncodePackage()
        {
            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append("><");

            #region Packet Head
            sbMsg.Append(CommandBase.EncodePacketHead(PacketHead));
            #endregion

            #region Command come from
            sbMsg.Append("<");            
            sbMsg.Append("Command come from=" + _ComeFrom);
            sbMsg.Append(">");
            #endregion

            #region Command Head
            sbMsg.Append("<");
            sbMsg.Append("CommandGUID=" + CommandGUID);
            sbMsg.Append("%Result=" + _CommandResult);
            sbMsg.Append(">");
            #endregion

            string sMsg = sbMsg.ToString();
            return sMsg;
        }

        public bool DecodePackagea(string sMsg, EncodingInfo AEncodingInfo)
        {
            
            int bPos;//, ePos;
            // Decode Packet Head
            PacketHead = CommandBase.DecodePacketHead(sMsg);

            if (PacketHead.PacketType != PacketType) return false;

            // Decode Commandcomefrom
            bPos = sMsg.IndexOf("Command come from", 0);
            _ComeFrom = CommandBase.ExtractValue(sMsg, bPos);

            //CommandGUID, Result
            bPos = sMsg.IndexOf("CommandGUID", bPos);
            _CommandGUID = CommandBase.ExtractValue(sMsg, bPos);

            bPos = sMsg.IndexOf("Result", bPos);
            _CommandResult = CommandBase.ExtractValue(sMsg, bPos);
            


            return true;
        }
    }
}
