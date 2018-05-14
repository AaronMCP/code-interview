using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hys.CareRIS.EntityFramework.Repositories;
using Hys.CareRIS.Application.Services.ServiceImpl;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.Applicatiton.Test.Mock;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Test.Utils;
using Hys.CrossCutting.Common.Extensions;
using System.Collections.Generic;
using Hys.CareRIS.EntityFramework;
using Hys.CareRIS.Application.Dtos.Report;

namespace Hys.CareRIS.Application.Test.Tests
{
    [TestClass]
    public class ReportServiceTest : TestBase
    {
        private static IReportRepository _reportRepository;
        private static IReportFileRepository _reportFileRepository;
        private static IPrintTemplateRepository _printTemplateRepository;
        private static IPrintTemplateFieldsRepository _printTemplateFieldsRepository;
        private static IReportTemplateDirecRepository _reportTemplateDirecRepository;
        private static IReportTemplateRepository _reportTemplateRepository;
        private static IReportPrintLogRepository _reportPrintLogRepository;

        private static ReportService _reportService;
        private static ReportPrintService _reportPrintService;
        private static ReportLockService _reportLockService;
        private static ReportTemplateService _reportTemplateService;

        private static string _reportID;
        private static Report _report1;
        private static Report _report2;
        private static Report _report3;

        private static string _patientID;
        private static Patient _patient1;

        private static string _orderID;
        private static Order _order1;

        private static string _procedureID;
        private static string _procedureID2;
        private static Procedure _procedure1;
        private static Procedure _procedure2;

        private static string _reportFileID;
        private static ReportFile _reportFile1;

        private static string _printTemplateID;
        private static PrintTemplate _printTemplate1;
        private static PrintTemplate _printTemplate2;

        private static string _printTemplateFieldsID;
        private static PrintTemplateFields _printTemplateFields1;

        // Report Template
        private static string _reportTemplateDirecID;
        private static string _reportTemplateDirecParentID;
        private static string _reportTemplateDirecUserID;
        private static ReportTemplateDirec _reportTemplateDirec;
        private static string _reportTemplateID;
        private static ReportTemplate _reportTemplate;

        //history list
        private static ReportList _reportList;

        private static User _user;
        private static string _userID;

        private static Site _site;
        private static string _siteName;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _reportRepository = new MockReportRepository(_MockRisProContext);
            _reportFileRepository = new MockReportFileRepository(_MockRisProContext);
            _printTemplateRepository = new MockPrintTemplateRepository(_MockRisProContext);
            _printTemplateFieldsRepository = new MockPrintTemplateFieldsRepository(_MockRisProContext);
            _reportTemplateDirecRepository = new MockReportTemplateDirecRepository(_MockRisProContext);
            _reportTemplateRepository = new MockReportTemplateRepository(_MockRisProContext);
            _reportPrintLogRepository = new MockReportPrintLogRepository(_MockRisProContext);

            _reportPrintService = new ReportPrintService
               (
               _reportRepository,
               _reportFileRepository,
               _printTemplateRepository,
               _printTemplateFieldsRepository,
               _reportPrintLogRepository,
               _MockRisProContext
               );
            _reportLockService = new ReportLockService
               (
               _MockRisProContext
               );
            _reportTemplateService = new ReportTemplateService
               (
               _reportTemplateRepository,
                _reportTemplateDirecRepository,
               _MockRisProContext
               );

            _reportService = new ReportService
                (
                _reportRepository,
                _reportFileRepository,
                _MockRisProContext,
                _reportPrintService
                );

            _userID = Guid.NewGuid().ToString();
            _user = new User { UniqueID = _userID, LoginName = "1", Password = "2g+c5KgNxEGZOC66DGMKcw==" };

            _siteName = Guid.NewGuid().ToString();
            _site = new Site { SiteName = _siteName, PacsWebServer = "http://10.184.193.236/masterview/mv.jsp?server_name=FIR&user_name={user_name}&password={password}&patient_id=&accession_number={tbRegOrder__AccNo}&close_on_exit=true" };

