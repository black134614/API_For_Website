
using API._Services.Interfaces;
using API.Dtos;
using API.Helpers.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountCategoryController : ApiController
    {
        private readonly IAccountCategoryService _service;
        public AccountCategoryController(IAccountCategoryService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] AccountCategory_Dto dto)
        {
            return Ok(await _service.Create(dto));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(AccountCategory_Dto dto)
        {
            return Ok(await _service.Delete(dto));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] AccountCategory_Dto dto)
        {
            return Ok(await _service.Update(dto));
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, string keyword)
        {
            return Ok(await _service.GetDataPagination(pagination, keyword));
        }


        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(string id)
        {
            return Ok(await _service.GetDetail(id));
        }

        [HttpGet("GetListAccountCategory")]
        public async Task<IActionResult> GetListAccountCategory()
        {
            return Ok(await _service.GetListAccountCategory());
        }
    }
}