using System.Xml;
using DataImporter.Domain;

namespace DataImporter.Business.Interfaces
{
    public interface IMappingService
    {
        Expense MapExpenseEmailXmlToDomain(Expense expense, XmlElement emailXml);
    }
}