using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace Hys.Common
{
    public sealed class FtpClient : IFtpClient
    {
        #region Private Fields
        private FtpClientCore core = new FtpClientCore();
        private string homeDirectory = null;
        #endregion

        #region Constructor
        public FtpClient()
        {

        }

        public FtpClient(String server, int port, String userName, String password)
        {
            core.Server = server;
            core.Port = port;
            core.Username = userName;
            core.Password = password;
            core.RemotePath = ".";
        }
        #endregion

        #region Property
        public string FTPServer
        {
            get
            {
                return core.Server;
            }
        }

        public string Port 
        {
            get
            {
                return core.Port.ToString();
            }
        }

        public string UserName 
        {
            get
            {
                return core.Username;
            }
        }

        public string Password 
        {
            get
            {
                return core.Password;
            }
        }

        public int ResponseTimeOut
        {
            get
            {
                return core.Timeout;
            }
            set
            {
                core.Timeout = value;
            }
        }
        #endregion

        #region Interface Section
        public string UploadFile(string fileName)
        {
            return UploadFile(fileName, core.RemotePath);
        }

        /// <summary>
        /// Upload a file to the specified location in FTP.
        /// </summary>
        /// <param name="fileName">Full path file in local disk.</param>
        /// <param name="filePathInServer">The path relative to the Home Directory in FTP.</param>
        /// <returns>The full file path in the FTP.</returns>
        public string UploadFile(string fileName, string filePathInServer)
        {
      
           
                //Make dir according to the current date
                string[] pathItem = filePathInServer.Split('/');
                foreach (string item in pathItem)
                {
                    core.MakeDir(item);
                    core.ChangeDir(item);
                }
                
                core.Upload(fileName);

                //Change Dir to the Home Directory
                core.ChangeDir("/");

                //Quit
                core.Close();
          


            return filePathInServer;
        }

        /// <summary>
        /// Upload a image with sepcial qualityLevel to the specified location in FTP
        /// </summary>
        /// <param name="fileName">Full path file in local disk.</param>
        /// <param name="filePathInServer">The path relative to the Home Directory in FTP.</param>
        /// <param name="ImageQualityLevel">The image quality level</param>
        /// <returns>The full file path in the FTP.</returns>
        public string UploadFile(string fileName, string filePathInServer, Int64 ImageQualityLevel)
        {
            //Make dir according to the current date
            string[] pathItem = filePathInServer.Split('/');
            foreach (string item in pathItem)
            {
                core.MakeDir(item);
                core.ChangeDir(item);
            }
            //compress the image
           // System.Diagnostics.Debug.Print("Start compressing image " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //if (ImageQualityLevel >= 0 && ImageQualityLevel <= 100)
            //{
            //    Bitmap bmp = null, bmp1 = null;
            //    try
            //    {
            //        bmp = new Bitmap(fileName);
                    //int width = bmp.Size.Width;
                    //int height = bmp.Size.Height;
                    //if (bmp.Size.Width > bmp.Size.Height)
                    //{
                    //    if (bmp.Size.Width > 1024)
                    //    {
                    //        height = bmp.Size.Height * 1024 / bmp.Size.Width;
                    //        width = 1024;
                    //    }
                    //}
                    //else
                    //{
                    //    if (bmp.Size.Height > 1024)
                    //    {
                    //        width = bmp.Size.Width * 1024 / bmp.Size.Height;
                    //        height = 1024;
                    //    }
                    //}
                    //Size size = new Size(width, height);
                    ////  bmp1 = new Bitmap(tempbmp, size);
                    //bmp1 = new Bitmap(bmp, size);
                    //bmp.Dispose();
                    //ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Png);
                    //System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                    //EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, ImageQualityLevel);
                    //EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    //myEncoderParameters.Param[0] = myEncoderParameter;
                    //bmp1.Save(fileName, jgpEncoder, myEncoderParameters);
                   // bmp1.Save(fileName);
              //  }
                //finally
                //{
                //    //bmp1.Dispose();
                //}
         //   }
           // System.Diagnostics.Debug.Print("End compressing image " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            core.Upload(fileName);

            //Change Dir to the Home Directory
            core.ChangeDir("/");

            //Quit
            core.Close();

            return filePathInServer;
        }

        public  static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        /// <summary>
        /// Download the specified file from the FTP to the specified location of local.
        /// </summary>
        /// <param name="remoteFileName">The file to be download in remote machine.</param>
        /// <param name="localFileName">The local location to be stored the file from FTP.</param>
        /// <returns>true if download successfully,else false.</returns>
        public bool DownloadFile(string remoteFileName, string localFileName)
        {
            try
            {
                core.Download(remoteFileName, localFileName);
                //Quit
                core.Close();
                return true;
            }
            catch(Exception ex){
                return false;
            }
        }

        /// <summary>
        /// Download the specified file from the FTP to the specified location of local.
        /// </summary>
        /// <param name="remoteFileName">The file to be download in remote machine.</param>
        /// <param name="localFileName">The local location to be stored the file from FTP.</param>
        /// <returns>true if download successfully,else false.</returns>
        public bool DownloadFile(string remoteFileName, string localFileName, FtpConnectionState state)
        {
            core.Download(remoteFileName, localFileName);
            if (state == FtpConnectionState.CloseOnExit)
            {
                //Quit
                core.Close();
            }
            return true;
        }

        /// <summary>
        /// Delete the specified file from the FTP.
        /// </summary>
        /// <param name="fileName">The full path file name.</param>
        /// <returns>true if delete successfully,else false.</returns>
        public bool DeleteFile(string fileName)
        {
            core.DeleteFile(fileName);
            //Quit
            core.Close();
            return true;
        }

        /// <summary>
        /// Delete a ftp directory
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        public bool DeleteDirectory(string dirName)
        {
            try
            {
                core.RemoveDir(dirName);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return false;
        }

        public string[] GetFileList(string mask)
        {
            return core.GetFileList(mask);
        }

        public void Close()
        {
            core.Close();
        }

        //rename file and directory
        public bool RenameFile(string oldFileName,string newFileName,bool overwrite)
        {
            try
            {
                core.RenameFile(oldFileName, newFileName, overwrite);
                core.Close();
                return true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return false;
        }

        //makedir
        public bool MakeDir(string dir)
        {
            try
            {
                core.MakeDir(dir);
                core.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return false;
        }

        //MakePath
        public bool MakePath(string Path)
        {
            try
            {
                string[] pathItem = Path.Split('/');
                foreach (string item in pathItem)
                {
                    core.MakeDir(item);
                    core.ChangeDir(item);
                }
                core.ChangeDir("/");
                core.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// set the response from server timeout(seconds)
        /// </summary>
        /// <param name="timeOut"></param>
        public void SetResponseTimeOut(int timeOut)
        {
            ResponseTimeOut = timeOut;
        }
        #endregion
    }
}
