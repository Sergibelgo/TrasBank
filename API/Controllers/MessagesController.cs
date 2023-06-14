using APITrassBank.Models;
using APITrassBank.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace APITrassBank.Controllers
{
    /// <summary>
    /// Clase Controler for all message related
    /// </summary>
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
        /// <summary>
        /// Get all messages of self
        /// </summary>
        /// <returns>List of messages</returns>
        [HttpGet("self")]
        public async Task<IActionResult> Get()
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var messages = await _messagesService.GetMessages(idSelf);
            return Ok(JsonConvert.SerializeObject(messages));
        }
        /// <summary>
        /// Endpoint to create a new message
        /// </summary>
        /// <param name="model">DTO with new message info</param>
        /// <returns>200 if created, 400 if bad model 500 otherwise</returns>
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
        /// <summary>
        /// Enpoint to call when a message that was not read needs to change status
        /// </summary>
        /// <param name="Id">Id of message</param>
        /// <returns>200 if changed, 404 if not found, 403 if message not from client</returns>
        [HttpPost("Read")]
        public async Task<IActionResult> Read([FromBody] string Id)
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                await _messagesService.ReadMessage(idSelf, Id);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch(ArgumentException ex)
            {
                return Forbid();
            }
            return Ok();
        }

    }
}
