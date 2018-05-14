using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Platform.Domain.Ris
{
    public class RisDBService
    {
        private static string _connString = "";
        static RisDBService()
        {
            _connString = ConfigurationManager.ConnectionStrings["RisProContext"].ConnectionString;
        }

        public static bool GenerateAccNo(string locationAccnoPrefix, string modalityType, string site, ref string accno)
        {
            DbCommand dbCommand = null;
            try
            {
                SqlDatabase db = new SqlDatabase(_connString);
                dbCommand = db.GetStoredProcCommand("procGenerateAccNo");
                db.AddInParameter(dbCommand, "@locationaccprefix", DbType.String, locationAccnoPrefix);
                db.AddInParameter(dbCommand, "@modalitytype", DbType.String, modalityType);
                db.AddInParameter(dbCommand, "@site", DbType.String, site);
                db.AddOutParameter(dbCommand, "@accno", DbType.String, 128);
                db.AddOutParameter(dbCommand, "@errormsg", DbType.String, 64);
                db.ExecuteNonQuery(dbCommand);
                if (string.IsNullOrWhiteSpace(db.GetParameterValue(dbCommand, "@errormsg") as string))
                {
                    accno = db.GetParameterValue(dbCommand, "@accno") as string;
                    return true;                    
                }
            }
            finally
            {
                if (dbCommand != null) dbCommand.Dispose();
            }

            return false;

        }

        public static bool GeneratePatientID(string site, ref string patientID)
        {
            DbCommand dbCommand = null;
            try
            {
                SqlDatabase db = new SqlDatabase(_connString);
                dbCommand = db.GetStoredProcCommand("procGeneratePatientId");
                db.AddInParameter(dbCommand, "@site", DbType.String, site);
                db.AddOutParameter(dbCommand, "@patientid", DbType.String, 128);
                db.AddOutParameter(dbCommand, "@errormsg", DbType.String, 64);
                db.ExecuteNonQuery(dbCommand);
                if (string.IsNullOrWhiteSpace(db.GetParameterValue(dbCommand, "@errormsg") as string))
                {
                    patientID = db.GetParameterValue(dbCommand, "@patientid") as string;
                    return true;
                }
            }
            finally
            {
                if (dbCommand != null) dbCommand.Dispose();
            }

            return false;

        }

        public static DataTable GetSimilarPatient(string globalID, string risPatientID, string hisID, string patientName, string site)
        {
            DbCommand dbCommand = null;
            try
            {
                SqlDatabase db = new SqlDatabase(_connString);
                dbCommand = db.GetStoredProcCommand("procPatientExistDecide");
                db.AddInParameter(dbCommand, "@globalid", DbType.String, globalID);
                db.AddInParameter(dbCommand, "@rispatientid", DbType.String, risPatientID);
                db.AddInParameter(dbCommand, "@hisid", DbType.String, hisID);
                db.AddInParameter(dbCommand, "@patientname", DbType.String, patientName);
                db.AddInParameter(dbCommand, "@site", DbType.String, site);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            finally
            {
                if (dbCommand != null) dbCommand.Dispose();
            }

            return new DataTable();

        }

        public static string GetPDFURL(string serviceURL, string templateType, string accno, string modalityType, string reportGuid = "", int reportSrc = 0)
        {
            SelfServiceReport webproxy = new SelfServiceReport(serviceURL);
            string strPDFURL = string.Empty;

            if (templateType == "3")
            {
                string[] filelist = webproxy.GetPDFReportList(accno, modalityType, reportGuid, reportSrc, false);
                foreach (string strfile in filelist)
                {
                    strPDFURL = strfile;
                }
            }
            else if ((templateType == "1") || (templateType == "2") || (templateType == "4") || (templateType == "5") || (templateType == "16") || (templateType == "20"))
            {
                string[] filelist = webproxy.GetPDFListtoPrint(accno, modalityType, templateType);
                foreach (string strfile in filelist)
                {
                    strPDFURL = strfile;
                }
            }

            return strPDFURL;
        }

        /// <summary>
        /// Calcute the current age by birthday of patient
        /// </summary>
        /// <param name="birthday">birthday of patient</param>
        /// <param name="minYearNumber">from system configure</param>
        /// <param name="minMonthNumber">from system configure</param>
        /// <param name="minWeekNumber">NA</param>
        /// <param name="minDayNumber">from system configure</param>             
        /// <returns>current age  as  12Year, 3Month or 16Hour</returns>
        public static string CalCurrentAge(DateTime birthday, int minYearNumber, int minMonthNumber, int minWeekNumber, int minDayNumber)
        {

            string currentAge = string.Empty;

            //计算年龄的基准时间,如果是修改，用order的Create时间， 如果是新建,用当前时间
            DateTime baseTime = DateTime.Now;


            if (birthday.CompareTo(baseTime) > 0)
            {
                birthday = baseTime;

            }
            else
            {


                int nYear = GetYearDefference(birthday, baseTime);
                int nMonth = GetMonthDefference(birthday, baseTime);


                if (nYear >= minYearNumber)
                {

                    currentAge = nYear.ToString() + " Year";
                }
                else if (nMonth >= minMonthNumber)
                {
                    currentAge = nMonth.ToString() + " Month";
                }
                else
                {
                    TimeSpan ts = baseTime - birthday.Date;
                    int nHour = (int)ts.TotalHours;
                    int nDay = nHour / 24;

                    if (nDay >= minDayNumber)
                    {
                        currentAge = nDay.ToString() + " Day";
                    }
                    else
                    {

                        currentAge = nHour.ToString() + " Hour";

                    }

                }
            }


            return currentAge;
        }

        /// <summary>
        /// Get the year defference between dtbegin and dtend
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private static int GetYearDefference(DateTime dtBegin, DateTime dtEnd)
        {
            int nYear = 0;
            try
            {
                TimeSpan ts = dtEnd - dtBegin;
                nYear = dtEnd.Year - dtBegin.Year;

                if (dtEnd.Month < dtBegin.Month)
                {
                    nYear--;
                }
                else if (dtEnd.Month == dtBegin.Month)
                {
                    if (dtEnd.Day < dtBegin.Day)
                    {
                        nYear--;
                    }
                }

                if (nYear < 0)
                {
                    nYear = 0;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return nYear;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private static int GetMonthDefference(DateTime dtBegin, DateTime dtEnd)
        {
            int nMonth = 0;
            try
            {
                TimeSpan ts = dtEnd - dtBegin;
                nMonth = (dtEnd.Year - dtBegin.Year) * 12;

                nMonth += dtEnd.Month - dtBegin.Month;

                if (dtEnd.Day < dtBegin.Day)
                {
                    nMonth--;
                }

                if (nMonth < 0)
                {
                    nMonth = 0;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return nMonth;
        }
    }
}
