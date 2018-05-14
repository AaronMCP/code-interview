using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Windows.Forms;
using System.Text;
using System.Runtime.Remoting.Lifetime;
using System.Configuration;

namespace CommonGlobalSettings.BillBoard
{
    public class InfoCenter : MarshalByRefObject
    {
        private List<ClientInfo> m_clients;
        private object objLock = new object();

        public InfoCenter()
        {
            m_clients = new List<ClientInfo>();
        }

        public override object InitializeLifetimeService()
        {
            return null; //make the life time unlimited
        }

        /// <summary>
        /// Revork
        /// </summary>
        public void Revork()
        {
            return;
        }

        /// <summary>
        /// Broadcast info to client according to note info.
        /// </summary>
        /// <param name="note">billboard info</param>
        public void BroadCast(GCRISNote note)
        {
            BroadcastEventArgs e = new BroadcastEventArgs();
            e.Note = note;
            List<ClientInfo> matchedLists;
            string[] arrTmp;
            arrTmp = note.GroupId.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            switch (note.GroupType)
            {
                case (int)BulletinGroupType.Department:
                    if (note.GroupId == "0") //Send to all clients
                        matchedLists =  m_clients.FindAll(
                            x => (string.IsNullOrEmpty(note.Site) == true ? true : x.ClientUserInfo.Site.Equals(note.Site, StringComparison.InvariantCultureIgnoreCase)));
                    else
                        matchedLists = m_clients.FindAll(
                                x => Array.Exists(arrTmp, m => m.Equals(x.ClientUserInfo.Department, StringComparison.OrdinalIgnoreCase))
                                && (string.IsNullOrEmpty(note.Site) == true ? true : x.ClientUserInfo.Site.Equals(note.Site, StringComparison.InvariantCultureIgnoreCase)));
                    break;
                case (int)BulletinGroupType.Role:
                    matchedLists = m_clients.FindAll(
                            x => Array.Exists(arrTmp, m => m.Equals(x.ClientUserInfo.UserRole,StringComparison.OrdinalIgnoreCase))
                            && (string.IsNullOrEmpty(note.Site) == true ? true : x.ClientUserInfo.Site.Equals(note.Site, StringComparison.InvariantCultureIgnoreCase)));
                    break;
                case (int)BulletinGroupType.Person:
                    matchedLists = m_clients.FindAll(
                        x => string.Compare(x.ClientUserInfo.UserId,
                            note.GroupId,
                            StringComparison.OrdinalIgnoreCase) == 0);
                    break;
                default:
                    return;
            }

            SendMsg2ClientsHandler asyncSendMsg = new SendMsg2ClientsHandler(SendMsg2Clients);
            asyncSendMsg.BeginInvoke(matchedLists, e, null, null);
        }

        /// <summary>
        /// Register client infomation to server
        /// </summary>
        /// <param name="userInfo">User infomation</param>
        /// <param name="brdCastEvtHandler">Receive billboard func at client</param>
        public void RegisterClient(UserInfo userInfo, BroadcastEventHandler brdCastEvtHandler)
        {
            ClientInfo clientInfo = m_clients.Find(x => x.ClientUserInfo.UserId == userInfo.UserId);
            if (clientInfo != null)
            {
                m_clients.Remove(clientInfo);
            }
            m_clients.Add(new ClientInfo(userInfo, brdCastEvtHandler));
        }

        public void UnRegister(string userId)
        {
            ClientInfo client = m_clients.Find(x => x.ClientUserInfo.UserId == userId);
            m_clients.Remove(client);
        }

        private delegate void SendMsg2ClientsHandler(List<ClientInfo> clients, BroadcastEventArgs e);

