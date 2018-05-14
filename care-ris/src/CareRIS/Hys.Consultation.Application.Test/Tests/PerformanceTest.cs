using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hys.Consultation.Application.Services;
using Hys.CrossCutting.Common.Extensions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hys.Consultation.Application.Test.Tests
{
    [TestClass]
    public class PerformanceTest : TestBase
    {
        private readonly IConsultationConfigurationService _service = Container.Resolve<IConsultationConfigurationService>();

        [TestMethod]
        public void RunTest()
        {
            GetRoleTest(); //Preheat

            new[] { 1, 9, 99, 999, 9999 }.ForEach(times =>
            {
                MeasuringExecutionTime(GetRoleTest, "GetRoleTest", times);
                MeasuringExecutionTime(() => GetRoleWithoutTraceAsyncTest(), "GetRoleWithoutTraceAsyncTest", times);
                Debug.WriteLine("");
            });
        }

        public void MeasuringExecutionTime(Action action, string name = "", int times = 1)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < times; i++)
            {
                action();
            }
            stopwatch.Stop();
            Debug.WriteLine("{0} Times {1} elapsed: {2}", times, name, stopwatch.ElapsedMilliseconds);
        }

        private void GetRoleTest()
        {
            var count = _service.GetRoles().Count();
        }

        private async Task GetRoleWithoutTraceAsyncTest()
        {
            var count = (await _service.GetRolesAsync()).Count();
        }
    }
}