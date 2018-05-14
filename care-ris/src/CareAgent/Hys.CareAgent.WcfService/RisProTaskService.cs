#region

using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Threading;
using Hys.CareAgent.Common;
using Hys.CareAgent.WcfService.Contract;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.Drawing;
using System.Web.Security;
using System.Linq;
using Hys.CareAgent.DAP;
using Hys.CareAgent.DAP.Entity;
using Newtonsoft.Json;
using Hys.CareAgent.WcfService.security;

#endregion

namespace Hys.CareAgent.WcfService
{
    [ServiceContract]
    public class RisProTaskService : IRisProTaskService
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");
        private static string pacsConfig;

        static RisProTaskService()
        {
            pacsConfig = Directory.GetCurrentDirectory() + @"\userconfig\pacs.json";
        }
        DAPBussiness bussiness = new DAPBussiness();

        public IEnumerable<string> AllPrinters()
        {
            return PrintHelper.GetAllPrinters();
        }

        public string DefaultPrinter()
        {
            return PrintHelper.GetDefaultPrinter();
        }

        public void Print(string accno, string modalityType, string templateType, string url)
        {
            _logger.Debug(accno + "-" + modalityType + "-" + templateType + "-" + url);
            PrintHelper.PrintPDF(accno, modalityType, templateType, url);
        }

        public string PrintOtherReport(string accno, string modalityType, string templateType, string site, string url, string printer)
        {
            _logger.Debug(accno + "-" + modalityType + "-" + templateType + "-" + url + "-" + printer);

            try
            {
                PrintHelper.PrintOtherReport(accno, modalityType, templateType, site, url, printer);
                return "0";
            }
            catch (Exception exception)
            {
                _logger.Debug(exception.ToString());
            }

            return "1";
        }

        public string PrintReport(string id, string site, string domain, string url, string printtemplateid, string printer)
        {
            _logger.Debug(id + "-" + site + "-" + domain + "-" + url + "-" + printtemplateid + "-" + printer);

            try
            {
                PrintHelper.PrintReport(id, site, domain, url, printtemplateid, printer);

                if (string.IsNullOrEmpty(printer) || printer == "null" || printer == "undefined")
                {
                    return PrintHelper.GetDefaultPrinter();
                }
                else
                {
                    return printer;
                }

            }
            catch (Exception exception)
            {
                _logger.Debug(exception.ToString());
                return String.Empty;
            }
        }

        public string PrintPdfReport(string url, string printer, string server, int port, string name, string pwd)
        {
            _logger.Debug(url + "-" + printer);

            try
            {
                PrintHelper.PrintJpgReport(url, server, port, name, pwd);

                if (string.IsNullOrEmpty(printer) || printer == "null")
                {
                    return PrintHelper.GetDefaultPrinter();
                }
                else
                {
                    return printer;
                }

            }
            catch (Exception exception)
            {
                _logger.Debug(exception.ToString());
                return String.Empty;
            }
        }

        public string ShowHtmlData(string id, string domain, string site, string url, string printtemplateid)
        {
            _logger.Debug(url);

            try
            {
                return PrintHelper.ShowHtmlData(id, domain, site, url, printtemplateid);
            }
            catch (Exception exception)
            {
                _logger.Debug(exception.ToString());
            }

            return "";
        }

        public void PrintHtml(string htmlValue)
        {
            try
            {
                PrintHelper.PrintHtml(htmlValue);
            }
            catch (Exception exception)
            {
                _logger.Debug(exception.ToString());
            }
        }

        public string GetSrcInfo(string damid, string apihost)
        {
            if (!string.IsNullOrEmpty(damid))
            {
                List<Dam> dams = new List<Dam>();
                Dam dam = new Dam
                {
                    ID = damid,
                    WebApiHostUrl = apihost
                };
                dams.Add(dam);
                DAMCommon.SetDams(dams);
            }
            return Utilities.GetProcessID();
        }

        public string GetProcessID()
        {
            return Utilities.GetProcessID();
        }

