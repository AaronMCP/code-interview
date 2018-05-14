using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Windows.Forms;
using System.Configuration;
using Server.ClientFramework.Common;
using System.Runtime.Remoting.Lifetime;
namespace CommonGlobalSettings.BillBoard
{
    public static class Utility
    {
        private static InfoCenter server = null;
        private static HttpChannel channel = null;

        /// <summary>
        /// Send note 2 BillboardServer.
        /// Do not throw exception
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static int SendNote(GCRISNote note)
        {
            try
            {
                GetBillBoardServiceObject();

                if (server != null)
                {
                    server.BroadCast(note);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("BillBoardService", ex.Message, System.Diagnostics.EventLogEntryType.Error);
                return -1;
            }
        }

        public static int SendNote(GCRISNote note, ref string strError)
        {
            try
            {
                GetBillBoardServiceObject();

                if (server != null)
                {
                    server.BroadCast(note);
                    return 0;
                }
                else
                {
                    strError = "Could not get BillBoardServiceObject";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// Get bill board remoting service client object
        /// </summary>
        /// <returns>Bill board remoting service client object reference, if failed, return null</returns>
        public static InfoCenter GetBillBoardServiceObject()
        {
            string strUrl = System.Configuration.ConfigurationManager.AppSettings["BillBoardServiceURL"];

            return GetBillBoardServiceObject(strUrl);
        }


        public static InfoCenter GetBillBoardServiceObject(string billboardUrl)
        {
            try
            {
                if (server == null)
                {
                    if (billboardUrl == null || billboardUrl == "")
                        return null;
                    MyCryptography myCrypt = new MyCryptography("GCRIS2-20061025");
                    string strDeEncyptUrl = myCrypt.DeEncrypt(billboardUrl);
                    //string strDeEncyptUrl = "http://150.245.176.128:8080/BillBoardBroadcastService.soap";

                    BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
                    BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
                    serverProvider.TypeFilterLevel = TypeFilterLevel.Full;

                    IDictionary props = new Hashtable();
                    props["port"] = 0;

                    int timeOut = 5;//5 seconds
                    Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["BillboardTimeOut"], out timeOut);

                    props["connectionTimeout"] = timeOut;

                    channel = new HttpChannel(props, clientProvider, serverProvider);
                    ChannelServices.RegisterChannel(channel, false);

                    server = (InfoCenter)Activator.GetObject(
                        typeof(InfoCenter), strDeEncyptUrl);
                }

                return server;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
