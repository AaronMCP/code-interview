using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Collections;

namespace Hys.CareAgent.Upload
{
    public class FTPClient
    {
        //private string RemoteHost;
        //private int RemotePort;
        ////private string RemotePath;
        //private string RemoteUser;
        //private string RemotePass;

        private bool AbortFlag = false;

        //public FTPClient(string Host, string User, string Pass, int Port)
        //{
        //    this.RemoteHost = Host;
        //    this.RemoteUser = User;
        //    this.RemotePass = Pass;
        //    this.RemotePort = Port;
        //    this.AbortFlag = false;
        //    //this.RemotePath = RemotePath;
        //}

        public FTPClient()
        {
            int block_SIZE = 102400;
            buffer = new Byte[block_SIZE];
        }

        public int BLOCK_SIZE
        {
            set
            {
                if (value > 0)
                {
                    buffer = new Byte[value];
                }
            }
        }

        public delegate void ReadParamEventHandler(int progress, long iTransferedSize,
            string id, bool saveDB);
        private ReadParamEventHandler OnReadParamEvent;
        public event ReadParamEventHandler ReadParam
        {
            add { OnReadParamEvent += new ReadParamEventHandler(value); }
            remove { OnReadParamEvent -= new ReadParamEventHandler(value); }
        }

        private bool ftpStop = false;
        public bool FTPStop
        {
            get
            {
                return ftpStop;
            }
            set
            {
                ftpStop = value;
            }
        }

        public bool UploadFile(string FilePath, string RemotePath, string id)
        {
            return Put(FilePath, RemotePath, true, id);
        }

        public bool FTPLogin(string RemoteHost, int RemotePort, string RemoteUser, string RemotePass)
        {
            socketControl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(RemoteHost), RemotePort);
            // 链接
            try
            {
                socketControl.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Couldn't connect to remote server");
            }

            // 获取应答码
            ReadReply();
            if (iReplyCode != 220)
            {
                FTPLogout();
                throw new IOException(strReply.Substring(4));
            }

            // 登陆
            SendCommand("USER " + RemoteUser);
            if (!(iReplyCode == 331 || iReplyCode == 230))
            {
                CloseSocketConnect();//关闭连接
                throw new IOException(strReply.Substring(4));
            }
            if (iReplyCode != 230)
            {
                SendCommand("PASS " + RemotePass);
                if (!(iReplyCode == 230 || iReplyCode == 202))
                {
                    CloseSocketConnect();//关闭连接
                    throw new IOException(strReply.Substring(4));
                }
            }
            //bConnected = true;

            return true;
        }


        public void FTPLogout()
        {
            if (socketControl != null)
            {
                SendCommand("QUIT");
            }
            CloseSocketConnect();
        }

        public bool ChDir(string strDirName)
        {
            if (strDirName.Equals(".") || strDirName.Equals(""))
            {
                return false;
            }

            SendCommand("CWD " + strDirName);
            if (iReplyCode != 250)
            {
                //throw new IOException(strReply.Substring(4));
                return false;
            }
            //RemotePath = strDirName;
            return true;
        }