            _reportID = Guid.NewGuid().ToString();
            _report1 = new Report { UniqueID = _reportID.ToString(), ReportName = "ReportName1", ReportText = "ReportText1", PrintTemplateID = "dd5e991d-b595-4ed2-9958-126d1ea82b10" };
            _report2 = new Report { UniqueID = Guid.NewGuid().ToString(), ReportName = "ReportName2", ReportText = "ReportText2" };
            _report3 = new Report { UniqueID = Guid.NewGuid().ToString(), ReportName = "ReportName3", ReportText = "ReportText3" };

            //order
            _patientID = Guid.NewGuid().ToString();
            _patient1 = new Patient { UniqueID = _patientID.ToString(), LocalName = "LocalName1" };

            //order
            _orderID = Guid.NewGuid().ToString();
            _order1 = new Order { UniqueID = _orderID.ToString(), PatientID = _patientID, ExamSite = _siteName };

            //procedure
            _procedureID = Guid.NewGuid().ToString();
            _procedureID2 = Guid.NewGuid().ToString();
            _procedure1 = new Procedure { UniqueID = _procedureID.ToString(), ReportID = _reportID.ToString(), OrderID = _orderID, Status = 50 };
            _procedure2 = new Procedure { UniqueID = _procedureID2.ToString() };

            //reportfile
            _reportFileID = Guid.NewGuid().ToString();
            _reportFile1 = new ReportFile { UniqueID = _reportFileID.ToString(), ReportID = _reportID.ToString(), FileName = "ReportFileName1" };

            //PrintTemplate
            _printTemplateID = Guid.NewGuid().ToString();
            _printTemplate1 = new PrintTemplate { UniqueID = _printTemplateID.ToString(), TemplateName = "TemplateName1", Type = 1 };
            string templateInfo = @"<?xml version='1.0' encoding='utf-8'?><Reports><Report version='4.6.20131.54624'>"
        + "<Name>Template</Name>"
        + "<ReportInfo>"
          + "<Author>10027008</Author>"
        + "</ReportInfo>"
        + "<DataSource>"
          + @"<ConnectionString>C:\Documents and Settings\Administrator\Local Settings\Apps\2.0\YW0P4GHB.B7T\6P43HV32.GD3\care..tion_324c4122a6cd491e_0002.0001_781aa2e02dbf3870\PrintTemplateFiles\SystemFiles\2052\Basic_Field.xml</ConnectionString>"
          + "<RecordSource>Fields</RecordSource>"
          + "<DataProvider>0</DataProvider>"
        + "</DataSource>"
        + "<Layout>"
          + "<Width>9120</Width>"
         + " <Orientation>1</Orientation>"
        + "</Layout>"
        + "<Font>"
        + "  <Name>Times New Roman</Name>"
        + "  <Size>9</Size>"
        + "</Font>"
        + "<CompatibilityOptions />"
        + "<Groups />"
        + "<Sections>"
        + "  <Section>"
        + "<Name>Detail</Name>"
        + "<Type>0</Type>"
        + "<Height>1200</Height>"
        + "</Section>"
        + "<Section>"
        + "<Name>Header</Name>"
        + "<Type>1</Type>"
        + "<Height>20</Height>"
        + "</Section>"
        + "<Section>"
        + "<Name>Footer</Name>"
        + "<Type>2</Type>"
        + "<Visible>0</Visible>"
        + "</Section>"
        + "<Section>"
        + "<Name>PageHeader</Name>"
        + "<Type>3</Type>"
        + "</Section>"
        + "<Section>"
        + "<Name>PageFooter</Name>"
        + "<Type>4</Type>"
        + "    <Height>20</Height>"
        + "  </Section>"
        + "</Sections>"
        + "<Fields>"
         + " <Field>"
        + "<Name>CityCtl1</Name>"
        + "<Section>0</Section>"
       + "<Text>tbRegPatient__LocalName</Text>"
        + "<Calculated>-1</Calculated>"
            + "<Left>1000</Left>"
            + "<Top>300</Top>"
            + "<Width>2280</Width>"
            + "<Height>300</Height>"
            + "<Align>6</Align>"
            + "<Font>"
            + "<Name>Arial</Name>"
             + " <Size>9</Size>"
           + " </Font>"
          + "</Field>"
          + "<Field>"
           + " <Name>CityLbl1</Name>"
           + " <Section>0</Section>"
           + " <Text>病人姓名</Text>"
            + "<Left>150</Left>"
           + " <Top>300</Top>"
           + " <Width>885</Width>"
           + " <Height>300</Height>"
           + " <Align>6</Align>"
            + "<ForeColor>128</ForeColor>"
           + " <WordWrap>0</WordWrap>"
           + " <Font>"
           + "   <Name>Arial</Name>"
           + "   <Size>9</Size>"
           + " </Font>"
          + "</Field>"
        + "</Fields>"
      + "</Report>"
        + "</Reports>";
            _printTemplate2 = new PrintTemplate { UniqueID = "dd5e991d-b595-4ed2-9958-126d1ea82b10", TemplateName = "baseinfo", Type = 0, Version = 0, TemplateInfo = System.Text.Encoding.Unicode.GetBytes(templateInfo) };

