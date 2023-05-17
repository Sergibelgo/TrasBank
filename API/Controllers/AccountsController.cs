using APITrassBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APITrassBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        private readonly HttpContext _httpContext;

        public AccountsController(IAccountsService accountsService, IHttpContextAccessor contextAccessor)
        {
            _accountsService = accountsService;
            _httpContext = contextAccessor.HttpContext;
        }
        // GET: api/<AccountsController>
        [HttpGet]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<AccountResponseDTO> list;
            try
            {
                list = await _accountsService.GetAll();
            }
            catch
            {
                return StatusCode(500, "Couldnt connect to the server");
            }
            var response = JsonConvert.SerializeObject(list);
            return Ok(response);
        }
        [HttpGet("self")]
        public async Task<IActionResult> GetAllSelf()
        {
            IEnumerable<AccountResponseDTO> list;
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                list = await _accountsService.GetByUserId(idSelf);
            }
            catch
            {
                return StatusCode(500, "Couldnt connect to the server");
            }
            var response = JsonConvert.SerializeObject(list);
            return Ok(response);
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> GetById(string id)
        {

            AccountResponseDTO account;
            try
            {
                account = await _accountsService.GetById(id, null);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500, "Couldnt connect to the server");
            }
            var response = JsonConvert.SerializeObject(account);
            return Ok(response);
        }
        [HttpPost("self/ById")]
        public async Task<IActionResult> GetByIdAndSelf([FromBody] string id)
        {
            AccountResponseDTO account;
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                account = await _accountsService.GetById(id, idSelf);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (FieldAccessException)
            {
                return Forbid();
            }
            var response = JsonConvert.SerializeObject(account);
            return Ok(response);
        }

        // POST api/<AccountsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                var responseError = JsonConvert.SerializeObject(model);
                return BadRequest(responseError);
            }
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            AccountResponseDTO account;
            try
            {
                account = await _accountsService.Create(model, idSelf);
            }
            catch (ArgumentOutOfRangeException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, JsonConvert.SerializeObject(ex.Message));
            }
            var response = JsonConvert.SerializeObject(account);
            return Created("api/[controller]/self/ById", response);
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return BadRequest("Invalid name");
            }
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                await _accountsService.UpdateName(name, id, idSelf);
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
            return Ok("Name changed");
        }
        [HttpPost("GetByUserName")]
        public async Task<IActionResult> GetByUserName([FromBody] string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                return BadRequest(username);
            }
            IEnumerable<AccountByUsernameDTO> result;
            try
            {
                result = await _accountsService.GetByUserName(username);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
