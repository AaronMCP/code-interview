using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.Platform.CrossCutting.Extensions;
using NUnit.Framework;

namespace Hys.Platform.Tests
{
    [TestFixture]
    public class ExtensionTest
    {
        [Test]
        public void FormatTimeSpan()
        {
            TimeSpan ts = new TimeSpan(0,1,2,3,300);
            var result = ts.FormatTimeSpanHmsm();
            Assert.AreEqual("01:02:03.300",result);
        }
    }
}
