using System.Xml;
using DataImporter.Domain;

namespace DataImporter.Business.Interfaces
{
    public interface IValidationService
    {
        void ValidateEmailXml(Response<Expense> response, XmlElement emailXml);
        void ValidateTotalCostInExpenseEmail(Response<Expense> response, XmlElement emailXml);
    }
}