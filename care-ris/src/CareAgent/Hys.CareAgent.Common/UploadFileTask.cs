using Hys.CareAgent.Upload;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;

namespace Hys.CareAgent.Common
{

    public class UploadFileTask
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");
        private static int _MaxThreadCount = 10;
        private static List<string> _UploadingIDs = new List<string>();
        private static List<UploadTask> _UploadingTaskList = new List<UploadTask>();
        private static int _DeleteDCMTempFile = Convert.ToInt32(ConfigurationManager.AppSettings["DeleteDCMTempFile"]);

        public static void UploadERMFile()
        {

            UploadERMFileProcess();
            if (_UploadingTaskList.Count > _MaxThreadCount)
            {
                return;
            }
            //read xml, get dam

            //get reg status
            //load xml, read dam
            List<Dam> dams = DAMCommon.GetDamConfig();
            foreach (Dam dam in dams)
            {
                DAMCommon.SetDamDetail(dam);
            }

            if (DAMCommon.CommonDamDetails.Count == 0)
            {
                System.Threading.Monitor.Enter(_UploadingIDs);
                _UploadingIDs.Clear();
                System.Threading.Monitor.Exit(_UploadingIDs);

                System.Threading.Monitor.Enter(_UploadingTaskList);
                _UploadingTaskList.Clear();
                System.Threading.Monitor.Exit(_UploadingTaskList);
                return;
            }

            string scrInfo = Utilities.GetProcessID();
            _logger.Debug("scrInfo:" + scrInfo);
            try
            {
                foreach (DamDetail dam in DAMCommon.CommonDamDetails)
                {
                    //get task
                    using (var client = new HttpClient())
                    {
                        var parameters = String.Format("/api/v1/upload/uploadtasks/{0}", scrInfo);
                        _logger.Debug("url:" + dam.WebApiurl + parameters);
                        HttpResponseMessage response = client.GetAsync(dam.WebApiurl + parameters).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsAsync<List<UploadFileTaskWorkDto>>().Result;
                            foreach (UploadFileTaskWorkDto uploadFileTaskWorkDto in result)
                            {
                                UploadTask uploadTask = new UploadTask();
                                uploadTask.UniqueID = uploadFileTaskWorkDto.UniqueID;
                                uploadTask.UploadFileTaskWorkDto = uploadFileTaskWorkDto;
                                uploadTask.Dam = dam;
                                System.Threading.Monitor.Enter(_UploadingTaskList);
                                if (_UploadingTaskList.Where(t => t.UniqueID == uploadFileTaskWorkDto.UniqueID).FirstOrDefault() == null)
                                {
                                    _UploadingTaskList.Add(uploadTask);
                                    _logger.Debug("new task:" + uploadFileTaskWorkDto.UniqueID);
                                }
                                System.Threading.Monitor.Exit(_UploadingTaskList);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Threading.Monitor.Enter(_UploadingIDs);
                _UploadingIDs.Clear();
                System.Threading.Monitor.Exit(_UploadingIDs);

                System.Threading.Monitor.Enter(_UploadingTaskList);
                _UploadingTaskList.Clear();
                System.Threading.Monitor.Exit(_UploadingTaskList);
                _logger.Error("UploadERMFile error:" + ex.ToString());
            }
        }

        private static void UploadERMFileProcess()
        {
            if (_UploadingIDs.Count > _MaxThreadCount)
            {
                return;
            }

            foreach (UploadTask uploadTask in _UploadingTaskList)
            {
                bool isNew = false;
                System.Threading.Monitor.Enter(_UploadingIDs);
                if (!_UploadingIDs.Contains(uploadTask.UniqueID))
                {
                    _UploadingIDs.Add(uploadTask.UniqueID);
                    isNew = true;
                }
                System.Threading.Monitor.Exit(_UploadingIDs);
                if (isNew)
                {
                    UploadFileTaskProcess(uploadTask);
                }

                if (_UploadingIDs.Count >= _MaxThreadCount)
                {
                    return;
                }

            }
        }

        private static void UploadFileTaskProcess(UploadTask uploadTask)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {

                    UploadFileItem uploadFileItem = new UploadFileItem(uploadTask);
                    uploadFileItem.Upload();
                }
                catch (Exception ex)
                {
                    System.Threading.Monitor.Enter(_UploadingIDs);
                    if (_UploadingIDs.Contains(uploadTask.UniqueID))
                    {
                        _UploadingIDs.Remove(uploadTask.UniqueID);
                    }
                    System.Threading.Monitor.Exit(_UploadingIDs);
                    _logger.Error("UploadFile error:" + ex.ToString());
                }
            });
        }

