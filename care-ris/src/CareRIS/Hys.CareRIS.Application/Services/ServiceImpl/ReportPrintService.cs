using AutoMapper;
using C1.C1Report;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Application.Mappers;
using Hys.CareRIS.EnterpriseLib;
using Hys.CrossCutting.Common.Utils;
using Hys.Platform.Application;
using Hys.Platform.Domain.Ris;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Security;
using System.Xml;
using Hys.Common;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class ReportPrintService : DisposableServiceBase, IReportPrintService
    {
        private string REPORT_TEMPORARY_PATH;
        private string PRINT_TEMPLATE_PATH;

        private IReportRepository _ReportRepository;
        private IReportFileRepository _ReportFileRepository;
        private IPrintTemplateRepository _PrintTemplateRepository;
        private IPrintTemplateFieldsRepository _PrintTemplateFieldsRepository;

        private IReportPrintLogRepository _reportPrintLogRepository;

        private IRisProContext _dbContext;

        public ReportPrintService(
            IReportRepository reportRepository,
            IReportFileRepository reportFileRepository,
            IPrintTemplateRepository printTemplateRepository,
            IPrintTemplateFieldsRepository printTemplateFieldsRepository,
            IReportPrintLogRepository reportPrintLogRepository,
            IRisProContext dbContext)
        {
            _ReportRepository = reportRepository;
            _ReportFileRepository = reportFileRepository;
            _PrintTemplateRepository = printTemplateRepository;
            _PrintTemplateFieldsRepository = printTemplateFieldsRepository;
            _reportPrintLogRepository = reportPrintLogRepository;
            _dbContext = dbContext;

            AddDisposableObject(reportRepository);
            AddDisposableObject(reportFileRepository);
            AddDisposableObject(printTemplateRepository);
            AddDisposableObject(printTemplateFieldsRepository);
            AddDisposableObject(reportPrintLogRepository);
            AddDisposableObject(dbContext);
            REPORT_TEMPORARY_PATH = Directory.GetCurrentDirectory() + "\\Temp";
            PRINT_TEMPLATE_PATH = Directory.GetCurrentDirectory() + "\\Template";
        }

        #region Print
        public bool UpdateReportPrintTemplate(string reportID, string printTemplateID)
        {
            var report = _dbContext.Set<Report>().Where(p => p.UniqueID == reportID).FirstOrDefault();
            if (report != null)
            {
                report.PrintTemplateID = printTemplateID;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }


        public PrintTemplateDto GetPrintTemplate(string printTemplateID)
        {
            var printTemplate = _PrintTemplateRepository.Get(p => p.UniqueID == printTemplateID).FirstOrDefault();
            if (printTemplate != null)
            {
                return Mapper.Map<PrintTemplate, PrintTemplateDto>(printTemplate);
            }
            return null;
        }


        public List<PrintTemplateDto> GetPrintTemplateByCriteria(PrintTemplateDto criteria, string domain, string site)
        {
            criteria.Site = site; //domain;
            criteria.Domain = domain;//site;

            //site
            var printTemplates = _PrintTemplateRepository
                    .Get(p => (criteria.Type == null || p.Type == criteria.Type)
                    && (criteria.Version == null || p.Version == criteria.Version)
                    && (string.IsNullOrEmpty(criteria.ModalityType) || p.ModalityType == criteria.ModalityType)
                    && (p.Site == criteria.Site)
                    && (criteria.IsDefaultByModality == null || p.IsDefaultByModality == (int?)(criteria.IsDefaultByModality.Value ? 1 : 0))
                    && (string.IsNullOrEmpty(criteria.PropertyTag) || p.PropertyTag.Contains(criteria.PropertyTag)))
                    .ToList().Select(d => Mapper.Map<PrintTemplate, PrintTemplateDto>(d)).ToList();
            if (printTemplates.Count > 0)
            {
                return printTemplates;
            }
            else
            {
                //site
                printTemplates = _PrintTemplateRepository
                    .Get(p => (criteria.Type == null || p.Type == criteria.Type)
                    && (criteria.Version == null || p.Version == criteria.Version)
                    && (string.IsNullOrEmpty(criteria.ModalityType) || p.ModalityType == criteria.ModalityType)
                    && (p.Domain == criteria.Domain)
                    && (p.Site == null || p.Site == "")
                    && (criteria.IsDefaultByModality == null || p.IsDefaultByModality == (int?)(criteria.IsDefaultByModality.Value ? 1 : 0))
                    && (string.IsNullOrEmpty(criteria.PropertyTag) || p.PropertyTag.Contains(criteria.PropertyTag)))
                    .ToList().Select(d => Mapper.Map<PrintTemplate, PrintTemplateDto>(d)).ToList();
                if (printTemplates.Count > 0)
                {
                    return printTemplates;
                }
                else
                {
                    printTemplates = _PrintTemplateRepository
                            .Get(p => (criteria.Type == null || p.Type == criteria.Type)
                           && (criteria.Version == null || p.Version == criteria.Version)
                           && (string.IsNullOrEmpty(criteria.ModalityType) || p.ModalityType == criteria.ModalityType)
                           && (p.Domain == null)
                           && (p.Site == null)
                           && (criteria.IsDefaultByModality == null || p.IsDefaultByModality == (int?)(criteria.IsDefaultByModality.Value ? 1 : 0))
                           && (string.IsNullOrEmpty(criteria.PropertyTag) || p.PropertyTag.Contains(criteria.PropertyTag)))
                           .ToList().Select(d => Mapper.Map<PrintTemplate, PrintTemplateDto>(d)).ToList();
                    if (printTemplates.Count > 0)
                    {
                        return printTemplates;
                    }
                }
            }
            return null;
        }

        public PrintTemplateFieldsDto GetPrintTemplateFields(string uniqueID)
        {
            var printTemplateFields = _PrintTemplateFieldsRepository.Get(p => p.UniqueID == uniqueID).FirstOrDefault();
            if (printTemplateFields != null)
            {
                return Mapper.Map<PrintTemplateFields, PrintTemplateFieldsDto>(printTemplateFields);
            }
            return null;
        }

        public string GetReportPrintTemplateID(string reportID, string loginDomain, string loginSite)
        {
            List<ProcedureDto> procedureDtos = GetProcedureByReportID(reportID).ToList();
            ProcedureDto procedure = procedureDtos[0];

            DataTable dt;

            //use store procedure
            Order order = _dbContext.Set<Order>().Where(p => p.UniqueID == procedure.OrderID).FirstOrDefault();
            string accNo = order.AccNo;
            string modalityType = procedure.ModalityType;
            ReportDBService reportDBService = new ReportDBService();
            string templateID = "";
            reportDBService.GetReportPrintTemplate(accNo, modalityType, reportID, loginSite, out templateID, out dt);

            return templateID;

        }

        public string GetReportPrintUrl(string genPDFServiceURL, string reportID)
        {
            List<ProcedureDto> procedureDtos = GetProcedureByReportID(reportID).ToList();
            ProcedureDto procedureDto = procedureDtos[0];
            Order order = _dbContext.Set<Order>().Where(p => p.UniqueID == procedureDto.OrderID).FirstOrDefault();
            string accNo = order.AccNo;
            string modalityType = procedureDtos[0].ModalityType;

            var url = RisDBService.GetPDFURL(genPDFServiceURL, "3", accNo, modalityType, reportID, 0);
            return url;
        }

        public bool UpdateReportPrintStatusByProcedureID(string procedureID, string printer, string site, string domain)
        {
            bool result = false;
            var procedure = _dbContext.Set<Procedure>().Where(p => p.UniqueID == procedureID).FirstOrDefault();
            if (procedure != null && !String.IsNullOrEmpty(procedure.ReportID))
            {
                var report = _dbContext.Set<Report>().Where(p => p.UniqueID == procedure.ReportID).FirstOrDefault();
                var order = _dbContext.Set<Order>().Where(p => p.UniqueID == procedure.OrderID).FirstOrDefault();
                var accNo = order.AccNo;
                var modalityType = procedure.ModalityType;
                var reportDBService = new ReportDBService();
                var templateID = String.Empty;
                DataTable templateDT = null;
                reportDBService.GetReportPrintTemplate(accNo, modalityType, procedure.ReportID, site, out templateID, out templateDT);
                if (procedure.Status == (int)RPStatus.FirstApprove && !String.IsNullOrEmpty(templateID))
                {
                    report.IsPrint = 1;
                    report.Optional1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //report.PrintCopies++; Will be handled in tbReportPrintLog trigger
                    if (String.IsNullOrEmpty(report.PrintTemplateID))
                    {
                        report.PrintTemplateID = templateID;
                    }

                    result = true;
                }

                // update report print log
                _reportPrintLogRepository.Add(new ReportPrintLog
                {
                    UniqueID = Guid.NewGuid().ToString(),
                    ReportID = procedure.ReportID,
                    TemplateID = templateID,
                    Printer = printer,
                    PrintDate = DateTime.Now,
                    Type = "Print",
                    Counts = 1,
                    Domain = domain
                });
                _dbContext.SaveChanges();

            }

            return result;
        }

        public PrintDataDto GetOtherReportPrintData(string accno, string modalityType, string templateType, string site)
        {
            var service = new ReportDBService();
            string template = String.Empty;
            DataTable data = null;
            string templateID = "";
            service.GetOtherReportPrintTemplate(accno, modalityType, templateType, site, out template, out data, 0, out templateID);
            return new PrintDataDto { Template = template, data = data };
        }

        public string GetOtherReportPrintID(string accno, string modalityType, string templateType, string site)
        {
            var service = new ReportDBService();
            string template = String.Empty;
            DataTable data = null;
            string templateID = "";
            service.GetOtherReportPrintTemplate(accno, modalityType, templateType, site, out template, out data, 1, out templateID);
            return templateID;
        }

        private IEnumerable<ProcedureDto> GetProcedureByReportID(string reportID)
        {
            var query = _dbContext.Set<Procedure>().Where(p => p.ReportID == reportID).ToList();
            List<ProcedureDto> procedureDtos = new List<ProcedureDto>();
            foreach (Procedure procedure in query)
            {
                procedureDtos.Add(Mapper.Map<Procedure, ProcedureDto>(procedure));
            }

            return procedureDtos;
        }
        #endregion

        #region HTML
        public DataTable GetBaseInfoByProcedure(ProcedureDto procedure)
        {
            DataTable dt = ReportUtils.CreateDTReportTemplate();
            DataRow dr = dt.NewRow();

            if (procedure != null)
            {
                ReportUtils.SetDTDataForProcedure(procedure, ref dr);

                if (!string.IsNullOrEmpty(procedure.ReportID))
                {
                    Report report = _ReportRepository.Get(p => p.UniqueID == procedure.ReportID).FirstOrDefault();
                    if (report != null)
                    {
                        //report
                        ReportUtils.SetDTDataForReport(report, ref dr);
                    }
                }

                if (!string.IsNullOrEmpty(procedure.ProcedureCode))
                {
                    List<Procedurecode> procedurecodes = _dbContext.Set<Procedurecode>().Where(p => procedure.ProcedureCode.Contains(p.ProcedureCode)).ToList();
                    if (procedurecodes != null)
                    {
                        ReportUtils.SetDTDataForProcedurecode(procedurecodes, ref dr);
                    }
                }

                //
                if (!string.IsNullOrEmpty(procedure.OrderID))
                {
                    Order order = _dbContext.Set<Order>().Where(p => p.UniqueID == procedure.OrderID).FirstOrDefault();
                    if (order != null)
                    {
                        ReportUtils.SetDTDataForOrder(order, ref dr);

                        if (!string.IsNullOrEmpty(order.PatientID))
                        {
                            Patient patient = _dbContext.Set<Patient>().Where(p => p.UniqueID == order.PatientID).FirstOrDefault();
                            ReportUtils.SetDTDataForPatient(patient, ref dr);
                        }
                    }
                }
            }

            dt.Rows.Add(dr);

            return dt;
        }

        public string GetBaseInfoHtmlByProcedure(ProcedureDto procedure, string domain, string site)
        {
            DataTable dt = GetBaseInfoByProcedure(procedure);

            string templateInfo = getPrintTemplateForBaseInfo(domain, site);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(templateInfo);
            using (C1Report c1rpt = new C1Report())
            {

                c1rpt.Load(xmlDocument, "Template");
                c1rpt.DataSource.Recordset = generateDataTable4Printing(dt, c1rpt);

                //to html
                using (MemoryStream streamHtml = new MemoryStream())
                {
                    c1rpt.RenderToStream(streamHtml, FileFormatEnum.HTML);
                    streamHtml.Position = 0;
                    using (StreamReader reader = new StreamReader(streamHtml, Encoding.UTF8))
                    {
                        string html = reader.ReadToEnd();
                        return html;
                    }
                }
            }
        }

        private string getPrintTemplateForBaseInfo(string domain, string site)
        {
            PrintTemplateDto criteria = new PrintTemplateDto
            {
                Type = 0,
                TemplateName = "baseinfo"
            };

            List<PrintTemplateDto> printTemplateDtoList = GetPrintTemplateByCriteria(criteria, domain, site);
            if (printTemplateDtoList == null || printTemplateDtoList.Count == 0)
            {
                return "";
            }
            PrintTemplateDto printTemplateDto = printTemplateDtoList[0];

            string templateInfo = ReportUtils.GetUnicodeStringFromBytes(printTemplateDto.TemplateInfo);
            return templateInfo;
        }

        private DataTable generateDataTable4Printing(DataTable dtSrc, C1Report c1rpt)
        {
            DataTable dt = ReportUtils.CreateDTBaseInfoTemplate();

            foreach (Field fld in c1rpt.Fields)
            {
                if (fld.Picture != null)
                {
                    C1.C1Report.Util.PictureHolder ph = fld.Picture as C1.C1Report.Util.PictureHolder;

                    if (ph != null && ph.IsBound && !dt.Columns.Contains(ph.FieldName))
                        dt.Columns.Add(ph.FieldName, System.Type.GetType("System.Byte[]"));
                }
                else if (fld.Calculated)
                {
                    if (!dt.Columns.Contains(fld.Text))
                    {
                        dt.Columns.Add(fld.Text);

                        if (fld.Text.ToLower() == "tbreport__wys")
                        {
                            fld.RTF = false;
                            fld.Text = "tbReport__WYSTEXT";
                        }
                        else if (fld.Text.ToLower() == "tbreport__wyg")
                        {
                            fld.RTF = false;
                            fld.Text = "tbReport__WYGTEXT";
                        }
                    }
                }
            }

            //c1rpt.Save(templateFile);

            if (dtSrc.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();

                foreach (DataColumn fld in dt.Columns)
                {
                    if (dtSrc.Columns.Contains(fld.ColumnName))
                    {
                        if (fld.ColumnName.ToLower().EndsWith("reportname"))
                        {
                            dr[fld.ColumnName] = GetStringForPrinting(dtSrc, "tbRegPatient__LocalName")
                                + GetStringForPrinting(dtSrc, fld.ColumnName);
                        }
                        else
                        {
                            dr[fld.ColumnName] = GetStringForPrinting(dtSrc, fld.ColumnName);
                        }
                    }
                }

                dt.Rows.Add(dr);
            }

            SetExamNumber(dt);

            //SetSignImage(dt, dtSrc);

            return dt;
        }

        private void SetExamNumber(DataTable dataSrc)
        {
            if (!dataSrc.Columns.Contains(ReportCommon.FIELDNAME_EXAMNUMBER))
            {
                return;
            }

            string strPatientID = ReportUtils.GetStringFromDataTable(dataSrc, ReportCommon.FIELDNAME_PATIENTID);
            string strOrderGuid = ReportUtils.GetStringFromDataTable(dataSrc, ReportCommon.FIELDNAME_ORDERGUID);

            if (string.IsNullOrEmpty(strPatientID) || string.IsNullOrEmpty(strOrderGuid))
            {
                return;
            }

            int examNo = GetPatientExamNo(strPatientID, strOrderGuid);

            if (examNo == 0)
            {
                return;
            }

            foreach (DataRow dr in dataSrc.Rows)
            {
                dr[ReportCommon.FIELDNAME_EXAMNUMBER] = examNo;
            }
        }

        private string GetStringForPrinting(DataTable dt, string colName)
        {
            if (dt == null || dt.Rows.Count < 1 || !dt.Columns.Contains(colName) ||
                colName == null || colName.Length < 1)
            {
                System.Diagnostics.Debug.Assert(false);

                return "";
            }

            string key = colName.ToUpper().Trim();
            string ret = "";

            object objValue = dt.Rows[0][key];
            string fldValue = "";

            var dictionaries = _dbContext.Set<Dictionary>().ToList()
                .Select(d => Mapper.Map<Dictionary, DictionaryDto>(d)).ToList();
            var groupedDVs = _dbContext.Set<DictionaryValue>().ToList()
                .Select(dv => Mapper.Map<DictionaryValue, DictionaryValueDto>(dv)).GroupBy(d => d.Tag).ToList();
            dictionaries.ForEach(d =>
            {
                var group = groupedDVs.FirstOrDefault(g => g.Key == d.Tag);
                if (group != null)
                {
                    d.Values = group.Select(g => g).ToList();
                }
            });

            if (objValue.GetType() == System.Type.GetType("System.Byte[]"))
            {
                Byte[] buff = objValue as Byte[];

                fldValue = ReportUtils.GetStringFromBytes(buff);
            }
            if (objValue.GetType() == System.Type.GetType("System.DateTime"))
            {
                DateTime dateTime = Convert.ToDateTime(objValue);

                fldValue = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
            }
            else
            {
                fldValue = objValue.ToString();
            }

            if (objValue.GetType() != System.Type.GetType("System.DateTime") &&
                (key.StartsWith("tbProcedureCode") || key.StartsWith("tbProcedureCode")))
            {
                ret = ReportUtils.GetStringFromDataTable(dt, colName);
                if (ReportUtils.isNeedLocalizationAsUserName(colName))
                {
                    UserDto userDto = null;
                    var user = _dbContext.Set<User>().Where(p => p.UniqueID == fldValue).FirstOrDefault();
                    if (user != null)
                    {
                        userDto = Mapper.Map<User, UserDto>(user);
                    }
                    if (userDto != null)
                    {
                        ret = userDto.LocalName;
                    }
                    else
                    {
                        ret = fldValue;
                    }
                }
            }
            else if (key.EndsWith("SITE"))
            {
                var siteDto = _dbContext.Set<Site>().Where(p => p.SiteName.Equals(fldValue, StringComparison.OrdinalIgnoreCase))
                .ToList().Select(d => Mapper.Map<Site, SiteDto>(d)).FirstOrDefault();
                if (siteDto != null)
                {
                    ret = siteDto.Alias;
                }
                else
                {
                    ret = fldValue;
                }
            }
            else if (key.EndsWith("CURRENTAGE"))
            {
                ret = ReportUtils.LocalizeCurrentAge(fldValue, dictionaries);
            }
            else if (ReportUtils.isNeedLocalizationAsUserName(colName))
            {
                UserDto userDto = null;
                var user = _dbContext.Set<User>().Where(p => p.UniqueID == fldValue).FirstOrDefault();
                if (user != null)
                {
                    userDto = Mapper.Map<User, UserDto>(user);
                }
                if (userDto != null)
                {
                    ret = userDto.LocalName;
                }
                else
                {
                    ret = fldValue;
                }
            }
            else if (key.EndsWith("BEDSIDE") || key.EndsWith("THREEDREBUILD"))
            {
                ret = ReportUtils.GetDictionaryText(dictionaries, (int)DictionaryTag.YesNo, fldValue);
            }
            else if (key.EndsWith("ISPOSITIVE"))
            {
                ret = ReportUtils.GetDictionaryText(dictionaries, (int)DictionaryTag.Positive, fldValue);
            }
            else
            {
                ret = fldValue;
            }

            return ret;
        }

        private int GetPatientExamNo(string patientID, string orderID)
        {
            ReportDBService reportDBService = new ReportDBService();
            return reportDBService.GetPatientExamNo(patientID, orderID);
        }

        public string GetReportViewer(string reportID, string loginDomain, string loginSite)
        {
            List<ProcedureDto> procedureDtos = GetProcedureByReportID(reportID).ToList();
            ProcedureDto procedure = procedureDtos[0];

            DataTable dt;
            string templateInfo = "";

            //use store procedure
            Order order = _dbContext.Set<Order>().Where(p => p.UniqueID == procedure.OrderID).FirstOrDefault();
            string accNo = order.AccNo;
            string modalityType = procedure.ModalityType;
            ReportDBService reportDBService = new ReportDBService();
            string templateID = "";
            reportDBService.GetReportPrintTemplate(accNo, modalityType, reportID, loginSite, out templateID, out dt);
            if (templateID != "")
            {
                PrintTemplate printTemplate = _dbContext.Set<PrintTemplate>().Where(p => p.UniqueID == templateID).FirstOrDefault();
                if (printTemplate != null)
                {
                    templateInfo = generatePrintTemplate(printTemplate);
                }
            }
            else
            {
                return null;
            }


            string templateGuid = ReportUtils.GetFirstRowValueFromDataSet(dt.DataSet, ReportCommon.FIELDNAME_tbReport__PrintTemplateGuid);

            DataTable newdt = generateDataTable4PrintingForReport(dt.DataSet, ref templateInfo, reportID, loginDomain);

            DataSet newds = new DataSet();
            newds.Tables.Add(newdt);
            //get html
            return GetRenderedReportHtml(newds, templateInfo);
        }

        public ShowHtmlDataDto GetReportViewer2(string reportID, string loginDomain, string loginSite, string printTemplateID)
        {
            List<ProcedureDto> procedureDtos = GetProcedureByReportID(reportID).ToList();
            ProcedureDto procedure = procedureDtos[0];

            DataTable dt;
            string templateInfo = "";

            //use store procedure
            Order order = _dbContext.Set<Order>().Where(p => p.UniqueID == procedure.OrderID).FirstOrDefault();
            string accNo = order.AccNo;
            string modalityType = procedure.ModalityType;
            ReportDBService reportDBService = new ReportDBService();
            string templateID = "";
            reportDBService.GetReportPrintTemplate(accNo, modalityType, reportID, loginSite, out templateID, out dt);


            //print templateID
            if (!string.IsNullOrEmpty(printTemplateID))
            {
                Report report = _dbContext.Set<Report>().Where(r => r.UniqueID == reportID).FirstOrDefault();
                if (report != null && report.Status < (int)RPStatus.FirstApprove)
                {
                    templateID = printTemplateID;
                }
            }

            if (templateID != "")
            {
                PrintTemplate printTemplate = _dbContext.Set<PrintTemplate>().Where(p => p.UniqueID == templateID).FirstOrDefault();
                if (printTemplate != null)
                {
                    templateInfo = generatePrintTemplate(printTemplate);
                }
            }
            else
            {
                return null;
            }


            string templateGuid = ReportUtils.GetFirstRowValueFromDataSet(dt.DataSet, ReportCommon.FIELDNAME_tbReport__PrintTemplateGuid);

            DataTable newdt = generateDataTable4PrintingForReport(dt.DataSet, ref templateInfo, reportID, loginDomain);
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, newdt);

                return new ShowHtmlDataDto { Template = templateInfo, data = Convert.ToBase64String(ms.ToArray()) };
            }

        }

        private string GetRenderedReportHtml(DataSet dsReportData, string templateInfo)
        {
            if (dsReportData == null || dsReportData.Tables.Count < 1)
            {
                return string.Empty;
            }

            string content = "";

            string rptGuid = ReportUtils.GetFirstRowValueFromDataSet(dsReportData, "TBREPORT__REPORTGUID");

            string savePath = GenerateTempSaveFolder();
            string saveFile = Path.Combine(savePath, rptGuid + ".htm");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(templateInfo);
            using (C1Report c1rpt = new C1Report())
            {
                c1rpt.Load(xmlDocument, "Template");
                c1rpt.DataSource.Recordset = dsReportData.Tables[0];

                c1rpt.RenderToFile(saveFile, FileFormatEnum.HTML);
            }

            content = File.ReadAllText(saveFile);

            int startIndex = content.IndexOf("src=\"");
            while (startIndex > 0)
            {
                int endIndex = content.IndexOf("\"", startIndex + 6);
                if (endIndex > 0)
                {
                    string imagePath = content.Substring(startIndex + 5, endIndex - startIndex - 5);
                    byte[] imageArray = System.IO.File.ReadAllBytes(savePath + "\\" + imagePath);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    content = content.Substring(0, startIndex + 5) + "data:image/jpeg;base64," + base64ImageRepresentation + content.Substring(endIndex);
                }

                startIndex = content.IndexOf("src=\"", startIndex + 6);
            }

            return content;
        }

        private DataTable GenerateDataTable4Printing(DataTable dtSrc, C1Report c1rpt)
        {
            DataTable dt = new DataTable();

            foreach (Field fld in c1rpt.Fields)
            {
                if (fld.Picture != null)
                {
                    C1.C1Report.Util.PictureHolder ph = fld.Picture as C1.C1Report.Util.PictureHolder;

                    if (ph != null && ph.IsBound && !dt.Columns.Contains(ph.FieldName))
                        dt.Columns.Add(ph.FieldName, System.Type.GetType("System.Byte[]"));
                }
                else if (fld.Calculated)
                {
                    if (!dt.Columns.Contains(fld.Text))
                    {
                        dt.Columns.Add(fld.Text);
                    }
                }
            }

            if (dtSrc.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();

                foreach (DataColumn fld in dt.Columns)
                {
                    if (dtSrc.Columns.Contains(fld.ColumnName))
                    {
                        if (fld.DataType == System.Type.GetType("System.Byte[]"))
                        {
                            dr[fld.ColumnName] = ReportUtils.GetBytesFromBase64(dtSrc, fld.ColumnName);
                        }
                        else
                        {
                            dr[fld.ColumnName] = GetStringForPrinting(dtSrc, fld.ColumnName);
                        }
                    }
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        private DataTable generateDataTable4PrintingForReport(DataSet dsSrc, ref string templateInfo, string reportID, string domain)
        {
            DataTable dt = new DataTable();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(templateInfo);
            using (C1Report c1rpt = new C1Report())
            {
                c1rpt.Load(xmlDocument, "Template");
                dt = generateDataTable4Printing(dsSrc.Tables[0], c1rpt);

                readReportImages(dt, dsSrc.Tables[0], reportID, c1rpt, domain);

                using (StringWriter stringWriter = new StringWriter())
                {
                    using (XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(stringWriter))
                    {
                        c1rpt.Save(xmlTextWriter, true, true);
                        templateInfo = stringWriter.ToString();

                        templateInfo = "<?xml version=\"1.0\"?><Reports>" + templateInfo + "</Reports>";
                    }
                }
            }

            return dt;
        }

        private void readReportImages(DataTable dstDT, DataTable dtReportInfo, string reportID, C1Report c1rpt, string domain)
        {
            List<string> listTmpFile = getFTPFiles(dtReportInfo, reportID, domain);

            int idx = 0;

            foreach (Field fld in c1rpt.Fields)
            {
                if (fld.Picture != null)
                {
                    C1.C1Report.Util.PictureHolder ph = fld.Picture as C1.C1Report.Util.PictureHolder;

                    if (ph != null && ph.IsBound)
                    {
                        if (string.Compare(ph.FieldName, ReportCommon.FIELDNAME_CREATORSIGNIMAGE, true) == 0)
                        {
                            string creater = ReportUtils.GetStringFromDataTable(dtReportInfo, ReportCommon.FIELDNAME_tbReport__Creater);
                            if (!string.IsNullOrEmpty(creater))
                            {
                                User user = _dbContext.Set<User>().Where(u => u.UniqueID == creater).FirstOrDefault();
                                if (user != null)
                                {
                                    dstDT.Rows[0][ph.FieldName] = user.SignImage;
                                }
                            }
                        }
                        else if (string.Compare(ph.FieldName, ReportCommon.FIELDNAME_FIRSTAPPROVERSIGNIMAGE, true) == 0)
                        {
                            string approver = ReportUtils.GetStringFromDataTable(dtReportInfo, ReportCommon.FIELDNAME_tbReport__FirstApprover);
                            if (!string.IsNullOrEmpty(approver))
                            {
                                User user = _dbContext.Set<User>().Where(u => u.UniqueID == approver).FirstOrDefault();
                                if (user != null)
                                {
                                    dstDT.Rows[0][ph.FieldName] = user.SignImage;
                                }
                            }
                        }
                        else if (string.Compare(ph.FieldName, ReportCommon.FIELDNAME_SUBMITTERSIGNIMAGE, true) == 0)
                        {
                            string submitter = ReportUtils.GetStringFromDataTable(dtReportInfo, ReportCommon.FIELDNAME_tbReport__Submitter);
                            if (!string.IsNullOrEmpty(submitter))
                            {
                                User user = _dbContext.Set<User>().Where(u => u.UniqueID == submitter).FirstOrDefault();
                                if (user != null)
                                {
                                    dstDT.Rows[0][ph.FieldName] = user.SignImage;
                                }
                            }
                        }
                        else if (dstDT.Columns.Contains(ph.FieldName) && idx < listTmpFile.Count)
                        {
                            dstDT.Rows[0][ph.FieldName] = File.ReadAllBytes(listTmpFile[idx]);

                            ++idx;
                        }
                    }
                }
            }
        }

        private List<string> getFTPFiles(DataTable dtReportInfo, string reportID, string domain)
        {
            List<string> listTmpFile = new List<string>();
            List<ReportFile> reportFileList = _dbContext.Set<ReportFile>().Where(p => p.ReportID == reportID).OrderBy(p => p.ImagePosition).ToList();
            try
            {
                if (reportFileList == null || reportFileList.Count < 1)
                {
                    return listTmpFile;
                }

                string tmpFolder = GenerateTempSaveFolder();

                string accNo = ReportUtils.GetStringFromDataTable(dtReportInfo, ReportCommon.FIELDNAME_ACCNO);
                string reportGuid = ReportUtils.GetStringFromDataTable(dtReportInfo, ReportCommon.FIELDNAME_tbReport__ReportGuid);
                string tmpPath = System.IO.Path.Combine(tmpFolder, "images");

                if (!System.IO.Directory.Exists(tmpPath))
                {
                    System.IO.Directory.CreateDirectory(tmpPath);
                }

                //
                // Start to Ftp
                FtpClient ftpClient = getFTPObject(domain);
                if (ftpClient != null)
                {
                    foreach (ReportFile reportFile in reportFileList)
                    {
                        string fguid = reportFile.UniqueID;
                        string fname = reportFile.FileName;
                        string rpath = reportFile.RelativePath;
                        int fType = reportFile.fileType.Value;
                        int showWidth = reportFile.ShowWidth.Value;
                        int showHeight = reportFile.ShowHeight.Value;
                        int imageposition = reportFile.ImagePosition.Value;

                        if (1 == fType)
                        {
                            string localFilePath = tmpPath + "\\" + fname;

                            if (!File.Exists(localFilePath))
                            {
                                ftpClient.DownloadFile(rpath + "\\" + fname, localFilePath);
                            }
                            if (System.IO.File.Exists(localFilePath))
                            {
                                listTmpFile.Add(localFilePath);
                            }
                        }
                    }

                    ftpClient.Close();
                }
            }
            catch (Exception ex)
            {
                string msg = "Fail to get the images from FTP server! Message=" + ex.Message;
            }

            return listTmpFile;
        }

        private FtpClient getFTPObject(string domain)
        {
            DomainList domainList = _dbContext.Set<DomainList>().Where(p => p.DomainName == domain).FirstOrDefault();
            if (domainList != null)
            {
                return new FtpClient(
                    domainList.FtpServer,
                    Convert.ToInt32(domainList.FtpPort),
                    domainList.FtpUser,
                    domainList.FtpPassword);
            }

            return null;
        }

        private string GenerateTempSaveFolder()
        {
            string savePath = REPORT_TEMPORARY_PATH + "\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + "\\";

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            return savePath;
        }


        private string GetPrintTemplateByName(int type, string templateName, string loginDomain, string loginSite)
        {
            PrintTemplate printTemplate = _dbContext.Set<PrintTemplate>().Where(p => p.Type == type).FirstOrDefault();
            if (printTemplate != null)
            {
                // site at first
                printTemplate = _dbContext.Set<PrintTemplate>().Where(p => p.Type == type && p.Site == loginSite && p.TemplateName == templateName).FirstOrDefault();
                if (printTemplate != null)
                {
                    return generatePrintTemplate(printTemplate);
                }

                // domain then
                printTemplate = _dbContext.Set<PrintTemplate>().Where(p => p.Type == type && p.Domain == loginDomain && p.TemplateName == templateName).FirstOrDefault();
                if (printTemplate != null)
                {
                    return generatePrintTemplate(printTemplate);
                }

                // global
                printTemplate = _dbContext.Set<PrintTemplate>().Where(p => p.Type == type && p.TemplateName == templateName).FirstOrDefault();
                if (printTemplate != null)
                {
                    return generatePrintTemplate(printTemplate);
                }
            }

            return string.Empty;
        }

        private string generatePrintTemplate(PrintTemplate printTemplate)
        {
            string templateInfo = ReportUtils.GetUnicodeStringFromBytes(printTemplate.TemplateInfo);
            return templateInfo;
        }

        public string GetBaseInfoByOrderID(string id, string domain, string site)
        {
            List<ProcedureDto> procedureDtos = GetProcedureByOrderID(id).ToList();

            // assume procedures count should not be 0 because it is locked by client code, but return null
            // in case the api is not called from client
            if (procedureDtos.Count == 0)
            {
                return null;
            }

            ProcedureDto procedure = procedureDtos[0];

            foreach (ProcedureDto procedureItem in procedureDtos)
            {
                if (procedureItem.UniqueID != procedure.UniqueID)
                {
                    if (procedureItem.BodyPart != "")
                    {
                        procedure.BodyPart += "," + procedureItem.BodyPart;
                    }

                    if (procedureItem.ProcedureCode != "")
                    {
                        procedure.ProcedureCode += "," + procedureItem.ProcedureCode;
                    }

                    if (procedureItem.RPDesc != "")
                    {
                        procedure.RPDesc += "," + procedureItem.RPDesc;
                    }
                }
            }

            string content = GetBaseInfoHtmlByProcedure(procedure, domain, site);

            return content;
        }

        public string GetBaseInfoByProcedureID(ProcedureDto procedureDto, string id, string domain, string site)
        {
            ProcedureDto procedure = null;

            if (string.IsNullOrEmpty(procedureDto.ReportID))
            {
                procedure = procedureDto;
            }
            else
            {
                List<ProcedureDto> procedureDtos = GetProcedureByReportID(procedureDto.ReportID).ToList();
                procedure = procedureDtos[0];

                foreach (ProcedureDto procedureItem in procedureDtos)
                {
                    if (procedureItem.UniqueID != procedure.UniqueID)
                    {
                        if (procedureItem.BodyPart != "")
                        {
                            procedure.BodyPart += "," + procedureItem.BodyPart;
                        }

                        if (procedureItem.ProcedureCode != "")
                        {
                            procedure.ProcedureCode += "," + procedureItem.ProcedureCode;
                        }

                        if (procedureItem.RPDesc != "")
                        {
                            procedure.RPDesc += "," + procedureItem.RPDesc;
                        }
                    }
                }
            }

            string content = GetBaseInfoHtmlByProcedure(procedure, domain, site);

            return content;
        }

        private IEnumerable<ProcedureDto> GetProcedureByOrderID(string orderID)
        {
            var query = _dbContext.Set<Procedure>().Where(p => p.OrderID == orderID).ToList();
            List<ProcedureDto> procedureDtos = new List<ProcedureDto>();
            foreach (Procedure procedure in query)
            {
                if (procedure.Status == (int)RPStatus.Examination)
                {
                    procedureDtos.Add(Mapper.Map<Procedure, ProcedureDto>(procedure));
                }
            }

            return procedureDtos;
        }
        #endregion

    }
}
