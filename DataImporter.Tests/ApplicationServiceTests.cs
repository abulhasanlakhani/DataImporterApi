using DataImporter.Business;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataImporter.Tests
{
    [TestFixture]
    public class ApplicationServiceTests
    {
        private Mock<IExtractor> _extractor;
        private Mock<ITaxCalculator> _taxCalculator;
        private IApplicationService _applicationService;
        private string _path;
        private string _directory;

        [SetUp]
        public void Setup()
        {
            _directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _path = Path.Combine(_directory, @"ValidExpenseEmail.txt");
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml($"<EmailContentWrapper>{File.ReadAllText(_path)}</EmailContentWrapper>");

            _taxCalculator = new Mock<ITaxCalculator>();
            _extractor = new Mock<IExtractor>();
            _applicationService = new ApplicationService(_extractor.Object, _taxCalculator.Object);

            _taxCalculator.Setup(m => m.CalculateGstFromNetPrice(It.IsAny<double>(), It.IsAny<double>()))
                .Returns(134);
            _extractor.Setup(m => m.ExtractXmlFromEmailText(It.IsAny<string>()))
                .Returns(xmlDoc.DocumentElement);
            _extractor.Setup(m => m.GetXmlNodeFromElement(It.IsAny<XmlElement>(), "payment_method"))
                .Returns(xmlDoc.DocumentElement.SelectSingleNode($"//payment_method"));
            _extractor.Setup(m => m.GetXmlNodeFromElement(It.IsAny<XmlElement>(), "cost_centre"))
                .Returns(xmlDoc.DocumentElement.SelectSingleNode($"//cost_centre"));
            _extractor.Setup(m => m.GetXmlNodeFromElement(It.IsAny<XmlElement>(), "total"))
                .Returns(xmlDoc.DocumentElement.SelectSingleNode($"//total"));
        }

        [Test]
        public void Process_Email_Text_Test()
        {
            var response = _applicationService.ProcessEmailText(File.ReadAllText(_path));
        }
    }
}
