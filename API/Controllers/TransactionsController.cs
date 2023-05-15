using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;

namespace APITrassBank.Controllers
{
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
        [HttpGet("self")]
        public async IEnumerable<TransactionResponseDTO> Get()
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            IEnumerable<TransactionResponseDTO> result;
            try
            {
                result = await _transactionsService.GetSelf(idSelf);
            }catch(ArgumentOutOfRangeException)
            {
                return Forbid();
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }

        }
    }
}
