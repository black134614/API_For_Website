using API.Dtos;
using API.Helpers.Params;

namespace API._Services.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<UserForLoggedDto> Login(UserForLoginParam userForLogin);
        Task<UserForLoggedDto> RefreshToken(TokenRequestParam param);
    }
}