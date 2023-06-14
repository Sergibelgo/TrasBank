using APITrassBank.Services;
using Entitys.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APITrassBank.Controllers
{
    /// <summary>
    /// Class Controler for all payments related
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly HttpContext _httpContext;
        private readonly IPaymentsService _paymentsService;

        public PaymentsController(IHttpContextAccessor httpContextAccessor, IPaymentsService paymentsService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _paymentsService = paymentsService;
        }
        /// <summary>
        /// Endpoint for workers to get all payments
        /// </summary>
        /// <returns>List of all payments done</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Payment> result;
            try
            {
                result = await _paymentsService.GetAll();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
        /// <summary>
        /// Endpoint for customer to get all their payments dones
        /// </summary>
        /// <returns>List of payments by customer</returns>
        [HttpGet("self")]
        public async Task<IActionResult> GetSelfPayments()
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            IEnumerable<PaymentResponseDTO> result;
            try
            {
                result = await _paymentsService.GetById(idSelf);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
        /// <summary>
        /// Endpoint to make a new payment
        /// </summary>
        /// <param name="model">DTO for new payment</param>
        /// <returns>200 if created, 404 if loan is not found, 400 if bad model or 500 if server error</returns>
        [HttpPost("MakePayment")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            PaymentResponseDTO result;
            try
            {
                result = await _paymentsService.Make(model, idSelf);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
        /// <summary>
        /// Endpoint for workers to make a payment from a customer
        /// </summary>
        /// <param name="model">DTO for new payment</param>
        /// <returns>200 if created, 404 if loan not found, 500 otherwise</returns>
        [HttpPost("MakePayment/worker")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> MakePaymentWorker([FromBody] PaymentCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            PaymentResponseDTO result;
            try
            {
                result = await _paymentsService.Make(model, null);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }


    }
}
