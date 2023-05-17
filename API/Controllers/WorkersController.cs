using APITrassBank.Models;
using APITrassBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APITrassBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerService _workerService;
        private readonly IHttpContextAccessor contextAccessor;

        public WorkersController(IWorkerService workerService, IHttpContextAccessor contextAccessor)
        {
            this._workerService = workerService;
            this.contextAccessor = contextAccessor;
        }
        // GET: api/<WorkersController>
        [HttpGet]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Get()
        {

            return Ok(JsonConvert.SerializeObject(await _workerService.GetWorkers()));
        }

        // GET api/<WorkersController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _workerService.GetWorker(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(JsonConvert.SerializeObject(user));
        }

        // POST api/<WorkersController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] WorkerRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            var respuesta = await _workerService.NewWorker(model);
            if (respuesta is null)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            return Created($"api/[controller]/{respuesta.Id}", respuesta);
        }

        // PUT api/<WorkersController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Put(string id, [FromBody] WorkerEditDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            var respuesta = await _workerService.UpdateWorker(model, id);
            if (respuesta is null)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            return Ok(JsonConvert.SerializeObject(respuesta));
        }
        [HttpGet("GetWorkersMail")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWorkersMail()
        {
            IEnumerable<WorkersMailsDTO> result;
            try
            {
                result = await _workerService.GetWorkersMail();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
