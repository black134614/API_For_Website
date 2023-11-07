using API._Services.Interfaces.Auth;
using API.Dtos;
using API.Helpers.Params;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

         [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginParam userForLogin)
        {
            var user = await _authService.Login(userForLogin);

            if (user == null)
                return Unauthorized();

            return Ok(new UserReturnedDto
            {
                Token = user.Token,
                UserName = user.Username,
                UserId = user.UserId,
                Avartar = user.Avartar ?? string.Empty,
                Type = user.Type
            });
        }
    }
}