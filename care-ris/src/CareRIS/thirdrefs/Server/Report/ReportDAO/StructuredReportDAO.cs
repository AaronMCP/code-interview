using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using DataAccessLayer;

namespace Server.ReportDAO
{
    public class StructuredReportDAO
    {
        public object Execute(object param)
        {
            using (RisDAL oKodak = new RisDAL())
            {
                string clsType = string.Format("{0}_{1}", this.GetType().ToString(), oKodak.DriverClassName.ToUpper());
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }

                Type type = Type.GetType(clsType);
                IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
                return iRptDAO.Execute(param);
            }
        }

        #region Added by Kevin For SR - Save ReportContent,ReportContentList and ReportItem
        public static void AddReportContent(string reportContent, string reportID, string authorid, int status, string domain, string naturaltext, ref System.Data.SqlClient.SqlCommand cmd)
        {
            cmd.CommandTimeout = 0;
            cmd.Parameters.Clear();

            JavaScriptSerializer a = new JavaScriptSerializer();
            ReportContent temp = a.Deserialize<ReportContent>(reportContent);

            string sql = string.Format("insert into tReportContent(TemplateId,TemplateVersion,"
                           + "ReportId,ReportVersion,ContentHtml,AuthorName,AutherId,CreateDate,ModifyDate,Status, Domain, NaturalContentHtml) "
                           + "values(@TemplateId,@TemplateVersion,@ReportId,@ReportVersion,@ContentHtml,"
                           + "@AuthorName,@AutherId,@CreateDate,@ModifyDate,@Status,@Domain,@NaturalContentHtml)");

            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@TemplateId", temp.TemplateId);
            cmd.Parameters.AddWithValue("@TemplateVersion", temp.TemplateVersion);
            cmd.Parameters.AddWithValue("@ReportId", reportID);
            cmd.Parameters.AddWithValue("@ReportVersion", 0);
            cmd.Parameters.AddWithValue("@ContentHtml", temp.ContentHtml);
            cmd.Parameters.AddWithValue("@AuthorName", authorid);
            cmd.Parameters.AddWithValue("@AutherId", authorid);
            cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Domain", domain);
            cmd.Parameters.AddWithValue("@NaturalContentHtml", naturaltext);
        }

        public static void AddReportItem(string reportItem, string reportID, ref System.Data.SqlClient.SqlCommand cmd)
        {
            string sqldelete = "delete from tReportItem where ReportId = @ReportId";
            cmd.CommandTimeout = 0;
            cmd.Parameters.Clear();

            cmd.CommandText = sqldelete;
            cmd.Parameters.AddWithValue("@ReportId", reportID);

            cmd.ExecuteNonQuery();

            JavaScriptSerializer a = new JavaScriptSerializer();
            List<ReportItemList> items = a.Deserialize<List<ReportItemList>>(reportItem);

            string sql = string.Format("insert into tReportItem(ReportId,ItemPosition,"
                           + "ItemId,ItemName,ValueId,Value,PatientId) "
                           + "values(@ReportId,@ItemPosition,@ItemId,@ItemName,@ValueId,"
                           + "@Value,@PatientId)");

            foreach (var item in items)
            {
                cmd.CommandTimeout = 0;
                cmd.Parameters.Clear();

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@ReportId", reportID);
                cmd.Parameters.AddWithValue("@ItemPosition", "");
                cmd.Parameters.AddWithValue("@ItemId", item.Elements.Id);
                cmd.Parameters.AddWithValue("@ItemName", item.Elements.ContentValue);
                cmd.Parameters.AddWithValue("@ValueId", item.Elements.Id);
                cmd.Parameters.AddWithValue("@Value", item.Elements.Value);
                cmd.Parameters.AddWithValue("@PatientId", item.Elements.Pid);

                cmd.ExecuteNonQuery();
            }

        }

