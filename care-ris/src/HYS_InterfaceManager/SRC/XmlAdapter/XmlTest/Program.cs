using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Adapter.Base;
using HYS.XmlAdapter.Common.Net;
using HYS.XmlAdapter.Common.Objects;

namespace XmlTest
{
    static class Program
    {
        //public static string XSLFileName = @"../../Xml2Html.xslt";
        //public static string XMLFileName = @"../../XMLDemo.xml";

        public static string XSLFileName = @"../../Request2DataSet.xslt";
        public static string XSLFileNameOut = @"../../DataSet2Request.xslt";
        public static string XMLFileName = @"../../RequestDemo.xml";

        public class Node : XObject
        {
            private string _name = "";
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public Node()
            {
            }
            public Node(string name)
            {
                _name = name;
            }
        }
        public class NodeList : XObject
        {
            private XCollection<Node> _list = new XCollection<Node>();
            public XCollection<Node> List
            {
                get { return _list; }
                set { _list = value; }
            }

            private XCollection<HL7EventType> _events = new XCollection<HL7EventType>();
            public XCollection<HL7EventType> Events
            {
                get { return _events; }
                set { _events = value; }
            }
        }

        public static Logging Log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log = new Logging("XmlTest");
            Log.WriteAppStart("XmlTest");

            SocketLogMgt.DumpData = true;
            SocketLogMgt.OnLog += new SocketLogHandler(SocketLogMgt_OnLog);
            SocketLogMgt.OnError += new EventHandler(SocketLogMgt_OnError);

            //NodeList list = new NodeList();
            //list.List.Add(new Node("a"));
            //list.List.Add(new Node("b"));
            //list.List.Add(new Node("c"));

            //list.Events.Add(HL7EventType.A13);
            //list.Events.Add(HL7EventType.A12);

            //string xml = list.ToXMLString();
            //MessageBox.Show(xml);

            //list = XObjectManager.CreateObject(xml, typeof(NodeList)) as NodeList;
            //MessageBox.Show(list.ToXMLString());

            //return;

            //Type t = Type.GetType(typeof(Program).ToString());
            //MessageBox.Show(t.ToString(), t.FullName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormInbound());
            //Application.Run(new FormXSLT());
            //Application.Run(new FormList());
            //Application.Run(new FormXIS());
            Application.Run(new FormMain());


            Log.WriteAppExit("XmlTest");
        }

        static void SocketLogMgt_OnError(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[SOCKET_EXCEPTION] ");
            if (sender == null)
            {
                sb.Append("[NULL]");
            }
            else
            {
                sb.Append(sender.ToString());
            }
            Exception err = SocketLogMgt.LastError;
            if (err == null)
            {
                sb.Append(" [NULL]");
            }
            else
            {
                sb.Append("\r\n");
                sb.Append(err.ToString());
            }
            Program.Log.Write(sb.ToString());
        }

        static void SocketLogMgt_OnLog(SocketLogType type, object sender, string message)
        {
            StringBuilder sb = new StringBuilder();
            switch (type)
            {
                default:
                case SocketLogType.Debug:
                    {
                        if (sender == null)
                        {
                            sb.Append("[SOCKET_CLIENT] ");
                        }
                        else
                        {
                            sb.Append("[SOCKET_SERVER] ");
                        }
                        break;
                    }
                case SocketLogType.Error:
                    {
                        sb.Append("[SOCKET_ERROR] ");
                        break;
                    }
                case SocketLogType.Warning:
                    {
                        sb.Append("[SOCKET_WARNING] ");
                        break;
                    }
            }
            if (message == null)
            {
                sb.Append("[NULL]");
            }
            else
            {
                sb.Append(message);
            }
            Program.Log.Write(sb.ToString());
        }
    }
}