            //PrintTemplateFields
            _printTemplateFieldsID = Guid.NewGuid().ToString();
            _printTemplateFields1 = new PrintTemplateFields { UniqueID = _printTemplateFieldsID.ToString(), FieldName = "FieldName1" };

            // Report template
            _reportTemplateID = Guid.NewGuid().ToString();
            _reportTemplate = new ReportTemplate { UniqueID = _reportTemplateID, TemplateName = "templateName1" };
            _reportTemplateDirecID = Guid.NewGuid().ToString();
            _reportTemplateDirecParentID = Guid.NewGuid().ToString();
            _reportTemplateDirecUserID = Guid.NewGuid().ToString();
            _reportTemplateDirec = new ReportTemplateDirec { UniqueID = _reportTemplateDirecID, TemplateID = _reportTemplateID, ParentID = _reportTemplateDirecParentID, UserID = _reportTemplateDirecUserID, DirectoryType = "report", Type = 1, ItemOrder = 0 };

            //history
            _reportList = new ReportList { UniqueID = Guid.NewGuid().ToString(), ReportID = _reportID, OperationTime = DateTime.Now, Creater = _userID, Mender = _userID };


        }

        [TestInitialize]
        public void TestInitialize()
        {
            // init data
            _MockRisProContext.Reports.Add(_report1);
            _MockRisProContext.Reports.Add(_report2);
            _MockRisProContext.Reports.Add(_report3);
            _MockRisProContext.Procedures.Add(_procedure1);
            _MockRisProContext.Procedures.Add(_procedure2);
            _MockRisProContext.ReportFiles.Add(_reportFile1);
            _MockRisProContext.PrintTemplates.Add(_printTemplate1);
            _MockRisProContext.PrintTemplates.Add(_printTemplate2);
            _MockRisProContext.PrintTemplateFields.Add(_printTemplateFields1);
            _MockRisProContext.ReportTemplates.Add(_reportTemplate);
            _MockRisProContext.ReportTemplateDirecs.Add(_reportTemplateDirec);
            _MockRisProContext.ReportList.Add(_reportList);
            _MockRisProContext.Orders.Add(_order1);
            _MockRisProContext.Patients.Add(_patient1);
            _MockRisProContext.Users.Add(_user);
            _MockRisProContext.Sites.Add(_site);

        }

