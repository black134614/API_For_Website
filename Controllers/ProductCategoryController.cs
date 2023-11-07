
using System.Security.Claims;
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
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _service;
        public ProductCategoryController(IProductCategoryService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ProductCategory_Dto dto)
        {
            dto.CreateTime = DateTime.Now;
            dto.CreateBy = User.FindFirst(ClaimTypes.Name).Value;
            return Ok(await _service.Create(dto));
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(ProductCategory_Dto dto)
        {
            return Ok(await _service.Delete(dto));
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] ProductCategory_Dto dto)
        {
            return Ok(await _service.Update(dto));
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] ProductCategoryParam param)
        {
            return Ok(await _service.GetDataPagination(pagination, param));
        }


        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(int id)
        {
            return Ok(await _service.GetDetail(id));
        }

        [HttpGet("GetListProductCategory")]
        public async Task<IActionResult> GetListProductCategory()
        {
            return Ok(await _service.GetListProductCategory());
        }
    }
}