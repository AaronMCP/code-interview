using System;
using System.Collections.Generic;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Application.Services;
using Hys.CareRIS.Application.Services.ServiceImpl;
using Hys.CareRIS.Application.Test.Utils;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.EntityFramework;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hys.CareRIS.Application.Test.Tests
{
    [TestClass]
    public class UserManagementTest : TestBase
    {
        private static readonly IUserManagementService UserManagementService = Container.Resolve<IUserManagementService>();

        private static OnlineClient _onlineClient;

        [TestInitialize]
        public void TestInitialize()
        {
            _onlineClient = MockData.Generate<OnlineClient>(c =>
            {
                c.IsOnline = 1;
                c.Comments = "web login user";
            });
            var risProContext = Container.Resolve<IRisProContext>();
            risProContext.Set<OnlineClient>().RemoveRange(risProContext.Set<OnlineClient>());
            risProContext.Set<OnlineClient>().Add(_onlineClient);
            risProContext.SaveChanges();
        }

        [TestMethod]
        public void LoginToOnlineTest()
        {
            OnlineClientDto onlineClientDto = new OnlineClientDto();
            onlineClientDto.UniqueID = RandomHelper.GenerateString();
            onlineClientDto.MachineIP = _onlineClient.MachineIP;
            string message = "";
            int result = UserManagementService.LoginToOnline(onlineClientDto, "0", out message);

            Assert.AreEqual(1, result);

            onlineClientDto.UniqueID = _onlineClient.UniqueID;
            onlineClientDto.MachineIP = RandomHelper.GenerateString();
            result = UserManagementService.LoginToOnline(onlineClientDto, "0", out message);

            Assert.AreEqual(3, result);

            onlineClientDto.UniqueID = _onlineClient.UniqueID;
            onlineClientDto.MachineIP = _onlineClient.MachineIP;
            onlineClientDto.Comments = _onlineClient.Comments;
            result = UserManagementService.LoginToOnline(onlineClientDto, "0", out message);

            Assert.AreEqual(0, result);

        }

        [TestMethod]
        public void LogoutToOfflineTest()
        {
            OnlineClientDto onlineClientDto = new OnlineClientDto();
            onlineClientDto.UniqueID = _onlineClient.UniqueID;
            onlineClientDto.Comments = _onlineClient.Comments;
            onlineClientDto.MachineIP = _onlineClient.MachineIP;
            bool result = UserManagementService.LogoutToOffline(onlineClientDto);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsOnlineTest()
        {
            OnlineClientDto onlineClientDto = new OnlineClientDto();
            onlineClientDto.UniqueID = _onlineClient.UniqueID;
            onlineClientDto.Comments = _onlineClient.Comments;
            onlineClientDto.MachineIP = _onlineClient.MachineIP;

            bool result = UserManagementService.IsOnline(onlineClientDto);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void StartToOfflineTest()
        {
            bool result = UserManagementService.StartToOffline(_onlineClient.IISUrl);

            Assert.AreEqual(true, result);
        }

        //        [TestMethod]
        public void PerformanceTest()
        {
            var reg = new RegistrationDto
            {
                Patient = new PatientDto
                {
                    PatientNo = Guid.NewGuid().ToString(),
                    UniqueID = Guid.NewGuid().ToString(),
                    Domain = "renji",
                    Birthday = DateTime.Now.AddMonths(-9),
                    IsVip = false,
                    EnglishName = "1",
                    Gender = "男",
                    LocalName = "1",
                    Site = "rjwest",
                },
                Orders = new List<OrderDto>
                {
                    new OrderDto
                    {
                        CurrentAge = "12 Year",
                        PatientType = "门诊病人",
                        AccNo= MockData.GetRandom(),
                        CreateTime = DateTime.Now,
                        Domain ="renji"
                    }
                },
                Procedures = new List<ProcedureDto>
                {
                    new ProcedureDto
                    {
                        BodyCategory = "45床边片",
                        BodyPart = "手",
                        BookingNotice = "",
                        Charge = 0,
                        CheckingItem = "手掌正斜位（右）-床边片",
                        ContrastDose = "",
                        ContrastName = "",
                        Domain = "renji",
                        ExamSystem = "其他",
                        ExposalCount = 0,
                        FilmCount = 1,
                        FilmSpec = "",
                        ImageCount = 0,
                        Modality = "KODAK_CR",
                        ModalityType = "CR",
                        ProcedureCode = "0724",
                        Registrar = "4a88d04d-ade9-4621-ba2e-9c2e064d82cc",
                        RegistrarName = "test1",
                        ReportID = null,
                        RPDesc = "手掌正斜位（右）-床边片",
                        Status = 20
                    }
                },
            };
            var service = Container.Resolve<IRegistrationService>();
            service.AddNewRegistration(reg, "", "", "");
        }
    }
}
