
using System.Security.Claims;
using API._Services.Interfaces;
using API.Dtos;
using API.Helpers.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleMainCategoryController : ControllerBase
    {
        private readonly IArticleMainCategoryService _service;
        public ArticleMainCategoryController(IArticleMainCategoryService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ArticleMainCategory_Dto dto)
        {
            dto.CreateTime = DateTime.Now;
            dto.CreateBy = User.FindFirst(ClaimTypes.Name).Value;
            return Ok(await _service.Create(dto));
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(ArticleMainCategory_Dto dto)
        {
            return Ok(await _service.Delete(dto));
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] ArticleMainCategory_Dto dto)
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

        [HttpGet("GetListArticleMainCategory")]
        public async Task<IActionResult> GetListArticleMainCategory()
        {
            return Ok(await _service.GetListArticleMainCategory());
        }
    }
}