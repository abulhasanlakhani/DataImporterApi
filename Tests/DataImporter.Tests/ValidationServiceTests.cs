using DataImporter.Business;
using DataImporter.Business.Interfaces;
using DataImporter.Business.Services;
using DataImporter.Domain;
using Moq;
using NUnit.Framework;

namespace DataImporter.Tests
{
    [TestFixture]
    public class ValidationServiceTests
    {
        private IValidationService _validationService;
        private Mock<IExtractor> _extractor;

        [Test]
        public void Validate_Email_Xml_Test()
        {
            var response = new Response<Expense>();

            _extractor = new Mock<IExtractor>();
            _extractor.Setup(m => m.ExtractXmlFromEmailText(It.IsAny<string>())).Returns(() => null);

            _validationService = new ValidationService(_extractor.Object);

            _validationService.ValidateEmailXml(response, null);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.Payload, Is.Null);
            Assert.That(response.Validation.Message, Is.EqualTo("Problem in parsing XML or Missing opening/closing tags"));
        }
    }
}
