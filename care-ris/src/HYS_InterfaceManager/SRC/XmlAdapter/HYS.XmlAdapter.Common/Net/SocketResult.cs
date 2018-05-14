using System;
using System.Xml;
using System.Text;
using System.Collections.Generic;

namespace HYS.XmlAdapter.Common.Net
{
    public class SocketResult
    {
        public SocketResultType Type;
        public string ReceivedString;
        public string ExceptionInfor
        {
            get { return Exception == null ? "" : Exception.ToString(); }
        }
        public Exception Exception;

        public SocketResult(SocketResultType t, string str)
        {
            Type = t;
            ReceivedString = str;
        }
        public SocketResult(SocketResultType t)
        {
            Type = t;
        }
        public SocketResult(Exception err)
        {
            Type = SocketResultType.Exception;
            Exception = err;
        }
        public SocketResult(string str)
        {
            ReceivedString = str;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(str);

            XmlNode node = doc.SelectSingleNode(STATUS_CODE);
            if (node != null && node.InnerText.Trim().ToUpper() == "SUCCESS")
            {
                Type = SocketResultType.Success;
            }
            else
            {
                Type = SocketResultType.ReceiveFailed;
            }
        }

        public const string STATUS_CODE = "/XMLResponseMessage/Status/Code";

        public static SocketResult Disconnect = new SocketResult(SocketResultType.Disconnect);
        public static SocketResult SendFailed = new SocketResult(SocketResultType.SendFailed);
        public static SocketResult Empty = new SocketResult(SocketResultType.Unknown);
    }

    public enum SocketResultType
    {
        Unknown,
        Success,
        Disconnect,
        SendFailed,
        ReceiveFailed,
        Exception,
    }
}
