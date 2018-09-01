using DataImporter.Business;
using NUnit.Framework;
using System;

namespace DataImporter.Tests
{
    [TestFixture]
    public class ExtractorTests
    {
        [Test]
        public void Extract_Xml_From_Email_Test()
        {
            var extractor = new Extractor();

            var xmlText = extractor.ExtractXmlFromEmailText("Hello <expense><cost_centre>DEV002</cost_centre></expense> Hello");

            Assert.That(xmlText, Is.Not.Null);
        }
    }
}
