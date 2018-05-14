using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.Platform.Domain.Ris;
using NUnit.Framework;
namespace Hys.Platform.Domain.Ris.Tests
{
    [TestFixture()]
    public class RisDBServiceTests
    {
        [Test()]
        public void CalCurrentAgeTest()
        {
            //var birthday = DateTime.Parse("1960-03-04");
            var birthday = DateTime.Parse("2015-03-07");
            var currentage=RisDBService.CalCurrentAge(birthday,3,2,0,3);
            var s=currentage;

        }
    }
}
