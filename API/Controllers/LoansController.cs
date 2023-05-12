using APITrassBank.Models;
using APITrassBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APITrassBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoansService _loansService;

        public LoansController(ILoansService loansService)
        {
            _loansService = loansService;
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
            return Ok();
        }
    }
}
