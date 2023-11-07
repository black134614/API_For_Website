using System.Security.Claims;
using API.Helpers.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApiController : ControllerBase
    {
        protected int userId => User.FindFirst(ClaimTypes.NameIdentifier).Value.ToInt();
        protected string userName => User.FindFirst(ClaimTypes.Name).Value;
        protected int? customerId => User.FindFirst("CustomerId")?.Value?.ToInt();
        protected DateTime now => DateTime.Now;
    }
}