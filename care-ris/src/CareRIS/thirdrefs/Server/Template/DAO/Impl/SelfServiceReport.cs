using System;
using System.Collections.Generic;
using System.Web;
//using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using C1.C1Report;
using System.IO;
using SelfReportData;
using System.Drawing.Imaging;
using Server.Utilities.LogFacility;

namespace Server.DAO.Templates.Impl
{
    //public class SelfServiceReport : System.Web.Services.WebService
    public class SelfServiceReport
    {
        const string FIELDNAME_tReport__WYS = "TREPORT__WYS";
        const string FIELDNAME_tReport__WYSText = "TREPORT__WYSTEXT";
        const string FIELDNAME_tReport__WYG = "TREPORT__WYG";
        const string FIELDNAME_tReport__WYGText = "TREPORT__WYGTEXT";
        const string FIELDNAME_CREATORSIGNIMAGE = "CREATORSIGN";
        const string FIELDNAME_FIRSTAPPROVERSIGNIMAGE = "FIRSTAPPROVERSIGN";
        const string FIELDNAME_SUBMITTERSIGNIMAGE = "SUBMITTERSIGN";
        const string FIELDNAME_EXAMNUMBER = "PATIENTEXAMNO";
        const string FIELDNAME_REPORTGUID = "TREPORT__REPORTGUID";
        const string FIELDNAME_PATIENTID = "TREGPATIENT__PATIENTID";
        const string FIELDNAME_ORDERGUID = "TREGORDER__ORDERGUID";
        private static DataTable _dt4Print = null;
        private static List<string> listPrintTemplate = new List<string>();
        private static DataTable m_dtPrint;
        //protected log4net.ILog logger = log4net.LogManager.GetLogger("Webservice");
        LogManagerForServer logger2 = new LogManagerForServer("TemplateServerLoglevel", "0C00");
        private int iReportPics = 0;
        private string Connstring = "";


        public SelfServiceReport()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent();

