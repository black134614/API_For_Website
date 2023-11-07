using API._Repositories;
using API._Services.Interfaces.Auth;
using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        private readonly IJwtUtility _jwtUtility;
        private readonly IFunctionUtility _functionUtility;

        public AuthService(
            IRepositoryAccessor repositoryAccessor,
            IJwtUtility jwtUtility,
            IFunctionUtility functionUtility)
        {
            _repositoryAccessor = repositoryAccessor;
            _jwtUtility = jwtUtility;
            _functionUtility = functionUtility;
        }
        public async Task<UserForLoggedDto> Login(UserForLoginParam userForLogin)
        {
            // Kiểm tra tên đăng nhập và mật khẩu
            if (string.IsNullOrEmpty(userForLogin.Username) || string.IsNullOrEmpty(userForLogin.Password))
            {
                return null;
            }

            // Tiến hành đăng nhập
            var password = _functionUtility.HashPasswordUser(userForLogin.Password);
            var adminUser = new UserDto();
            if (userForLogin.Type == "Admin")
            {
                adminUser = await _repositoryAccessor.Account
                    .FindAll(x =>
                        x.Username.Trim() == userForLogin.Username.Trim() &&
                        x.Password.Trim() == userForLogin.Password.Trim())
                    .Select(x => new UserDto() { UserName = x.Username, Password = x.Password, Type = "Admin" })
                    .FirstOrDefaultAsync();
            }
            else
            {
                adminUser = await _repositoryAccessor.Client
                .FindAll(x =>
                    x.Email.Trim() == userForLogin.Username.Trim() &&
                    x.Password == userForLogin.Password.Trim())
                     .Select(x => new UserDto() { UserName = x.Email, Password = x.Password, Type = "Client" })
                .FirstOrDefaultAsync();
            }

            // Không tồn tại username hoặc mật khẩu
            if (adminUser == null)
                return null;

            var jwtToken = _jwtUtility.GenerateJwtToken(adminUser);
            // Khởi tạo user trả về
            //fix bug .net bắt lỗi casting int? -> int
            var userToReturn = new UserForLoggedDto
            {
                UserId = adminUser.UserId.HasValue ? (int)adminUser.UserId : 0,
                Username = adminUser.UserName,
                Avartar = adminUser.Avartar,
                Token = jwtToken,
                Type = adminUser.Type
            };
            return userToReturn;
        }

        public Task<UserForLoggedDto> RefreshToken(TokenRequestParam param)
        {
            throw new NotImplementedException();
        }
    }
}