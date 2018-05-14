using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using Hys.Platform.CrossCutting.LogContract;
using System.Diagnostics;
using System.Text;

namespace Hys.CareRIS.Web.Controllers
{
    public class FileDownloadController : Controller
    {
        public FileDownloadController(ICommonLog logger)
        {
            _Logger = logger;
        }
        #region Fields

        private ICommonLog _Logger;

        #endregion
        public ActionResult CareAgent(string version)
        {
            var dir = Server.MapPath("~/Download/");
            var directoryInfo = new DirectoryInfo(dir);
            var filePath = directoryInfo.GetFiles(string.Format("CareAgentSetup.{0}.exe", version)).FirstOrDefault();

            if (filePath == null)
            {
                return HttpNotFound();
            }

            return File(filePath.FullName, "application/zip-x-compressed", Url.Encode(filePath.Name));
        }

        [HttpPost]
        public ActionResult Word2Html(string wordUri)
        {
            Object result = null;

            try
            {
                Type officeType = Type.GetTypeFromProgID("Word.Application");
                if (officeType == null)
                {
                    throw new Exception("No Office installed");
                }

                Uri uri = new Uri(wordUri);

                var relativePath = "/Download" + uri.AbsolutePath;
                var targetPath = Path.ChangeExtension(relativePath, "html");

                var absoluteSourcePath = Server.MapPath("~" + relativePath);
                var absoluteTargetPath = Server.MapPath("~" + targetPath);

                FileInfo wordFile = new FileInfo(absoluteSourcePath);
                FileInfo htmlFile = new FileInfo(absoluteTargetPath);

                if (!wordFile.Directory.Exists)
                {
                    wordFile.Directory.Create();
                }

                if (!htmlFile.Exists)
                {
                    if (!wordFile.Exists)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(uri, absoluteSourcePath);
                        }
                    }

                    object o_nullobject = System.Reflection.Missing.Value;
                    object o_encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;
                    object o_endings = Word.WdLineEndingType.wdCRLF;
                    object o_sourceFileName = (Object)absoluteSourcePath;
                    object o_targetFilename = (Object)absoluteTargetPath;
                    object o_formatHtml = (Object)Word.WdSaveFormat.wdFormatFilteredHTML;
                    Word.Application word = null;
                    Word.Document doc = null;
                    try
                    {
                        word = new Word.Application();
                        word.Visible = false;
                        word.ScreenUpdating = false;
                        doc = word.Documents.Open(
                            ref o_sourceFileName,
                            ref o_nullobject,
                            ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject,
                            ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject,
                            ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject);

                        doc.Activate();
                        doc.SaveAs(ref o_targetFilename,
                            ref o_formatHtml,
                            ref o_nullobject,
                            ref o_nullobject,
                            ref o_nullobject,
                            ref o_nullobject,
                            ref o_nullobject,
                            ref o_nullobject,
                            ref o_nullobject,
                            ref o_nullobject,
                            ref o_nullobject,
                            ref o_encoding,
                            ref o_nullobject,
                            ref o_nullobject,
                            ref o_endings,
                            ref o_nullobject);
                    }
                    finally
                    {
                        object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;

                        var documentDoc = ((Word._Document)doc);
                        if (documentDoc != null)
                            documentDoc.Close(ref saveChanges, ref o_nullobject, ref o_nullobject);

                        doc = null;

                        var applicationWord = ((Word._Application)word);
                        if (applicationWord != null)
                            applicationWord.Quit(ref o_nullobject, ref o_nullobject, ref o_nullobject);

                        word = null;
                    }
                }
                result = new { success = true, url = targetPath };
            }
            catch (Exception ex)
            {
                result = new { success = false, message = ex.Message };
            }

            return Json(result);
        }
    }
}