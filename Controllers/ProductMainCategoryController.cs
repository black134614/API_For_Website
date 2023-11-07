using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.Dtos;
using API.Helpers.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductMainCategoryController : ControllerBase
    {
        private readonly IProductMainCategoryService _service;
        public ProductMainCategoryController(IProductMainCategoryService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ProductMainCategory_Dto dto)
        {
            return Ok(await _service.Create(dto));
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(ProductMainCategory_Dto dto)
        {
            return Ok(await _service.Delete(dto));
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] ProductMainCategory_Dto dto)
        {
            return Ok(await _service.Update(dto));
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, string keyword)
        {
            return Ok(await _service.GetDataPagination(pagination, keyword));
        }


        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(int id)
        {
            return Ok(await _service.GetDetail(id));
        }

        [HttpGet("GetListProductMainCategory")]
        public async Task<IActionResult> GetListProductMainCategory()
        {
            return Ok(await _service.GetListProductMainCategory());
        }
    }
}