        [TestCleanup()]
        public void TestCleanup()
        {
            // clean data
            _MockRisProContext.ReportFiles.ToList().ForEach(p => _MockRisProContext.ReportFiles.Remove(p));
            _MockRisProContext.Reports.ToList().ForEach(p => _MockRisProContext.Reports.Remove(p));
            _MockRisProContext.Procedures.ToList().ForEach(p => _MockRisProContext.Procedures.Remove(p));
            _MockRisProContext.PrintTemplates.ToList().ForEach(p => _MockRisProContext.PrintTemplates.Remove(p));
            _MockRisProContext.PrintTemplateFields.ToList().ForEach(p => _MockRisProContext.PrintTemplateFields.Remove(p));
            _MockRisProContext.ReportList.ToList().ForEach(p => _MockRisProContext.ReportList.Remove(p));
            _MockRisProContext.ReportTemplates.ToList().ForEach(p => _MockRisProContext.ReportTemplates.Remove(p));
            _MockRisProContext.ReportTemplateDirecs.ToList().ForEach(p => _MockRisProContext.ReportTemplateDirecs.Remove(p));
            _MockRisProContext.ReportList.ToList().ForEach(p => _MockRisProContext.ReportList.Remove(p));
            _MockRisProContext.Orders.ToList().ForEach(p => _MockRisProContext.Orders.Remove(p));
            _MockRisProContext.Patients.ToList().ForEach(p => _MockRisProContext.Patients.Remove(p));
            _MockRisProContext.Syncs.ToList().ForEach(p => _MockRisProContext.Syncs.Remove(p));
            _MockRisProContext.Users.ToList().ForEach(p => _MockRisProContext.Users.Remove(p));
            _MockRisProContext.Sites.ToList().ForEach(p => _MockRisProContext.Sites.Remove(p));
        }

        [TestMethod]
        public void GetReportTest()
        {
            var report = _reportService.GetReport(_reportID);
            Assert.IsNotNull(report);
            Assert.AreEqual(_reportID, report.UniqueID);
            Assert.AreEqual("ReportName1", report.ReportName);
            Assert.AreEqual("ReportText1", report.ReportText);
        }

        [TestMethod]
        public void AddReportTest()
        {
            var reportID = Guid.NewGuid().ToString();
            var reportName = RandomHelper.GenerateString();
            var reportDto = new ReportDto
            {
                UniqueID = reportID,
                ReportName = reportName,
            };

            _reportService.AddReport(reportDto);
            var report = _reportService.GetReport(reportID);
            Assert.IsNotNull(report);
            Assert.AreEqual(reportID, report.UniqueID);
            Assert.AreEqual(reportName, report.ReportName);
        }

        [TestMethod]
        public void UpdateReportTest()
        {
            var reportID = Guid.NewGuid().ToString();
            var reportName = RandomHelper.GenerateString();
            var reportDto = new ReportDto
            {
                UniqueID = reportID,
                ReportName = reportName,
            };

            _reportService.AddReport(reportDto);
            var report = _reportService.GetReport(reportID);
            var updatedReportName = RandomHelper.GenerateString();
            var updatedReportText = RandomHelper.GenerateString();
            report.ReportName = updatedReportName;
            _reportService.UpdateReport(report);

            report = _reportService.GetReport(reportID);
            Assert.IsNotNull(report);
            Assert.AreEqual(reportID, report.UniqueID);
            Assert.AreEqual(updatedReportName, report.ReportName);
        }

        [TestMethod]
        public void CreateReportTest()
        {
            var reportID = Guid.NewGuid().ToString();
            var reportName = RandomHelper.GenerateString();
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var reportDto = new ReportDto
            {
                UniqueID = reportID,
                ReportName = reportName,
                Status = (int)RPStatus.Draft,
                Domain = domain,
                Creater = userID,
                CreaterName = userName,
            };
            reportDto.ProcedureIDs = new List<string>();
            reportDto.ProcedureIDs.Add(_procedureID2.ToString());
            _reportService.CreateReport(reportDto, userName, userID, domain, site);
            var report = _reportService.GetReport(reportID);
            Assert.IsNotNull(report);
            Assert.AreEqual(reportID, report.UniqueID);
            Assert.AreEqual((int)RPStatus.Draft, report.Status);
            Assert.AreEqual(domain, report.Domain);
            Assert.AreEqual(userID, report.Creater);
            Assert.AreEqual(userName, report.CreaterName);

            //procedure
            var procedure = _MockRisProContext.Procedures.Where(c => c.UniqueID == _procedureID2.ToString()).FirstOrDefault();
            Assert.IsNotNull(procedure);
            Assert.AreEqual((int)RPStatus.Draft, procedure.Status);
        }

