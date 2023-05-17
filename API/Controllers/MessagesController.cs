using APITrassBank.Context;
using APITrassBank.Models;
using APITrassBank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;

namespace APITrassBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly HttpContext _httpContext;
        private readonly IMessagesService _messagesService;

        public MessagesController(IHttpContextAccessor httpContextAccessor, IMessagesService messagesService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _messagesService = messagesService;
        }
        [HttpGet("self")]
        public async Task<IActionResult> Get()
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var messages = await _messagesService.GetMessages(idSelf);
            return Ok(JsonConvert.SerializeObject(messages));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MessageCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { model, error = "The model is not valid" });
            }
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                 await _messagesService.Create(idSelf, model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            catch (OperationCanceledException ex)
            {
                return StatusCode(500, JsonConvert.SerializeObject(new { error = $"Error while trying to create a new message:{ex.Message}" }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return Ok(JsonConvert.SerializeObject(model));
        }

    }
}