        public static void AddReportContentList(string reportContent, string reportID, string authorid, int status, string domain, bool ispositive, string naturaltext, ref System.Data.SqlClient.SqlCommand cmd)
        {
            cmd.CommandTimeout = 0;
            cmd.Parameters.Clear();

            JavaScriptSerializer a = new JavaScriptSerializer();
            ReportContent temp = a.Deserialize<ReportContent>(reportContent);

            string sql = string.Format("insert into tReportContentList(TemplateId,TemplateVersion,"
                           + "ReportId,ReportVersion,ContentHtml,AuthorName,AutherId,CreateDate,Status,Doman,IsPositive,NaturalContentHtml) "
                           + "values(@TemplateId,@TemplateVersion,@ReportId,@ReportVersion,@ContentHtml,"
                           + "@AuthorName,@AutherId,@CreateDate,@Status,@Domain,@IsPositive,@NaturalContentHtml)");

            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@TemplateId", temp.TemplateId);
            cmd.Parameters.AddWithValue("@TemplateVersion", temp.TemplateVersion);
            cmd.Parameters.AddWithValue("@ReportId", reportID);
            cmd.Parameters.AddWithValue("@ReportVersion", 0);
            cmd.Parameters.AddWithValue("@ContentHtml", temp.ContentHtml);
            cmd.Parameters.AddWithValue("@AuthorName", authorid);
            cmd.Parameters.AddWithValue("@AutherId", authorid);
            cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Domain", domain);
            cmd.Parameters.AddWithValue("@IsPositive", ispositive);
            cmd.Parameters.AddWithValue("@NaturalContentHtml", naturaltext);
        }

        public static void UpdateReportContent(string reportContent, string reportID, string authorid, int status, string naturaltext, ref System.Data.SqlClient.SqlCommand cmd)
        {
            JavaScriptSerializer a = new JavaScriptSerializer();
            ReportContent temp = a.Deserialize<ReportContent>(reportContent);

            cmd.CommandTimeout = 0;
            cmd.Parameters.Clear();

            string sql = string.Format("update tReportContent set ContentHtml=@ContentHtml,"
                              + " AuthorName=@AuthorName, "
                              + " AutherId=@AutherId, "
                              + " Status=@Status, "
                              + " ModifyDate=@ModifyDate, "
                              + " NaturalContentHtml=@NaturalContentHtml "
                              + " where ReportId=@ReportId");
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@ReportId", reportID);
            cmd.Parameters.AddWithValue("@AuthorName", authorid);
            cmd.Parameters.AddWithValue("@AutherId", authorid);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@ContentHtml", temp.ContentHtml);
            cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@NaturalContentHtml", naturaltext);
        }

        public static void UpdateReportItem(string reportItem, string reportID, ref System.Data.SqlClient.SqlCommand cmd)
        {
            string sqldelete = "delete from tReportItem where ReportId = @ReportId";
            cmd.CommandTimeout = 0;
            cmd.Parameters.Clear();

            cmd.CommandText = sqldelete;
            cmd.Parameters.AddWithValue("@ReportId", reportID);

            cmd.ExecuteNonQuery();

            JavaScriptSerializer a = new JavaScriptSerializer();
            List<ReportItemList> items = a.Deserialize<List<ReportItemList>>(reportItem);

            string sql = string.Format("insert into tReportItem(ReportId,ItemPosition,"
                           + "ItemId,ItemName,ValueId,Value,PatientId) "
                           + "values(@ReportId,@ItemPosition,@ItemId,@ItemName,@ValueId,"
                           + "@Value,@PatientId)");

            foreach (var item in items)
            {
                cmd.CommandTimeout = 0;
                cmd.Parameters.Clear();

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@ReportId", reportID);
                cmd.Parameters.AddWithValue("@ItemPosition", "");
                cmd.Parameters.AddWithValue("@ItemId", item.Elements.Id);
                cmd.Parameters.AddWithValue("@ItemName", item.Elements.ContentValue);
                cmd.Parameters.AddWithValue("@ValueId", item.Elements.Id);
                cmd.Parameters.AddWithValue("@Value", item.Elements.Value);
                cmd.Parameters.AddWithValue("@PatientId", item.Elements.Pid);

                cmd.ExecuteNonQuery();
            }

        }
        #endregion
    }

   
    internal class ReportContent
    {
        public string TemplateId { get; set; }

        public int TemplateVersion { get; set; }

        public string ContentHtml { get; set; }
    }

    internal class ReportItem
    {
        public string Tid { get; set; }

        public string Pid { get; set; }

        public string Id { get; set; }

        public string Value { get; set; }

        public string ContentValue { get; set; }
    }

    internal class ReportItemList
    {
        public ReportItem Elements { get; set; }
    }
}
