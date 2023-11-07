
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
    public class StatiticController : ControllerBase
    {
        private readonly IStatiticService _service;
        public StatiticController(IStatiticService service)
        {
            _service = service;
        }
        [HttpGet("GetStatitic")]
        public async Task<IActionResult> GetStatitic()
        {
            return Ok(await _service.GetDataStatitic());
        }
    }
}