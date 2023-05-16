using APITrassBank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APITrassBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumsController : ControllerBase
    {
        private readonly IEnumsService _enumsService;

        public EnumsController(IEnumsService enumsService)
        {
            _enumsService = enumsService;
        }
        [HttpGet("TransactionTypes")]
        public async Task<IActionResult> GetTranssactionTypes()
        {
            var response = await _enumsService.GetTranssactionsTypesAsync();
            return Ok(JsonConvert.SerializeObject(response));
        }
        [HttpGet("CustomerWorkingStatuses")]
        public async Task<IActionResult> GetCustomerWorkingStatus()
        {
            var response = await _enumsService.GetCustomerWorkingStatusesAsync();
            return Ok(JsonConvert.SerializeObject(response));
        }
        [HttpGet("AccountStatuses")]
        public async Task<IActionResult> GetAccountStatuses()
        {
            var response = await _enumsService.GetAccountStatusesAsync();
            return Ok(JsonConvert.SerializeObject(response));
        }
        [HttpGet("LoanTypes")]
        public async Task<IActionResult> GetLoanTypes()
        {
            var response = await _enumsService.GetLoanTypesAsync();
            return Ok(JsonConvert.SerializeObject(response));
        }
        [HttpGet("WorkerStatuses")]
        public async Task<IActionResult> GetWorkerStatuses()
        {
            var response = await _enumsService.GetWorkerStatusesAsync();
            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}
