using APITrassBank.Models;
using APITrassBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APITrassBank.Controllers
{
    /// <summary>
    /// Class controler for workers related
    /// </summary>
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
        /// <summary>
        /// Endpoint for workers to get all workers information
        /// </summary>
        /// <returns>List of workers</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Get()
        {

            return Ok(JsonConvert.SerializeObject(await _workerService.GetWorkers()));
        }

        // GET api/<WorkersController>/5
        /// <summary>
        /// Endpoint for workers to get info of worker by id
        /// </summary>
        /// <param name="id">Id of worker</param>
        /// <returns>200 with info or 404 not found</returns>
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
        /// <summary>
        /// Endpoint for admin to create a new Worker
        /// </summary>
        /// <param name="model">DTO for new Worker</param>
        /// <returns>201 if created, 400 if bad model</returns>
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
        /// <summary>
        /// Endpoint to workers to update info
        /// </summary>
        /// <param name="id">Id of worker</param>
        /// <param name="model">DTO new info</param>
        /// <returns></returns>
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
        /// <summary>
        /// Endpoint to get worker information to select one from a list
        /// </summary>
        /// <returns>List of {Id:number,Name:String}</returns>
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
