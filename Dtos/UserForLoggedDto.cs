using System.Text.Json.Serialization;

namespace API.Dtos
{
    public class UserForLoggedDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Avartar { get; set; }
        [JsonIgnore]
        public string Token { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public string Type { get; set; }
    }
}