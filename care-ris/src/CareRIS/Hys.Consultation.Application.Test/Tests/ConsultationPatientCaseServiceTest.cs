using System;
using System.Collections.Generic;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Dtos.PatientCase;
using Hys.Consultation.Application.Services;
using Hys.Consultation.Application.Test.Utils;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hys.Consultation.Application.Test.Tests
{
    [TestClass]
    public class ConsultationPatientCaseServiceTest : TestBase
    {
        private static readonly IConsultationPatientCaseService ConsultationPatientCaseService = Container.Resolve<IConsultationPatientCaseService>();

        [TestInitialize]
        public void TestInitialize()
        {
            // ConsultationContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [tPatientCase]");
        }

        [TestMethod]
        public void CreatePatientCaseTest()
        {
            var patientCaseInfoDto = MockData.Generate<PatientCaseInfoDto>(c => c.Gender = "0");
            var patientCaseInfoDtoNew = ConsultationPatientCaseService.CreatePatientCase(patientCaseInfoDto, null,"","");
            Assert.IsNotNull(patientCaseInfoDtoNew);
        }

        [TestMethod]
        public void GetCombinePatientCaseListTest()
        {
            var identityCard = RandomHelper.GenerateString();
            var patientCaseInfoDto = new PatientCaseInfoDto
            {
                IdentityCard = identityCard,
                PatientName = RandomHelper.GenerateString(),
                InsuranceNumber = RandomHelper.GenerateString(),
                PatientNo = RandomHelper.GenerateString(),
                Age = RandomHelper.GenerateString(),
                Gender = "0",
                Birthday = DateTime.Now,
                CreateTime = DateTime.Now,
                LastEditTime = DateTime.Now
            };

            ConsultationPatientCaseService.CreatePatientCase(patientCaseInfoDto, null,"","");

            patientCaseInfoDto = new PatientCaseInfoDto
            {
                IdentityCard = identityCard,
                PatientName = RandomHelper.GenerateString(),
                InsuranceNumber = RandomHelper.GenerateString(),
                PatientNo = RandomHelper.GenerateString(),
                Age = RandomHelper.GenerateString(),
                Gender = "0",
                Birthday = DateTime.Now,
                CreateTime = DateTime.Now,
                LastEditTime = DateTime.Now
            };

            var patientCaseInfoDtoNew = ConsultationPatientCaseService.CreatePatientCase(patientCaseInfoDto, null,"","");

            List<PatientCaseInfoDto> list = ConsultationPatientCaseService.GetCombinePatientCaseList(patientCaseInfoDtoNew.UniqueID, identityCard);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void CombinePatientCaseTest()
        {
            var patientCaseInfoDto1 = MockData.Generate<PatientCaseInfoDto>(c => c.Gender = "0");
            ConsultationPatientCaseService.CreatePatientCase(patientCaseInfoDto1, null,"","");
            var patientCaseInfoDto2 = MockData.Generate<PatientCaseInfoDto>(c => c.Gender = "0");
            ConsultationPatientCaseService.CreatePatientCase(patientCaseInfoDto2, null,"","");

            var patientCaseCombineDto = new PatientCaseCombineDto
            {
                PatientCombineNo = new List<string> { patientCaseInfoDto1.PatientNo, patientCaseInfoDto2.PatientNo },
                PatientNo = patientCaseInfoDto2.PatientNo
            };
            var result = ConsultationPatientCaseService.CombinePatientCase(patientCaseCombineDto);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GetPatientCaseNoItemsTest()
        {
            var patientCaseInfoDto = MockData.Generate<PatientCaseInfoDto>(c => c.Gender = "0");
            PatientCaseInfoDto patientCaseInfoDtoNew = ConsultationPatientCaseService.CreatePatientCase(patientCaseInfoDto, null,"","");
            PatientCaseInfoDto result = ConsultationPatientCaseService.GetPatientCaseNoItems(patientCaseInfoDtoNew.UniqueID);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditPatientCaseTest()
        {
            string identityCard = RandomHelper.GenerateString();
            string patientNo1 = RandomHelper.GenerateString();
            string patientNo2 = RandomHelper.GenerateString();
            PatientCaseInfoDto patientCaseInfoDto = new PatientCaseInfoDto
            {
                IdentityCard = identityCard,
                PatientName = RandomHelper.GenerateString(),
                InsuranceNumber = RandomHelper.GenerateString(),
                PatientNo = patientNo1,
                Age = RandomHelper.GenerateString(),
                Gender = "0",
                Birthday = DateTime.Now,
                CreateTime = DateTime.Now,
                LastEditTime = DateTime.Now
            };

            PatientCaseInfoDto patientCaseInfoDtoNew = ConsultationPatientCaseService.CreatePatientCase(patientCaseInfoDto, null,"","");

            PatientCaseEditInfoDto editPatientCase = new PatientCaseEditInfoDto
            {
                UniqueID = patientCaseInfoDtoNew.UniqueID,
                IdentityCard = patientCaseInfoDtoNew.IdentityCard,
                PatientName = patientCaseInfoDtoNew.PatientName,
                InsuranceNumber = patientCaseInfoDtoNew.InsuranceNumber,
                PatientNo = patientCaseInfoDtoNew.PatientNo,
                Age = patientCaseInfoDtoNew.Age,
                Gender = patientCaseInfoDtoNew.Gender,
                Birthday = patientCaseInfoDtoNew.Birthday,
                CreateTime = patientCaseInfoDtoNew.CreateTime
            };
            bool result = ConsultationPatientCaseService.EditPatientCase(editPatientCase,"");
            Assert.AreEqual(true, result);
        }
    }
}
