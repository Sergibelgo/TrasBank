using APITrassBank.Models;
using APITrassBank.Services;
using Entitys.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APITrassBank.Controllers
{
    /// <summary>
    /// Class controler for all loans related
    /// </summary>
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
        /// <summary>
        /// Endpoint for workers to get all loans on the db
        /// </summary>
        /// <returns>JSON with array with all loans</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<LoanResponseDTO> loans = await _loansService.GetAll();
            return Ok(JsonConvert.SerializeObject(loans));
        }
        /// <summary>
        /// Endpoint for workers to get loans by id
        /// </summary>
        /// <param name="id">Id of the loan</param>
        /// <returns>Loan inforamtion or 404</returns>
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
        /// <summary>
        /// Endpoint for workers to create a new loan
        /// </summary>
        /// <param name="model">DTO for new loan</param>
        /// <returns>201 if created 400 if model invalid</returns>
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
        /// <summary>
        /// Endpoint for customer to get all theirs loans
        /// </summary>
        /// <returns>List of loans of the customer</returns>
        [HttpGet("self")]
        public async Task<IActionResult> GetSelfLoans()
        {
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            IEnumerable<LoanResponseDTO> loans = await _loansService.GetByUserId(idSelf);
            var response = JsonConvert.SerializeObject(loans);
            return Ok(response);

        }
        /// <summary>
        /// Endpoint of worker to get loans of a client
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <returns>List of loans by client</returns>
        [HttpGet("ByUserId/{id}")]
        [Authorize(Roles ="Worker")]
        public async Task<IActionResult> GetSelfLoans(string id)
        {
            IEnumerable<LoanResponseDTO> loans = await _loansService.GetByUserId(id);
            var response = JsonConvert.SerializeObject(loans);
            return Ok(response);

        }
        /// <summary>
        /// Endpoint for customer to get all self loans aproved
        /// </summary>
        /// <returns>List of loans</returns>
        [HttpGet("selfAproved")]
        public async Task<IActionResult> GetSelfLoansApproved()
        {
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            IEnumerable<LoanApprovedResponseDTO> loans = await _loansService.GetByUserIdApproved(idSelf);
            var response = JsonConvert.SerializeObject(loans);
            return Ok(response);
        }
        /// <summary>
        /// Endpoint for workers to approve a loan in waiting status
        /// </summary>
        /// <param name="id">Id of the loan</param>
        /// <returns>200 if changed, 404 if not found, 403 if loan is not attached to customer in charge of worker or 500</returns>
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
        /// <summary>
        /// Endpoint for workers to deny a loan
        /// </summary>
        /// <param name="id">Id of the loan</param>
        /// <returns>200 if changed, 404 if id not found or 500</returns>
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
        /// <summary>
        /// Endpoint for workers to get a list of pending loans of their clients
        /// </summary>
        /// <returns>List of pending loans</returns>
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
