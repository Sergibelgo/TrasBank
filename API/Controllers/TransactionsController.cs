using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APITrassBank.Controllers
{
    /// <summary>
    /// Class controller for transactions related
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;
        private readonly HttpContext _httpContext;

        public TransactionsController(ITransactionsService transactionsService, IHttpContextAccessor httpContextAccessor)
        {
            _transactionsService = transactionsService;
            _httpContext = httpContextAccessor.HttpContext;
        }
        /// <summary>
        /// Endpoint to customer to get all transaction for account id
        /// </summary>
        /// <param name="id">Id of account</param>
        /// <returns>List of all transactions</returns>
        [HttpGet("self/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            IEnumerable<TransactionResponseDTO> result;
            try
            {
                result = await _transactionsService.GetSelf(idSelf, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));

        }
        /// <summary>
        /// Endpoint to get all transactions of self, independent of account
        /// </summary>
        /// <returns>List of transactions</returns>
        [HttpGet("self/all")]
        public async Task<IActionResult> GetAllSelf()
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            IEnumerable<TransactionResponseDTO> result;
            try
            {
                result = await _transactionsService.GetByUserId(idSelf);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
        /// <summary>
        /// Endpoint for workers to get all transactions done by customer
        /// </summary>
        /// <param name="id">Id of customers</param>
        /// <returns>List of transaction</returns>
        [HttpGet("ByUserId/{id}")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> GetByUserId(string id)
        {
            IEnumerable<TransactionResponseDTO> result;
            try
            {
                result = await _transactionsService.GetByUserId(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
        /// <summary>
        /// Endpoint for workers to add or remove money from account
        /// </summary>
        /// <param name="id">Id account</param>
        /// <param name="model">DTO info for new transaction</param>
        /// <returns>200 if done,404 if account not found,400 if bad model 500 otherwise</returns>
        [HttpPost("AddorRemoveMoney/{id}")]
        [Authorize(Roles = "Worker,ATM")]
        public async Task<IActionResult> AddorRemoveMoney(string id, [FromBody] TransactionAddMoneyDTO model)
        {
            if (!ModelState.IsValid || model.Quantity == 0)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;

            try
            {
                await _transactionsService.AddorRemoveMoney(model.Quantity, idSelf, id,model.Concept);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }
        /// <summary>
        /// Endpoint for customers to make transactions
        /// </summary>
        /// <param name="model">DTO for new Transaction</param>
        /// <returns>200 if done, 404 if other acount not found, 400 if bad model, 500 otherwise</returns>
        [HttpPost("Transfer")]
        public async Task<IActionResult> TransferMoney([FromBody] TransferMoneyDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                await _transactionsService.TransferTo(model, idSelf);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }
    }
}
