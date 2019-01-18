using System.Xml;
using DataImporter.Domain.Entities;

namespace DataImporter.Business.Interfaces
{
    public interface IMappingService
    {
        Expense MapExpenseEmailXmlToDomain(Expense expense, XmlElement emailXml);
    }
}