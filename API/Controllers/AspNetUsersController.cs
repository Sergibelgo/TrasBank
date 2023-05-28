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
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var response = await _userService.GetUsers();
            return Ok(JsonConvert.SerializeObject(response));
        }

        // GET api/<AspNetUsersController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userService.GetUser(id);
            var response = JsonConvert.SerializeObject(user);
            return Ok(response);
        }

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
        [HttpGet("GenerarBasicos")]
        [AllowAnonymous]
        public async Task<IActionResult> Borrar()
        {
            await _userService.GenerateBasics();
            return Ok();
        }
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
