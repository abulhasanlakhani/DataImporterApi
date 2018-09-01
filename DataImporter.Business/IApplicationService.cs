using DataImporter.Domain;

namespace DataImporter.Business
{
    public interface IApplicationService
    {
        Response<Expense> ProcessEmailText(string emailText);
    }
}