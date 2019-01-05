using System.IO;
using System.Reflection;
using System.Xml;
using DataImporter.Business;
using DataImporter.Domain;
using Moq;
using NUnit.Framework;

namespace DataImporter.Tests
{
    [TestFixture]
    [Description("Just a placeholder test. Something I need to do in future")]
    public class MappingServiceTests
    {
        private Mock<IExtractor> _extractor;
        private Mock<ITaxCalculator> _taxCalculator;
        private IMappingService _mappingService;

        [Test]
        public void Mapping_Service_Test_Placeholder_Test()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(directory, "TestData", @"ValidExpenseEmail.txt");

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml($"<EmailContentWrapper>{File.ReadAllText(path)}</EmailContentWrapper>");

            _taxCalculator = new Mock<ITaxCalculator>();
            _extractor = new Mock<IExtractor>();

            _mappingService = new MappingService(_extractor.Object, _taxCalculator.Object);

            _taxCalculator.Setup(m => m.CalculateGstFromNetPrice(It.IsAny<double>(), It.IsAny<double>()))
                .Returns(134);
            _extractor.Setup(m => m.GetXmlNodeFromElement(It.IsAny<XmlElement>(), DomainConstants.PaymentMethod))
                .Returns(xmlDoc.DocumentElement.SelectSingleNode($"//{DomainConstants.PaymentMethod}"));
            _extractor.Setup(m => m.GetXmlNodeFromElement(It.IsAny<XmlElement>(), DomainConstants.CostCentre))
                .Returns(xmlDoc.DocumentElement.SelectSingleNode($"//{DomainConstants.CostCentre}"));
            _extractor.Setup(m => m.GetXmlNodeFromElement(It.IsAny<XmlElement>(), DomainConstants.Total))
                .Returns(xmlDoc.DocumentElement.SelectSingleNode($"//{DomainConstants.Total}"));

            var expense = new Expense();

            var mappedExpense = _mappingService.MapExpenseEmailXmlToDomain(expense, xmlDoc.DocumentElement);

            Assert.True(true);
        }
    }
}
