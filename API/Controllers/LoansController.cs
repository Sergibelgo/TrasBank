using APITrassBank.Models;
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
    public class LoansController : ControllerBase
    {
        private readonly ILoansService _loansService;

        private readonly HttpContext httpContext;
        public LoansController(ILoansService loansService, IHttpContextAccessor httpContextAccessor)
        {
            _loansService = loansService;
            httpContext = httpContextAccessor.HttpContext;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<LoanResponseDTO> loans = await _loansService.GetAll();
            return Ok(JsonConvert.SerializeObject(loans));
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> GetById(string id)
        {
            LoanResponseDTO loan = await _loansService.GetById(id);
            if (loan is null)
            {
                return NotFound();
            }
            return Ok(JsonConvert.SerializeObject(loan));
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Post([FromBody] LoanCreateWorkerDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            Loan loan;
            try
            {
                loan = await _loansService.CreateLoan(model);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
            return Created($"api/[controller]/{loan.Id}", loan);
        }
        [HttpGet("self")]
        public async Task<IActionResult> GetSelfLoans()
        {
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            IEnumerable<LoanResponseDTO> loans = await _loansService.GetByUserId(idSelf);
            var response = JsonConvert.SerializeObject(loans);
            return Ok(response);

        }
        [HttpGet("ByUserId/{id}")]
        [Authorize(Roles ="Worker")]
        public async Task<IActionResult> GetSelfLoans(string id)
        {
            IEnumerable<LoanResponseDTO> loans = await _loansService.GetByUserId(id);
            var response = JsonConvert.SerializeObject(loans);
            return Ok(response);

        }
        [HttpGet("selfAproved")]
        public async Task<IActionResult> GetSelfLoansApproved()
        {
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            IEnumerable<LoanApprovedResponseDTO> loans = await _loansService.GetByUserIdApproved(idSelf);
            var response = JsonConvert.SerializeObject(loans);
            return Ok(response);
        }
        [HttpPost("Aprove")]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> AproveLoan([FromBody] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(id);
            }
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                await _loansService.AproveLoan(id, idSelf);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }
        [HttpPost("Denied")]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> DeniedLoan([FromBody] string id)
        {
            try
            {
                await _loansService.DenyLoan(id);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }
        [HttpGet("LoansWaitingSelf")]
        [Authorize(Roles ="Worker")]
        public async Task<IActionResult> ListLoansToBe()
        {
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var loans = await _loansService.GetLoansToBe(idSelf);
            return Ok(JsonConvert.SerializeObject(loans));
        }
    }
}
