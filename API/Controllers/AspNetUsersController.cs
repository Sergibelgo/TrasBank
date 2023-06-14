using APITrassBank.Models;
using APITrassBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APITrassBank.Controllers
{
    /// <summary>
    /// Class controler for all auth related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUsersController : ControllerBase
    {
        private readonly IAuthUsersService _authService;
        private readonly IUserService _userService;
        private readonly HttpContext _httpContext;

        public AspNetUsersController(IAuthUsersService authService, IHttpContextAccessor accessor, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
            _httpContext = accessor.HttpContext;
        }
        // GET: api/<AspNetUsersController>
        /// <summary>
        /// Endpoint for admin to get all app users information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var response = await _userService.GetUsers();
            return Ok(JsonConvert.SerializeObject(response));
        }

        // GET api/<AspNetUsersController>/5
        /// <summary>
        /// Endpoint for admin to get a user information by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userService.GetUser(id);
            var response = JsonConvert.SerializeObject(user);
            return Ok(response);
        }
        /// <summary>
        /// Endpoint to login
        /// </summary>
        /// <param name="model">DTO for username and password</param>
        /// <returns>JSON with JWT, 400 if model is invalid or not valid credential</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO model)
        {
            if (string.IsNullOrEmpty(model.Username) && string.IsNullOrEmpty(model.Email))
            {
                var validationMessage = "Please provide UserName or Email.";
                ModelState.AddModelError("Username", validationMessage);
                ModelState.AddModelError("Email", validationMessage);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide Username or Email.");
            }
            IdentityUser user;
            try
            {
                user = await _userService.LogIn(model);
            }
            catch
            {
                return BadRequest(JsonConvert.SerializeObject("Username, Email or password invalid"));
            }
            var jwt = await _authService.GenerateJWT(user);
            return Ok(JsonConvert.SerializeObject(jwt));
        }
        //[HttpGet("GenerarBasicos")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Borrar()
        //{
        //    await _userService.GenerateBasics();
        //    return Ok();
        //}
        /// <summary>
        /// Endpoint for users to change the password
        /// </summary>
        /// <param name="model">DTO for old password and new password</param>
        /// <returns>200 if changed 400 if not valid</returns>
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordDTO model)
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var result = await _userService.ChangePassword(model, idSelf);
            if (!result)
            {
                return BadRequest(JsonConvert.SerializeObject("The password could not be changed"));
            }
            return Ok();
        }
    }
}
