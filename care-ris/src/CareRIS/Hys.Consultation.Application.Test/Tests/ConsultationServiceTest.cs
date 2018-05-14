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
    public class ConsultationServiceTest : TestBase
    {
        private static readonly IConsultationService ConsultationService = Container.Resolve<IConsultationService>();
        private static readonly IConsultationPatientCaseService ConsultationPatientCaseService = Container.Resolve<IConsultationPatientCaseService>();

        [TestMethod]
        public void CreateRequestTest()
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

            NewConsultationRequestDto newConsultationRequestDto = new NewConsultationRequestDto
            {
                ConsultationType = "0",
                ExpectedDate = DateTime.Now,
                ExpectedTimeRange = "0",
                PatientCaseID = patientCaseInfoDtoNew.UniqueID,
                RequestPurpose = RandomHelper.GenerateString(),
                RequestRequirement = RandomHelper.GenerateString(),
                SelectHospital = "1"
            };

            string result = ConsultationService.CreateRequest(newConsultationRequestDto,"","");
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void AcceptRequestTest()
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

            NewConsultationRequestDto newConsultationRequestDto = new NewConsultationRequestDto
            {
                ConsultationType = "0",
                ExpectedDate = DateTime.Now,
                ExpectedTimeRange = "0",
                PatientCaseID = patientCaseInfoDtoNew.UniqueID,
                RequestPurpose = RandomHelper.GenerateString(),
                RequestRequirement = RandomHelper.GenerateString(),
                SelectHospital = "1"
            };

            string result = ConsultationService.CreateRequest(newConsultationRequestDto,"","");

            RequestAcceptInfoDto requestAcceptInfoDto = new RequestAcceptInfoDto
            {
                ConsultationDate = DateTime.Now,
                consultationStartTime = "1",
                RequestID = result,
                ExpertList = new List<string> { "test" }
            };

            bool result2 = ConsultationService.AcceptRequest(requestAcceptInfoDto,"");
            Assert.AreEqual(true, result2);
        }

        [TestMethod]
        public void CompleteRequestTest()
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

            NewConsultationRequestDto newConsultationRequestDto = new NewConsultationRequestDto
            {
                ConsultationType = "0",
                ExpectedDate = DateTime.Now,
                ExpectedTimeRange = "0",
                PatientCaseID = patientCaseInfoDtoNew.UniqueID,
                RequestPurpose = RandomHelper.GenerateString(),
                RequestRequirement = RandomHelper.GenerateString(),
                SelectHospital = "1"
            };

            string result = ConsultationService.CreateRequest(newConsultationRequestDto,"","");

            RequestAcceptInfoDto requestAcceptInfoDto = new RequestAcceptInfoDto
            {
                ConsultationDate = DateTime.Now,
                consultationStartTime = "1",
                RequestID = result,
                ExpertList = new List<string> { "test" }
            };

            bool result2 = ConsultationService.AcceptRequest(requestAcceptInfoDto,"");

            bool result3 = ConsultationService.CompleteRequest(result);
            Assert.AreEqual(true, result3);
        }
    }
}
