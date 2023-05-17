using APITrassBank.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APITrassBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoringsController : ControllerBase
    {
        private readonly IScoringsService _scoringsService;
        private readonly HttpContext _context;

        public ScoringsController(IScoringsService scoringsService, IHttpContextAccessor httpContextAccessor)
        {
            _scoringsService = scoringsService;
            _context = httpContextAccessor.HttpContext;
        }
        [HttpPost("CheckScore")]
        public async Task<IActionResult> GetScoring([FromBody] ScoringCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            bool result;
            var idSelf = _context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                result = await _scoringsService.GetScoring(model, idSelf);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest("Bad model parameters");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
        [HttpPost("ConfirmScore")]
        public async Task<IActionResult> ConfirmScoring([FromBody] ScoringCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            bool result;
            var idSelf = _context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                result = await _scoringsService.GetScoring(model, idSelf);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest("Bad model parameters");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            if (!result)
            {
                return BadRequest("Invalid scoring  model");
            }
            LoanResponseDTO loan;
            try
            {
                loan = await _scoringsService.ConfirmScore(model, idSelf);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok(JsonConvert.SerializeObject(loan));
        }
    }
}
