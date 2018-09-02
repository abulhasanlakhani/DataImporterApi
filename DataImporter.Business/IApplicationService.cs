using DataImporter.Domain;

namespace DataImporter.Business
{
    public interface IApplicationService
    {
        Response<Expense> ProcessExpenseEmailText(string emailText);
        Response<Reservation> ProcessReservationEmailText(string emailText);
    }
}