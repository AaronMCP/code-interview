using System;
using System.Collections.Generic;
using System.Text;

namespace Hys.Common
{
    public interface IFtpClient
    {
        string UploadFile(string fileName, string filePathInServer, Int64 ImageQualityLevel);
        string UploadFile(string fileName, string filePathInServer);
        bool DownloadFile(string remoteFileName, string localFileName);
        bool DownloadFile(string remoteFileName, string localFileName, FtpConnectionState state);
        bool DeleteFile(string fileName);
        bool DeleteDirectory(string dirName);
        bool RenameFile(string oldFileName,string newFileName,bool overWrite);
        bool MakeDir(string dir);
        string[] GetFileList(string mask);
        void Close();
        void SetResponseTimeOut(int timeOut);
    }
}
