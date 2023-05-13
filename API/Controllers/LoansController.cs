using APITrassBank.Models;
using APITrassBank.Services;
using Entitys.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;

namespace APITrassBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoansService _loansService;

        private readonly HttpContext httpContext;
        public LoansController(ILoansService loansService,IHttpContextAccessor httpContextAccessor)
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
        public async Task<IActionResult> Post([FromBody] LoanCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            bool isValid = _loansService.IsValidCreate(model, out string errores);
            if (!isValid)
            {
                return BadRequest(JsonConvert.SerializeObject(new { model, error = errores }));
            }
            Loan loan;
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try { 
            loan = await _loansService.CreateLoan(model, idSelf);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
            return Created($"api/[controller]/{loan.Id}",loan);
        }
        [HttpGet("self")]
        public async Task<IActionResult> GetSelfLoans()
        {
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            IEnumerable<LoanResponseDTO> loans = await _loansService.GetByUserId(idSelf);
            return Ok(loans);
        }
    }
}
