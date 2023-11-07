
namespace API.Dtos
{
    public class UserReturnedDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Avartar { get; set; }
        public string Type { get; set; }
    }
}