        private void SendMsg2Clients(List<ClientInfo> clients, BroadcastEventArgs e)
        {
            lock (objLock)
            {
               // List<ClientInfo> invalidClients = new List<ClientInfo>();
                foreach (ClientInfo clientInfo in clients)
                {
                    try
                    {
                        clientInfo.ClientBrdCastHandler(this, e);
                    }
                    catch (Exception ex)
                    {
                        //System.Diagnostics.EventLog.WriteEntry("BillBoardService", string.Format("Fail to send userName{0};Title:{1];Error:{2}", clientInfo.ClientUserInfo.UserName, e.Note.Title, ex.Message), System.Diagnostics.EventLogEntryType.Error);
                        WriteLog(string.Format("Error:{0};Fail to send user:{1};Title:{2};", ex.Message, clientInfo.ClientUserInfo.UserName, e.Note.Title));
                 //       invalidClients.Add(clientInfo);
                        //Resend again
                        //Delay 2s
                        System.Threading.Thread.Sleep(2000); //2秒 
                        try
                        {
                            clientInfo.ClientBrdCastHandler(this, e);
                        }
                        catch (Exception exx)
                        {
                            WriteLog(string.Format("Delay 2s and ReSend Error:{0};Fail to send user:{1};Title:{2};", ex.Message, clientInfo.ClientUserInfo.UserName, e.Note.Title));
                            m_clients.Remove(clientInfo);
                        }


                    }
                }
                //clients.RemoveAll((x) => invalidClients.Contains(x));

                clients.Clear();
            }
        }

        private void WriteLog(string szDescription)
        {
            DateTime dtSystemTime = DateTime.Now;
            StringBuilder szLog = new StringBuilder();
            szLog.AppendFormat("{0:u}" + " ", dtSystemTime);
            szLog.AppendFormat("`" + "{0}" + "`" + "{1}" + "`", "BillbaordServer", szDescription);
            szLog.AppendFormat("`" + "{0:d}" + "\r\n", 0);

            string strLogPath = System.Environment.CurrentDirectory + "\\" + "RISGCBillboard.Log";
            Configuration configApp = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            if (configApp != null)
            {
                strLogPath = System.IO.Path.GetDirectoryName(configApp.FilePath) + "\\" + "RISGCBillboard.Log";
            }

            System.IO.File.AppendAllText(strLogPath, szLog.ToString());

        }
        #region old code
        ///// <summary>
        ///// BroadCast billdinfo to all clients
        ///// </summary>        
        //public void BroadCast(string pGuid, string pTitle, string pBody, string pLevel, int pType, string pBeginDateTime, string pEndDateTime, long pInterval,
        //    long pShowTime, string pAttachmentURL, string pCreateDate, string pCreator, string pSubmitter, string pSubmitDate, string pSubmitTo, string pApprover,
        //    string pApproveDate, string pRejector, string pRejectDate, string pRejectCause, int pState, string pOperationHistory)
        //{
        //    try
        //    {
        //        BroadcastEventArgs e = new BroadcastEventArgs(pGuid, pTitle, pBody, pLevel, pType, pBeginDateTime, pEndDateTime, pInterval, pShowTime, pAttachmentURL, pCreateDate, pCreator, pSubmitter, pSubmitDate, pSubmitTo, pApprover, pApproveDate, pRejector, pRejectDate, pRejectCause, pState, pOperationHistory);

        //        AsyncBroadCast asyncBroadCast = new AsyncBroadCast(DoBroadCasting);
        //        asyncBroadCast.BeginInvoke(e, null, null);

        //        Delegate[] invocationlist = BroadCaster.GetInvocationList();
        //    }
        //    catch (Exception err)
        //    {
        //        System.Diagnostics.EventLog.WriteEntry("BillBoardService", err.Message);
        //    }
        //}

        //private delegate void AsyncBroadCast(BroadcastEventArgs e);
        //private void DoBroadCasting(BroadcastEventArgs e)
        //{
        //    lock (objLock)
        //    {
        //        Delegate[] invocationlist = BroadCaster.GetInvocationList();

        //        if (invocationlist != null)
        //        {
        //            foreach (Delegate del in invocationlist)
        //            {
        //                try
        //                {
        //                    if (del.Method != null)
        //                    {
        //                        BroadcastEventHandler beh = (BroadcastEventHandler)del;
        //                        beh(this, e);//发出事件
        //                    }
        //                    else
        //                    {
        //                        BroadCaster = BroadCaster - (BroadcastEventHandler)del;
        //                    }
        //                }
        //                catch
        //                {
        //                    BroadCaster = BroadCaster - (BroadcastEventHandler)del;
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion
    }
}
