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

        public MessagesController(IContextDB contextDB, IHttpContextAccessor httpContextAccessor, IMessagesService messagesService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _messagesService = messagesService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var messages = await _messagesService.GetMessages(idSelf);
            return Ok(JsonConvert.SerializeObject(messages));
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(string id, [FromBody] MessageCreateDTO model)
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            if (!ModelState.IsValid)
            {
                return BadRequest(new { model, error = "The model is not valid" });
            }

            await _messagesService.Create(idSelf, model);
            return Ok();
        }

    }
}
