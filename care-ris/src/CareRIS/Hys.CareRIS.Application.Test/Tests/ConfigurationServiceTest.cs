using Hys.CareRIS.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hys.CareRIS.Application.Services;
using Hys.CareRIS.EntityFramework;
using Microsoft.Practices.Unity;

namespace Hys.CareRIS.Application.Test.Tests
{
    [TestClass]
    public class ConfigurationServiceTest : TestBase
    {
        private static readonly IConfigurationService ConfigurationService = Container.Resolve<IConfigurationService>();

        [TestMethod]
        public async void GetModalitiesTest()
        {
            var modalityID = Guid.NewGuid().ToString();
            var modality1 = new Modality { UniqueID = modalityID, ModalityType = "CR", ModalityName = "CR1", Site = "" };
            var modality2 = new Modality { UniqueID = Guid.NewGuid().ToString(), ModalityType = "DR", ModalityName = "DR1", Site = "S1" };
            var modality3 = new Modality { UniqueID = Guid.NewGuid().ToString(), ModalityType = "CT", ModalityName = "CT1", Site = "S2" };


            var risProContext = Container.Resolve<IRisProContext>();
            risProContext.Set<Modality>().AddRange(new[] { modality1, modality2, modality3 });
            risProContext.SaveChanges();

            // all modalities
            var modalities = (await ConfigurationService.GetModalities()).ToList();
            Assert.IsTrue(modalities.Any(p => p.UniqueID == modalityID));
            Assert.IsTrue(modalities.Any(p => p.ModalityType == "DR"));
            Assert.IsTrue(modalities.Any(p => p.ModalityName == "CT1"));

            // modalities for site
            modalities = (await ConfigurationService.GetModalities("S1")).ToList();
            Assert.IsTrue(modalities.Any(p => p.UniqueID == modalityID));
            Assert.IsTrue(modalities.Any(p => p.ModalityType == "DR"));
            Assert.IsFalse(modalities.Any(p => p.ModalityName == "CT1"));
        }

        [TestMethod]
        public async Task GetDictionariesTest()
        {
            var dictionary1 = new Dictionary { Tag = 1, Name = "D1" };
            var dictionary2 = new Dictionary { Tag = 2, Name = "D2" };
            var dictionary3 = new Dictionary { Tag = 3, Name = "D3" };
            var risProContext = Container.Resolve<IRisProContext>();
            risProContext.Set<Dictionary>().AddRange(new[] { dictionary1, dictionary2, dictionary3 });

            var dictionaryValue1 = new DictionaryValue { UniqueID = Guid.NewGuid().ToString(), Tag = 1, Value = "Value1", Text = "Text1", Site = "" };
            var dictionaryValue2 = new DictionaryValue { UniqueID = Guid.NewGuid().ToString(), Tag = 1, Value = "Value2", Text = "Text2", Site = "" };
            var dictionaryValue3 = new DictionaryValue { UniqueID = Guid.NewGuid().ToString(), Tag = 1, Value = "Value3", Text = "Text3", Site = "" };
            var dictionaryValue4 = new DictionaryValue { UniqueID = Guid.NewGuid().ToString(), Tag = 2, Value = "Value4", Text = "Text4", Site = "" };
            var dictionaryValue5 = new DictionaryValue { UniqueID = Guid.NewGuid().ToString(), Tag = 2, Value = "Value5", Text = "Text5", Site = "S1" };
            var dictionaryValue6 = new DictionaryValue { UniqueID = Guid.NewGuid().ToString(), Tag = 2, Value = "Value6", Text = "Text6", Site = "S2" };
            var dictionaryValue7 = new DictionaryValue { UniqueID = Guid.NewGuid().ToString(), Tag = 3, Value = "Value7", Text = "Text7", Site = "S2" };
            var dictionaryValue8 = new DictionaryValue { UniqueID = Guid.NewGuid().ToString(), Tag = 3, Value = "Value8", Text = "Text8", Site = "S2" };
            var dictionaryValue9 = new DictionaryValue { UniqueID = Guid.NewGuid().ToString(), Tag = 3, Value = "Value9", Text = "Text9", Site = "S2" };

            risProContext.Set<DictionaryValue>().AddRange(new[] { dictionaryValue1, dictionaryValue2, dictionaryValue3, dictionaryValue4, dictionaryValue5, dictionaryValue6, dictionaryValue7, dictionaryValue8, dictionaryValue9 });
            risProContext.SaveChanges();

            // dictionaries for tags with site value
            var dictionaries = (await ConfigurationService.GetDictionaries("S1")).ToList();
            var d1 = dictionaries.FirstOrDefault(d => d.Tag == 1);
            Assert.IsNotNull(d1);
            Assert.IsTrue(d1.Values.Any(v => v.UniqueID == dictionaryValue1.UniqueID));
            var d2 = dictionaries.FirstOrDefault(d => d.Tag == 2);
            Assert.IsNotNull(d2);
            Assert.IsTrue(d2.Values.Any(v => v.UniqueID == dictionaryValue5.UniqueID));
            var d3 = dictionaries.FirstOrDefault(d => d.Tag == 3);
            Assert.IsNotNull(d3);
            Assert.IsFalse(d3.Values.Any(v => v.UniqueID == dictionaryValue7.UniqueID));
        }

