using APITrassBank.Models;
using APITrassBank.Services;
using Entitys.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APITrassBank.Controllers
{
    /// <summary>
    /// Class controler for all customer related
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAuthUsersService _authUsersService;
        private readonly HttpContext httpContext;

        public CustomersController(ICustomerService customerService, IHttpContextAccessor accessor,IAuthUsersService authUsersService)
        {
            _customerService = customerService;
            _authUsersService = authUsersService;
            httpContext = accessor.HttpContext;
        }
        // GET: api/<CustomersController>
        /// <summary>
        /// Endpoint for workers to get all customer on app
        /// </summary>
        /// <returns>JSON all customer on app</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Get()
        {
            var customers = await _customerService.GetCustomersAsync();
            return Ok(JsonConvert.SerializeObject(customers));
        }
        /// <summary>
        /// Endpoint for workers to get all the customer assignated to himself
        /// </summary>
        /// <returns>JSON with array of customers</returns>
        [HttpGet("customerWorkersSelf")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> GetSelfCustomers()
        {
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var customers = await _customerService.GetCustomersSelfWorkerAsync(idSelf);
            return Ok(JsonConvert.SerializeObject(customers));
        }

        // GET api/<CustomersController>/5
        /// <summary>
        /// Endpoint for workers to get information about one customer
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <returns>JSON with the information or 404 if not found</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _customerService.GetCustomerAsync(id);
            if (customer is null)
            {
                return NotFound();
            }
            return Ok(ConstructResponse(customer, ""));
        }
        /// <summary>
        /// Endpoint for customer to get information about themshelf
        /// </summary>
        /// <returns>JSON with customer information or 404 if not found</returns>
        [HttpGet("self")]
        public async Task<IActionResult> GetSelf()
        {
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var self = await _customerService.GetSelfAsync(idSelf);
            if (self is null)
            {
                return NotFound();
            }
            return Ok(JsonConvert.SerializeObject(self));
        }

        // POST api/<CustomersController>
        /// <summary>
        /// Endpoint to register as a new customer on the app
        /// </summary>
        /// <param name="model"> DTO for all new customer information</param>
        /// <returns>201 if created or 400 with the problem as json</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CustomerRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ConstructResponse(model, "Invalid model"));
            }
            try { 
            var customer = await _customerService.CreateCustomer(model);
            if (customer is null)
            {
                return BadRequest(JsonConvert.SerializeObject(new { model, error = "The customer could not be created" }));
            }
            return Created($"api/[controller]/{customer.Id}", JsonConvert.SerializeObject( await _authUsersService.GenerateJWT(customer.AppUser)));
            }catch(ArgumentException ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.Message));
            }
            catch(Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.Message));
            }
        }

        // PUT api/<CustomersController>/5
        /// <summary>
        /// Endpoint for workers to change information about customer
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <param name="model">DTO with the new info</param>
        /// <returns>200 if changed, 400 if bad model or 404 if id not found</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CustomerEditDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ConstructResponse(model, "Invalid model"));
            }
            var user = await _customerService.GetCustomerAsync(id.ToString());
            if (user is null)
            {
                return NotFound(ConstructResponse(model, "The Id is not valid"));
            }
            bool isValidModel = await _customerService.IsValidModel(model);
            if (!isValidModel)
            {
                return BadRequest(ConstructResponse(model, "Invalid data on model"));
            }
            Customer updatedUser = await _customerService.UpdateCustomer(user, model);
            if (updatedUser is null)
            {
                return base.BadRequest(ConstructResponse(model, "Internal server error"));
            }
            return Ok(updatedUser);
        }


        /// <summary>
        /// Endpoint for user to edit their information
        /// </summary>
        /// <param name="model">DTO with new information</param>
        /// <returns>200 if updated, 400 if bad model or 404 if not found</returns>
        [HttpPut("self")]
        public async Task<IActionResult> PutSelf([FromBody] CustomerEditSelfDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ConstructResponse(model, "Invalid model"));
            }
            var idSelf = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var customer = await _customerService.GetCustomerAsync(idSelf);
            if (customer is null)
            {
                return NotFound(ConstructResponse(model, "The id was not found"));
            }
            var updatedCustomer = await _customerService.UpdateSelf(model, customer);
            if (updatedCustomer is null)
            {
                return BadRequest(ConstructResponse(model, "The customer couldnt be updated"));
            }
            return Ok(ConstructResponse(updatedCustomer, ""));
        }
        private static string ConstructResponse(object model, string msj)
        {
            return JsonConvert.SerializeObject(new { model, error = msj });
        }
    }
}
