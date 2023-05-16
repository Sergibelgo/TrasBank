﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;

namespace APITrassBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;
        private readonly HttpContext _httpContext;

        public TransactionsController(ITransactionsService transactionsService, IHttpContextAccessor httpContextAccessor)
        {
            _transactionsService = transactionsService;
            _httpContext = httpContextAccessor.HttpContext;
        }
        [HttpGet("self/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            IEnumerable<TransactionResponseDTO> result;
            try
            {
                result = await _transactionsService.GetSelf(idSelf, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));

        }
        [HttpGet("ByUserId/{id}")]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> GetByUserId(string id)
        {
            IEnumerable<TransactionResponseDTO> result;
            try
            {
                result = await _transactionsService.GetByUserId(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }
        [HttpPost("AddMoney{id}")]
        [Authorize(Roles = "Worker,ATM")]
        public async Task<IActionResult> AddMoney(string id, [FromBody] TransactionAddMoneyDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(JsonConvert.SerializeObject(model));
            }
            var idSelf = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;

            try
            {
                await _transactionsService.AddMoney(model.Quantity, idSelf,id);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }
    }
}