        [TestMethod]
        public async Task GetApplyDeptsTest()
        {
            var applyDept1 = new ApplyDept { UniqueID = Guid.NewGuid().ToString(), DeptName = "Dept1", Site = "" };
            var applyDept2 = new ApplyDept { UniqueID = Guid.NewGuid().ToString(), DeptName = "Dept2", Site = "S1" };
            var applyDept3 = new ApplyDept { UniqueID = Guid.NewGuid().ToString(), DeptName = "Dept3", Site = "S2" };

            var risProContext = Container.Resolve<IRisProContext>();
            risProContext.Set<ApplyDept>().AddRange(new[] { applyDept1, applyDept2, applyDept3 });
            risProContext.SaveChanges();

            // all apply depts
            var applyDepts = (await ConfigurationService.GetApplyDepts()).ToList();
            Assert.IsTrue(applyDepts.Any(p => p.UniqueID == applyDept3.UniqueID));
            Assert.IsTrue(applyDepts.Any(p => p.DeptName == "Dept2"));
            Assert.IsTrue(applyDepts.Any(p => p.Site == "S2"));

            // apply depts for site
            var applyLiteDepts = (await ConfigurationService.GetApplyDepts("S1")).ToList();
            Assert.IsTrue(applyLiteDepts.Any(p => p.UniqueID == applyDept2.UniqueID));
            Assert.IsTrue(applyLiteDepts.Any(p => p.DeptName == "Dept2"));
            //  Assert.IsFalse(applyLiteDepts.Any(p => p.Site == "S2"));
        }

        [TestMethod]
        public async Task GetApplyDoctorsTest()
        {
            var risProContext = Container.Resolve<IRisProContext>();
            var applyDoctor1 = new ApplyDoctor { UniqueID = Guid.NewGuid().ToString(), DoctorName = "Doctor1", Site = "" };
            var applyDoctor2 = new ApplyDoctor { UniqueID = Guid.NewGuid().ToString(), DoctorName = "Doctor2", Site = "S1" };
            var applyDoctor3 = new ApplyDoctor { UniqueID = Guid.NewGuid().ToString(), DoctorName = "Doctor3", Site = "S2" };
            risProContext.Set<ApplyDoctor>().AddRange(new[] { applyDoctor1, applyDoctor2, applyDoctor3 });
            risProContext.SaveChanges();


            // all apply doctors
            var applyDoctors = (await ConfigurationService.GetApplyDoctors()).ToList();
            Assert.IsTrue(applyDoctors.Any(p => p.UniqueID == applyDoctor1.UniqueID));
            Assert.IsTrue(applyDoctors.Any(p => p.DoctorName == applyDoctor1.DoctorName));
            Assert.IsTrue(applyDoctors.Any(p => p.Site == applyDoctor1.Site));

            // apply depts for site
            var applyLiteDoctors = (await ConfigurationService.GetApplyDoctors(applyDoctor2.Site)).ToList();
            Assert.IsTrue(applyLiteDoctors.Any(p => p.UniqueID == applyDoctor2.UniqueID));
            Assert.IsTrue(applyLiteDoctors.Any(p => p.DoctorName == applyDoctor2.DoctorName));
            //Assert.IsTrue(applyLiteDoctors.Any(p => p.Site == applyDoctor2.Site));
        }

        [TestMethod]
        public async Task GetSystemProfilesTest()
        {
            var profile = MockData.Generate<SystemProfile>();
            var risProContext = Container.Resolve<IRisProContext>();
            risProContext.Set<SystemProfile>().Add(profile);
            risProContext.SaveChanges();

            var profiles = (await ConfigurationService.GetSystemProfiles());
            Assert.IsTrue(profiles.Any(p => p.UniqueID == profile.UniqueID));
        }

        [TestMethod]
        public async Task GetSiteProfilesTest()
        {
            var profile = MockData.Generate<SiteProfile>();
            var risProContext = Container.Resolve<IRisProContext>();
            risProContext.Set<SiteProfile>().Add(profile);
            risProContext.SaveChanges();

            var profiles = (await ConfigurationService.GetSiteProfiles(profile.Site));
            Assert.IsTrue(profiles.Any(p => p.UniqueID == profile.UniqueID));
        }

        [TestMethod]
        public async Task GetRoleProfilesTest()
        {
            var profile = MockData.Generate<RoleProfile>();
            var risProContext = Container.Resolve<IRisProContext>();
            risProContext.Set<RoleProfile>().Add(profile);
            risProContext.SaveChanges();

            var profiles = (await ConfigurationService.GetRoleProfiles(profile.RoleName, ""));
            Assert.IsTrue(profiles.Any(p => p.UniqueID == profile.UniqueID));
        }

        [TestMethod]
        public async Task GetUserProfilesTest()
        {
            var profile = MockData.Generate<UserProfile>();
            var risProContext = Container.Resolve<IRisProContext>();
            risProContext.Set<UserProfile>().Add(profile);
            risProContext.SaveChanges();

            var profiles = (await ConfigurationService.GetUserProfiles(profile.UserID));
            Assert.IsTrue(profiles.Any(p => p.UniqueID == profile.UniqueID));
        }
    }
}