        public string[] Dir(string strMask)
        {
            //建立进行数据连接的socket
            Socket socketData = CreateDataSocket();

            //传送命令
            SendCommand("NLST " + strMask);

            //分析应答代码
            if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226))
            {
                throw new IOException(strReply.Substring(4));
            }

            //获得结果
            strMsg = "";
            while (true)
            {
                int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                strMsg += ASCII.GetString(buffer, 0, iBytes);
                if (iBytes < buffer.Length)
                {
                    break;
                }
            }
            char[] seperator = { '\n' };
            string[] strsFileList = strMsg.Split(seperator);
            socketData.Close();//数据socket关闭时也会有返回码
            if (iReplyCode != 226)
            {
                ReadReply();
                if (iReplyCode != 226)
                {
                    throw new IOException(strReply.Substring(4));
                }
            }
            return strsFileList;
        }

        public bool MakeDirectory(string strDirName)
        {

            string[] dirPaths = strDirName.Split('\\');
            for (int i = 0; i < dirPaths.Length; i++)
            {
                if (dirPaths[i] != "")
                {
                    string newPath = "";
                    for (int j = 0; j <= i; j++)
                    {
                        newPath += dirPaths[j] + "\\";
                    }
                    SendCommand("MKD " + newPath.Substring(0, newPath.Length - 1));

                    //if (iReplyCode != 257)
                    //{
                    //    return false;
                    //}
                }

            }

            return true;

        }

        //private Boolean bConnected;

        private enum TransferType { Binary, ASCII };

        private void SetTransferType(TransferType ttType)
        {
            if (ttType == TransferType.Binary)
            {
                SendCommand("TYPE I");//binary类型传输
            }
            else
            {
                SendCommand("TYPE A");//ASCII类型传输
            }
            if (iReplyCode != 200)
            {
                throw new IOException(strReply.Substring(4));
            }
            else
            {
                trType = ttType;
            }
        }

        private TransferType GetTransferType()
        {
            return trType;
        }

        private long GetFileSize(string strFileName)
        {
            //if (!bConnected)
            //{
            //    Connect();
            //}
            SendCommand("SIZE " + strFileName);
            long lSize = 0;
            if (iReplyCode == 213)
            {
                lSize = Int64.Parse(strReply.Substring(4));
            }
            else
            {
                throw new IOException(strReply.Substring(4));
            }
            return lSize;
        }

        private void Delete(string strFileName)
        {
            //if (!bConnected)
            //{
            //    Connect();
            //}
            SendCommand("DELE " + strFileName);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
        }

        private void Rename(string strOldFileName, string strNewFileName)
        {
            //if (!bConnected)
            //{
            //    Connect();
            //}
            SendCommand("RNFR " + strOldFileName);
            if (iReplyCode != 350)
            {
                throw new IOException(strReply.Substring(4));
            }
            //  如果新文件名与原有文件重名,将覆盖原有文件
            SendCommand("RNTO " + strNewFileName);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
        }

        private void Get(string strFileNameMask, string strFolder)
        {
            //if (!bConnected)
            //{
            //    Connect();
            //}
            string[] strFiles = Dir(strFileNameMask);
            foreach (string strFile in strFiles)
            {
                if (!strFile.Equals(""))//一般来说strFiles的最后一个元素可能是空字符串
                {
                    if (strFile.LastIndexOf(".") > -1)
                    {
                        Get(strFile.Replace("\r", ""), strFolder, strFile.Replace("\r", ""), false);
                    }
                }
            }
        }

        private void Get(string strRemoteFileName, string strFolder, string strLocalFileName, bool resume)
        {
            //if (!bConnected)
            //{
            //    Connect();
            //}
            SetTransferType(TransferType.Binary);
            if (strLocalFileName.Equals(""))
            {
                strLocalFileName = strRemoteFileName;
            }
            if (!File.Exists(strFolder + "\\" + strLocalFileName))
            {
                Stream st = File.Create(strFolder + "\\" + strLocalFileName);
                st.Close();
            }
            using (FileStream output = new FileStream(strFolder + "\\" + strLocalFileName, FileMode.Open))
            {
                Socket socketData = CreateDataSocket();
                long offset = 0;
                if (resume)
                {
                    offset = output.Length;
                    if (offset > 0)
                    {
                        SendCommand("REST   " + offset);
                        if (iReplyCode != 350)
                        {
                            offset = 0;
                        }
                    }
                    if (offset > 0)
                    {
                        long npos = output.Seek(offset, SeekOrigin.Begin);
                    }
                }
                SendCommand("RETR " + strRemoteFileName);
                if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }
                while (true)
                {
                    int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                    output.Write(buffer, 0, iBytes);
                    if (iBytes <= 0)
                    {
                        break;
                    }
                }
                output.Close();
                if (socketData.Connected)
                {
                    socketData.Close();
                }
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    ReadReply();
                    if (!(iReplyCode == 226 || iReplyCode == 250))
                    {
                        throw new IOException(strReply.Substring(4));
                    }
                }
            }
        }

        public bool DownLoad(string strLocalFileName, long fileSize, string strRemoteFileName, string id)
        {
            //if (!bConnected)
            //{
            //    Connect();
            //}
            SetTransferType(TransferType.Binary);
            using (FileStream output = new FileStream(strLocalFileName, FileMode.Open))
            {
                Socket socketData = CreateDataSocket();
                long offset = 0;
                int iTransferStep = 0;
                long progress = 0;
                long writeNumber = fileSize / 20;
                long bufferRead = 0;

                SendCommand("RETR " + strRemoteFileName);
                if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }

                long size = 0;
                while (true)
                {
                    int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                    output.Write(buffer, 0, iBytes);

                    size += iBytes;
                    bufferRead += iBytes;

                    if (bufferRead > writeNumber)
                    {
                        bufferRead = 0;
                        progress = size * 100 / fileSize;
                        if (progress > 100)
                        {
                            progress = 100;
                        }
                        if (progress > iTransferStep)
                        {
                            OnReadParamEvent(int.Parse(progress.ToString()), size, id, true);
                            iTransferStep += 10;
                        }
                        else
                        {
                            OnReadParamEvent(int.Parse(progress.ToString()), size, id, false);
                        }
                    }

                    if (iBytes <= 0)
                    {
                        break;
                    }
                }
                output.Close();
                OnReadParamEvent(100, fileSize, id, true);
                if (socketData.Connected)
                {
                    socketData.Close();
                }
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    ReadReply();
                    if (!(iReplyCode == 226 || iReplyCode == 250))
                    {
                        throw new IOException(strReply.Substring(4));
                        return false;
                    }
                }

                return true;
            }
        }

        //For SelfServie Print
        public bool DownLoadFile(string strLocalFileName, string strRemoteFileName)
        {
            SetTransferType(TransferType.Binary);
            using (FileStream output = new FileStream(strLocalFileName, FileMode.Open))
            {
                Socket socketData = CreateDataSocket();

                SendCommand("RETR " + strRemoteFileName);
                if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226 || iReplyCode == 250))
                {
                    throw new IOException(strReply.Substring(4));
                }

                output.Close();
                if (socketData.Connected)
                {
                    socketData.Close();
                }
                return true;
            }
        }

        public void Abort()
        {

        }

        private bool Put(string strFileName, string RemotePath, bool resume, string id)
        {
            bool flg = true;

            Socket socketData = CreateDataSocket();
            long offset = 0;
            if (resume)
            {
                try
                {
                    SetTransferType(TransferType.Binary);
                    offset = GetFileSize(RemotePath);
                }
                catch (Exception)
                {
                    offset = 0;
                }
            }
            if (offset > 0)
            {
                SendCommand("REST " + offset);
                if (iReplyCode != 350)
                {
                    offset = 0;
                }
            }
            //SendCommand("STOR " + Path.GetFileName(strFileName));
            SendCommand("STOR " + RemotePath);
            if (!(iReplyCode == 125 || iReplyCode == 150))
            {
                throw new IOException(strReply.Substring(4));
            }
            //FileStream input = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
            FileStream input = File.Open(strFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (offset != 0)
            {
                input.Seek(offset, SeekOrigin.Begin);
            }
            int iBytes = 0;
            FileInfo f = new FileInfo(strFileName);
            long length = f.Length;
            long size = offset;
            long writeNumber = (length - size) / 20;
            long bufferRead = 0;
            long progress = 0;
            int iTransferStep = 0;

            while ((iBytes = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (this.AbortFlag)
                {
                    break;
                }

                socketData.Send(buffer, iBytes, 0);

                size += iBytes;
                bufferRead += iBytes;

                if (bufferRead > writeNumber)
                {
                    bufferRead = 0;
                    progress = size * 100 / length;
                    if (progress > 100)
                    {
                        progress = 100;
                    }
                    if (progress > iTransferStep)
                    {
                        OnReadParamEvent(int.Parse(progress.ToString()), size, id, true);
                        iTransferStep += 10;
                    }
                    else
                    {
                        OnReadParamEvent(int.Parse(progress.ToString()), size, id, false);
                    }

                    if (FTPStop)
                    {
                        input.Close();
                        if (socketData.Connected)
                        {
                            socketData.Close();
                        }
                        return false;
                    }
                }
            }
            input.Close();
            OnReadParamEvent(100, length, id, true);
            if (socketData.Connected)
            {
                socketData.Close();
            }
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                ReadReply();
                if (!(iReplyCode == 226 || iReplyCode == 250))
                {
                    //throw new IOException(strReply.Substring(4));
                    flg = false;
                }
            }

            return flg;
        }


        //public void UpdateProgress(long progress, long iTransferedSize, int fileTaskID, int iTaskID, int iPatientCaseID, bool bSaveDB, bool bUpload)
        //{
        //    NotifyStatusObject nso = new NotifyStatusObject();
        //    nso.iPatientCaseID = iPatientCaseID;
        //    nso.iTaskID = iTaskID;
        //    if (bUpload)
        //    {
        //        nso.iStatus = (int)CaseStatus.SENDING;
        //    }
        //    else
        //    {
        //        nso.iStatus = (int)CaseStatus.RESTORING;
        //    }
        //    nso.iFileTaskID = fileTaskID;
        //    nso.iTransferedSize = (int)iTransferedSize;
        //    Controller.statusNotifier.StatusNotify(nso);

        //    if (bSaveDB)
        //    {
        //        if (fileTaskID > 0)
        //        {
        //            DAPFileTask.UpdateProgress(fileTaskID, (int)progress, (int)iTransferedSize);
        //        }
        //        else
        //        {
        //            //use pack files to transfer file, so update progress for task
        //            //2013-06-04
        //            DAPFileTask.UpdateProgressForTask(iTaskID, (int)progress, (int)iTransferedSize);
        //        }
        //    }
        //}




        private void RmDir(string strDirName)
        {
            //if (!bConnected)
            //{
            //    Connect();
            //}
            SendCommand("RMD " + strDirName);
            if (iReplyCode != 250)
            {
                throw new IOException(strReply.Substring(4));
            }
        }

        private string strMsg;

        private string strReply;

        private int iReplyCode;

        private Socket socketControl;

        private TransferType trType;

        //private static int BLOCK_SIZE = 102400;
        //Byte[] buffer = new Byte[BLOCK_SIZE];
        Byte[] buffer = null;

        Encoding ASCII = Encoding.ASCII;

        private void ReadReply()
        {
            strMsg = "";
            strReply = ReadLine();
            iReplyCode = Int32.Parse(strReply.Substring(0, 3));
        }

        private Socket CreateDataSocket()
        {
            SendCommand("PASV");
            if (iReplyCode != 227)
            {
                throw new IOException(strReply.Substring(4));
            }
            int index1 = strReply.IndexOf('(');
            int index2 = strReply.IndexOf(')');
            string ipData =
             strReply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];
            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            for (int i = 0; i < len && partCount <= 6; i++)
            {
                char ch = Char.Parse(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                {
                    throw new IOException("Malformed PASV strReply: " +
                     strReply);
                }
                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("Malformed PASV strReply: " + strReply);
                    }
                }
            }
            string ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];
            int port = (parts[4] << 8) + parts[5];
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            try
            {
                s.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Can't connect to remote server");
            }
            return s;
        }

        private void CloseSocketConnect()
        {
            if (socketControl != null)
            {
                socketControl.Close();
                socketControl = null;
            }
            //bConnected = false;
        }

        private string ReadLine()
        {
            while (true)
            {
                Thread.Sleep(100);
                int iBytes = socketControl.Receive(buffer, buffer.Length, 0);
                strMsg += ASCII.GetString(buffer, 0, iBytes);
                if (iBytes < buffer.Length)
                {
                    break;
                }
            }
            char[] seperator = { '\n' };
            string[] mess = strMsg.Split(seperator);
            if (strMsg.Length > 2)
            {
                strMsg = mess[mess.Length - 2];
            }
            else
            {
                strMsg = mess[0];
            }

            if (!strMsg.Substring(3, 1).Equals(" "))
            {
                return ReadLine();
            }
            return strMsg;
        }

        private void SendCommand(String strCommand)
        {
            Byte[] cmdBytes = Encoding.Default.GetBytes((strCommand + "\r\n").ToCharArray());
            socketControl.Send(cmdBytes, cmdBytes.Length, 0);
            ReadReply();
        }
    }
}
