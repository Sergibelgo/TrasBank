using APITrassBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APITrassBank.Controllers
{
    /// <summary>
    /// Class controller to all account buisness related
    /// </summary>
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
        /// <summary>
        /// Endpoint for workers that returns all the accounts on the DB
        /// </summary>
        /// <returns>JSON with all accounts or 500 if the db couldnt be connected</returns>
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
        /// <summary>
        /// Endpoint for customers to get all of their accounts
        /// </summary>
        /// <returns>JSON with all of the customer accounts or 500 if the db could be connected</returns>
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
        /// <summary>
        /// Endpoint for workers to get all accounts associated with a customer id
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <returns>JSON with all accounts of the customer or 500 if db couldnt be connected</returns>
        [HttpGet("ByCustomerId/{id}")]
        [Authorize(Roles ="Worker")]
        public async Task<IActionResult> GetAllByUserId(string id)
        {
            IEnumerable<AccountResponseDTO> list;
            try
            {
                list = await _accountsService.GetByUserId(id);
            }
            catch
            {
                return StatusCode(500, "Couldnt connect to the server");
            }
            var response = JsonConvert.SerializeObject(list);
            return Ok(response);
        }

        // GET api/<AccountsController>/5
        /// <summary>
        /// Endpoint for workers to get the information of an account by the id
        /// </summary>
        /// <param name="id">Id of the account</param>
        /// <returns>JSON with the information of the account, 404 if no account is found or 500 if db couldnt be connected</returns>
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
        /// <summary>
        /// Endpoint for customers to get information about one of his account
        /// </summary>
        /// <param name="id"> Id of the account</param>
        /// <returns>JSON with the information, 404 if not account found, 403 if account doesnt 
        /// belong to customer or 500 if db couldnt be connected</returns>
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
        /// <summary>
        /// Endpoint for customers to create a new account
        /// </summary>
        /// <param name="model">DTO of account with information to create a new one</param>
        /// <returns>201 if created,400 if model is not valid, 403 if user is not customer or 500 if db couldn't be connected </returns>
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
            catch (ArgumentException ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, JsonConvert.SerializeObject(ex.Message));
            }
            var response = JsonConvert.SerializeObject(account);
            return Created("api/[controller]/self/ById", response);
        }

        // PUT api/<AccountsController>/5
        /// <summary>
        /// Endpoint to update the name of an account of yourself
        /// </summary>
        /// <param name="id">Id of the account to update</param>
        /// <param name="name">New name</param>
        /// <returns>200 if changed,400 if name not valid,404 if id not found, 403 if account doesnt belong to customer or 500 if db couldnt be connected</returns>
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
        // GET api/<AccountsController>/5
        /// <summary>
        /// Endpoint for workes to change the status of an account
        /// </summary>
        /// <param name="id">Id of the account</param>
        /// <param name="status">Id of the new status</param>
        /// <returns>200 if changed, 404 if id not found or 500 if db couldnt be connected</returns>
        [HttpPut("Status/{id}")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> UpdateAccountStatus(string id, [FromBody] int status)
        {

            try
            {
                 await _accountsService.ChangeStatus(id,status);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500, "Couldnt connect to the server");
            }
            return Ok();
        }
        /// <summary>
        /// Endpoint to get account id and name by customer username
        /// </summary>
        /// <param name="username">Username of the customer</param>
        /// <returns>JSON with all accounts name and id, 400 if name not valid, 404 if no user is found or 500 if db couldnt be connected</returns>
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
