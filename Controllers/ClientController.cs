using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;
        protected int userId => User.FindFirst(ClaimTypes.NameIdentifier).Value.ToInt();
        protected string userName => User.FindFirst(ClaimTypes.Name).Value;
        protected int? customerId => User.FindFirst("CustomerId")?.Value?.ToInt();
        protected DateTime now => DateTime.Now;
        public ClientController(IClientService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] Client_Dto dto)
        {
            dto.CreateTime = now;
            return Ok(await _service.Create(dto));
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(Client_Dto dto)
        {
            return Ok(await _service.Delete(dto));
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] ClientSercure_Dto dto)
        {
            return Ok(await _service.Update(dto));
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] ClientParam param)
        {
            return Ok(await _service.GetDataPagination(pagination, param));
        }


        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(string email)
        {
            return Ok(await _service.GetDetail(email));
        }
    }
}