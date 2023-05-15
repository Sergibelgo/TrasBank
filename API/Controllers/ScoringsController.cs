using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
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
        [HttpGet]
        public async Task<IActionResult> GetScoring(ScoringCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            ScoringResponseDTO result;
            var idSelf = _context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
               result = await _scoringsService.GetScoring(model,idSelf);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
