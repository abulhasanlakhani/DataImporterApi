using System.Xml;
using DataImporter.Business.Interfaces;
using DataImporter.Domain;

namespace DataImporter.Business.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IExtractor _extractor;

        public ValidationService(IExtractor extractor)
        {
            _extractor = extractor;
        }

        public void ValidateEmailXml(Response<Expense> response, XmlElement emailXml)
        {
            if (emailXml == null)
            {
                response.Payload = null;
                response.Validation = new Validation(Domain.ValidationType.Error)
                {
                    Message = "Problem in parsing XML or Missing opening/closing tags"
                };
            }
        }

        public void ValidateTotalCostInExpenseEmail(Response<Expense> response, XmlElement emailXml)
        {
            var totalNode = _extractor.GetXmlNodeFromElement(emailXml, DomainConstants.Total);

            if (totalNode == null)
            {
                var validation = new Validation(Domain.ValidationType.Error)
                {
                    Message = "Total Field Missing"
                };

                response.Validation = validation;
                response.Payload = null;
            }
        }
    }
}
