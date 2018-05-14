using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.Platform.CrossCutting.Globalization;
using NUnit.Framework;
using System.Reflection;
using System.IO;

namespace Hys.Platform.Tests
{
    [TestFixture]
    public class TranslationTest
    {
        [Test]
        public void LoadTranslationFiles()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TranslationFiles");
            Console.WriteLine(path);
            Translation translation = new Translation("en-US",path);
            Assert.IsTrue(translation.LanguageName == "en-US");

            var result = translation.GetAllTranslationInfo("en-US", "Report");
            Assert.AreEqual(2,result.Count);
        }
    }
}
