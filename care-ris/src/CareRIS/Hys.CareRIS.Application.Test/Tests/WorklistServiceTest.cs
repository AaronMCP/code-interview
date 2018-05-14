using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hys.CareRIS.Application.Services.ServiceImpl;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Application.Dtos;
using System.Collections.Generic;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.EntityFramework.Repositories;
using Hys.CareRIS.Applicatiton.Test.Mock;
using Hys.CareRIS.Application.Mappers;
using Hys.CareRIS.Application.Test.Utils;
using System.Data;
using Hys.CareRIS.Application.Services;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Test.Tests
{
    [TestClass]
    public class WorklistServiceTest : TestBase
    {
        private static WorklistService _worklistService;
        private static IShortcutRepository _shortcutRepository;
        private static IConfigurationService _configurationService;

        private static string _patientId1;
        private static Patient _patient1;
        private static string _orderId1;
        private static Order _order1;
        private static string _procedureId1;
        private static Procedure _procedure1;
        private static string _orderId2;
        private static Order _order2;
        private static string _procedureId2;
        private static Procedure _procedure2;

        private static string _userId;
        private static string _shortcutId1;
        private static Shortcut _shortcut1;
        private static string _shortcutId2;
        private static Shortcut _shortcut2;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _shortcutRepository = new MockShortcutRepository(_MockRisProContext);
            _worklistService = new WorklistService(_MockRisProContext, _shortcutRepository, _configurationService);

            _patientId1 = Guid.NewGuid().ToString();
            _patient1 = new Patient { UniqueID = _patientId1.ToString(), LocalName = "LocalName1", EnglishName = "EnglishName1", PatientNo = "001" };
            _orderId1 = Guid.NewGuid().ToString();
            _order1 = new Order { UniqueID = _orderId1, AccNo = "000001", PatientID = _patientId1.ToString(), PatientType = "住院病人", CreateTime = DateTime.Now.AddDays(-1), CurrentSite = "rjwest" };
            _procedureId1 = Guid.NewGuid().ToString();
            _procedure1 = new Procedure { UniqueID = _procedureId1, OrderID = _orderId1, ModalityType = "CR", Modality = "CR1", Status = 20, ExamineTime = DateTime.Now.AddDays(-1) };
            _orderId2 = Guid.NewGuid().ToString();
            _order2 = new Order { UniqueID = _orderId2, AccNo = "000002", PatientID = _patientId1.ToString(), PatientType = "急诊病人", CreateTime = DateTime.Now, CurrentSite = "rjwest" };
            _procedureId2 = Guid.NewGuid().ToString();
            _procedure2 = new Procedure { UniqueID = _procedureId2, OrderID = _orderId2, ModalityType = "CT", Modality = "CT1", Status = 50, ExamineTime = DateTime.Now };

            _MockRisProContext.Patients.Add(_patient1);
            _MockRisProContext.Orders.Add(_order1);
            _MockRisProContext.Orders.Add(_order2);
            _MockRisProContext.Procedures.Add(_procedure1);
            _MockRisProContext.Procedures.Add(_procedure2);
        }

        [ClassCleanup]
        public static void ClasscleanUp()
        {
            _MockRisProContext.Patients.ToList().ForEach(p => _MockRisProContext.Patients.Remove(p));
            _MockRisProContext.Orders.ToList().ForEach(p => _MockRisProContext.Orders.Remove(p));
            _MockRisProContext.Procedures.ToList().ForEach(p => _MockRisProContext.Procedures.Remove(p));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _userId = Guid.NewGuid().ToString();
            _shortcutId1 = Guid.NewGuid().ToString();
            _shortcut1 = new Shortcut
            {
                UniqueID = _shortcutId1,
                Category = WorklistService.Category,
                Name = RandomHelper.GenerateString(),
                Owner = _userId,
                Type = 0
            };
            _shortcutId2 = Guid.NewGuid().ToString();
            _shortcut2 = new Shortcut
            {
                UniqueID = _shortcutId2,
                Category = WorklistService.Category,
                Name = RandomHelper.GenerateString(),
                Owner = _userId,
                Type = 1
            };
            _MockRisProContext.Shortcuts.Add(_shortcut1);
            _MockRisProContext.Shortcuts.Add(_shortcut2);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            _MockRisProContext.Shortcuts.ToList().ForEach(s => _MockRisProContext.Shortcuts.Remove(s));
        }

        [TestMethod]
        public async Task AdvancedSearchTest(string userID, string site, string role)
        {
            var criteria = new WorklistSearchCriteriaDto
            {
                PatientName = "*001*",
                Pagination = new PaginationDto { PageIndex = 1, PageSize = 5 }
            };
            var result = await _worklistService.SearchWorklist(criteria,userID, site, role);
            Assert.AreEqual(0, result.OrderItems.Count());

            criteria.PatientName = "*LocalName1*";
            result = await _worklistService.SearchWorklist(criteria, userID, site, role);
            Assert.AreEqual(2, result.OrderItems.Count());

            criteria.AccNo = "*00000*";
            result = await _worklistService.SearchWorklist(criteria, userID, site, role);
            Assert.AreEqual(2, result.OrderItems.Count());

            criteria.AccNo = "*000001*";
            result = await _worklistService.SearchWorklist(criteria, userID, site, role);
            Assert.AreEqual(1, result.OrderItems.Count());

            criteria.AccNo = "*000002*";
            result = await _worklistService.SearchWorklist(criteria, userID, site, role);
            Assert.AreEqual(1, result.OrderItems.Count());

            criteria.PatientTypes = new List<string>() { "住院病人" };
            result = await _worklistService.SearchWorklist(criteria, userID, site, role);
            Assert.AreEqual(0, result.OrderItems.Count());

            criteria.PatientTypes.Add("急诊病人");
            result = await _worklistService.SearchWorklist(criteria, userID, site, role);
            Assert.AreEqual(1, result.OrderItems.Count());
        }

        [TestMethod]
        public void GetSearchCriteriaShortcutTest()
        {
            var result = _worklistService.GetSearchCriteriaShortcuts(_userId);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(s => s.IsDefault));
            Assert.IsTrue(result.Any(s => !s.IsDefault));
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void AddSearchCriteriaShortcutTest()
        {
            var shortcutId = Guid.NewGuid().ToString();
            var shortcutName = RandomHelper.GenerateString();
            // add shortcut for this user
            var shortcutDto = new SearchCriteriaShortcutDto
            {
                UniqueID = shortcutId,
                Owner = _userId,
                Name = shortcutName,
                IsDefault = false,
                criteria = new WorklistSearchCriteriaDto()
            };
            _worklistService.AddSearchCriteriaShortcut(shortcutDto);
            // this user's shortcut count should be 3, originally is 2
            var result = _worklistService.GetSearchCriteriaShortcuts(_userId);
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(s => s.UniqueID == shortcutId));

            // add shortcut for another user
            var userId = Guid.NewGuid().ToString();
            shortcutDto = new SearchCriteriaShortcutDto
            {
                UniqueID = shortcutId,
                Owner = userId,
                Name = shortcutName,
                IsDefault = false,
                criteria = new WorklistSearchCriteriaDto()
            };
            _worklistService.AddSearchCriteriaShortcut(shortcutDto);
            // this user's shortcut count is still 3 
            result = _worklistService.GetSearchCriteriaShortcuts(_userId);
            Assert.AreEqual(3, result.Count());
            // another user's shortcut count is 1
            result = _worklistService.GetSearchCriteriaShortcuts(userId);
            Assert.AreEqual(1, result.Count());

            // add duplicated shorcut name for this user
            shortcutDto = new SearchCriteriaShortcutDto
            {
                UniqueID = shortcutId,
                Owner = _userId,
                Name = shortcutName,
                IsDefault = false,
                criteria = new WorklistSearchCriteriaDto()
            };
            // except DuplicatedException
            _worklistService.AddSearchCriteriaShortcut(shortcutDto);
        }

        [TestMethod]
        public void DeleteSearchCriteriaShortcutTest()
        {
            _worklistService.DeleteSearchCriteriaShortcut(_shortcutId1);
            var result = _worklistService.GetSearchCriteriaShortcuts(_userId);
            // originally 2 shortcuts, now only exists 1 default record
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Any(s => s.IsDefault));
            Assert.IsFalse(result.Any(s => !s.IsDefault));
        }

        [TestMethod]
        public void SetDetaultSearchCriteriaShortcutTest()
        {
            var shortcuts1 = _worklistService.GetSearchCriteriaShortcut(_shortcutId1);
            var shortcuts2 = _worklistService.GetSearchCriteriaShortcut(_shortcutId2);
            Assert.IsFalse(shortcuts1.IsDefault);
            Assert.IsTrue(shortcuts2.IsDefault);

            _worklistService.SetDetaultSearchCriteriaShortcut(_shortcutId1);
            shortcuts1 = _worklistService.GetSearchCriteriaShortcut(_shortcutId1);
            shortcuts2 = _worklistService.GetSearchCriteriaShortcut(_shortcutId2);
            Assert.IsTrue(shortcuts1.IsDefault);
            Assert.IsFalse(shortcuts2.IsDefault);
        }
    }
}