        public class UploadFileItem
        {
            private readonly UploadTask _uploadTask;
            private readonly FTPClient ftpClient = new FTPClient();

            public UploadFileItem(UploadTask uploadTask)
            {
                _uploadTask = uploadTask;
                ftpClient.ReadParam += OnProgress;
            }

            public void Upload()
            {
                _logger.Debug("uploading task:" + _uploadTask.UniqueID);
                bool status = true;
                string srcFilePath = _uploadTask.UploadFileTaskWorkDto.SrcFilePath;
                string newSrcFilePath = srcFilePath;
                string message = "";
                try
                {
                    //if dcm, parse
                    if (_uploadTask.UploadFileTaskWorkDto.FileType == (int)UploadFileType.DICOM &&
                        !ModifyDICOMInfo(srcFilePath, ref newSrcFilePath, out message))
                    {
                        //if modify error, clear upload task
                        UploadErrorProcess(srcFilePath + ":" + message);
                        return;
                    }

                    //wait after modified
                    Thread.Sleep(5000);

                    //to ftp server
                    if (!LoginFTP())
                    {
                        UploadErrorProcess("SendFTPError001");
                        return;
                    }

                    string rootPath = _uploadTask.UploadFileTaskWorkDto.DestFilePath.Substring(0, _uploadTask.UploadFileTaskWorkDto.DestFilePath.LastIndexOf(@"\"));
                    ftpClient.MakeDirectory(rootPath);

                    status = ftpClient.UploadFile(newSrcFilePath,
                         _uploadTask.UploadFileTaskWorkDto.DestFilePath, _uploadTask.UniqueID);
                }
                catch (Exception ex)
                {
                    ClearCurrentUploadTask();
                    status = false;
                    message = ex.Message;
                    _logger.Error("UploadFile error:" + ex.ToString());
                }

                //upload complete
                if (status)
                {
                    UploadComplete(newSrcFilePath);
                }
                else
                {
                    if (message == "")
                    {
                        message = "FTP: SendFTPError002";
                    }
                    UploadErrorProcess(message);
                }
            }

            private void UploadErrorProcess(string description)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var parameters = String.Format("/api/v1/upload/uploadfail?id={0}&description={1}", _uploadTask.UploadFileTaskWorkDto.UniqueID, description);
                        HttpResponseMessage response = client.GetAsync(_uploadTask.Dam.WebApiurl + parameters).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            DeleteTask(_uploadTask.UploadFileTaskWorkDto.UniqueID);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ClearCurrentUploadTask();
                    _logger.Error("uploadfail error:" + ex.ToString());
                }
            }

