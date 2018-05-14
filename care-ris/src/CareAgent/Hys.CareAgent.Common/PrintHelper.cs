#region

using C1.C1Report;
using Foxit.PDF.Printing;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Hys.CareAgent.Upload;
//using RISLite.AXControls.FtpClientLibrary;

#endregion

namespace Hys.CareAgent.Common
{
    public static class PrintHelper
    {
        private const string _tempLocation = @"c:\Haoyisheng\temppdf";
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");

        public static IEnumerable<string> GetAllPrinters()
        {
            var printers = PrinterSettings.InstalledPrinters;
            return printers.Cast<string>();
        }

        public static string GetDefaultPrinter()
        {
            var settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }
            return string.Empty;
        }

        private static string SaveUrlToLocalPdf(string url)
        {
            string urlLink = Uri.UnescapeDataString(url);
            urlLink = urlLink.Substring(urlLink.LastIndexOf('/') + 1);
            if (!Directory.Exists(_tempLocation)) Directory.CreateDirectory(_tempLocation);
            var fileName = Path.Combine(_tempLocation, urlLink);

            using (var client = new WebClient())
            {
                var buffer = client.DownloadData(url);
                using (var file = File.Open(fileName, FileMode.Create))
                {
                    file.Write(buffer, 0, buffer.Length);
                }
            }
            return fileName;
        }

        private static string SaveUrlToLocalPdfviaFtp(string url, string server, int port, string name, string pwd)
        {
            FTPClient ftpClient = new FTPClient();
            ftpClient.FTPLogin(server, port, name, pwd);
            if (!Directory.Exists(_tempLocation)) Directory.CreateDirectory(_tempLocation);
            string urlLink = Uri.UnescapeDataString(url);
            urlLink = urlLink.Substring(urlLink.LastIndexOf('/') + 1);
            var fileName = Path.Combine(_tempLocation, urlLink);
            ftpClient.DownLoadFile(fileName, url);

            return fileName;
        }

