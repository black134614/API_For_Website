using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ApiController
    {
        private readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] Account_Dto dto)
        {
            dto.CreateTime = now;
            return Ok(await _service.Create(dto));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Account_Dto dto)
        {
            return Ok(await _service.Delete(dto));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] Account_Dto dto)
        {
            return Ok(await _service.Update(dto));
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] AccountParam param)
        {
            return Ok(await _service.GetDataPagination(pagination, param));
        }


        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(string username)
        {
            return Ok(await _service.GetDetail(username));
        }
    }
}