        [TestMethod]
        public void ModifyReportTest()
        {
            var reportID = Guid.NewGuid().ToString();
            var reportName = RandomHelper.GenerateString();
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var reportDto = new ReportDto
            {
                UniqueID = reportID,
                ReportName = reportName,
                Status = (int)RPStatus.Draft
            };

            _reportService.AddReport(reportDto);

            reportDto = new ReportDto
            {
                UniqueID = reportID,
                ReportName = reportName,
                Status = (int)RPStatus.Submit
            };

            reportDto.ProcedureIDs = new List<string>();
            reportDto.ProcedureIDs.Add(_procedureID2.ToString());
            _reportService.ModifyReport(reportDto, userName, userID, domain, site);

            ReportDto report = _reportService.GetReport(reportID);
            Assert.IsNotNull(report);
            Assert.AreEqual(reportID, report.UniqueID);
            Assert.AreEqual((int)RPStatus.Submit, report.Status);
            Assert.AreEqual(domain, report.SubmitDomain);
            Assert.AreEqual(userID, report.Submitter);
            Assert.AreEqual(site, report.SubmitSite);
        }

        [TestMethod]
        public void DeleteReportTest()
        {
            var reportID = Guid.NewGuid().ToString();
            var reportName = RandomHelper.GenerateString();
            var reportText = RandomHelper.GenerateString();
            var reportDto = new ReportDto
            {
                UniqueID = reportID,
                ReportName = reportName,
                ReportText = reportText
            };

            _reportService.AddReport(reportDto);
            var report = _reportService.GetReport(reportID);
            Assert.IsNotNull(report);

            _reportService.DeleteReport(reportID);
            report = _reportService.GetReport(reportID);
            Assert.IsNull(report);
        }

        [TestMethod]
        public void DeleteReportObjTest()
        {
            var reportID = Guid.NewGuid().ToString();
            var reportName = RandomHelper.GenerateString();
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var reportDto = new ReportDto
            {
                UniqueID = reportID,
                ReportName = reportName,
                Status = (int)RPStatus.Draft,
                Domain = domain,
                Creater = userID,
                CreaterName = userName,
            };
            reportDto.ProcedureIDs = new List<string>();
            reportDto.ProcedureIDs.Add(_procedureID2.ToString());
            _reportService.CreateReport(reportDto, userName, userID, domain, site);
            _reportService.DeleteReport(reportDto);
            ReportDto report = _reportService.GetReport(reportID);
            Assert.IsNull(report);
        }

        [TestMethod]
        public void GetReportByProcedureIDTest()
        {
            var report = _reportService.GetReportByProcedureID(_procedureID);
            Assert.IsNotNull(report);
            Assert.AreEqual(_reportID, report.UniqueID);
            Assert.AreEqual("ReportName1", report.ReportName);
            Assert.AreEqual("ReportText1", report.ReportText);
        }

        [TestMethod]
        public void GetReportFilesByReportIDTest()
        {
            IEnumerable<ReportFileDto> reportFiles = _reportService.GetReportFilesByReportID(_reportID);
            Assert.IsNotNull(reportFiles);
            List<ReportFileDto> reportFileList = reportFiles.ToList();
            Assert.AreEqual(1, reportFileList.Count());

            Assert.AreEqual(_reportFileID, reportFileList[0].UniqueID);
            Assert.AreEqual(_reportID, reportFileList[0].ReportID);
            Assert.AreEqual("ReportFileName1", reportFileList[0].FileName);
        }

        [TestMethod]
        public void GetPrintTemplateTest()
        {
            var printTemplate = _reportPrintService.GetPrintTemplate(_printTemplateID);
            Assert.IsNotNull(printTemplate);
            Assert.AreEqual(_printTemplateID, printTemplate.UniqueID);
            Assert.AreEqual("TemplateName1", printTemplate.TemplateName);
        }

