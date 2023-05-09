using APITrassBank.Context;
using APITrassBank.Models;
using APITrassBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Entitys.Entity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Newtonsoft.Json.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APITrassBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUsersController : ControllerBase
    {
        private readonly ContextDB _contextDB;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthUsersService _authService;
        private readonly HttpContext _httpContext;

        public AspNetUsersController(ContextDB contextDB, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IAuthUsersService authService, IHttpContextAccessor accessor)
        {
            _contextDB = contextDB;
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
            _httpContext=accessor.HttpContext;
        }
        // GET: api/<AspNetUsersController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            var response = JsonConvert.SerializeObject(_contextDB.Users.ToList());
            return Ok(response);
        }

        // GET api/<AspNetUsersController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IdentityUser Get(string id)
        {
            var user = _contextDB.Users.Where(user => user.Id == id).FirstOrDefault();
            var response = JsonConvert.SerializeObject(user);
            return _contextDB.Users.Where(user => user.Id == id).FirstOrDefault(user);
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
            var user = string.IsNullOrEmpty(model.Username) ? await _userManager.FindByEmailAsync(model.Email) : await _userManager.FindByNameAsync(model.Username);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return BadRequest("User or password not valid");
            }
            var jwt = await _authService.GenerateJWT(user);
            return Ok(new
            {
                AccessToken = jwt
            });
        }
        [HttpGet("GenerarBasicos")]
        [AllowAnonymous]
        public async Task<IActionResult> Borrar()
        {
            if (!(await _contextDB.Roles.AnyAsync()))
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Worker"
                });
            }
            if (!(await _contextDB.Users.AnyAsync()))
            {
                IdentityUser user = new()
                {
                    Email = "sergibelgo@gmail.com",
                    UserName = "sergibelgo"
                };
                await _userManager.CreateAsync(user, password: "aA123456!");
                await _userManager.AddToRoleAsync(user, "Admin");

            }
            if (!(await _contextDB.WorkerStatuses.AnyAsync()))
            {
                _contextDB.WorkerStatuses.Add(new WorkerStatus() { Name = "Alta" });
            }
            if (!(await _contextDB.WorkingStates.AnyAsync()))
            {
                _contextDB.WorkingStates.Add(new CustomerWorkingStatus() { Name = "Working" });
            }
            await _contextDB.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordDTO model)
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var user = await _userManager.FindByIdAsync(idSelf);
            if (user is null)
            {
                return Forbid();
            }
            var result = await _userManager.ChangePasswordAsync(user, model.OldPass, model.NewPass);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
            //var passValidator = new PasswordValidator<IdentityUser>();
            //var validPass = await passValidator.ValidateAsync(_userManager, null, newPassword);
            //if (!validPass.Succeeded)
            //{
            //    return BadRequest(new { error = "The password is not valid, try again" });
            //}

        }

        // POST api/<AspNetUsersController>
        //[HttpPost("Register")]
        //public async Task<IActionResult> Post([FromBody] UserRegisterDTO model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(model);
        //    }
        //    var user = new IdentityUser()
        //    {
        //        Email = model.Email,
        //        UserName = model.Username
        //    };
        //    var result = await _userManager.CreateAsync(user, password: model.Password);
        //    if (result.Succeeded)
        //    {
        //        return Created("api/AspNetUsersController/" + user.Id, model);
        //    }
        //    else
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //        return UnprocessableEntity(model);
        //    }
        //}
        //[HttpGet("Test")]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Test()
        //{
        //    var user = contextAccessor.HttpContext.User;
        //    return Ok(new
        //    {
        //        Claims = user.Claims.Select(s => new
        //        {
        //            s.Type,
        //            s.Value
        //        }).ToList(),
        //        user.Identity.Name,
        //        user.Identity.IsAuthenticated,
        //        user.Identity.AuthenticationType
        //    });
        //}
        //[HttpGet("Borrar2")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Crearusuario()
        //{
        //    IdentityUser user = new()
        //    {
        //        Email = "sergibelgo@gmail.com",
        //        UserName = "sergibelgo"
        //    };
        //    await _userManager.CreateAsync(user,password:"aA123456!");
        //    await _userManager.AddToRoleAsync(user, "Admin");
        //    return Ok();

        //}

        // PUT api/<AspNetUsersController>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Edit(string id, [FromBody] UserEditDTO model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(model);
        //    }
        //    var user = await _contextDB.Users.FindAsync(id);
        //    if (user is null)
        //    {
        //        return BadRequest(model);
        //    }
        //    user.UserName = model.Username;
        //    user.Email = model.Email;
        //    var result = await _userManager.UpdateAsync(user);
        //    if (result.Succeeded)
        //    {
        //        return Ok(user);
        //    }
        //    else
        //    {
        //        return BadRequest(model);
        //    }
        //}

        //// DELETE api/<AspNetUsersController>/5
        //[HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var user = await _contextDB.Users.FindAsync(id);
        //    if (user is null)
        //    {
        //        return BadRequest(id);
        //    }
        //    var result = await _userManager.DeleteAsync(user);
        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
