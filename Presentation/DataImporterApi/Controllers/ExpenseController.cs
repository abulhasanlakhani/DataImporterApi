using DataImporter.Business.Interfaces;
using DataImporterApi.Extensions;
using DataImporterApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataImporterApi.Controllers
{
    [Route("api/expense")]
    [Produces("application/json")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ExpenseController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity,
            Type = typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary))]
        public ActionResult<ResponseModel<ExpenseModel>> CreateExpense(ExpenseRequestModel expenseRequest)
        {
            var response = _applicationService.ProcessExpenseEmailText(expenseRequest.EmailText).ToViewModel();

            if (!response.Success)
            {
                return BadRequest(response.Validation);
            }

            return Created("", response.Payload);
        }
    }
}
