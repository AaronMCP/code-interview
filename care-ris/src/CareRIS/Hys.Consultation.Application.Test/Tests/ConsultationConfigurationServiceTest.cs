using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Services;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.EntityFramework;
using Hys.CrossCutting.Common.Utils;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hys.Consultation.Domain.Enums;

namespace Hys.Consultation.Application.Test.Tests
{

    [TestClass]
    public class ConsultationConfigurationServiceTest : TestBase
    {
        private readonly IConsultationConfigurationService _service = Container.Resolve<IConsultationConfigurationService>();
        private readonly IConsultationContext _consultationContext = Container.Resolve<IConsultationContext>();
        /*
         IEnumerable<ConsultationDictionaryDto> GetDictionaryByType(int type);
        IEnumerable<ConsultationDictionaryDtos> GetDictionaryByTypes(int[] types);
        IEnumerable<ExamModuleDto> GetUserExamModule();
        bool UpdateExamModule(ExamModuleDto module);
        HospitalProfileDto GetHospital();
        DAMInfoDto GetDam();
        DAMInfoDto GetDamByID(string id);
        IEnumerable<RoleDto> GetRolesAsync();
        bool SaveRole(RoleDto role);
        IEnumerable<DepartmentDto> GetDepartments();
        List<ServiceTypeDto> GetServiceType();
        IEnumerable<HospitalDefaultDto> GetRecipientConfigs();
        bool SaveRecipientConfigs(HospitalDefaultDto hospitalDefaultDto);
        List<HospitalDefaultDto> GetHospitalDefaultForHospital();
        List<HospitalDefaultDto> GetHospitalDefaultForExpert();
        bool ValidateRoleName(string roleID, string roleName);
        List<DAMInfoDto> GetDams();
        List<MeetingRoomDto> GetMeetingRoomList();
        IEnumerable<HospitalDefaultDto> GetRecipientConfigsForReceiver();
        */

        [TestMethod]
        public void GetRolesTest()
        {
            var task = _service.GetRolesAsync();
            Task.WaitAll(task);
            var roles = task.Result.ToList();
            Assert.AreEqual(5, roles.Count); // system defined 5 default role.
            Assert.IsTrue(roles.All(r => r.IsSystem));
        }

        [TestMethod]
        public async Task GetRolesAsyncTest()
        {
            var roles = (await _service.GetRolesAsync()).ToList();
            Assert.AreEqual(5, roles.Count); // system defined 5 default role.
            Assert.IsTrue(roles.All(r => r.IsSystem));
        }

        [TestMethod]
        public void GetDictionaryByTypeTest()
        {
            var dic = MockData.Generate<ConsultationDictionary>(3, d => { d.Type = DictionaryType.ConsultationStauts; }).ToList();
            _consultationContext.Set<ConsultationDictionary>().AddRange(dic);
            _consultationContext.SaveChanges();
            var result = _service.GetDictionaryByType(1, "");
            Assert.IsTrue(dic.All(d => result.Any(i => i.DictionaryID == d.DictionaryID)));
        }

        [TestMethod]
        public void GetDictionaryByTypesTest()
        {
            var dic = MockData.Generate<ConsultationDictionary>(3, d => { d.Type = DictionaryType.ConsultationStauts; })
                .Concat(MockData.Generate<ConsultationDictionary>(3, d => { d.Type = DictionaryType.TimeRange; })).ToList();
            _consultationContext.Set<ConsultationDictionary>().AddRange(dic);
            _consultationContext.SaveChanges();
            var result = _service.GetDictionaryByTypes(new[] { 1, 2 },"").ToList();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any(r => r.Type == 1));
            Assert.IsTrue(result.Any(r => r.Type == 2));
        }


