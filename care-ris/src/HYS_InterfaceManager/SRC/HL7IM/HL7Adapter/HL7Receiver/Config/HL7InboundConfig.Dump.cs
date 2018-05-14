using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using HYS.Common.Xml;
using System.IO;
using HYS.IM.Messaging.Objects;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Objects.Entity;
using System.Runtime.CompilerServices;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Config
{
    public partial class HL7InboundConfig : EntityConfigBase
    {
        internal ProgramContext _contextForDump;

        private MessageDumpSetting _messageDump = new MessageDumpSetting();
        public MessageDumpSetting MessageDump
        {
            get { return _messageDump; }
            set { _messageDump = value; }
        }

        private void DumpErrorMessage(string msgContext, string fileName)
        {
            string path = ConfigHelper.GetFullPath(_contextForDump.AppArgument.ConfigFilePath, MessageDump.ErrorMessageFolder);
            path = Path.Combine(path, DateTime.Today.ToString(LogHelper.DateFomat));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = Path.Combine(path, fileName);
            _contextForDump.Log.Write("Dumping orginal error message to file : " + path);
            File.WriteAllText(path, msgContext, Encoding.UTF8);
        }
        private void DumpSuccessMessage(string msgContext, string fileName)
        {
            string path = ConfigHelper.GetFullPath(_contextForDump.AppArgument.ConfigFilePath, MessageDump.SuccessMessageFolder);
            path = Path.Combine(path, DateTime.Today.ToString(LogHelper.DateFomat));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = Path.Combine(path, fileName);
            _contextForDump.Log.Write("Dumping orginal success message to file : " + path);
            File.WriteAllText(path, msgContext, Encoding.UTF8);
        }
        public void DumpMessage(Message internalMsg, string orgMsgContent, bool orgMsgIsXml, bool dispatchSuccess)
        {
            if (MessageDump.DumpErrorMessage == false && MessageDump.DumpSuccessMessage == false) return;
            string fileName = (internalMsg == null) ? EntityDictionary.GetRandomNumber() :
                string.Format("{0}_{1}", GetIncreaseNumber(), internalMsg.Header.ID.ToString());
            fileName = fileName + (orgMsgIsXml ? ".xml" : ".txt");
            string msgContent = orgMsgIsXml ? LogControler.XMLHeader + orgMsgContent : orgMsgContent;
            if (MessageDump.DumpErrorMessage == true && dispatchSuccess == false) DumpErrorMessage(msgContent, fileName);
            if (MessageDump.DumpSuccessMessage == true && dispatchSuccess == true) DumpSuccessMessage(msgContent, fileName);
        }

        private static char count = char.MinValue; //(char)(char.MaxValue - 5);
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string GetIncreaseNumber()
        {
            string str = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            return str + string.Format("{0:00000}", (int)unchecked(count++));
        }
    }

    public class MessageDumpSetting : XObject
    {
        public MessageDumpSetting()
        {
            ErrorMessageFolder = "Dump\\Error";
            SuccessMessageFolder = "Dump\\Success";
        }

        public bool DumpErrorMessage { get; set; }
        public string ErrorMessageFolder { get; set; }

        public bool DumpSuccessMessage { get; set; }
        public string SuccessMessageFolder { get; set; }
    }
}
