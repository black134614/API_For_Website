using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _service;
        public ArticleController(IArticleService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] Article_Dto dto)
        {
            dto.CreateTime = DateTime.Now;
            dto.CreateBy = User.FindFirst(ClaimTypes.Name).Value;
            return Ok(await _service.Create(dto));
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(Article_Dto dto)
        {
            return Ok(await _service.Delete(dto));
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] Article_Dto dto)
        {
            return Ok(await _service.Update(dto));
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] ArticleParam param)
        {
            return Ok(await _service.GetDataPagination(pagination, param));
        }

        [HttpGet("GetDataPaginationNotPagination")]
        public async Task<IActionResult> GetDataPaginationNotPagination([FromQuery] ArticleParam param)
        {
            return Ok(await _service.GetDataPaginationNotPagination(param));
        }
        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(int id)
        {
            return Ok(await _service.GetDetail(id));
        }
    }
}