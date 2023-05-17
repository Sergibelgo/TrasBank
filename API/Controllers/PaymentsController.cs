using APITrassBank.Services;
using Entitys.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APITrassBank.Controllers
{
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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
        [HttpPost("MakePayment/{id}")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> MakePayment(string id, [FromBody] PaymentCreateDTO model)
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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }


    }
}