        [TestMethod]
        public void GetPrintTemplateByCriteriaTest()
        {
            PrintTemplateDto criteria = new PrintTemplateDto();
            criteria.Type = 1;
            var printTemplate = _reportPrintService.GetPrintTemplateByCriteria(criteria, "", "");
            Assert.IsNotNull(printTemplate);
            Assert.AreEqual(_printTemplateID, printTemplate[0].UniqueID);
            Assert.AreEqual("TemplateName1", printTemplate[0].TemplateName);
        }

        [TestMethod]
        public void GetPrintTemplateFieldsTest()
        {
            var printTemplateFields = _reportPrintService.GetPrintTemplateFields(_printTemplateFieldsID);
            Assert.IsNotNull(printTemplateFields);
            Assert.AreEqual(_printTemplateFieldsID, printTemplateFields.UniqueID);
            Assert.AreEqual("FieldName1", printTemplateFields.FieldName);
        }

        [TestMethod]
        public void GetReportTemplateDirecByIDTest()
        {
            var reportTemplateDirecDto = _reportTemplateService.GetReportTemplateDirecByID(_reportTemplateDirecID);
            Assert.IsNotNull(reportTemplateDirecDto);
            Assert.AreEqual(_reportTemplateDirecID, reportTemplateDirecDto.UniqueID);
            Assert.IsNotNull(reportTemplateDirecDto.ReportTemplateDto);
            Assert.AreEqual(_reportTemplateID, reportTemplateDirecDto.ReportTemplateDto.UniqueID);
        }

        [TestMethod]
        public void GetReportTemplateNodesTest()
        {
            var reportTemplateDirecDtoList = _reportTemplateService.GetReportTemplateNodes(_reportTemplateDirecParentID, _reportTemplateDirecUserID, "");

            Assert.AreEqual(1, reportTemplateDirecDtoList.Count());
            var reportTemplateDirecDto = reportTemplateDirecDtoList.ToList()[0];
            Assert.AreEqual(_reportTemplateDirecID, reportTemplateDirecDto.UniqueID);
        }

        [TestMethod]
        public void GetReportListByReportIDTest()
        {
            var reportList = _reportService.GetReportListByReportID(_reportID);

            Assert.AreEqual(1, reportList.Count());
        }

        [TestMethod]
        public void GetProcedureByReportIDTest()
        {
            IEnumerable<ProcedureDto> procedureList = _reportService.GetProcedureByReportID(_reportID);

            Assert.AreEqual(1, procedureList.Count());
            var procedureDto = procedureList.ToList()[0];
            Assert.AreEqual(_reportID, procedureDto.ReportID);
        }

        [TestMethod]
        public void GetProcedureByOrderIDTest()
        {
            IEnumerable<ProcedureDto> procedureList = _reportService.GetProcedureByOrderID(_orderID);

            Assert.AreEqual(1, procedureList.Count());
            var procedureDto = procedureList.ToList()[0];
            Assert.AreEqual(_orderID, procedureDto.OrderID);
        }

        [TestMethod]
        public void AddLockByProcedureIDTest()
        {
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var ip = RandomHelper.GenerateString();
            var result = _reportLockService.AddLockByProcedureID(_procedureID, userName, userID, domain, site, ip);

            Assert.AreEqual(true, result);

            SyncDto syncDto = _reportLockService.GetLock(_orderID, LockType.Register);
            Assert.IsNotNull(syncDto);
        }

        [TestMethod]
        public void AddLockByOrderIDTest()
        {
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var ip = RandomHelper.GenerateString();
            var result = _reportLockService.AddLockByOrderID(_orderID, userName, userID, domain, site, ip);

            Assert.AreEqual(true, result);

            SyncDto syncDto = _reportLockService.GetLock(_orderID, LockType.Register);
            Assert.IsNotNull(syncDto);
        }

