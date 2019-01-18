using System;
using DataImporter.Business;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Xml;
using DataImporter.Business.Interfaces;
using DataImporter.Business.Services;
using DataImporter.Domain.Entities;
using DataImporter.Domain.Infrastructure;

namespace DataImporter.Tests
{
    [TestFixture]
    public class ApplicationServiceTests
    {
        private Mock<IExtractor> _extractor;
        private Mock<ITaxCalculator> _taxCalculator;
        private Mock<IMappingService> _mappingService;
        private Mock<IValidationService> _validationService;

        private IApplicationService _applicationService;
        private string _path;
        private string _directory;

        [SetUp]
        public void Setup()
        {
            _SetupEmailWithEmbeddedXmlContent();

            _applicationService = new ApplicationService(_extractor.Object, _mappingService.Object, _validationService.Object);
        }

        [Test]
        public void Process_Valid_Expense_Email_Text_Test()
        {
            var response = _applicationService.ProcessExpenseEmailText(File.ReadAllText(_path));

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Payload, Is.Not.Null);
            Assert.That(response.Payload.CostCentre, Is.EqualTo("DEV002"));
            Assert.That(response.Payload.Gst, Is.EqualTo(133.57));
            Assert.That(response.Payload.GrossTotal, Is.EqualTo(890.44));
            Assert.That(response.Payload.Total, Is.EqualTo(1024.01));
        }

        [Test]
        public void Expense_Object_Should_Not_Be_Null()
        {
            Assert.Throws<ArgumentNullException>(() => _applicationService.CreateNewExpense(null));
        }

        [Test]
        public void New_Expense_Should_Not_Be_Created_Without_Total_Cost()
        {
            var expenseToCreate = new Expense
            {
                CostCentre = "CC1",
                PaymentMethod = "Credit Card"
            };

            Assert.Throws<ArgumentNullException>(() => _applicationService.CreateNewExpense(expenseToCreate));
        }

        #region Private Methods

        private void _SetupEmailWithEmbeddedXmlContent()
        {
            _directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _path = Path.Combine(_directory, "TestData", @"ValidExpenseEmail.txt");

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml($"<EmailContentWrapper>{File.ReadAllText(_path)}</EmailContentWrapper>");

            _taxCalculator = new Mock<ITaxCalculator>();
            _extractor = new Mock<IExtractor>();
            _validationService = new Mock<IValidationService>();
            _mappingService = new Mock<IMappingService>();

            _taxCalculator.Setup(m => m.CalculateGstFromNetPrice(It.IsAny<double>(), It.IsAny<double>()))
                .Returns(134);
            _extractor.Setup(m => m.ExtractXmlFromEmailText(It.IsAny<string>()))
                .Returns(xmlDoc.DocumentElement);
            _extractor.Setup(m => m.GetXmlNodeFromElement(It.IsAny<XmlElement>(), DomainConstants.PaymentMethod))
                .Returns(xmlDoc.DocumentElement.SelectSingleNode($"//{DomainConstants.PaymentMethod}"));
            _extractor.Setup(m => m.GetXmlNodeFromElement(It.IsAny<XmlElement>(), DomainConstants.CostCentre))
                .Returns(xmlDoc.DocumentElement.SelectSingleNode($"//{DomainConstants.CostCentre}"));
            _extractor.Setup(m => m.GetXmlNodeFromElement(It.IsAny<XmlElement>(), DomainConstants.Total))
                .Returns(xmlDoc.DocumentElement.SelectSingleNode($"//{DomainConstants.Total}"));
            _mappingService.Setup(m => m.MapExpenseEmailXmlToDomain(It.IsAny<Expense>(), It.IsAny<XmlElement>()))
                .Returns(new Expense
                {
                    CostCentre = "DEV002",
                    Total = 1024.01,
                    PaymentMethod = "Personal Card",
                    Gst = 133.57,
                    GrossTotal = 890.44
                });
        }

        private void _SetupEmailWithXmlStyleMarkUp()
        {
            _directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _path = Path.Combine(_directory, "TestData", @"ValidReservationEmail.txt");
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml($"<EmailContentWrapper>{File.ReadAllText(_path)}</EmailContentWrapper>");

            _taxCalculator = new Mock<ITaxCalculator>();
            _extractor = new Mock<IExtractor>();
            _validationService = new Mock<IValidationService>();
            _mappingService = new Mock<IMappingService>();

            _applicationService = new ApplicationService(_extractor.Object, _mappingService.Object, _validationService.Object);

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

        #endregion
    }
}