        [TestMethod]
        public void UpdateExamModuleTest()
        {
            var data = MockData.Generate<ExamModule>(m => m.Owner = "");
            _consultationContext.Set<ExamModule>().Add(data);
            _consultationContext.SaveChanges();

            var result = _service.GetUserExamModule("","").ToList();
            Assert.IsTrue(result.All(r => r.Owner == ""));
            var existingModule = result.FirstOrDefault(r => r.ID == data.ID);
            Assert.IsNotNull(existingModule);
            existingModule.Title = MockData.RandomString();
            Assert.IsTrue(_service.UpdateExamModule(existingModule,""));
            var updatedModule = _service.GetUserExamModule("","").FirstOrDefault(r => r.ID == data.ID);
            Assert.IsNotNull(updatedModule);
            Assert.AreEqual(existingModule.Title, updatedModule.Title);
        }

        [TestMethod]
        public void GetHospitalTest()
        {
            var dam = MockData.Generate<DAMInfo>();

            var hospital = MockData.Generate<HospitalProfile>(h =>
            {
                h.Dam1ID = dam.UniqueID;
            });
            var department = MockData.Generate<Department>();

            var existingUser = _consultationContext.Set<UserExtention>().FirstOrDefault(u => u.UniqueID == "");
            if (existingUser != null)
            {
                _consultationContext.Set<UserExtention>().Remove(existingUser);

                var user = MockData.Generate<UserExtention>(t =>
                {
                    t.UniqueID = "";
                    t.HospitalID = hospital.UniqueID;
                    t.DepartmentID = department.UniqueID;
                });
                _consultationContext.Set<UserExtention>().Add(user);
            }

            _consultationContext.Set<DAMInfo>().Add(dam);
            _consultationContext.Set<Department>().Add(department);
            _consultationContext.Set<HospitalProfile>().Add(hospital);
            _consultationContext.SaveChanges();

            var result = _service.GetHospital("");
            Assert.IsNotNull(result);
            Assert.AreEqual(hospital.HospitalName, result.HospitalName);
        }

        [TestMethod]
        public void GetDamsTest()
        {
            var dams = MockData.Generate<DAMInfo>(3).ToList();
            _consultationContext.Set<DAMInfo>().AddRange(dams);
            _consultationContext.SaveChanges();

            var result = _service.GetDams().ToList();
            Assert.IsNotNull(result);
            Assert.IsTrue(dams.All(d => result.Any(r => r.UniqueID == d.UniqueID)));
        }

        [TestMethod]
        public void GetDamTest()
        {
            var dam = MockData.Generate<DAMInfo>();
            _consultationContext.Set<DAMInfo>().Add(dam);
            _consultationContext.SaveChanges();
            var result = _service.GetDam();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDamByIDTest()
        {
            var dam = MockData.Generate<DAMInfo>();
            _consultationContext.Set<DAMInfo>().Add(dam);
            _consultationContext.SaveChanges();
            var result = _service.GetDamByID(dam.UniqueID);
            Assert.IsNotNull(result);
        }

        public void SaveRoleTest()
        {
            throw new System.NotImplementedException();
        }

        public void GetDepartmentsTest()
        {
            throw new System.NotImplementedException();
        }

        public void GetServiceTypeTest()
        {
            throw new System.NotImplementedException();
        }

        public void GetRecipientConfigsTest()
        {
            throw new System.NotImplementedException();
        }

        public bool SaveRecipientConfigsTest()
        {
            throw new System.NotImplementedException();
        }

        public void GetHospitalDefaultForHospitalTest()
        {
            throw new System.NotImplementedException();
        }

        public void GetHospitalDefaultForExpertTest()
        {
            throw new System.NotImplementedException();
        }

        public void ValidateRoleNameTest()
        {
            throw new System.NotImplementedException();
        }

        public void GetMeetingRoomListTest()
        {
            throw new System.NotImplementedException();
        }

        public void GetRecipientConfigsForReceiverTest()
        {
            throw new System.NotImplementedException();
        }

        public void GeneratePatientNoTest()
        {
            throw new System.NotImplementedException();
        }
    }
}