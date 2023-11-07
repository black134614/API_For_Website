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
    public class ArticleCategoryController : ControllerBase
    {
        private readonly IArticleCategoryService _service;
        public ArticleCategoryController(IArticleCategoryService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ArticleCategory_Dto dto)
        {
            dto.CreateTime = DateTime.Now;
            dto.CreateBy = User.FindFirst(ClaimTypes.Name).Value;
            return Ok(await _service.Create(dto));
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(ArticleCategory_Dto dto)
        {
            return Ok(await _service.Delete(dto));
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] ArticleCategory_Dto dto)
        {
            return Ok(await _service.Update(dto));
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] ArticleCategoryParam param)
        {
            return Ok(await _service.GetDataPagination(pagination, param));
        }


        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(int id)
        {
            return Ok(await _service.GetDetail(id));
        }

        [HttpGet("GetListArticleCategory")]
        public async Task<IActionResult> GetListArticleCategory()
        {
            return Ok(await _service.GetListArticleCategory());
        }
    }
}