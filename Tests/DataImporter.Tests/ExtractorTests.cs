using DataImporter.Business;
using DataImporter.Domain;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using DataImporter.Business.Services;
using DataImporter.Domain.Infrastructure;

namespace DataImporter.Tests
{
    [TestFixture]
    public class ExtractorTests
    {
        private string _path;
        private string _directory;

        [Test]
        public void Valid_Xml_Test()
        {
            _directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _path = Path.Combine(_directory, "TestData", @"ValidExpenseEmail.txt");

            var extractor = new Extractor();

            var xmlElement = extractor.ExtractXmlFromEmailText($"<EmailContentWrapper>{File.ReadAllText(_path)}</EmailContentWrapper>");

            Assert.That(xmlElement, Is.Not.Null);
            Assert.That(extractor.GetXmlNodeFromElement(xmlElement, "expense"), Is.Not.Null);
            Assert.That(extractor.GetXmlNodeFromElement(xmlElement, DomainConstants.Total), Is.Not.Null);
            Assert.That(extractor.GetXmlNodeFromElement(xmlElement, DomainConstants.CostCentre), Is.Not.Null);
            Assert.That(extractor.GetXmlNodeFromElement(xmlElement, DomainConstants.PaymentMethod), Is.Not.Null);
        }

        [Test]
        public void Invalid_Xml_With_Missing_Closing_Tag_Test()
        {
            _directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _path = Path.Combine(_directory, "TestData", @"InvalidExpenseEmail.txt");

            var extractor = new Extractor();

            var xmlElement = extractor.ExtractXmlFromEmailText($"<EmailContentWrapper>{File.ReadAllText(_path)}</EmailContentWrapper>");

            Assert.That(xmlElement, Is.Null);
        }

        [Test]
        public void Valid_Xml_With_Missing_Total_Tag_Test()
        {
            _directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _path = Path.Combine(_directory, "TestData", @"ExpenseEmailWithMissingTotalField.txt");

            var extractor = new Extractor();

            var xmlElement = extractor.ExtractXmlFromEmailText($"<EmailContentWrapper>{File.ReadAllText(_path)}</EmailContentWrapper>");

            var totalNode = extractor.GetXmlNodeFromElement(xmlElement, DomainConstants.Total);

            Assert.That(xmlElement, Is.Not.Null);
            Assert.That(extractor.GetXmlNodeFromElement(xmlElement, "expense"), Is.Not.Null);
            Assert.That(extractor.GetXmlNodeFromElement(xmlElement, DomainConstants.Total), Is.Null);
            Assert.That(extractor.GetXmlNodeFromElement(xmlElement, DomainConstants.CostCentre), Is.Not.Null);
            Assert.That(extractor.GetXmlNodeFromElement(xmlElement, DomainConstants.PaymentMethod), Is.Not.Null);
        }
    }
}
