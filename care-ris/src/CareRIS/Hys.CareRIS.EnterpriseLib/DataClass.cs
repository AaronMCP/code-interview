using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Hys.CareRIS.EnterpriseLib
{
    public sealed class DataClass : IDisposable
    {

        private static DataClass _dataclass;
        private bool Disposed = false;

        private static List<string> _listPatient = new List<string>();
        private static List<string> _listOrder = new List<string>();
        private static List<string> _listYesNo = new List<string>();
        private static List<string> _listUserGuid = new List<string>();
        private static List<string> _listSite = new List<string>();
        private static List<string> _listDomain = new List<string>();
        private static List<string> _listYesNo2 = new List<string>();
        private static List<string> _listUserGuid2 = new List<string>();
        private static List<string> _listSite2 = new List<string>();
        private static List<string> _listDomain2 = new List<string>();
        private static DataTable _dtDictionary = null;
        private static DataTable _dtUser = null;
        private static DataTable _dtSiteList = null;
        private static DataTable _dtDomainList = null;
        private static DataTable _dtPrintTemplate = null;
        private static DataTable _dtDomain = null;
        private static bool _bUpdatePrintTemplate = false;
        private static string _PdfFileUrl;
        private static string _Printer;
        private static string _Domain;
        private static string _Sql;
        private static DataTable _dtPrintBarorNoteTemplate = null;
        private static string _SqlGetPatientInfo;
        private static string ftpServer;
        private static int iftpPort;
        private static string ftpUserID;
        private static string ftpPassword;

        //constructor
        private DataClass()
        {

        }

        static DataClass()
        {

            string strPatient = ConfigurationManager.AppSettings["Patient"];
            string strOrder = ConfigurationManager.AppSettings["Order"];
            string strYesNo = ConfigurationManager.AppSettings["YesNo"];
            string strUserGuid = ConfigurationManager.AppSettings["UserGuid"];
            string strSiteField = ConfigurationManager.AppSettings["SiteField"];
            string strDomainField = ConfigurationManager.AppSettings["DomainField"];
            _PdfFileUrl = ConfigurationManager.AppSettings["PdfFileUrl"];
            _Printer = ConfigurationManager.AppSettings["Printer"];
            _Domain = ConfigurationManager.AppSettings["Domain"];
            _Sql = ConfigurationManager.AppSettings["SQL"];

            //added 
            _SqlGetPatientInfo = ConfigurationManager.AppSettings["getPaientInfo"];

            if (!string.IsNullOrEmpty(strPatient))
            {
                string[] arr1 = strPatient.Split('|');
                foreach (string str1 in arr1)
                {
                    if (!_listPatient.Contains(str1.Trim()))
                    {
                        _listPatient.Add(str1.Trim().ToUpper());
                    }
                }
            }

            if (!string.IsNullOrEmpty(strOrder))
            {
                string[] arr1 = strOrder.Split('|');
                foreach (string str1 in arr1)
                {
                    if (!_listOrder.Contains(str1.Trim()))
                    {
                        _listOrder.Add(str1.Trim().ToUpper());
                    }
                }
            }
            if (!string.IsNullOrEmpty(strYesNo))
            {
                string[] arr1 = strYesNo.Split('|');
                foreach (string str1 in arr1)
                {
                    if (!_listYesNo.Contains(str1.Trim()))
                    {
                        _listYesNo.Add(str1.Trim().ToUpper());
                    }
                    string[] sep = { "__" };
                    string[] arr2 = str1.Split(sep, StringSplitOptions.None);
                    string temp;
                    if (arr2.Length != 2)
                    {
                        temp = str1;
                    }
                    else
                    {
                        temp = arr2[1];
                    }
                    if (!_listYesNo2.Contains(temp.Trim()))
                    {
                        _listYesNo2.Add(temp.Trim().ToUpper());
                    }
                }
            }
            if (!string.IsNullOrEmpty(strUserGuid))
            {
                string[] arr1 = strUserGuid.Split('|');
                foreach (string str1 in arr1)
                {
                    if (!_listUserGuid.Contains(str1.Trim()))
                    {
                        _listUserGuid.Add(str1.Trim().ToUpper());
                    }
                    string[] sep = { "__" };
                    string[] arr2 = str1.Split(sep, StringSplitOptions.None);
                    string temp;
                    if (arr2.Length != 2)
                    {
                        temp = str1;
                    }
                    else
                    {
                        temp = arr2[1];
                    }
                    if (!_listUserGuid2.Contains(temp.Trim()))
                    {
                        _listUserGuid2.Add(temp.Trim().ToUpper());
                    }
                }
            }
            if (!string.IsNullOrEmpty(strSiteField))
            {
                string[] arr1 = strSiteField.Split('|');
                foreach (string str1 in arr1)
                {
                    if (!_listSite.Contains(str1.Trim()))
                    {
                        _listSite.Add(str1.Trim().ToUpper());
                    }
                    string[] sep = { "__" };
                    string[] arr2 = str1.Split(sep, StringSplitOptions.None);
                    string temp;
                    if (arr2.Length != 2)
                    {
                        temp = str1;
                    }
                    else
                    {
                        temp = arr2[1];
                    }
                    if (!_listSite2.Contains(temp.Trim()))
                    {
                        _listSite2.Add(temp.Trim().ToUpper());
                    }
                }
            }
            if (!string.IsNullOrEmpty(strDomainField))
            {
                string[] arr1 = strDomainField.Split('|');
                foreach (string str1 in arr1)
                {
                    if (!_listDomain.Contains(str1.Trim()))
                    {
                        _listDomain.Add(str1.Trim().ToUpper());
                    }
                    string[] sep = { "__" };
                    string[] arr2 = str1.Split(sep, StringSplitOptions.None);
                    string temp;
                    if (arr2.Length != 2)
                    {
                        temp = str1;
                    }
                    else
                    {
                        temp = arr2[1];
                    }
                    if (!_listDomain2.Contains(temp.Trim()))
                    {
                        _listDomain2.Add(temp.Trim().ToUpper());
                    }
                }
            }
            //GetDictionary

            SqlConnection conn = new SqlConnection();
            try
            {


                conn.ConnectionString = ConfigurationManager.ConnectionStrings["RisProContext"].ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                string strSQL = string.Format("select * from tbdictionaryvalue");
                cmd.CommandText = strSQL;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "dictionary");
                _dtDictionary = ds.Tables["dictionary"];



                strSQL = string.Format("select * from  tbUser");
                cmd.CommandText = strSQL;
                sda.SelectCommand = cmd;
                sda.Fill(ds, "user");
                _dtUser = ds.Tables["user"];

                strSQL = string.Format("select * from tbSiteList");
                cmd.CommandText = strSQL;
                sda.SelectCommand = cmd;
                sda.Fill(ds, "sitelist");
                _dtSiteList = ds.Tables["sitelist"];

                strSQL = string.Format("select * from tbDomainList");
                cmd.CommandText = strSQL;
                sda.SelectCommand = cmd;
                sda.Fill(ds, "domainlist");
                _dtDomainList = ds.Tables["domainlist"];

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

        public static string GetDictionaryText(int nTag, string strValue)
        {
            string strText = strValue;
            try
            {
                string strExpress = string.Format("Tag={0} and Value='{1}'", nTag.ToString(), strValue);
                DataRow[] drfound = _dtDictionary.Select(strExpress);
                if (drfound.Length > 0)
                {
                    strText = Convert.ToString(drfound[0]["Text"]);
                }
            }
            catch (Exception ex)
            {
            }
            return strText;
        }

        public static string GetUserLocalName(string strUserGuid)
        {
            string strText = strUserGuid;
            try
            {
                string strExpress = string.Format("UserGuid='{0}'", strUserGuid);
                DataRow[] drfound = _dtUser.Select(strExpress);
                if (drfound.Length > 0)
                {
                    strText = Convert.ToString(drfound[0]["LocalName"]);
                }
            }
            catch (Exception ex)
            {
            }
            return strText;
        }

        public static string GetSiteAlias(string strSite)
        {
            string strText = strSite;
            try
            {
                string strExpress = string.Format("Site='{0}'", strSite);
                DataRow[] drfound = _dtSiteList.Select(strExpress);
                if (drfound.Length > 0)
                {
                    strText = Convert.ToString(drfound[0]["Alias"]);
                }
            }
            catch (Exception ex)
            {
            }
            return strText;
        }

        public static string GetDomainAlias(string strDomain)
        {
            string strText = strDomain;
            try
            {
                string strExpress = string.Format("domain='{0}'", strDomain);
                DataRow[] drfound = _dtDomainList.Select(strExpress);
                if (drfound.Length > 0)
                {
                    strText = Convert.ToString(drfound[0]["Alias"]);
                }
            }
            catch (Exception ex)
            {
            }
            return strText;
        }

        private static void ModifyAppSetting(string key, string strValue)
        {
            string XPath = "/configuration/appSettings/add[@key='?']";
            XmlDocument domWebConfig = new XmlDocument();

            domWebConfig.Load((HttpContext.Current.Server.MapPath("web.config")));
            XmlNode addKey = domWebConfig.SelectSingleNode((XPath.Replace("?", key)));
            if (addKey == null)
            {
                throw new ArgumentException("没有找到的配置节");
            }
            addKey.Attributes["value"].InnerText = strValue;
            domWebConfig.Save((HttpContext.Current.Server.MapPath("web.config")));

        }

        public static DataTable GetPrintBarorNoteTemplate(string domain)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["RisProContext"].ConnectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format("select * from tbRegPatient where Domain='{0}'", domain);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                _dtPrintBarorNoteTemplate = ds.Tables[0];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return _dtPrintBarorNoteTemplate;
        }

        public static DataTable GetPrintTemplate()
        {

            if (ConfigurationManager.AppSettings["PrintTemplateChanged"] == "1")
            {
                _dtPrintTemplate = null;
                ModifyAppSetting("PrintTemplateChanged", "0");
                _bUpdatePrintTemplate = true;
            }

            if (_dtPrintTemplate == null)
            {
                SqlConnection conn = new SqlConnection();
                try
                {


                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["RisProContext"].ConnectionString;
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    string strSQL = string.Format("select * from  tbRegPatient where type=3");
                    cmd.CommandText = strSQL;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "printtemplate");
                    _dtPrintTemplate = ds.Tables["printtemplate"];

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }

            return _dtPrintTemplate;
        }

        public static bool InitalFtpParams(string domain)
        {
            bool bResult = false;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.AppSettings["Connstring"];
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                string strGetFTPSQL = string.Format("select * from tbDomainlist where domain='{0}' ", domain);
                cmd.CommandText = strGetFTPSQL;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "Domainlist");
                _dtDomain = ds.Tables["Domainlist"];
                ftpServer = Convert.ToString(_dtDomain.Rows[0]["FtpServer"]);

                if (_dtDomain.Rows[0]["FtpPort"] != null || _dtDomain.Rows[0]["FtpPort"] != DBNull.Value)
                {
                    iftpPort = Convert.ToInt32(_dtDomain.Rows[0]["FtpPort"]);
                }
                else
                {
                    //logger.Error("ftp port is wrong");
                }

                ftpUserID = Convert.ToString(_dtDomain.Rows[0]["FtpUser"]);
                ftpPassword = Convert.ToString(_dtDomain.Rows[0]["FtpPassword"]);
                bResult = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return bResult;
        }

        public static DataTable GetReportFileDt(string strReportGUID)
        {
            DataTable dt = null;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.AppSettings["Connstring"];
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                string strsql = string.Format("select * from tbReportFile where ReportGuid='{0}'", strReportGUID);
                cmd.CommandText = strsql;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "ReportFile");
                dt = ds.Tables["ReportFile"];
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
        }

        public static int MaxPDFImgCount
        {
            get
            {
                string strReportPics = ConfigurationManager.AppSettings["MaxReportPics"];
                return Convert.ToInt32(strReportPics);
            }

        }

        public static string FtpServer
        {
            get
            {
                return ftpServer;
            }
        }

        public static int FtpPort
        {
            get
            {
                return iftpPort;
            }
        }

        public static string FtpUserID
        {
            get
            {
                return ftpUserID;
            }
        }

        public static string FtpPassword
        {
            get
            {
                return ftpPassword;
            }
        }

        public static List<string> listPatient
        {
            get
            {
                return _listPatient;
            }
        }

        public static List<string> listOrder
        {
            get
            {
                return _listOrder;
            }
        }

        public static List<string> listYesNo
        {
            get
            {
                return _listYesNo;
            }
        }

        public static List<string> listSite
        {
            get
            {
                return _listSite;
            }
        }

        public static List<string> listDomain
        {
            get
            {
                return _listDomain;
            }
        }

        public static List<string> listUserGuid
        {
            get
            {
                return _listUserGuid;
            }
        }

        public static List<string> listYesNo2
        {
            get
            {
                return _listYesNo2;
            }
        }

        public static List<string> listUserGuid2
        {
            get
            {
                return _listUserGuid2;
            }
        }

        public static List<string> listSite2
        {
            get
            {
                return _listSite2;
            }
        }

        public static List<string> listDomain2
        {
            get
            {
                return _listDomain2;
            }
        }

        public static string PdfFileUrl
        {
            get
            {
                string strLast = _PdfFileUrl.Substring(_PdfFileUrl.Length - 1, 1);
                if (strLast != "/")
                {
                    _PdfFileUrl += "/";
                }

                return _PdfFileUrl;
            }
        }

        public static string Printer
        {
            get
            {
                return _Printer;
            }
        }

        public static string Domain
        {
            get
            {
                return _Domain;
            }
        }

        public static string SQL_FOR_GetInfo
        {
            get
            {
                return _SqlGetPatientInfo;
            }
        }

        public static string SQL
        {
            get
            {
                return _Sql;
            }
        }

        public static bool UpdatePrintTemplate
        {
            get
            {
                return _bUpdatePrintTemplate;
            }
            set
            {
                _bUpdatePrintTemplate = false;
            }
        }

        private static object objLock = new object();
        //Singlton 
        public static DataClass Instance
        {
            get
            {
                if (_dataclass == null)
                {
                    lock (objLock)
                    {
                        if (_dataclass == null)
                        {
                            _dataclass = new DataClass();
                        }
                    }
                }
                return _dataclass;
            }
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                Disposed = true;
            }
        }
    }
}