            Connstring = ConfigurationManager.AppSettings["Connectionstring"];
            DataAccessLayer.MyCryptography crytoGraphy = new DataAccessLayer.MyCryptography("GCRIS2-20061025");
            Connstring = crytoGraphy.DeEncrypt(Connstring);
        }

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        //该函数根据请求的门诊卡号(或医保卡号)返回RIS系统中对应的患者编号    
        //card_type: ReferenceNo,MedicareNo,GlobalID,cardno,hisid,inhospitalno,clinicno
        //返回值：病人编号，异常时为空
        //[WebMethod]
        public string GetPatientID(string card_number, string card_type)
        {
            this.Debug("GetPatientID: card_number=" + card_number);
            this.Debug("GetPatientID: card_type=" + card_type);

            string strPatientID = "";
            SqlConnection conn = new SqlConnection();

            try
            {
                if (string.IsNullOrEmpty(card_number) || string.IsNullOrEmpty(card_type))
                {
                    if (string.IsNullOrEmpty(card_number))
                    {
                        this.Error("card_number is null");
                    }

                    if (string.IsNullOrEmpty(card_number))
                    {
                        this.Error("card_type is null");
                    }

                    return strPatientID;
                }

                string strSQL = "";
                if (DataClass.listPatient.Contains(card_type.ToUpper()))
                {
                    strSQL = string.Format("select PatientID from tRegPatient where {0}='{1}'", card_type, card_number);
                    this.Debug(strSQL);
                }

                if (DataClass.listOrder.Contains(card_type.ToUpper()))
                {
                    strSQL = string.Format("select top 1 PatientID from tRegPatient where patientguid in(select patientguid from tregorder where {0}='{1}')", card_type, card_number);
                    this.Debug(strSQL);
                }
                if (string.IsNullOrEmpty(strSQL))
                {
                    this.Debug("SQL Sentence is null");
                    return "";
                }

                conn.ConnectionString = Connstring;
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strSQL;
                this.Debug(strSQL);
                Object obj = cmd.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                {
                    strPatientID = Convert.ToString(obj);
                }

            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return strPatientID;
        }

        /// <summary>
        /// 取病人信息
        /// </summary>
        /// <param name="card_number"></param>
        /// <param name="card_type"></param>
        /// <param name="strPatientID"></param>
        /// <param name="strPatientName"></param>
        /// <returns></returns>
        //[WebMethod]
        public bool GetPatientInfo(string card_number, string card_type, out string strPatientID, out string strPatientName)
        {
            this.Debug("GetPatientID: card_number=" + card_number);
            this.Debug("GetPatientID: card_type=" + card_type);
            strPatientID = "";
            strPatientName = "";

            SqlConnection conn = new SqlConnection();

            try
            {
                if (string.IsNullOrEmpty(card_number) || string.IsNullOrEmpty(card_type))
                {
                    if (string.IsNullOrEmpty(card_number))
                    {
                        this.Error("card_number is null");
                    }

                    if (string.IsNullOrEmpty(card_number))
                    {
                        this.Error("card_type is null");
                    }

                    return false;
                }

                string strSQL = "";
                if (DataClass.listPatient.Contains(card_type.ToUpper()))
                {
                    strSQL = string.Format("select PatientID,LocalName from tRegPatient where {0}='{1}'", card_type, card_number);
                    this.Debug(strSQL);
                }

                if (DataClass.listOrder.Contains(card_type.ToUpper()))
                {
                    strSQL = string.Format("select PatientID,LocalName from tRegPatient where patientguid in(select patientguid from tregorder where {0}='{1}')", card_type, card_number);
                    this.Debug(strSQL);
                }
                if (string.IsNullOrEmpty(strSQL))
                {
                    this.Debug("SQL Sentence is null");
                    return false;
                }

                conn.ConnectionString = Connstring;
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strSQL;
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds, "patientinfo");
                this.Debug(strSQL);
                if (ds.Tables.Count == 0 || ds.Tables["patientinfo"].Rows.Count == 0)
                {
                    return false;
                }
                strPatientID = Convert.ToString(ds.Tables["patientinfo"].Rows[0]["PatientID"]);
                strPatientName = Convert.ToString(ds.Tables["patientinfo"].Rows[0]["LocalName"]);


            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
                return false;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return true;
        }

        //该函数返回指定检查的报告状态
        //返回值：
        // 1 未报告   该检查没有报告
        // 2 审核中   该检查有报告在审核中
        // 3 已审核   该检查所有报告都已经审核完成
        //[WebMethod]
        public int GetReportStatus(string accession_number)
        {
            this.Debug("GetReportStatus: accession_number=" + accession_number);

            int nStatus = 1;
            SqlConnection conn = new SqlConnection();
            try
            {
                if (string.IsNullOrEmpty(accession_number))
                {
                    this.Error("accession_number is null");
                    return nStatus;
                }


                conn.ConnectionString = Connstring;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                string strSQL = string.Format("select orderguid from tregorder where accno='{0}'", accession_number);
                this.Debug(strSQL);
                cmd.CommandText = strSQL;

                Object obj = cmd.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                {
                    return nStatus;
                }
                string strOrderGuid = Convert.ToString(obj);
                strSQL = string.Format("select status from tregprocedure where orderguid='{0}' group by status", strOrderGuid);
                cmd.CommandText = strSQL;
                this.Debug(strSQL);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                DataTable dt = ds.Tables[0];
                string strExpress = "status<100";
                DataRow[] drfound = dt.Select(strExpress);
                if (drfound.Length == dt.Rows.Count)
                {
                    nStatus = 1;
                    return nStatus;
                }

                strExpress = "status=120";
                drfound = dt.Select(strExpress);
                if (drfound.Length == dt.Rows.Count)
                {
                    nStatus = 3;
                    return nStatus;
                }
                nStatus = 2;


            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return nStatus;
        }

        //生成指定检查的所有报告的对应A4纸张的已排版的pdf报告文件，并返回生成的pdf文件的url列表    
        //只有当所有报告都是已审核状态才生成报告
        //pdf文件必须至少保存1个小时
        //[WebMethod]
        //public void GetPDFReportList(string accession_number, string modality_type, string report_guid, int report_src, bool is_thirdparty, out string[] pdf_urls)
        //{
        //    CleanOldFiles();

        //    this.Debug("GetPDFReportList: accession_number=" + accession_number);
        //    SqlConnection conn = new SqlConnection();
        //    pdf_urls = null;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(accession_number))
        //        {
        //            this.Error("accession_number is null");
        //            return;
        //        }


        //        conn.ConnectionString = Connstring;
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = conn;

        //        string[] paramsA = new string[2] { accession_number, report_guid };
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        string site = ConfigurationManager.AppSettings["Site"];
        //        cmd.Parameters.Add(new SqlParameter("@AccNo", accession_number));
        //        cmd.Parameters.Add(new SqlParameter("@Type", "3"));
        //        cmd.Parameters.Add(new SqlParameter("@ModalityType", modality_type));
        //        cmd.Parameters.Add(new SqlParameter("@Site", site));
        //        cmd.Parameters.Add(new SqlParameter("@ReportGuid", report_guid));

        //        SqlParameter sqlTemplateGUID = new SqlParameter("@TemplateGuid", SqlDbType.VarChar, 128);
        //        sqlTemplateGUID.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(sqlTemplateGUID);
        //        SqlParameter MergeMultiProcedures = new SqlParameter("@MergeMultiProcedures", SqlDbType.Int, 8);
        //        MergeMultiProcedures.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(MergeMultiProcedures);

        //        cmd.CommandText = "usp_GetPrintTemplate";
        //        SqlDataAdapter sda = new SqlDataAdapter();
        //        sda.SelectCommand = cmd;
        //        DataSet ds = new DataSet();
        //        sda.Fill(ds);
        //        DataTable dt = ds.Tables[0];
        //        string strTemplateGuid = cmd.Parameters["@TemplateGuid"].Value.ToString();

        //        List<string> listPdf = new List<string>();

        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                if (dr["tReport__ReportGuid"] == null || dr["tReport__ReportGuid"] is DBNull || Convert.ToString(dr["tReport__ReportGuid"]).Trim().Length == 0)
        //                {
        //                    continue;
        //                }

        //                if (dr["tReport__PrintTemplateGuid"] != null && !(dr["tReport__ReportGuid"] is DBNull))
        //                {
        //                    string strPdfFile = GenPdfFile(dr, dt, strTemplateGuid, report_src, is_thirdparty);
        //                    listPdf.Add(DataClass.PdfFileUrl + strPdfFile);
        //                    break;
        //                }
        //            }
        //        }

        //        pdf_urls = listPdf.ToArray();
        //        foreach (string strPdfFile in listPdf)
        //        {
        //            this.Debug("GetPDFReportList: pdffile=" + strPdfFile);
        //        }


        //        ////update tReport print template
        //        cmd.Parameters.Clear();
        //        cmd.CommandType = CommandType.Text;

        //        cmd.CommandText = string.Format("select PrintTemplateGuid from tReport where ReportGuid = '{0}'", report_guid);

        //        object oldobjPrintTemplateGuid = cmd.ExecuteScalar();

        //        //if (oldobjPrintTemplateGuid == null || oldobjPrintTemplateGuid == DBNull.Value)
        //        //{
        //        //    cmd.CommandText = string.Format("update tReport set PrintTemplateGuid='{0}' where ReportGuid = '{1}'", strTemplateGuid, report_guid);
        //        //    cmd.ExecuteNonQuery();
        //        //}
        //        //else
        //        //{
        //        //    string oldstrPrintTemplateGuid = oldobjPrintTemplateGuid.ToString();
        //        //    if (string.IsNullOrEmpty(oldstrPrintTemplateGuid))
        //        //    {
        //        //        cmd.CommandText = string.Format("update tReport set PrintTemplateGuid='{0}' where ReportGuid = '{1}'", strTemplateGuid, report_guid);
        //        //        cmd.ExecuteNonQuery();
        //        //    }
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        this.Error(ex.Message);
        //    }
        //    finally
        //    {
        //        if (conn != null && conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //    }

        //}

        /// <summary>
        /// update report template guid
        /// </summary>
        /// <param name="strTemplateGUID"></param>
        public void UpdateReportTemplateGUID(string strTemplateGUID)
        {

        }

        /// <summary>
        /// 初始化m_dtPrint结构
        /// </summary>
        public void InitialTemplateDatatable()
        {
            m_dtPrint = new DataTable();
            m_dtPrint.Columns.Add("Room", typeof(string));
            m_dtPrint.Columns.Add("PatientID", typeof(string));
            m_dtPrint.Columns.Add("LocalName", typeof(string));
            m_dtPrint.Columns.Add("EnglishName", typeof(string));
            m_dtPrint.Columns.Add("Birthday", typeof(DateTime));
            m_dtPrint.Columns.Add("Gender", typeof(string));
            m_dtPrint.Columns.Add("Telephone", typeof(string));
            m_dtPrint.Columns.Add("ReferenceNo", typeof(string));
            m_dtPrint.Columns.Add("Address", typeof(string));
            m_dtPrint.Columns.Add("InhospitalNo", typeof(string));
            m_dtPrint.Columns.Add("PatientType", typeof(string));
            m_dtPrint.Columns.Add("ClinicNo", typeof(string));
            m_dtPrint.Columns.Add("BedNo", typeof(string));
            m_dtPrint.Columns.Add("InhospitalRegion", typeof(string));
            m_dtPrint.Columns.Add("ApplyDoctor", typeof(string));
            m_dtPrint.Columns.Add("ApplyDept", typeof(string));
            m_dtPrint.Columns.Add("RegisterDt", typeof(DateTime));
            m_dtPrint.Columns.Add("Description", typeof(string));
            m_dtPrint.Columns.Add("ModalityType", typeof(string));
            m_dtPrint.Columns.Add("Modality", typeof(string));
            m_dtPrint.Columns.Add("BodyPart", typeof(string));
            m_dtPrint.Columns.Add("CheckingItem", typeof(string));
            m_dtPrint.Columns.Add("BookingBeginDt", typeof(string));
            m_dtPrint.Columns.Add("BookingEndDt", typeof(string));
            m_dtPrint.Columns.Add("VisitComment", typeof(string));
            m_dtPrint.Columns.Add("HealthHistory", typeof(string));
            m_dtPrint.Columns.Add("Observation", typeof(string));
            m_dtPrint.Columns.Add("Alias", typeof(string));
            m_dtPrint.Columns.Add("QueueNo", typeof(string));
            m_dtPrint.Columns.Add("RemotePID", typeof(string));
            m_dtPrint.Columns.Add("HisID", typeof(string));
            m_dtPrint.Columns.Add("RemoteAccNo", typeof(string));
            m_dtPrint.Columns.Add("CardNo", typeof(string));
            m_dtPrint.Columns.Add("MedicareNo", typeof(string));
            m_dtPrint.Columns.Add("BedSide", typeof(string));
            m_dtPrint.Columns.Add("BodyWeight", typeof(string));
            m_dtPrint.Columns.Add("BookingTimeAlias", typeof(string));
            m_dtPrint.Columns.Add("Age", typeof(string));
            m_dtPrint.Columns.Add("OrderComment", typeof(string));
            m_dtPrint.Columns.Add("ErethismType", typeof(string));
            m_dtPrint.Columns.Add("ErethismCode", typeof(string));
            m_dtPrint.Columns.Add("ErethismGrade", typeof(string));
            m_dtPrint.Columns.Add("Optional1", typeof(string));
            m_dtPrint.Columns.Add("TakeReportDate", typeof(string));
            m_dtPrint.Columns.Add("BookingNotice", typeof(string));
            m_dtPrint.Columns.Add("Image", typeof(Image));
            m_dtPrint.Columns.Add("AccNo", typeof(string));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accno"></param>
        /// <param name="modalityType"></param>
        /// <param name="templateType"></param>
        /// <param name="pdf_urls"></param>
        public void CleanOldFiles()
        {
            string folder = HttpContext.Current.Server.MapPath("~/");
            string pdfFolder = folder + "PdfReport";
            string[] pdfFiles = Directory.GetFiles(pdfFolder, "*.pdf");
            foreach (string pdfFile in pdfFiles)
            {
                DateTime fileCreatedDate = File.GetCreationTime(pdfFile);
                TimeSpan t = DateTime.Now - fileCreatedDate;
                if (t.TotalDays > 1)
                {
                    File.Delete(pdfFile);
                }
            }
        }

        ///<summary>
        ///按放射编号,设备类型,和打印类型(条形码，通知单)生成pdf和对应的pdf url
        ///<summary>
        //[WebMethod]
        //public void GetPDFListtoPrint(string accno, string modalityType, string templateType, out string[] pdf_urls)
        public void GetPDFListtoPrint(string accno, string modalityType, string templateType, ref string template, ref DataTable data)
        {
            //CleanOldFiles();            

            //pdf_urls = null;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Connstring;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            string site = ConfigurationManager.AppSettings["Site"];
            cmd.Parameters.Add(new SqlParameter("@AccNo", accno));
            cmd.Parameters.Add(new SqlParameter("@Type", templateType));
            cmd.Parameters.Add(new SqlParameter("@ModalityType", modalityType));
            cmd.Parameters.Add(new SqlParameter("@Site", site));
            cmd.Parameters.Add(new SqlParameter("@ReportGuid", ""));

            SqlParameter sqlTemplateGUID = new SqlParameter("@TemplateGuid", SqlDbType.VarChar, 128);
            sqlTemplateGUID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sqlTemplateGUID);
            SqlParameter MergeMultiProcedures = new SqlParameter("@MergeMultiProcedures", SqlDbType.Int, 8);
            MergeMultiProcedures.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(MergeMultiProcedures);

            cmd.CommandText = "usp_GetPrintTemplate";
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            string strTemplateGuid = cmd.Parameters["@TemplateGuid"].Value.ToString();
            string strMergeMultiProcedures = cmd.Parameters["@MergeMultiProcedures"].Value.ToString();
            try
            {
                if (m_dtPrint != null)
                {
                    m_dtPrint.Rows.Clear();
                }
                else
                {
                    InitialTemplateDatatable();
                }


                Barcode bc = new Barcode();
                Bitmap bmp = null;
                bc.GenerateBarcode("", true, accno, ref bmp);
                DataRow dr = m_dtPrint.NewRow();
                for (int ir = 0; ir < dt.Rows.Count; ir++)
                {
                    if (ir == 0 || strMergeMultiProcedures.CompareTo("0") == 0)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            foreach (DataColumn col in m_dtPrint.Columns)
                            {
                                if (col.ColumnName == dc.ColumnName)
                                {
                                    object objValue = dt.Rows[ir][dc.ColumnName];
                                    string Value;
                                    if (objValue.GetType() == System.Type.GetType("System.Byte[]"))
                                    {
                                        Byte[] buff = objValue as Byte[];
                                        if (dc.ColumnName == "BookingNotice")
                                        {
                                            Value = System.Text.Encoding.UTF8.GetString(buff);
                                        }
                                        else
                                        {
                                            Value = System.Text.Encoding.Default.GetString(buff);
                                        }
                                        dr[dc.ColumnName] = Value;
                                    }
                                    else if (col.DataType == typeof(System.DateTime))
                                    {
                                        dr[dc.ColumnName] = objValue;
                                    }
                                    else
                                    {
                                        Value = objValue.ToString().Trim();
                                        dr[dc.ColumnName] = GetTranslationString(dc.ColumnName, Value);
                                    }
                                    break;
                                }
                            }
                        }
                        if (strMergeMultiProcedures.CompareTo("0") == 0)
                        {
                            m_dtPrint.Rows.Add(dr);
                            dr = m_dtPrint.NewRow();
                        }
                    }
                    else
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == "CheckingItem" || dc.ColumnName == "Modality" || dc.ColumnName == "ModalityType" || dc.ColumnName == "Description")
                            {
                                if (dt.Rows[ir][dc.ColumnName].ToString().Trim() != dr[dc.ColumnName].ToString().Trim())
                                {
                                    dr[dc.ColumnName] = dr[dc.ColumnName] + "," + dt.Rows[ir][dc.ColumnName];
                                }
                            }
                            if (dc.ColumnName == "BookingNotice")
                            {
                                object objValue = dt.Rows[ir][dc.ColumnName];
                                string Value;
                                if (objValue.GetType() == System.Type.GetType("System.Byte[]"))
                                {
                                    Byte[] buff = objValue as Byte[];
                                    Value = System.Text.Encoding.UTF8.GetString(buff);
                                    dr[dc.ColumnName] = MergeRTFFiles(dr[dc.ColumnName].ToString(), Value);
                                }
                            }
                        }
                    }
                    dr["AccNo"] = accno;
                    dr["Image"] = bmp;
                }
                if (strMergeMultiProcedures.CompareTo("0") != 0)
                {
                    m_dtPrint.Rows.Add(dr);
                }

                data = m_dtPrint;
                template = LoadPrintTemplateInfo(strTemplateGuid);


                ////get print template by type, modality type
                ////string templateGuid = string.Empty;
                ////string templateName = string.Empty;
                //string templateFileName = MakePrintOtherTemplateFile(strTemplateGuid);//genPrintTemplate(accno, templateType, modalityType, out templateName, out templateGuid);

                //List<string> listPdf = new List<string>();
                //if (string.IsNullOrEmpty(templateFileName) || !System.IO.File.Exists(templateFileName))
                //{
                //    this.Error("can not get print template,error");
                //}

                //else
                //{
                //    using (C1.C1Report.C1Report c1ReportNb = new C1Report())
                //    {
                //        c1ReportNb.Load(templateFileName, "Template");

                //        c1ReportNb.DataSource.Recordset = m_dtPrint.DefaultView;

                //        string strPdfFileName = Guid.NewGuid().ToString() + ".pdf";

                //        string strPdfFile = GetCurrentWorkPath("PdfReport") + "\\" + strPdfFileName;
                //        this.Debug("GenPdfFile generate file: " + strPdfFile);

                //        c1ReportNb.RenderToFile(strPdfFile, C1.C1Report.FileFormatEnum.PDFEmbedFonts);

                //        this.Debug("GenPdfFile generate file: " + strPdfFile);
                //        listPdf.Add(DataClass.PdfFileUrl + strPdfFileName);
                //    }
                //}
                //pdf_urls = listPdf.ToArray();
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = string.Format("select distinct tRegProcedure.ReportGuid  from tRegOrder,tRegProcedure where tRegOrder.OrderGuid=tRegProcedure.OrderGuid  and tRegOrder.AccNo='{0}'", accno);
                //string strReportGuid = cmd.ExecuteScalar().ToString();

                ////WritePrintLog(strReportGuid, templateGuid, 0);
            }
            catch (Exception ex)
            {
                this.Error(ex.ToString());
            }

        }

        private string MergeRTFFiles(string strDest, string strSource)
        {
            string strMerge = strDest;
            try
            {
                if (strDest.Contains(strSource))
                {
                    return strMerge;
                }

                RichTextBox richTextBox1 = new RichTextBox();
                RichTextBox richTextBox2 = new RichTextBox();
                RichTextBox richTextBox3 = new RichTextBox();//for restore the original content in clipboard

                richTextBox3.Paste();

                richTextBox2.Copy();
                richTextBox2.Rtf = strDest;
                if (richTextBox2.Text.Trim().Length == 0)
                {
                    richTextBox2.Rtf = strSource;
                }
                else
                {
                    richTextBox2.Text = richTextBox2.Text + "\r\n";
                    richTextBox1.Rtf = strSource;
                    if (richTextBox1.Text.Trim().Length > 0)
                    {
                        richTextBox1.SelectAll();
                        richTextBox1.Copy();
                        richTextBox2.Select(richTextBox2.TextLength, 0);
                        richTextBox2.Paste();
                    }
                }
                strMerge = richTextBox2.Rtf;

                richTextBox1.Dispose();
                richTextBox2.Dispose();
                richTextBox1 = null;
                richTextBox2 = null;

                richTextBox3.SelectAll();
                richTextBox3.Copy();
                richTextBox3.SelectAll();
                richTextBox3.Paste();
                richTextBox3.Dispose();
                richTextBox3 = null;
            }
            catch (Exception ex)
            {
                this.Error("MergeRTFFiles " + ex.Message);
            }
            return strMerge;
        }

        private string GetTranslationString(string colName, string Value)
        {
            try
            {
                colName = colName.ToUpper().Trim();

                if (colName == "AGE")
                {
                    string[] split = Value.Split(new Char[] { ' ' });
                    if (split.Length != 2)
                    {
                        return Value;
                    }

                    Value = split[0] + " " + DataClass.GetDictionaryText(6, split[1]);
                }
                else if (DataClass.listUserGuid2.Contains(colName))
                {
                    Value = DataClass.GetUserLocalName(Value);
                }
                else if (DataClass.listYesNo2.Contains(colName))
                {
                    Value = DataClass.GetDictionaryText(70, Value);
                }
                else if (DataClass.listSite2.Contains(colName))
                {
                    Value = DataClass.GetSiteAlias(Value);
                }
                else if (DataClass.listDomain2.Contains(colName))
                {
                    Value = DataClass.GetDomainAlias(Value);
                }
            }
            catch (Exception ex)
            {
                this.Error("GetTranslationString " + ex.Message);
            }

            return Value;
        }

        public string GetExistBarorNoteSite(string accno)
        {
            string site = string.Empty;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = Connstring;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format("select COUNT(*) as counts from [tRegOrder] where AccNo='{0}'", accno);
                object objCounts = cmd.ExecuteScalar();
                if ((Int32)objCounts == 0)
                {
                    return "No-Site-Configured";
                }
                else
                {
                    cmd.CommandText = string.Format("select [RegSite] from [tRegOrder] where AccNo='{0}'", accno);

                    object objsite = cmd.ExecuteScalar();
                    if ((objsite is DBNull) || objsite == null)
                    {
                        site = "No-Site-Configured";
                    }
                    else
                    {
                        site = objsite.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error(ex.ToString());
                return site;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return site;
        }

        public string GetNotExistBarorNoteSite(string templateType, string domain, string modalityType)
        {
            string site = string.Empty;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = Connstring;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = string.Format("select COUNT(*) as counts from where [Type]='{0}' and [Domain]='{1}' and [ModalityType]='{2}' and [IsDefaultByModality]='1'", templateType, domain, modalityType);
                object objCounts = cmd.ExecuteScalar();
                if ((Int32)objCounts == 0)
                {
                    return "No-Site-Configured";
                }
                else
                {
                    cmd.CommandText = string.Format("select [Site] from tPrintTemplate where [Type]='{0}' and [Domain]='{1}' and IsDefaultByType='1'", templateType, domain);

                    object objsite = cmd.ExecuteScalar();
                    if ((objsite is DBNull) || objsite == null)
                    {
                        site = "No-Site-Configured";
                    }
                    else
                    {
                        site = objsite.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error(ex.ToString());
                return site;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return site;
        }

        public bool checkIfGetFixPrintTemplate(string templateType, string domain, string modalityType, string site)
        {

            bool hastype = false;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = Connstring;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;

                if (modalityType == "")
                {
                    cmd.CommandText = string.Format("select COUNT(*) as counts from tPrintTemplate where [Type]='{0}' and [Domain]='{1}' and [Site]='{3}' and IsDefaultByType='1'", templateType, domain, modalityType, site);
                }
                else
                {
                    cmd.CommandText = string.Format("select COUNT(*) as counts from tPrintTemplate where [Type]='{0}' and [Domain]='{1}' and [ModalityType]='{2}' and [Site]='{3}' and IsDefaultByModality='1'", templateType, domain, modalityType, site);
                }
                object objCounts = cmd.ExecuteScalar();
                if ((Int32)objCounts != 0)
                {
                    hastype = true;
                }
            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
                return (!hastype);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return (!hastype);

        }

        public string MakePrintOtherTemplateFile(string strTemplateGuid)
        {
            string strTemplatePath = GetCurrentWorkPath("PrintTemplate");
            string latestVersion = GetLastedtVersion(strTemplateGuid);
            string strTemplateFileName = strTemplatePath + "\\" + strTemplateGuid + "_Template_" + latestVersion + ".xml";
            FileInfo latestFileInfo = new FileInfo(strTemplateFileName);
            if (!latestFileInfo.Exists)
            {
                string strTemplateInfo = LoadPrintTemplateInfo(strTemplateGuid);
                StreamWriter fs = new StreamWriter(strTemplateFileName, false);
                fs.Write(strTemplateInfo);
                fs.Close();
            }
            return strTemplateFileName;
        }

        public string GetLastedtVersion(string strTemplateGuid)
        {
            SqlConnection conn = new SqlConnection();
            string latestVersion = "-1";
            try
            {

                conn.ConnectionString = Connstring;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format("select Version from tPrintTemplate where TemplateGuid = '{0}'", strTemplateGuid);
                this.Debug(cmd.CommandText);

                try
                {
                    latestVersion = (cmd.ExecuteScalar()).ToString();
                }
                catch (Exception ex)
                {
                    this.Error(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                this.Error(ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return latestVersion;
        }

        public string LoadPrintTemplateInfo(string strTemplateGuid)
        {
            SqlConnection conn = new SqlConnection();
            string strTemplateInfo = string.Empty;
            try
            {
                conn.ConnectionString = ConfigurationManager.AppSettings["Connstring"];
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Clear();
                cmd.CommandText = string.Format("select  TemplateInfo from tPrintTemplate where TemplateGuid='{0}'", strTemplateGuid);
                strTemplateInfo = System.Text.Encoding.Unicode.GetString(cmd.ExecuteScalar() as byte[]);
            }
            catch (Exception e)
            {
                this.Error(e.ToString());
                return null;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return strTemplateInfo;
        }

        /// <summary>
        /// 下载完成， 
        /// </summary>
        /// <param name="pdf_urls"></param>
        //[WebMethod]
        public void DownloadComplete(string[] pdf_urls)
        {
            this.Debug("DownloadComplete: pdf_urls=" + pdf_urls.ToString());
            try
            {
                foreach (string strPdfFile in pdf_urls)
                {
                    string filename = System.IO.Path.GetFileName(strPdfFile);
                    string strLocalFile = GetCurrentWorkPath("PdfReport") + "\\" + filename;
                    if (File.Exists(strLocalFile))
                    {
                        File.Delete(strLocalFile);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }

        }

        /// <summary>
        /// 根据打印报告模板产生打印表结构
        /// </summary>
        /// <param name="drReport"></param>
        private string GenPrintTable(DataRow drReport, C1.C1Report.C1Report c1rpt, string strLastTemplateGuid)
        {
            //string strLastTemplateGuid = "";
            try
            {
                //
                this.Debug("GenPrintTable begin");
                //strLastTemplateGuid = Convert.ToString(drReport["treport__PrintTemplateGuid"]);
                string strModalityType = Convert.ToString(drReport["tregprocedure__modalitytype"]);
                string strReportGuid = Convert.ToString(drReport["treport__reportguid"]);

                string strTemplateFile = "";
                strTemplateFile = GetPrintTemplate(strModalityType, ref strLastTemplateGuid);

                this.Debug("DownloadComplete: LastTemplateGuid=" + strLastTemplateGuid.ToString());
                this.Debug("DownloadComplete: ModalityType=" + strModalityType.ToString());
                this.Debug("DownloadComplete: ReportGuid=" + strReportGuid.ToString());




                _dt4Print = new DataTable();


                c1rpt.Load(strTemplateFile, "Template");

                _dt4Print.Clear();

                // add columns from template
                foreach (C1.C1Report.Field fld in c1rpt.Fields)
                {
                    string key = "";
                    if (fld.Text != null && fld.Calculated)
                    {
                        key = fld.Text.ToUpper().Trim();
                        if (!_dt4Print.Columns.Contains(key))
                            _dt4Print.Columns.Add(key, System.Type.GetType("System.String"));
                    }
                    else if (fld.Picture != null)
                    {
                        C1.C1Report.Util.PictureHolder ph = fld.Picture as C1.C1Report.Util.PictureHolder;

                        if (!string.IsNullOrEmpty(fld.Name))
                        {
                            key = string.IsNullOrEmpty(ph.FieldName) ? fld.Name.ToUpper().Trim() : ph.FieldName; //default use picture's field name
                            if (ph != null && ph.IsBound && !_dt4Print.Columns.Contains(key))
                            {
                                _dt4Print.Columns.Add(key, System.Type.GetType("System.Byte[]"));
                                iReportPics++;
                            }
                        }
                    }
                }
                //Doctor signer image
                if (!_dt4Print.Columns.Contains(FIELDNAME_CREATORSIGNIMAGE))
                {
                    _dt4Print.Columns.Add(FIELDNAME_CREATORSIGNIMAGE, System.Type.GetType("System.Byte[]"));
                }

                if (!_dt4Print.Columns.Contains(FIELDNAME_FIRSTAPPROVERSIGNIMAGE))
                {
                    _dt4Print.Columns.Add(FIELDNAME_FIRSTAPPROVERSIGNIMAGE, System.Type.GetType("System.Byte[]"));
                }

                if (!_dt4Print.Columns.Contains(FIELDNAME_SUBMITTERSIGNIMAGE))
                {
                    _dt4Print.Columns.Add(FIELDNAME_SUBMITTERSIGNIMAGE, System.Type.GetType("System.Byte[]"));
                }

                if (!_dt4Print.Columns.Contains(FIELDNAME_EXAMNUMBER))
                {
                    _dt4Print.Columns.Add(FIELDNAME_EXAMNUMBER);
                }


            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }
            return strLastTemplateGuid;
        }

        /// <summary>
        /// 打印报告用日志
        /// </summary>
        /// <param name="strPatientID"></param>
        /// <param name="strOrderGuid"></param>
        /// <param name="iReportSRC">0--rislite,1--selfservice</param>
        /// <param name="dr"></param>
        private void WritePrintLog(string strReportGuid, string strLastTemplateGuid, int iReportSRC, bool isThirdParty)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = Connstring;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = string.Format("select Status from tReport where ReportGuid = '{0}'", strReportGuid);
                object oStatus = cmd.ExecuteScalar();
                int iStatus = 0;
                if (isThirdParty)
                {
                    if (oStatus != null || oStatus != DBNull.Value)
                    {
                        string strStatus = oStatus.ToString();
                        if (int.TryParse(strStatus, out iStatus))
                        {
                            if (iStatus == 120)
                            {
                                String printdDT = DateTime.Now.ToString();
                                if (iReportSRC == 0)//report from rislite
                                {
                                    ////记录打印日志
                                    cmd.CommandText = string.Format("insert into tReportPrintLog(FileGuid,ReportGuid,Printer,PrintDt,Counts,Comments,BackupMark,BackupComment,SnapShotSrvPath,Type,PrintTemplateGuid,Domain) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                                        Guid.NewGuid().ToString(), strReportGuid, DataClass.Printer, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1, "", "", "", "", "Print", strLastTemplateGuid, DataClass.Domain);
                                    this.Debug(cmd.CommandText);
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = string.Format("UPDATE [tReport] SET isprint=1,PrintCopies=case when ([PrintCopies] is null) then 1 else PrintCopies + 1 end,PrintTemplateGuid='{0}',Optional1='{1}' where  ReportGuid = '{2}'", strLastTemplateGuid, printdDT, strReportGuid);
                                }
                                else if (iReportSRC == 1)//report from selfservice
                                {
                                    cmd.CommandText = string.Format("UPDATE [tReport] SET isprint=1,PrintTemplateGuid='{0}' ,Optional1='{1}' where  ReportGuid = '{2}'", strLastTemplateGuid, printdDT, strReportGuid);
                                }
                            }
                            else if (iStatus < 120)
                            {
                                cmd.CommandText = string.Format("UPDATE [tReport] SET isprint=0,PrintCopies=0 where  ReportGuid = '{0}'", strReportGuid);
                            }
                            this.Debug(cmd.CommandText);
                            cmd.ExecuteNonQuery();
                        }

                    }

                    //更新打印用报告模板的guid
                    //cmd.CommandText = string.Format("update treport set isprint=1, printtemplateguid='{0}' where reportguid='{1}'", strLastTemplateGuid, strReportGuid);
                    //this.Debug(cmd.CommandText);
                }



            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 设置医生鉴名
        /// </summary>
        /// <param name="strReportGuid"></param>
        /// <param name="dr"></param>
        private void SetDoctorSign(string strReportGuid, ref DataRow dr)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = Connstring;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                string strSQL = string.Format("select Creater,Submitter,FirstApprover,SecondApprover from treport where reportguid='{0}'", strReportGuid);
                this.Debug(strSQL);
                cmd.CommandText = strSQL;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }

                string strCreater = "", strFirstApprover = "", strSubmitter = "";
                if (dt.Rows[0]["Creater"] != null)
                {
                    strCreater = Convert.ToString(dt.Rows[0]["Creater"]);
                }

                if (dt.Rows[0]["FirstApprover"] != null)
                {
                    strFirstApprover = Convert.ToString(dt.Rows[0]["FirstApprover"]);
                }

                if (dt.Rows[0]["Submitter"] != null)
                {
                    strSubmitter = Convert.ToString(dt.Rows[0]["Submitter"]);
                }

                if (strCreater.Length > 0)
                {

                    strSQL = string.Format("SELECT SignImage FROM tUser WHERE userguid = '{0}'", strCreater);
                    cmd.CommandText = strSQL;
                    this.Debug(strSQL);
                    sda.Fill(ds, "CreaterSignImage");

                }
                if (strSubmitter.Length > 0)
                {
                    strSQL = string.Format("SELECT SignImage FROM tUser WHERE userguid = '{0}'", strSubmitter);
                    cmd.CommandText = strSQL;
                    this.Debug(strSQL);
                    sda.Fill(ds, "SubmitterSignImage");
                }

                if (strFirstApprover.Length > 0)
                {
                    strSQL = string.Format("SELECT SignImage FROM tUser WHERE userguid = '{0}'", strFirstApprover);
                    cmd.CommandText = strSQL;
                    this.Debug(strSQL);
                    sda.Fill(ds, "FirstApproverSignImage");
                }

                if (ds.Tables["CreaterSignImage"] != null && ds.Tables["CreaterSignImage"].Rows.Count > 0)
                {
                    dr[FIELDNAME_CREATORSIGNIMAGE] = ds.Tables["CreaterSignImage"].Rows[0]["SignImage"];

                }
                if (ds.Tables["SubmitterSignImage"] != null && ds.Tables["SubmitterSignImage"].Rows.Count > 0)
                {
                    dr[FIELDNAME_SUBMITTERSIGNIMAGE] = ds.Tables["SubmitterSignImage"].Rows[0]["SignImage"];

                }

                if (ds.Tables["FirstApproverSignImage"] != null && ds.Tables["FirstApproverSignImage"].Rows.Count > 0)
                {
                    dr[FIELDNAME_FIRSTAPPROVERSIGNIMAGE] = ds.Tables["FirstApproverSignImage"].Rows[0]["SignImage"];

                }


            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        //设置检查数
        private void SetExamNum(string strPatientID, string strOrderGuid, ref DataRow dr)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = Connstring;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GetPatientExamNo";
                this.Debug(cmd.CommandText);
                cmd.Parameters.AddWithValue("@PatientID", strPatientID);
                cmd.Parameters.AddWithValue("@OrderGuid", strOrderGuid);
                Object obj = cmd.ExecuteScalar();


                if (obj != null && obj != DBNull.Value)
                {
                    dr[FIELDNAME_EXAMNUMBER] = Convert.ToString(obj);

                }



            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 生成PDF报告文件，并放置在指定目录下
        /// </summary>
        /// <param name="drReport"></param>
        /// <param name="dtReport"></param>
        /// <param name="reportSrc">0--RISLite,1--SelfService</param>
        /// <returns>生成的报告文件名</returns>
        //private string GenPdfFile(DataRow drReport, DataTable dtReport, string templateGUID, int reportSrc, bool isThirdParty)
        //{
        //    string strPdfFileName = "";
        //    string strTemplateGuid = "";
        //    SqlConnection conn = new SqlConnection();
        //    try
        //    {
        //        this.Debug("GenPdfFile begin");

        //        using (C1.C1Report.C1Report c1rpt = new C1Report())
        //        {

        //            strTemplateGuid = GenPrintTable(drReport, c1rpt, templateGUID);

        //            this.Debug("GenPdfFile new print row");
        //            DataRow drPrint = _dt4Print.NewRow();
        //            this.Debug("GenPdfFile set print row data begin");
        //            foreach (DataColumn dataFld in _dt4Print.Columns)
        //            {
        //                try
        //                {
        //                    string fieldName = dataFld.ColumnName;
        //                    string key = fieldName.ToUpper();

        //                    if (fieldName.ToUpper() == FIELDNAME_CREATORSIGNIMAGE.ToUpper() || fieldName.ToUpper() == FIELDNAME_SUBMITTERSIGNIMAGE.ToUpper() || fieldName.ToUpper() == FIELDNAME_FIRSTAPPROVERSIGNIMAGE.ToUpper())
        //                    {
        //                        continue;
        //                    }
        //                    //drPrint[key] = GetStringFromDr(drReport, dtReport, fieldName);
        //                    if (fieldName.ToUpper() == FIELDNAME_tReport__WYS.ToUpper() || fieldName.ToUpper() == FIELDNAME_tReport__WYG.ToUpper())
        //                    {
        //                        drPrint[key] = RemoveUnderline(GetStringFromDr(drReport, fieldName));
        //                    }
        //                    else
        //                    {
        //                        drPrint[key] = GetStringFromDr(drReport, dtReport, fieldName);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    this.Error("GenPdfFile error:  " + dataFld.ColumnName + "," + ex.Message);

        //                }
        //            }
        //            this.Debug("GenPdfFile set print row data end");
        //            string strReportGuid = GetStringFromDr(drReport, FIELDNAME_REPORTGUID);
        //            ///////////////////////////////////////////////////////////////////////////////
        //            //to rich report, get image from ftp
        //            //c1rpt.Fields
        //            deleteTemplateFileFolderContent();
        //            List<string> reportPiclist = DownloadImgFromFTP(strReportGuid);
        //            int iReportPicIndex = 0;
        //            int iMaxReportPics = (reportPiclist.Count > iReportPics) ? iReportPics : reportPiclist.Count;
        //            if (iMaxReportPics != 0)
        //            {
        //                foreach (Field fld in c1rpt.Fields)
        //                {

        //                    if ((fld.Picture != null) && (iReportPicIndex < iMaxReportPics))
        //                    {
        //                        C1.C1Report.Util.PictureHolder ph = fld.Picture as C1.C1Report.Util.PictureHolder;
        //                        string fieldName = string.IsNullOrEmpty(ph.FieldName) ? fld.Name : ph.FieldName;
        //                        if (ph != null && ph.IsBound && _dt4Print.Columns.Contains(fieldName) && fld.Section == SectionTypeEnum.Detail)
        //                        {
        //                            Byte[] imageBytes = null;
        //                            MemoryStream mem = new MemoryStream();
        //                            FileInfo fInfo = new FileInfo(reportPiclist[iReportPicIndex]);
        //                            if (fInfo.Extension.ToLower().Equals(".jpg"))
        //                            {
        //                                Image img = Image.FromFile(reportPiclist[iReportPicIndex]);
        //                                img.Save(mem, ImageFormat.Jpeg);
        //                                img.Dispose();
        //                                img = null;
        //                            }
        //                            else if (fInfo.Extension.ToLower().Equals(".bmp"))
        //                            {
        //                                Image img = Image.FromFile(reportPiclist[iReportPicIndex]);
        //                                img.Save(mem, ImageFormat.Bmp);
        //                                img.Dispose();
        //                                img = null;
        //                            }
        //                            imageBytes = mem.ToArray();
        //                            drPrint[fieldName] = (Byte[])imageBytes;
        //                            imageBytes = null;
        //                            if (mem != null)
        //                            {
        //                                mem.Close();
        //                                mem.Dispose();
        //                                mem = null;
        //                            }
        //                            GC.Collect();
        //                        }
        //                        iReportPicIndex++;
        //                    }
        //                    else if (fld.Picture != null && (iReportPicIndex >= iReportPics))
        //                    {
        //                        C1.C1Report.Util.PictureHolder ph = fld.Picture as C1.C1Report.Util.PictureHolder;
        //                        if (ph != null && ph.IsBound && fld.Section == SectionTypeEnum.Detail)
        //                        {
        //                            if (ph != null && ph.IsBound && ph.FieldName.Contains("Photo"))
        //                            {
        //                                fld.Visible = false;
        //                                fld.CanShrink = true;
        //                            }
        //                        }
        //                        iReportPicIndex++;
        //                    }
        //                }
        //            }
        //            deleteTemplateFileFolderContent();
        //            ////////////////////////////////////////////////////////////////////////////


        //            string strPatientID = GetStringFromDr(drReport, FIELDNAME_PATIENTID);
        //            string strOrderGuid = GetStringFromDr(drReport, FIELDNAME_ORDERGUID);
        //            this.Debug("GenPdfFile set doctor sing");
        //            SetDoctorSign(strReportGuid, ref drPrint);
        //            this.Debug("GenPdfFile set exam num");
        //            SetExamNum(strPatientID, strOrderGuid, ref drPrint);

        //            _dt4Print.Rows.Add(drPrint);

        //            c1rpt.DataSource.Recordset = _dt4Print.DefaultView;
        //            strPdfFileName = Guid.NewGuid().ToString() + ".pdf";
        //            string strPdfFile = GetCurrentWorkPath("PdfReport") + "\\" + strPdfFileName;
        //            this.Debug("GenPdfFile generate file: " + strPdfFile);
        //            try
        //            {
        //                c1rpt.RenderToFile(strPdfFile, C1.C1Report.FileFormatEnum.PDF);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }

        //            c1rpt.DataSource.Recordset = null;
        //            if (c1rpt != null)
        //            {
        //                c1rpt.Clear();

        //            }

        //            //Print log for RIS System
        //            this.Debug("write print log and set print status");

        //            //WritePrintLog(strReportGuid, strTemplateGuid,reportSrc,isThirdParty);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strPdfFileName = "";
        //        this.Error(ex.Message);
        //    }
        //    finally
        //    {
        //        if (conn != null && conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //    }

        //    if (string.IsNullOrEmpty(strPdfFileName))
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return strPdfFileName;
        //    }
        //}


        private void deleteTemplateFileFolderContent()
        {
            string strReportImgFolder = GetCurrentWorkPath("PdfImage");
            System.IO.DirectoryInfo downloadedFileFolder = new DirectoryInfo(strReportImgFolder);
            try
            {
                foreach (FileInfo file in downloadedFileFolder.GetFiles())
                {
                    file.IsReadOnly = false;
                    file.Delete();
                }
                foreach (DirectoryInfo dir in downloadedFileFolder.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                this.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        //private List<string> DownloadImgFromFTP(string strReportGUID)
        //{
        //    List<string> listFile = null;
        //    bool bIniFtp = DataClass.InitalFtpParams();

        //    if (bIniFtp)
        //    {
        //        FtpClient ftpC = new FtpClient(DataClass.FtpServer, DataClass.FtpPort, DataClass.FtpUserID, DataClass.FtpPassword);
        //        listFile = new List<string>();
        //        string strReportImgFolder = GetCurrentWorkPath("PdfImage\\" + strReportGUID);
        //        DataTable dtReportFile = DataClass.GetReportFileDt(strReportGUID);
        //        if (dtReportFile != null)
        //        {
        //            foreach (DataRow dr in dtReportFile.Rows)
        //            {
        //                string fname = dr["filename"] as string;
        //                string rpath = dr["RelativePath"] as string;
        //                string localFileName = strReportImgFolder + "\\" + fname;
        //                bool bdownload = ftpC.DownloadFile(rpath + "\\" + fname, localFileName);
        //                if (bdownload)
        //                {
        //                    listFile.Add(localFileName);
        //                }
        //                else
        //                {
        //                    this.Error("download from ftp" + DataClass.FtpServer + " error");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            this.Error("No report file pics in database for ReportGUID" + strReportGUID);
        //        }
        //    }
        //    else
        //    {
        //        this.Error("Initial FTP failed");
        //    }
        //    return listFile;
        //}


        /// <summary>
        /// 根据规则取相应的打印模板  
        /// </summary>
        /// <param name="strModalityType"></param>
        /// <param name="strLastTemplateGuid"></param>
        /// <returns>返回打印模板文件路径名</returns>
        private string GetPrintTemplate(string strModalityType, ref string strLastTemplateGuid)
        {
            string strTemplatePath = GetCurrentWorkPath("PrintTemplate");
            string strTemplateFileName = "";

            string strTemplateInfo = "";
            SqlConnection conn = new SqlConnection();
            try
            {
                if (!string.IsNullOrEmpty(strLastTemplateGuid))
                {
                    strTemplateFileName = strTemplatePath + "\\" + strLastTemplateGuid + ".xml";
                    string strExpress = string.Format("TemplateGuid='{0}'", strLastTemplateGuid);
                    DataRow[] drfound = DataClass.GetPrintTemplate().Select(strExpress);
                    if (drfound.Length > 0)
                    {
                        strTemplateInfo = GetUnicodeBinaryFromDr(drfound[0], "TemplateInfo");

                    }
                }


                ////设备默认模板
                //if (!string.IsNullOrEmpty(strModalityType) && string.IsNullOrEmpty(strLastTemplateGuid))
                //{
                //    string strExpress = string.Format("ModalityType='{0}' and IsDefaultByModality=1", strModalityType);
                //    DataRow[] drfound = DataClass.GetPrintTemplate().Select(strExpress);
                //    if (drfound.Length > 0)
                //    {
                //        strLastTemplateGuid = GetStringFromDr(drfound[0], "TemplateGuid");
                //        strTemplateFileName = GetCurrentWorkPath("PrintTemplate") + "\\" + strLastTemplateGuid + ".xml";
                //        strTemplateInfo = GetUnicodeBinaryFromDr(drfound[0], "TemplateInfo");


                //    }
                //}


                ////类别默认模板
                //if (!string.IsNullOrEmpty(strModalityType) && string.IsNullOrEmpty(strLastTemplateGuid))
                //{
                //    string strExpress = string.Format("ModalityType='{0}' and IsDefaultByType=1", strModalityType);
                //    DataRow[] drfound = DataClass.GetPrintTemplate().Select(strExpress);
                //    if (drfound.Length > 0)
                //    {
                //        strLastTemplateGuid = GetStringFromDr(drfound[0], "TemplateGuid");
                //        strTemplateFileName = GetCurrentWorkPath("PrintTemplate") + "\\" + strLastTemplateGuid + ".xml";
                //        strTemplateInfo = GetUnicodeBinaryFromDr(drfound[0], "TemplateInfo");

                //    }
                //}

                ////设备类型中取任意一个模板
                //if (!string.IsNullOrEmpty(strModalityType) && string.IsNullOrEmpty(strLastTemplateGuid))
                //{

                //    string strExpress = string.Format("ModalityType='{0}'", strModalityType);
                //    DataRow[] drfound = DataClass.GetPrintTemplate().Select(strExpress);
                //    if (drfound.Length > 0)
                //    {
                //        strLastTemplateGuid = GetStringFromDr(drfound[0], "TemplateGuid");
                //        strTemplateFileName = GetCurrentWorkPath("PrintTemplate") + "\\" + strLastTemplateGuid + ".xml";
                //        strTemplateInfo = GetUnicodeBinaryFromDr(drfound[0], "TemplateInfo");
                //    }
                //}

                if (DataClass.UpdatePrintTemplate)
                {
                    listPrintTemplate.Clear();
                    DataClass.UpdatePrintTemplate = false;
                }


                if ((!File.Exists(strTemplateFileName)) || (!listPrintTemplate.Contains(strLastTemplateGuid)))
                {
                    if (!listPrintTemplate.Contains(strLastTemplateGuid))
                    {
                        listPrintTemplate.Add(strLastTemplateGuid);
                    }
                    MakePrintTemplateFile(strTemplateFileName, strTemplateInfo);
                }

            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return strTemplateFileName;
        }

        /// <summary>
        /// 生成打印模板文件
        /// </summary>
        /// <param name="strTemplateFile"></param>
        /// <param name="strTemplateInfo"></param>
        private void MakePrintTemplateFile(string strTemplateFile, string strTemplateInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(strTemplateFile) || string.IsNullOrEmpty(strTemplateInfo))
                {
                    return;
                }
                StreamWriter fs = new StreamWriter(strTemplateFile, false);
                fs.Write(strTemplateInfo);
                fs.Close();


            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
                return;
            }
        }

        /// <summary>
        /// GetCurrentWorkPath
        /// </summary>
        /// <returns></returns>
        private string GetCurrentWorkPath(string strSubDir)
        {
            ////string str = System.Windows.Forms.Application.ExecutablePath;
            ////int pos = str.LastIndexOf('\\');
            ////if (pos > 0)
            ////{
            ////    str = str.Substring(0, pos);
            ////}

            //string strPath = this.Server.MapPath(strSubDir);
            //if (!Directory.Exists(strPath))
            //{
            //    Directory.CreateDirectory(strPath);
            //}
            //return strPath;
            return "";
        }

        /// <summary>
        /// 去除所见所得中的下划线
        /// </summary>
        /// <param name="strRtf"></param>
        /// <returns></returns>
        private string RemoveUnderline(string strRtf)
        {
            string rtf = "";
            RichTextBox rtb1 = new RichTextBox();
            try
            {
                rtb1.Rtf = strRtf;
                rtb1.SelectAll();
                int rtb1start = rtb1.SelectionStart;
                int len = rtb1.SelectionLength;

                //if len <= 1 and there is a selection font then just handle and return
                if (len <= 1 && rtb1.SelectionFont != null)
                {
                    //remove style 
                    rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style & ~FontStyle.Underline);
                }
                else
                {
                    // Step through the selected text one char at a time	
                    //    rtbTemp.HideSelection = false;
                    RichTextBox rtbTemp = new RichTextBox();
                    rtbTemp.Rtf = rtb1.Rtf;

                    for (int i = 0; i < len; ++i)
                    {
                        rtbTemp.Select(rtb1start + i, 1);
                        //remove style 
                        rtbTemp.SelectionFont = new Font(rtbTemp.SelectionFont, rtbTemp.SelectionFont.Style & ~FontStyle.Underline);
                    }
                    rtb1.Rtf = rtbTemp.Rtf;
                }

            }
            catch (Exception ex)
            {
                this.Error("RemoveUnderline " + ex.Message);
            }
            finally
            {
                rtf = rtb1.Rtf;
                rtb1.Dispose();
            }
            return rtf;
        }

        /// <summary>
        /// 部位， 需要多个部位叠加
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        private string GetMultiRow(DataTable dt, string colName)
        {
            string ret = "";
            string ret1 = "";
            try
            {
                //may be multi-rows
                if (dt == null || dt.Rows.Count < 1)
                {
                    this.Error("dt is null or not any rows");
                    return "";
                }

                if (!dt.Columns.Contains(colName))
                {
                    return "";
                }

                System.Collections.Specialized.StringCollection strCol = new System.Collections.Specialized.StringCollection();

                if (dt.Columns[colName].DataType == System.Type.GetType("System.Byte[]"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Byte[] buff = dr[colName] as Byte[];
                        string tmp = System.Text.Encoding.Default.GetString(buff);
                        ret = MergeRTFFiles(ret, tmp);
                    }
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string tmp = dr[colName].ToString().Trim().ToUpper();
                        if (!strCol.Contains(tmp))
                        {
                            strCol.Add(tmp);
                            if (DataClass.listUserGuid.Contains(colName))
                            {
                                ret1 = DataClass.GetUserLocalName(tmp);
                            }
                            else if (DataClass.listYesNo.Contains(colName))
                            {
                                ret1 = DataClass.GetDictionaryText(70, tmp);
                            }
                            else if (DataClass.listSite.Contains(colName))
                            {
                                ret1 = DataClass.GetSiteAlias(tmp);
                            }
                            else if (DataClass.listDomain.Contains(colName))
                            {
                                ret1 = DataClass.GetDomainAlias(tmp);
                            }
                            else
                            {
                                ret1 = tmp;
                            }
                            ret += ret1 + ",";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }
            ret = ret.Trim(", ".ToCharArray());

            return ret;
        }

        private string GetStringFromDr(DataRow dr, DataTable dt, string colName)
        {
            string ret = "";
            try
            {
                if (dr == null)
                {
                    this.Error("dr is null");

                    return "";
                }
                if (!dr.Table.Columns.Contains(colName))
                {
                    return "";
                }
                string key = colName.ToUpper().Trim();


                object objValue = dr[key];
                string fldValue = "";

                if (objValue.GetType() == System.Type.GetType("System.Byte[]"))
                {
                    Byte[] buff = objValue as Byte[];
                    fldValue = System.Text.Encoding.Default.GetString(buff);
                }
                else
                {
                    fldValue = objValue.ToString().Trim().ToUpper();
                }

                if (key.StartsWith("TREGPROCEDURE") || key.StartsWith("TPROCEDURECODE"))
                {
                    ret = GetMultiRow(dt, colName);
                }
                else if (key == "TREGORDER__CURRENTAGE")
                {
                    string[] split = fldValue.Split(new Char[] { ' ' });
                    if (split.Length != 2)
                    {
                        return fldValue;
                    }
                    ret = split[0] + " " + DataClass.GetDictionaryText(6, split[1]);
                }
                else if (key == "TREPORT__ISPOSITIVE")
                {
                    if (fldValue == "1")
                    {
                        ret = "阳性";
                    }
                    else if (fldValue == "2")
                    {
                        ret = "阴性";
                    }
                    else if (fldValue == "0")
                    {
                        ret = "未知";
                    }
                }
                else if (DataClass.listUserGuid.Contains(key))
                {
                    ret = DataClass.GetUserLocalName(fldValue);
                }
                else if (DataClass.listYesNo.Contains(key))
                {
                    ret = DataClass.GetDictionaryText(70, fldValue);
                }
                else if (DataClass.listSite.Contains(key))
                {
                    ret = DataClass.GetSiteAlias(fldValue);
                }
                else if (DataClass.listDomain.Contains(key))
                {
                    ret = DataClass.GetDomainAlias(fldValue);
                }
                else
                {
                    ret = fldValue;
                }
            }
            catch (Exception ex)
            {
                this.Error("GetStringFromDr " + ex.Message);
            }

            return ret;
        }

        private string GetStringFromDr(DataRow dr, string colName)
        {
            string ret = "";
            try
            {
                if (dr == null)
                {
                    this.Error("dr is null");
                    return "";
                }

                if (!dr.Table.Columns.Contains(colName))
                {
                    return "";
                }


                string key = colName.ToUpper().Trim();


                object objValue = dr[key];
                string fldValue = "";

                if (objValue.GetType() == System.Type.GetType("System.Byte[]"))
                {
                    Byte[] buff = objValue as Byte[];
                    fldValue = System.Text.Encoding.Default.GetString(buff);

                }
                else
                {
                    fldValue = objValue.ToString();
                }
                ret = fldValue;
            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }


            return ret;
        }

        private string GetUnicodeBinaryFromDr(DataRow dr, string colName)
        {
            string ret = "";
            try
            {
                if (dr == null)
                {
                    this.Error("dr is null");
                    return "";
                }
                if (!dr.Table.Columns.Contains(colName))
                {
                    return "";
                }
                string key = colName.ToUpper().Trim();


                object objValue = dr[key];
                string fldValue = "";

                if (objValue.GetType() == System.Type.GetType("System.Byte[]"))
                {
                    Byte[] buff = objValue as Byte[];
                    fldValue = System.Text.Encoding.Unicode.GetString(buff);

                }
                else
                {
                    fldValue = objValue.ToString();
                }

                ret = fldValue;
            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }

            return ret;
        }

        private void Error(string errorMsg)
        {
            logger2.Error((long)0x0C03, "Kodak GC RIS Templates Module", 1, errorMsg, Application.StartupPath.ToString(),
                       (new System.Diagnostics.StackFrame(true)).GetFileName(),
                       Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
        }

        private void Debug(string info)
        {
            logger2.Debug((long)0x0C03, "Kodak GC RIS Templates Module", 1, info, Application.StartupPath.ToString(),
                       (new System.Diagnostics.StackFrame(true)).GetFileName(),
                       Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
        }

    }
}