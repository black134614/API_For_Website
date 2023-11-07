
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] Product_Dto dto)
        {
            dto.CreateTime = DateTime.Now;
            dto.CreateBy = User.FindFirst(ClaimTypes.Name).Value;
            return Ok(await _service.Create(dto));
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(Product_Dto dto)
        {
            return Ok(await _service.Delete(dto));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] Product_Dto dto)
        {
            return Ok(await _service.Update(dto));
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] ProductParam param)
        {
            return Ok(await _service.GetDataPagination(pagination, param));
        }

        [HttpGet("UserGetDataPagination")]
        public async Task<IActionResult> UserGetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] ProductParam param)
        {
            return Ok(await _service.UserGetDataPagination(pagination, param));
        }

        [HttpGet("GetDataPaginationNotPagination")]
        public async Task<IActionResult> GetDataPaginationNotPagination(int? mainCateID, int? quantity)
        {
            return Ok(await _service.GetDataByMainCategory(mainCateID, quantity));
        }
        [HttpGet("GetDataNewProduct")]
        public async Task<IActionResult> GetDataNewProduct()
        {
            return Ok(await _service.GetDataNewProduct());
        }
        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(int id)
        {
            return Ok(await _service.GetDetail(id));
        }

        [HttpGet("Top20ViewProduct")]
        public async Task<IActionResult> Top20ViewProduct()
        {
            return Ok(await _service.Top20ViewProduct());
        }
    }
}