            private void UploadComplete(string srcFilePath)
            {
                try
                {
                    //get folder or dcm, unreg
                    using (var client = new HttpClient())
                    {
                        var parameters = String.Format("/api/v1/upload/uploadcomplete/{0}", _uploadTask.UploadFileTaskWorkDto.UniqueID);
                        HttpResponseMessage response = client.GetAsync(_uploadTask.Dam.WebApiurl + parameters).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            DeleteTask(_uploadTask.UploadFileTaskWorkDto.UniqueID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClearCurrentUploadTask();
                    _logger.Error("uploadcomplete error:" + ex.ToString());
                }

                //if dcm, delete temp file, config item: 1 : delete tempfile
                if (_uploadTask.UploadFileTaskWorkDto.FileType == (int)UploadFileType.DICOM && _DeleteDCMTempFile == 1)
                {
                    try
                    {
                        File.Delete(srcFilePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Delete error:" + ex.ToString());
                    }
                }
            }

            private bool LoginFTP()
            {
                bool logstatus = ftpClient.FTPLogin(_uploadTask.Dam.IP, int.Parse(_uploadTask.Dam.Port), _uploadTask.Dam.User, _uploadTask.Dam.Password);
                int retrylog = 0;
                while (!logstatus && retrylog < 3)
                {
                    //stop
                    Thread.Sleep(1500);
                    retrylog++;
                    logstatus = ftpClient.FTPLogin(_uploadTask.Dam.IP, int.Parse(_uploadTask.Dam.Port), _uploadTask.Dam.User, _uploadTask.Dam.Password);

                }
                //cannot login ftp server
                if (!logstatus)
                {
                    LoginFTPError();
                    return false;
                }

                return true;
            }

            private void LoginFTPError()
            {
                System.Threading.Monitor.Enter(_UploadingIDs);
                if (_UploadingIDs.Contains(_uploadTask.UniqueID))
                {
                    _UploadingIDs.Remove(_uploadTask.UniqueID);
                }
                System.Threading.Monitor.Exit(_UploadingIDs);
                _logger.Debug("can not log in " + _uploadTask.Dam.IP);
            }

            private bool ModifyDICOMInfo(string srcFilePath, ref string newSrcFilePath, out string message)
            {
                newSrcFilePath = "";
                message = "";
                string savePath = Utilities.GenerateTempSaveFolder() + _uploadTask.UploadFileTaskWorkDto.FileID;
                if (File.Exists(savePath))
                {
                    newSrcFilePath = savePath;
                    return true;
                }
                Utilities.DeleteOutdatedFolder();
                bool isModify = DCMParser.ModifyAccNoAndPatientID(srcFilePath, savePath, _uploadTask.UploadFileTaskWorkDto.DicomPrefix, out message);
                if (isModify)
                {
                    newSrcFilePath = savePath;
                }
                else
                {
                    DeleteTask(_uploadTask.UploadFileTaskWorkDto.UniqueID);
                    _logger.Debug("can not modify dcm file:" + srcFilePath);
                    return false;
                }
                return true;
            }

            private void ClearCurrentUploadTask()
            {
                System.Threading.Monitor.Enter(_UploadingIDs);
                if (_UploadingIDs.Contains(_uploadTask.UniqueID))
                {
                    _UploadingIDs.Remove(_uploadTask.UniqueID);
                }
                System.Threading.Monitor.Exit(_UploadingIDs);
            }

            /// <summary>
            /// delete task
            /// </summary>
            /// <param name="id"></param>
            private static void DeleteTask(string id)
            {
                System.Threading.Monitor.Enter(_UploadingTaskList);
                UploadTask uploadTask = _UploadingTaskList.Where(t => t.UniqueID == id).FirstOrDefault();
                if (uploadTask != null)
                {
                    _UploadingTaskList.Remove(uploadTask);
                }
                System.Threading.Monitor.Exit(_UploadingTaskList);

                System.Threading.Monitor.Enter(_UploadingIDs);
                if (_UploadingIDs.Contains(id))
                {
                    _UploadingIDs.Remove(id);
                }
                System.Threading.Monitor.Exit(_UploadingIDs);
            }

            private void OnProgress(int progress, long iTransferedSize,
                string id, bool saveDB)
            {
                //update progress
                //if delete, stop
                //get folder or dcm, unreg
                UploadingProgressDto uploadingProgressDto = new UploadingProgressDto();
                uploadingProgressDto.UniqueID = id;
                uploadingProgressDto.TransferedSize = iTransferedSize;
                uploadingProgressDto.Progress = progress;
                try
                {
                    //update progress to server
                    using (var client = new HttpClient())
                    {
                        var parameters = "/api/v1/upload/uploadingProgress";
                        HttpResponseMessage response = client.PostAsync<UploadingProgressDto>(_uploadTask.Dam.WebApiurl + parameters, uploadingProgressDto, new JsonMediaTypeFormatter()).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsAsync<bool>().Result;
                            //if not find task, close connect and delete task
                            if (!result)
                            {
                                ftpClient.FTPStop = true;
                                DeleteTask(id);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClearCurrentUploadTask();
                    _logger.Error("progress error:" + ex.ToString());
                }
            }
        }
    }
}
