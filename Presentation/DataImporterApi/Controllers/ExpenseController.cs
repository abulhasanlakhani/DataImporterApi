using DataImporterApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataImporterApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        [HttpPost(Name = nameof(CreateExpense))]
        [ProducesResponseType(200)]
        public ActionResult CreateExpense([FromBody] ExpenseRequest expenseRequest)
        {
            return Ok(new { expenseRequest.EmailText });
        }
    }
}