        private static void PrintPDF(string template, DataTable dtData, string accno, string templateType, string printer = "")
        {
            try
            {
                _logger.Debug(dtData.Rows.Count.ToString());

                if (templateType == "4" || templateType == "5")
                {
                    dtData.Columns.Remove("Image"); // this column type maybe changed to string by serialization, correct it
                    dtData.Columns.Add("Image", typeof(Image));
                    // generate barcode
                    Barcode bc = new Barcode();
                    Bitmap bmp = null;
                    bc.GenerateBarcode("", true, accno, ref bmp);

                    foreach (var row in dtData.Rows)
                    {
                        ((DataRow)row)["Image"] = bmp;
                    }
                }

                byte[] byteArray = Encoding.UTF8.GetBytes(template);
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    using (C1.C1Report.C1Report c1ReportNb = new C1Report())
                    {
                        c1ReportNb.Load(stream, "Template");
                        c1ReportNb.DataSource.Recordset = dtData.DefaultView;

                        c1ReportNb.Render();
                        if (string.IsNullOrEmpty(printer) || printer == "null")
                        {
                            c1ReportNb.Document.PrinterSettings.PrinterName = GetDefaultPrinter();
                        }
                        else
                        {
                            c1ReportNb.Document.PrinterSettings.PrinterName = printer;
                        }
                        c1ReportNb.Document.Print();

                        _logger.Debug("PrintPDF End");
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.Debug(exception.ToString());
            }
        }

        public static void PrintPDF(string accno, string modalityType, string templateType, string url)
        {
            var context = new HISConClient.GCRISService.Context();
            context.MessageName = "GetPrintData";
            context.Parameters = string.Format("Accno={0}&ModalityType={1}&TemplateType={2}", accno, modalityType, templateType);
            var client = new HISConClient.GCRISService.GCRISController(url);
            HISConClient.GCRISService.DataSetActionResult result
                = client.DoCommandNoLogin(context) as HISConClient.GCRISService.DataSetActionResult;
            _logger.Debug(result.Result.ToString());
            if (result.Result)
            {
                var dataTable = result.DataSetData.Tables[0];
                var template = result.ReturnString;

                PrintPDF(template, dataTable, accno, templateType);
            }
        }

        public static void PrintOtherReport(string accno, string modalityType, string templateType, string site, string url, string printer)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                using (var client = new HttpClient())
                {
                    var parameters = String.Format("?accno={0}&modalityType={1}&site={2}&templateType={3}", accno, modalityType, site, templateType);
                    HttpResponseMessage response = client.GetAsync(url + parameters).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsAsync<PrintDataDto>().Result;
                        if (result != null && result.data != null && !String.IsNullOrEmpty(result.Template))
                        {
                            PrintPDF(result.Template, result.data, accno, templateType, printer);
                        }
                    }
                    else
                    {
                        _logger.DebugFormat("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    }
                }
            });
        }

        public static void PrintReport(string id, string site, string domain, string url, string printtemplateid, string printer)
        {
            using (var client = new HttpClient())
            {
                var parameters = String.Format("?id={0}&site={1}&domain={2}&printtemplateid={3}", id, site, domain, printtemplateid);
                HttpResponseMessage response = client.GetAsync(url + parameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<ShowHtmlDataDto>().Result;

                    if (result != null && result.data != null && !String.IsNullOrEmpty(result.Template))
                    {
                        //base64 to database
                        byte[] dataBytes = Convert.FromBase64String(result.data);
                        using (MemoryStream ms = new MemoryStream(dataBytes, 0, dataBytes.Length))
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            DataTable dt = (DataTable)bf.Deserialize(ms);
                            //use c1 to print
                            PrintReportProcess(result.Template, dt, printer);
                        }
                    }
                }
                else
                {
                    _logger.DebugFormat("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        public static void PrintJpgReport(string jpgurl, string server, int port, string name, string pwd)
        {
            string localpdfpath;
            localpdfpath = jpgurl.Contains("ftp") ? SaveUrlToLocalPdfviaFtp(jpgurl, server, port, name, pwd) : SaveUrlToLocalPdf(jpgurl);
            ProcessStartInfo info = new ProcessStartInfo(localpdfpath); //in this pass the file path
            info.Verb = "Print";
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(info);
        }

        /// <summary>
        /// c1 print
        /// </summary>
        /// <param name="templateInfo"></param>
        /// <param name="dtData"></param>
        private static void PrintReportProcess(string templateInfo, DataTable dtData, string printer)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(templateInfo);
            using (C1Report c1rpt = new C1Report())
            {
                c1rpt.Load(xmlDocument, "Template");
                DataSet newds = new DataSet();
                newds.Tables.Add(dtData);
                c1rpt.DataSource.Recordset = newds.Tables[0];

                c1rpt.Render();
                if (string.IsNullOrEmpty(printer) || printer == "null" || printer == "undefined")
                {
                    c1rpt.Document.PrinterSettings.PrinterName = GetDefaultPrinter();
                }
                else
                {
                    c1rpt.Document.PrinterSettings.PrinterName = printer;
                }
                c1rpt.Document.Print();
            }
        }

        public static void PrintHtml(string htmlValue)
        {
            _logger.Debug("Start decode html...");

            string newHtmlValue = htmlValue.Trim("\"".ToCharArray()).Replace(@"<title>Template</title>", @"<title></title>");

            _logger.Debug("Start print html...");

            WebBrowser webBrowserForPrinting = new WebBrowser();
            webBrowserForPrinting.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(PrintDocument);
            webBrowserForPrinting.DocumentText = newHtmlValue;
        }

        public static string ShowHtmlData(string id, string domain, string site, string url, string printTemplateID)
        {

            using (var client = new HttpClient())
            {
                var parameters = String.Format("?id={0}&domain={1}&site={2}&printtemplateid={3}", id, domain, site, printTemplateID);
                HttpResponseMessage response = client.GetAsync(url + parameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<ShowHtmlDataDto>().Result;

                    if (result != null && result.data != null && !String.IsNullOrEmpty(result.Template))
                    {
                        byte[] dataBytes = Convert.FromBase64String(result.data);
                        using (MemoryStream ms = new MemoryStream(dataBytes, 0, dataBytes.Length))
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            DataTable dt = (DataTable)bf.Deserialize(ms);
                            return CovertDataToHtml(result.Template, dt);
                        }
                    }
                }
                else
                {
                    var responseText = response.Content.ReadAsStringAsync().Result;
                    _logger.DebugFormat("{0} ({1})", (int)response.StatusCode, responseText);
                }
            }

            return "";
        }

        private static string CovertDataToHtml(string templateInfo, DataTable dtData)
        {
            string content;

            string rptGuid = GetFirstRowValueFromDataSet(dtData, "TREPORT__REPORTGUID");

            string savePath = Utilities.GenerateTempSaveFolder();
            Utilities.DeleteOutdatedFolder();
            string saveFile = Path.Combine(savePath, rptGuid + ".htm");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(templateInfo);
            using (C1Report c1rpt = new C1Report())
            {
                c1rpt.Load(xmlDocument, "Template");
                DataSet newds = new DataSet();
                newds.Tables.Add(dtData);
                c1rpt.DataSource.Recordset = newds.Tables[0];

                c1rpt.RenderToFile(saveFile, FileFormatEnum.HTML);
            }

            content = File.ReadAllText(saveFile);

            int startIndex = content.IndexOf("src=\"", StringComparison.Ordinal);
            while (startIndex > 0)
            {
                int endIndex = content.IndexOf("\"", startIndex + 6, StringComparison.Ordinal);
                if (endIndex > 0)
                {
                    string imagePath = content.Substring(startIndex + 5, endIndex - startIndex - 5);
                    byte[] imageArray = System.IO.File.ReadAllBytes(savePath + "\\" + imagePath);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    content = content.Substring(0, startIndex + 5) + "data:image/jpeg;base64," + base64ImageRepresentation + content.Substring(endIndex);
                }

                startIndex = content.IndexOf("src=\"", startIndex + 6, StringComparison.Ordinal);
            }

            return content;
        }



        private static string GetFirstRowValueFromDataSet(DataTable dt, string colName)
        {
            // ignoring multi rows
            if (dt == null || dt.Rows.Count <= 0)
            {
                return "";
            }

            if (!dt.Columns.Contains(colName))
            {
                return "";
            }

            string ret = "";

            foreach (DataRow dr in dt.Rows)
            {
                ret = dr[colName].ToString();

                break;
            }

            return ret;
        }

        private static void PrintDocument(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var webBrowser = sender as WebBrowser;
            if (webBrowser != null)
            {
                webBrowser.Print();
                webBrowser.Dispose();
            }

            _logger.Debug("Print html completed...");
        }

        public static void Print(string pdfPrinter, string printerName, string pdfFileName)
        {
            PrintJob printJob = new PrintJob(printerName, pdfFileName);
            printJob.Print();
        }
    }

    public class PrintDataDto
    {
        public string Template { get; set; }
        public DataTable data { get; set; }
    }

    public class ShowHtmlDataDto
    {
        public string Template { get; set; }
        public string data { get; set; }
    }
}