        [TestMethod]
        public void AddLockByReportIDTest()
        {
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var ip = RandomHelper.GenerateString();
            var result = _reportLockService.AddLockByReportID(_reportID, userName, userID, domain, site, ip);

            Assert.AreEqual(true, result);

            SyncDto syncDto = _reportLockService.GetLock(_orderID, LockType.Register);
            Assert.IsNotNull(syncDto);
        }

        [TestMethod]
        public void GetLockTest()
        {
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var ip = RandomHelper.GenerateString();
            var result = _reportLockService.AddLockByReportID(_reportID, userName, userID, domain, site, ip);

            Assert.AreEqual(true, result);

            SyncDto syncDto = _reportLockService.GetLock(_orderID, LockType.Register);
            Assert.IsNotNull(syncDto);
            Assert.AreEqual(true, syncDto.ProcedureIDs.Contains(_procedureID));
        }

        [TestMethod]
        public void DeleteLock1Test()
        {
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var ip = RandomHelper.GenerateString();
            var result = _reportLockService.AddLockByReportID(_reportID, userName, userID, domain, site, ip);

            Assert.AreEqual(true, result);

            _reportLockService.DeleteLock(_orderID, LockType.Register, userID);
            SyncDto syncDto = _reportLockService.GetLock(_orderID, LockType.Register);
            Assert.IsNull(syncDto);
        }

        [TestMethod]
        public void DeleteLock2Test()
        {
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var ip = RandomHelper.GenerateString();
            var result = _reportLockService.AddLockByReportID(_reportID, userName, userID, domain, site, ip);

            Assert.AreEqual(true, result);
            List<string> procedureIDs = new List<string>();
            procedureIDs.Add(_procedureID);
            _reportLockService.DeleteLock(_orderID, procedureIDs, LockType.Register, userID);
            SyncDto syncDto = _reportLockService.GetLock(_orderID, LockType.Register);
            Assert.IsNull(syncDto);
        }

        [TestMethod]
        public void DeleteLockByReportIDTest()
        {
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var ip = RandomHelper.GenerateString();
            var result = _reportLockService.AddLockByReportID(_reportID, userName, userID, domain, site, ip);

            Assert.AreEqual(true, result);
            _reportLockService.DeleteLockByReportID(_reportID, LockType.Register, userID);
            SyncDto syncDto = _reportLockService.GetLock(_orderID, LockType.Register);
            Assert.IsNull(syncDto);
        }

        [TestMethod]
        public void DeleteLockByUserIDTest()
        {
            var userName = RandomHelper.GenerateString();
            var userID = RandomHelper.GenerateString();
            var domain = RandomHelper.GenerateString();
            var site = RandomHelper.GenerateString();
            var ip = RandomHelper.GenerateString();
            var result = _reportLockService.AddLockByReportID(_reportID, userName, userID, domain, site, ip);

            Assert.AreEqual(true, result);
            _reportLockService.DeleteLockByUserID(LockType.Register, userID);
            SyncDto syncDto = _reportLockService.GetLock(_orderID, LockType.Register);
            Assert.IsNull(syncDto);
        }

        [TestMethod]
        public void GetBaseInfoDescByProcedureTest()
        {
            var procedureDto = new ProcedureDto
            {
                UniqueID = _procedure1.UniqueID,
                ReportID = _procedure1.ReportID,
                OrderID = _procedure1.OrderID
            };
            var result = _reportService.GetBaseInfoDescByProcedure(procedureDto);

            Assert.AreEqual(false, string.IsNullOrEmpty(result));
            Assert.AreEqual(true, result.Contains("LocalName1"));
        }

        [TestMethod]
        public void GetBaseInfoHtmlByProcedureTest()
        {
            var procedureDto = new ProcedureDto
            {
                UniqueID = _procedure1.UniqueID,
                ReportID = _procedure1.ReportID,
                OrderID = _procedure1.OrderID
            };
            var result = _reportPrintService.GetBaseInfoHtmlByProcedure(procedureDto, "", "");

            Assert.AreEqual(false, string.IsNullOrEmpty(result));
            Assert.AreEqual(true, result.Contains("LocalName1"));
        }

