
using API.Dtos;
using API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Helpers.Utilities
{
    public interface IJwtUtility
    {
        public string GenerateJwtToken(UserDto user);
        // public string ValidateJwtToken(string token);
        // public RefreshToken GenerateRefreshToken(UserDto user);
    }

    public class JwtUtility : IJwtUtility
    {
        private readonly IConfiguration _configuration;

        public JwtUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(UserDto user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(type: "Type", user.Type)
            };
            if (user.CustomerId != null)
                claims.Add(new Claim(type: "CustomerId", user.CustomerId?.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // public string ValidateJwtToken(string token)
        // {
        //     if (token == null)
        //         return null;

        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
        //     try
        //     {
        //         tokenHandler.ValidateToken(token, new TokenValidationParameters
        //         {
        //             ValidateIssuerSigningKey = true,
        //             IssuerSigningKey = new SymmetricSecurityKey(key),
        //             ValidateIssuer = false,
        //             ValidateAudience = false,
        //             ClockSkew = TimeSpan.Zero
        //         }, out SecurityToken validatedToken);

        //         var jwtToken = (JwtSecurityToken)validatedToken;
        //         var username = jwtToken.Claims.First(x => x.Type == "nameid").Value;

        //         return username;
        //     }
        //     catch
        //     {
        //         return null;
        //     }
        // }

        // public RefreshToken GenerateRefreshToken(UserDto user)
        // {
        //     // Tạo Refrest Token có hạn là 3 ngày
        //     var rngCryptoServiceProvider = RandomNumberGenerator.Create();
        //     var randomBytes = new byte[64];
        //     rngCryptoServiceProvider.GetBytes(randomBytes);

        //     var refreshToken = new RefreshToken
        //     {
        //         Token = Convert.ToBase64String(randomBytes),
        //         Expires = DateTime.Now.AddDays(3),
        //         CreatedTime = DateTime.Now,
        //         UserName = user.UserName
        //     };

        //     return refreshToken;
        // }
    }
}