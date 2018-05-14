using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Config
{
    public partial class CSBrokerInboundConfig
    {
        internal ProgramContext _context;
        private string GetFullPath(string relativePath)
        {
            string fullPath = ConfigHelper.GetFullPath(Path.Combine(_context.AppArgument.ConfigFilePath, relativePath));
            return fullPath;
        }

        public const string BrokerErrorMessageFileName = "BrokerErrorMessage.xml";
        internal string GetBrokerErrorMessageFileFullPath()
        {
            if (Path.IsPathRooted(BrokerErrorMessageFileName)) return BrokerErrorMessageFileName;
            return GetFullPath(BrokerErrorMessageFileName);
        }
        internal void EnsureBrokerErrorMessageFile()
        {
            try
            {
                string fname = GetBrokerErrorMessageFileFullPath();
                if (File.Exists(fname)) return;

                using (StreamWriter sw = File.CreateText(fname))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    sw.WriteLine("<csb:Index xmlns:csb=\"http://www.carestream.com/csbroker\">");
                    sw.WriteLine(" <csb:Table>");
                    //sw.WriteLine("  <csb:DATAINDEX_DATA_SOURCE/>");
                    //sw.WriteLine("  <csb:DATAINDEX_EVENT_TYPE></csb:DATAINDEX_EVENT_TYPE>");
                    sw.WriteLine("  <csb:DATAINDEX_PROCESS_FLAG>0</csb:DATAINDEX_PROCESS_FLAG>");
                    sw.WriteLine("  <csb:DATAINDEX_RECORD_INDEX_1>EXCEPTION</csb:DATAINDEX_RECORD_INDEX_1>");
                    sw.WriteLine("  <csb:DATAINDEX_RECORD_INDEX_2>Can not outbound this message according to current system configuration, please see log files for details.</csb:DATAINDEX_RECORD_INDEX_2>");
                    sw.WriteLine("  <csb:DATAINDEX_RECORD_INDEX_3/>");
                    sw.WriteLine("  <csb:DATAINDEX_RECORD_INDEX_4/>");
                    sw.WriteLine(" </csb:Table>");
                    sw.WriteLine("</csb:Index>");
                }
            }
            catch (Exception err)
            {
                _context.Log.Write(err);
            }
        }

        private string _brokerErrorMessageContent;
        internal string GetBrokerErrorMessageContent()
        {
            if (_brokerErrorMessageContent == null)
            {
                try
                {
                    _brokerErrorMessageContent = File.ReadAllText(GetBrokerErrorMessageFileFullPath());
                }
                catch (Exception e)
                {
                    _context.Log.Write(e);
                }
            }
            return _brokerErrorMessageContent;
        }
    }
}