        public string OpenSelectFiles()
        {

            FormShell formSelectFiles = new FormShell() { ShowInTaskbar = false, WindowState = FormWindowState.Minimized };
            formSelectFiles.Show();
            formSelectFiles.TopMost = true;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "(*.jpg;*.bmp;*.png;*.gif;)|*.jpg;*.bmp;*.png;*.gif;|(*.pdf;*.doc;*.docx;*.txt;)|*.pdf;*.doc;*.docx;*.txt;|(*.wav;*.mp3;*.avi;)|*.wav;*.mp3;*.avi;|(*.dcm )|*.dcm";
            openFileDialog.Filter = "(*.jpg;*.bmp;*.png;*.gif;*.pdf;*.doc;*.docx;*.txt;*.wav;*.mp3;*.avi;*.dcm)|*.jpg;*.bmp;*.png;*.gif;*.pdf;*.doc;*.docx;*.txt;*.wav;*.mp3;*.avi;*.dcm";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog(formSelectFiles) == DialogResult.OK)
            {
                formSelectFiles.Close();
                return string.Join(",", openFileDialog.FileNames);
            }

            formSelectFiles.Close();
            return "";
        }

        public string OpenSelectFolder()
        {

            FormShell formSelectFiles = new FormShell() { ShowInTaskbar = false, WindowState = FormWindowState.Minimized };
            formSelectFiles.Show();
            formSelectFiles.TopMost = true;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog(formSelectFiles) == DialogResult.OK)
            {
                formSelectFiles.Close();
                return string.Join(",", folderBrowserDialog.SelectedPath);
            }
            formSelectFiles.Close();
            return "";
        }

        public bool StartMeeting(string ipaddress, string username, string userpassword, string conferenceid, string conferencepass, string showname)
        {
            string fileName = "MeetingConfig.xml";
            string templateStr = ReadMeetingTemplateFiles(fileName);
            if (templateStr == "")
            {
                return false;
            }

            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
            dataDictionary.Add("@ipaddress@", ipaddress);
            dataDictionary.Add("@username@", username);
            dataDictionary.Add("@userpassword@", userpassword);
            dataDictionary.Add("@conferenceid@", conferenceid);
            dataDictionary.Add("@conferencepass@", conferencepass);
            dataDictionary.Add("@showname@", showname);

            foreach (var itemDic in dataDictionary)
            {
                templateStr = templateStr.Replace(itemDic.Key, itemDic.Value);
            }
            Utilities.DeleteOutdatedFolder();
            string filePath = Utilities.GenerateTempSaveFolder() + fileName;
            try
            {
                File.WriteAllText(filePath, templateStr, Encoding.UTF8);
                Process process = Process.Start(Path.GetDirectoryName(Application.ExecutablePath) + Utilities.ConfPath, filePath);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
            return true;
        }

        private string ReadMeetingTemplateFiles(string fileName)
        {
            try
            {
                string fileStr = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName, Encoding.UTF8);
                return fileStr;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            return "";
        }

        public bool IfAdobeReaderInstalled()
        {
            RegistryKey adobe = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Adobe");
            if (adobe != null)
            {
                RegistryKey acroRead = adobe.OpenSubKey("Acrobat Reader");
                if (acroRead != null)
                {
                    string[] acroReadVersions = acroRead.GetSubKeyNames();
                    if (acroReadVersions.Length > 0)
                        return true;
                }
            }
            return false;
        }



        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public void OpenVideo()
        {
            try
            {
                VideoForm.VideoInstance.Show();
                VideoForm.VideoInstance.TopMost = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Hide camera
        /// </summary>
        /// <returns></returns>
        public void CloseVideo()
        {
            try
            {
                VideoForm.VideoInstance.Hide();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string CapturePhoto(int width)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                Bitmap bm = CameraSingleton.Instance.CameraCpature.GetImage();
                Size size = new Size(width, (int)(width * 0.75));
                //  bmp1 = new Bitmap(tempbmp, size);
                var bmp1 = new Bitmap(bm, size);
                bmp1.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = @"data:image/png;base64," + Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /// <summary>
        /// Play audio for remind message
        /// </summary>
        /// <param name="text">the remind message</param>
        public void PlayAudio(string text)
        {
            try
            {
                if (text.Trim().Length == 0)
                    return;
                int iErr = AudioHelper.jTTS_Init(null, null);
                AudioHelper.JTTS_CONFIG config = new AudioHelper.JTTS_CONFIG();
                iErr = AudioHelper.jTTS_Get(out config);
                config.nCodePage = (ushort)Encoding.Default.CodePage;
                AudioHelper.jTTS_Set(ref config);
                AudioHelper.jTTS_Play(text, 0);
            }
            catch (Exception ex)
            {
                _logger.Debug(ex);
            }
        }

        /// <summary>
        /// Stop playing the audio
        /// </summary>
        public void StopAudio()
        {
            try
            {
                AudioHelper.jTTS_Stop();
            }
            catch (Exception ex)
            {
                _logger.Debug(ex);
            }
        }

        public void ShowDICOMViewer(string studyinstanceuid)
        {
            FormDcmInfo formDcmInfo = new FormDcmInfo(studyinstanceuid);
            formDcmInfo.ShowInTaskbar = false;
            formDcmInfo.Show();
            formDcmInfo.TopMost = true;
        }

        public SearchResult SearchDICOMData(string patientName, string patientId, string accessionNo, int pageSize, int pageIndex)
        {
            return bussiness.SearchDICOMData(patientName, patientId, accessionNo, pageSize, pageIndex);
        }

        /// <summary>
        /// Delete DICOM
        /// </summary>
        /// <returns></returns>
        public Utilities.StatusCode DeleteDICOM(string accessionNo, string studyInstanceUid)
        {
            return bussiness.DeleteDICOM(accessionNo, studyInstanceUid);
        }

        /// <summary>
        /// Check updated DICOM
        /// </summary>
        /// <returns></returns>
        public bool CheckDICOMData(string time)
        {
            return bussiness.CheckDICOMData(time);
        }

        private bool viewImageByDesktop(DesktopClient config, string patientId, string accNo)
        {
            config.param.PatientID = patientId;
            config.param.AccessionNumber = accNo;

            var paramJson = JsonConvert.SerializeObject(config.param);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(paramJson);
            var base64String = System.Convert.ToBase64String(plainTextBytes);

            config.args.Add(base64String);

            config.args.ForEach(a => a = "\"" + a + "\"");
            var finalArgs = config.args.Select(a => "\"" + a + "\"");
            var args = string.Join(" ", finalArgs);

            var path = Path.Combine(Directory.GetCurrentDirectory(), config.path);
            if (!File.Exists(path))
            {
                return false;
            }

            var pro = Process.Start(path, args);
            pro.Dispose();

            return true;
        }

        private bool viewImageByWeb(WebClient config, string patientId, string studyId)
        {
            var url = string.Format("{0}?patientID={1}&studyUID={2}", config.url, patientId, studyId);

            var cefContainer = new CefContainer(url);
            cefContainer.Show();
            cefContainer.Activate();
            return true;
        }


        public bool ViewImage(string patientId, string accNo, string studyId)
        {
            PacsConfig pacs = JsonConvert.DeserializeObject<PacsConfig>(File.ReadAllText(pacsConfig));
            if (!pacs.desktopClient.disabled)
            {
                return viewImageByDesktop(pacs.desktopClient, patientId, accNo);
            }
            if (!pacs.webClient.disabled)
            {
                return viewImageByWeb(pacs.webClient, patientId, studyId);
            }
            return false;
        }
        /// <summary>
        /// 获取pacs配置
        /// </summary>
        /// <returns></returns>
        public string PacsConfig()
        {
            string jsonStr = (File.ReadAllText(pacsConfig));
            return jsonStr;
        }
        public bool EditPacsConfig(PacsConfig jsonPacs)
        {

            if (jsonPacs == null)
                return false;

            //File.WriteAllText(@"pacs.json", JsonConvert.SerializeObject(jsonPacs));
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(pacsConfig))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonPacs);
            }

            return true;
        }
    }
}