        [TestMethod]
        public void UpdateCommentsTest()
        {
            var reportID = Guid.NewGuid().ToString();
            var reportName = RandomHelper.GenerateString();
            var reportDto = new ReportDto
            {
                UniqueID = reportID,
                ReportName = reportName,
            };

            _reportService.AddReport(reportDto);
            var report = _reportService.GetReport(reportID);
            var comments = RandomHelper.GenerateString();

            report.Comments = comments;
            _reportService.UpdateComments(report);

            report = _reportService.GetReport(reportID);
            Assert.IsNotNull(report);
            Assert.AreEqual(reportID, report.UniqueID);
            Assert.AreEqual(comments, report.Comments);

        }

        [TestMethod]
        public void GetPacsUrlTest()
        {
            string pacsUrl = _reportService.GetPacsUrl(_procedureID, _userID);
            Assert.AreNotEqual(0, pacsUrl.Length);
        }

        [TestMethod]
        public void CreateReportTemplateTest()
        {
            var templateName = RandomHelper.GenerateString();
            var modalityType = RandomHelper.GenerateString();
            var bodyPart = RandomHelper.GenerateString();
            var gender = RandomHelper.GenerateString();
            var wygText = RandomHelper.GenerateString();
            var wysText = RandomHelper.GenerateString();
            ReportTemplateDto newReportTemplateDto = new ReportTemplateDto
            {
                TemplateName = templateName,
                ModalityType = modalityType,
                BodyPart = bodyPart,
                Gender = gender,
                WYGText = wygText,
                WYSText = wysText
            };
            ReportTemplateDto reportTemplateDto = _reportTemplateService.CreateReportTemplate(newReportTemplateDto, _userID);
            Assert.AreNotEqual(null, reportTemplateDto);
        }

        [TestMethod]
        public void UpdateReportTemplateTest()
        {
            var templateName = RandomHelper.GenerateString();
            var modalityType = RandomHelper.GenerateString();
            var bodyPart = RandomHelper.GenerateString();
            var gender = RandomHelper.GenerateString();
            var wygText = RandomHelper.GenerateString();
            var wysText = RandomHelper.GenerateString();
            ReportTemplateDto newReportTemplateDto = new ReportTemplateDto
            {
                TemplateName = templateName,
                ModalityType = modalityType,
                BodyPart = bodyPart,
                Gender = gender,
                WYGText = wygText,
                WYSText = wysText
            };
            ReportTemplateDto reportTemplateDto = _reportTemplateService.CreateReportTemplate(newReportTemplateDto, _userID);
            Assert.AreNotEqual(null, reportTemplateDto);

            var newTemplateName = RandomHelper.GenerateString();
            reportTemplateDto.TemplateName = newTemplateName;
            ReportTemplateDto updateReportTemplateDto = _reportTemplateService.UpdateReportTemplate(reportTemplateDto, _userID);
            Assert.AreEqual(newTemplateName, updateReportTemplateDto.TemplateName);

        }

        [TestMethod]
        public void DeleteTemplateByIDTest()
        {
            var templateName = RandomHelper.GenerateString();
            var modalityType = RandomHelper.GenerateString();
            var bodyPart = RandomHelper.GenerateString();
            var gender = RandomHelper.GenerateString();
            var wygText = RandomHelper.GenerateString();
            var wysText = RandomHelper.GenerateString();
            ReportTemplateDto newReportTemplateDto = new ReportTemplateDto
            {
                TemplateName = templateName,
                ModalityType = modalityType,
                BodyPart = bodyPart,
                Gender = gender,
                WYGText = wygText,
                WYSText = wysText
            };
            ReportTemplateDto reportTemplateDto = _reportTemplateService.CreateReportTemplate(newReportTemplateDto, _userID);
            Assert.AreNotEqual(null, reportTemplateDto);

            bool result = _reportTemplateService.DeleteTemplateByID(reportTemplateDto.UniqueID);
            Assert.AreEqual(true, result);

        }
    }
}
